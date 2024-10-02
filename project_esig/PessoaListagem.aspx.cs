using System;
using System.Data;
using System.Diagnostics;
using System.Web.UI.WebControls;
using Oracle.ManagedDataAccess.Client;

namespace project_esig
{
    public partial class PessoaListagem : System.Web.UI.Page
    {
        private int TotalRows
        {
            get
            {
                return ViewState["TotalRows"] != null ? (int)ViewState["TotalRows"] : 0;
            }
            set
            {
                ViewState["TotalRows"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["mensagem"] != null)
                {
                    string mensagem = Request.QueryString["mensagem"];
                    if (mensagem == "editada")
                    {
                        lblMensagem.Text = "Pessoa editada com sucesso!";
                        mensagemContainer.Attributes["class"] = "alert alert-success";
                        mensagemContainer.Style["display"] = "block";
                    }
                    if (mensagem == "newPessoa")
                    {
                        lblMensagem.Text = "Pessoa criada com sucesso!";
                        mensagemContainer.Attributes["class"] = "alert alert-success";
                        mensagemContainer.Style["display"] = "block";
                    }
                }
                CarregarPessoas();
            }
        }

        private void CarregarPessoas()
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString;

            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                string query = "SELECT \"Id\", \"Nome\", \"Cidade\", \"Email\", \"Telefone\" FROM \"Pessoa\" ORDER BY \"Id\" ASC";

                OracleCommand command = new OracleCommand(query, connection);
                connection.Open();

                OracleDataAdapter adapter = new OracleDataAdapter(command);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                TotalRows = dt.Rows.Count;
                // Paginação manual
                int currentPage = GridViewPessoas.PageIndex + 1; // PageIndex começa em 0
                int pageSize = GridViewPessoas.PageSize;

                // Criar uma nova DataTable apenas com os itens da página atual
                DataTable paginatedTable = dt.Clone();
                int startRow = (currentPage - 1) * pageSize;
                int endRow = Math.Min(startRow + pageSize, TotalRows);

                for (int i = startRow; i < endRow; i++)
                {
                    paginatedTable.ImportRow(dt.Rows[i]);
                }

                GridViewPessoas.DataSource = paginatedTable;
                GridViewPessoas.DataBind();

                AtualizarPaginacao();
            }
        }

        private void AtualizarPaginacao()
        {
            int totalPages = (int)Math.Ceiling((double)TotalRows / GridViewPessoas.PageSize);
            int currentPage = GridViewPessoas.PageIndex + 1;
            DataTable dt = new DataTable();
            dt.Columns.Add("PageNumber");
            dt.Columns.Add("Text");
            dt.Columns.Add("CssClass");

            // Adicionar sempre a primeira página
            DataRow firstPage = dt.NewRow();
            firstPage["PageNumber"] = 1;
            firstPage["Text"] = "1";
            firstPage["CssClass"] = (currentPage == 1) ? "page-item active" : "page-item";
            dt.Rows.Add(firstPage);

            // Adicionar reticências após a primeira página, se necessário
            if (currentPage > 3)
            {
                DataRow dots = dt.NewRow();
                dots["Text"] = "...";
                dots["CssClass"] = "page-item disabled"; // Não clicável
                dt.Rows.Add(dots);
            }

            // Páginas adjacentes à atual (2 anteriores e 2 posteriores)
            for (int i = Math.Max(2, currentPage - 1); i <= Math.Min(totalPages - 1, currentPage + 1); i++)
            {
                DataRow page = dt.NewRow();
                page["PageNumber"] = i;
                page["Text"] = i.ToString();
                page["CssClass"] = (i == currentPage) ? "page-item active" : "page-item";
                dt.Rows.Add(page);
            }

            // Adicionar reticências antes da última página, se necessário
            if (currentPage < totalPages - 2)
            {
                DataRow dots = dt.NewRow();
                dots["Text"] = "...";
                dots["CssClass"] = "page-item disabled"; // Não clicável
                dt.Rows.Add(dots);
            }

            // Adicionar sempre a última página
            DataRow lastPage = dt.NewRow();
            lastPage["PageNumber"] = totalPages;
            lastPage["Text"] = totalPages.ToString();
            lastPage["CssClass"] = (currentPage == totalPages) ? "page-item active" : "page-item";
            dt.Rows.Add(lastPage);

            // Definir o `Repeater` com o DataTable
            PagerRepeater.DataSource = dt;
            PagerRepeater.DataBind();

            // Habilitar ou desabilitar os botões de "Anterior" e "Próximo"
            PreviousPageButton.Enabled = (currentPage > 1);
            NextPageButton.Enabled = (currentPage < totalPages);
        }

        protected void PageLinkButton_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            string commandArgument = btn.CommandArgument;

            if (commandArgument == "Previous" && GridViewPessoas.PageIndex > 0)
            {
                GridViewPessoas.PageIndex--;
            }
            else if (commandArgument == "Next" && GridViewPessoas.PageIndex < (TotalRows / GridViewPessoas.PageSize))
            {
                GridViewPessoas.PageIndex++;
            }
            else if (!string.IsNullOrEmpty(commandArgument) && int.TryParse(commandArgument, out int pageIndex))
            {
                GridViewPessoas.PageIndex = pageIndex - 1;
            }

            CarregarPessoas();
        }

        protected void GridViewPessoas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewPessoas.PageIndex = e.NewPageIndex;
            CarregarPessoas();
        }

        protected void GridViewPessoas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Editar")
            {
                if (e.CommandArgument != null)
                {
                    int id = Convert.ToInt32(e.CommandArgument);
                    Response.Redirect($"PessoaEditar.aspx?id={id}");
                }
            }
            else if (e.CommandName == "Excluir")
            {
                if (e.CommandArgument != null)
                {
                    int id = Convert.ToInt32(e.CommandArgument);
                    ExcluirPessoa(id);
                }
            }
        }

        private void ExcluirPessoa(int id)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString;

            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                string query = "DELETE FROM \"Pessoa\" WHERE \"Id\" = :id";
                OracleCommand command = new OracleCommand(query, connection);
                command.Parameters.Add(new OracleParameter("id", id));
                connection.Open();

                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    lblMensagem.Text = "Pessoa excluída com sucesso!";
                    mensagemContainer.Attributes["class"] = "alert alert-success";
                    mensagemContainer.Style["display"] = "block";
                    CarregarPessoas();
                }
                else
                {
                    lblMensagem.Text = "A Pessoa não foi excluída!";
                    mensagemContainer.Attributes["class"] = "alert alert-danger";
                    mensagemContainer.Style["display"] = "block";
                }
            }
        }
        protected void btnNovaPessoa_Click(object sender, EventArgs e)
        {
            Response.Redirect("PessoaCreate.aspx"); // Redireciona para a página de criação
        }
    }
}