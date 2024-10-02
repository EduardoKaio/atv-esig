using System;
using System.Configuration;
using Oracle.ManagedDataAccess.Client;

namespace project_esig
{
    public partial class PessoaCreate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString;
            int novoId = ObterProximoId(connectionString);
            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                // Certifique-se de que a instrução VALUES tem a mesma quantidade de valores que colunas.
                string query = "INSERT INTO \"Pessoa\" (\"Id\", \"Nome\", \"Cidade\", \"Email\", \"CEP\", \"Endereco\", \"Pais\", \"Usuario\", \"Telefone\", \"Data_Nascimento\", \"CargoId\") " +
                               "VALUES (:Id, :Nome, :Cidade, :Email, :CEP, :Endereco, :Pais, :Usuario, :Telefone, :Data_Nascimento, :CargoId)";

                using (OracleCommand command = new OracleCommand(query, connection))
                {
                    // Adicione o parâmetro Id corretamente
                    command.Parameters.Add(new OracleParameter("Id", novoId));
                    command.Parameters.Add(new OracleParameter("Nome", txtNome.Text));
                    command.Parameters.Add(new OracleParameter("Cidade", txtCidade.Text));
                    command.Parameters.Add(new OracleParameter("Email", txtEmail.Text));
                    command.Parameters.Add(new OracleParameter("CEP", txtCEP.Text));
                    command.Parameters.Add(new OracleParameter("Endereco", txtEndereco.Text));
                    command.Parameters.Add(new OracleParameter("Pais", txtPais.Text));
                    command.Parameters.Add(new OracleParameter("Usuario", txtUsuario.Text));
                    command.Parameters.Add(new OracleParameter("Telefone", txtTelefone.Text));

                    // Formatação de data
                    string dataNascimento = Convert.ToDateTime(txtDataNascimento.Text).ToString("yyyy-MM-dd");
                    command.Parameters.Add(new OracleParameter("Data_Nascimento", dataNascimento));

                    // Corrigido o nome do parâmetro para manter a consistência
                    command.Parameters.Add(new OracleParameter("CargoId", Convert.ToInt32(ddlCargoId.SelectedValue)));

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }

            // Redirecionar após a criação
            Response.Redirect("PessoaListagem.aspx?mensagem=newPessoa");
        }

        private int ObterProximoId(string connectionString)
        {
            int proximoId = 1; // Valor padrão caso não exista nenhuma entrada

            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                string query = "SELECT MAX(\"Id\") FROM \"Pessoa\"";
                using (OracleCommand command = new OracleCommand(query, connection))
                {
                    connection.Open();
                    var result = command.ExecuteScalar();
                    if (result != DBNull.Value)
                    {
                        proximoId = Convert.ToInt32(result) + 1; // Incrementa o maior ID encontrado
                    }
                }
            }

            return proximoId;
        }
    }
}