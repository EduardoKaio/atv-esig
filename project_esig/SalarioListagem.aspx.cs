using System;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Web.UI.WebControls;
using Oracle.ManagedDataAccess.Client;
using System.IO;
using System.Threading.Tasks;

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
               
                Task.Run(async () =>
                {
                    await CarregarSalariosAsync();
                    AtualizarPaginacao();
                }).Wait(); 
            }
        }

        private async Task CarregarSalariosAsync()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString;

            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                string countQuery = "SELECT COUNT(*) FROM \"pessoa_salario\"";
                OracleCommand countCommand = new OracleCommand(countQuery, connection);

                await connection.OpenAsync();
                TotalRows = Convert.ToInt32(await countCommand.ExecuteScalarAsync());
                if (TotalRows == 0)
                {
                    lblSemDados.Visible = true; 
                    GridViewSalarios.Visible = false; 
                    PagerRepeater.Visible = false;
                    return;
                }

                lblSemDados.Visible = false; 
                GridViewSalarios.Visible = true; 

                // Carregar dados da página atual
                string query = "SELECT \"PessoaID\", \"Nome\", \"Salario\" FROM \"pessoa_salario\" ORDER BY \"PessoaID\" ASC OFFSET :Offset ROWS FETCH NEXT :PageSize ROWS ONLY";
                OracleCommand command = new OracleCommand(query, connection);
                command.Parameters.Add("Offset", OracleDbType.Int32).Value = GridViewSalarios.PageIndex * GridViewSalarios.PageSize;
                command.Parameters.Add("PageSize", OracleDbType.Int32).Value = GridViewSalarios.PageSize;

                OracleDataAdapter adapter = new OracleDataAdapter(command);
                DataTable dt = new DataTable();
                await Task.Run(() => adapter.Fill(dt));

                GridViewSalarios.DataSource = dt;
                GridViewSalarios.DataBind();
            }
        }

        private void AtualizarPaginacao()
        {
            
            if (TotalRows == 0)
            {
                PagerRepeater.Visible = false; 
                return;
            }

            PagerRepeater.Visible = true;

            
            int totalPages = Math.Max(1, (int)Math.Ceiling((double)TotalRows / GridViewSalarios.PageSize));
            int currentPage = GridViewSalarios.PageIndex + 1;
            DataTable dt = new DataTable();
            dt.Columns.Add("PageNumber");
            dt.Columns.Add("Text");
            dt.Columns.Add("CssClass");

            
            DataRow firstPage = dt.NewRow();
            firstPage["PageNumber"] = 1;
            firstPage["Text"] = "1";
            firstPage["CssClass"] = (currentPage == 1) ? "page-item active" : "page-item";
            dt.Rows.Add(firstPage);

           
            if (currentPage > 3)
            {
                DataRow dots = dt.NewRow();
                dots["Text"] = "...";
                dots["CssClass"] = "page-item disabled";
                dt.Rows.Add(dots);
            }

            
            for (int i = Math.Max(2, currentPage - 1); i <= Math.Min(totalPages - 1, currentPage + 1); i++)
            {
                DataRow page = dt.NewRow();
                page["PageNumber"] = i;
                page["Text"] = i.ToString();
                page["CssClass"] = (i == currentPage) ? "page-item active" : "page-item";
                dt.Rows.Add(page);
            }

            
            if (currentPage < totalPages - 2)
            {
                DataRow dots = dt.NewRow();
                dots["Text"] = "...";
                dots["CssClass"] = "page-item disabled"; 
                dt.Rows.Add(dots);
            }

            
            DataRow lastPage = dt.NewRow();
            lastPage["PageNumber"] = totalPages;
            lastPage["Text"] = totalPages.ToString();
            lastPage["CssClass"] = (currentPage == totalPages) ? "page-item active" : "page-item";
            dt.Rows.Add(lastPage);

           
            PagerRepeater.DataSource = dt;
            PagerRepeater.DataBind();

            
            PreviousPageButton.Enabled = (currentPage > 1);
            NextPageButton.Enabled = (currentPage < totalPages);
        }

        protected void PreviousPageButton_Click(object sender, EventArgs e)
        {
            if (GridViewSalarios.PageIndex > 0)
            {
                GridViewSalarios.PageIndex--;
                Task.Run(async () =>
                {
                    await CarregarSalariosAsync();
                    AtualizarPaginacao();
                }).Wait();
            }
        }

        protected void NextPageButton_Click(object sender, EventArgs e)
        {
            int totalPages = (int)Math.Ceiling((double)TotalRows / GridViewSalarios.PageSize);
            if (GridViewSalarios.PageIndex < totalPages - 1)
            {
                GridViewSalarios.PageIndex++;
                Task.Run(async () =>
                {
                    await CarregarSalariosAsync();
                    AtualizarPaginacao();
                }).Wait();
            }
        }

        protected void PageLinkButton_Click(object sender, EventArgs e)
        {
            LinkButton linkButton = (LinkButton)sender;
            int pageNumber = Convert.ToInt32(linkButton.CommandArgument);

            GridViewSalarios.PageIndex = pageNumber - 1; 
            Task.Run(async () =>
            {
                await CarregarSalariosAsync();
                AtualizarPaginacao();
            }).Wait();
        }

        protected async void btnCalcularSalarios_Click(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString;

            using (OracleConnection conn = new OracleConnection(connectionString))
            {
                try
                {
                    await conn.OpenAsync(); // Abre a conexão de forma assíncrona

                   
                    string sqlScript;
                    using (StreamReader reader = new StreamReader(Server.MapPath("~/SQLScripts/CreateProcedure_CalcularSalarios.sql")))
                    {
                        sqlScript = await reader.ReadToEndAsync(); 
                    }
                    using (OracleCommand cmd = new OracleCommand(sqlScript, conn))
                    {
                        cmd.CommandType = CommandType.Text;
                        await cmd.ExecuteNonQueryAsync();
                    }

                    
                    using (OracleCommand cmd = new OracleCommand("CALCULARSALARIOS", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        await cmd.ExecuteNonQueryAsync();
                    }

                    mensagemContainer.Attributes["class"] = "alert alert-success"; 
                    lblMensagem.Text = "Salários calculados com sucesso!";
                    mensagemContainer.Style["display"] = "block"; 

                    await CarregarSalariosAsync();
                    AtualizarPaginacao();
                }
                catch (Exception ex)
                {
                    mensagemContainer.Attributes["class"] = "alert alert-danger"; 
                    lblMensagem.Text = "Erro ao calcular os salários: " + ex.Message;
                    mensagemContainer.Style["display"] = "block"; 
                }
            }
        }
    }
}