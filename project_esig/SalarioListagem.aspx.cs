using System;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Web.UI.WebControls;
using Oracle.ManagedDataAccess.Client;
using System.IO;

namespace project_esig
{
    public partial class SalarioListagem : System.Web.UI.Page
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
                CarregarSalarios();
                AtualizarPaginacao();
            }
        }

        private void CarregarSalarios()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString;

            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                string query = "SELECT \"PessoaID\", \"Nome\", \"Salario\" FROM \"pessoa_salario\" ORDER BY \"PessoaID\" ASC";

                OracleCommand command = new OracleCommand(query, connection);
                connection.Open();

                OracleDataAdapter adapter = new OracleDataAdapter(command);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                TotalRows = dt.Rows.Count; // Armazenar o total de registros

                // Paginação manual
                int currentPage = GridViewSalarios.PageIndex + 1; // PageIndex começa em 0
                int pageSize = GridViewSalarios.PageSize;

                // Criar uma nova DataTable apenas com os itens da página atual
                DataTable paginatedTable = dt.Clone();
                int startRow = (currentPage - 1) * pageSize;
                int endRow = Math.Min(startRow + pageSize, TotalRows);

                for (int i = startRow; i < endRow; i++)
                {
                    paginatedTable.ImportRow(dt.Rows[i]);
                }

                GridViewSalarios.DataSource = paginatedTable;
                GridViewSalarios.DataBind();

                AtualizarPaginacao();
            }
        }

        private void AtualizarPaginacao()
        {
            int totalPages = (int)Math.Ceiling((double)TotalRows / GridViewSalarios.PageSize);
            int currentPage = GridViewSalarios.PageIndex + 1;
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

        protected void PreviousPageButton_Click(object sender, EventArgs e)
        {
            if (GridViewSalarios.PageIndex > 0)
            {
                GridViewSalarios.PageIndex--;
                CarregarSalarios();
                AtualizarPaginacao();
            }
        }

        protected void NextPageButton_Click(object sender, EventArgs e)
        {
            int totalPages = (int)Math.Ceiling((double)TotalRows / GridViewSalarios.PageSize);
            if (GridViewSalarios.PageIndex < totalPages - 1)
            {
                GridViewSalarios.PageIndex++;
                CarregarSalarios();
                AtualizarPaginacao();
            }
        }

        protected void PageLinkButton_Click(object sender, EventArgs e)
        {
            LinkButton linkButton = (LinkButton)sender;
            int pageNumber = Convert.ToInt32(linkButton.CommandArgument);

            GridViewSalarios.PageIndex = pageNumber - 1; // PageIndex começa em 0
            CarregarSalarios();
            AtualizarPaginacao();
        }

        protected void btnCalcularSalarios_Click(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString;

            using (OracleConnection conn = new OracleConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // Ler o script da procedure do arquivo SQL
                    string sqlScript = File.ReadAllText(Server.MapPath("~/SQLScripts/CreateProcedure_CalcularSalarios.sql"));

                    using (OracleCommand cmd = new OracleCommand(sqlScript, conn))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.ExecuteNonQuery(); // Executa o script SQL
                    }

                    // Agora chama a procedure criada para calcular os salários
                    using (OracleCommand cmd = new OracleCommand("CALCULARSALARIOS", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.ExecuteNonQuery();
                    }

                    mensagemContainer.Attributes["class"] = "alert alert-success"; // Cor verde para sucesso
                    lblMensagem.Text = "Salários calculados com sucesso!";
                    mensagemContainer.Style["display"] = "block"; // Exibir a mensagem

                    CarregarSalarios();
                }
                catch (Exception ex)
                {
                    mensagemContainer.Attributes["class"] = "alert alert-danger"; // Cor vermelha para erro
                    lblMensagem.Text = "Erro ao calcular os salários: " + ex.Message;
                    mensagemContainer.Style["display"] = "block"; // Exibir a mensagem
                }
            }
        }
    }
}