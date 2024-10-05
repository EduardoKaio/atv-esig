using System;
using System.Data;
using Oracle.ManagedDataAccess.Client;

namespace project_esig
{
    public partial class PessoaEditar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (Request.QueryString["id"] != null)
                {
                    int id = Convert.ToInt32(Request.QueryString["id"]);
                    CarregarPessoa(id);
                }
            }
        }


        private void CarregarPessoa(int id)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString;

            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                string query = "SELECT \"Id\", \"Nome\", \"Cidade\", \"Email\", \"CEP\", \"Endereco\", \"Pais\", \"Usuario\", \"Telefone\", \"Data_Nascimento\", \"CargoId\" FROM \"Pessoa\" WHERE \"Id\" = :id";
                OracleCommand command = new OracleCommand(query, connection);
                command.Parameters.Add(new OracleParameter("id", id));
                connection.Open();

                OracleDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {

                    txtId.Text = reader["Id"].ToString();
                    txtNome.Text = reader["Nome"].ToString();
                    txtCidade.Text = reader["Cidade"].ToString();
                    txtEmail.Text = reader["Email"].ToString();
                    txtCep.Text = reader["CEP"].ToString();
                    txtEndereco.Text = reader["Endereco"].ToString();
                    txtPais.Text = reader["Pais"].ToString();
                    txtUsuario.Text = reader["Usuario"].ToString();
                    txtTelefone.Text = reader["Telefone"].ToString();

                    DateTime dataNascimento = Convert.ToDateTime(reader["Data_Nascimento"]);
                    txtDataNascimento.Text = dataNascimento.ToString("yyyy-MM-dd");

                    ddlCargoId.SelectedValue = reader["CargoId"].ToString();
                }
            }
        }


        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString;

            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                string query = "UPDATE \"Pessoa\" SET \"Nome\" = :nome, \"Cidade\" = :cidade, \"Email\" = :email, \"CEP\" = :cep, \"Endereco\" = :endereco, \"Pais\" = :pais, \"Usuario\" = :usuario, \"Telefone\" = :telefone, \"Data_Nascimento\" = :dataNascimento, \"CargoId\" = :cargoId WHERE \"Id\" = :id";
                OracleCommand command = new OracleCommand(query, connection);
                command.Parameters.Add(new OracleParameter("nome", txtNome.Text));
                command.Parameters.Add(new OracleParameter("cidade", txtCidade.Text));
                command.Parameters.Add(new OracleParameter("email", txtEmail.Text));
                command.Parameters.Add(new OracleParameter("cep", txtCep.Text));
                command.Parameters.Add(new OracleParameter("endereco", txtEndereco.Text));
                command.Parameters.Add(new OracleParameter("pais", txtPais.Text));
                command.Parameters.Add(new OracleParameter("usuario", txtUsuario.Text));
                command.Parameters.Add(new OracleParameter("telefone", txtTelefone.Text));

                DateTime dataNascimento = DateTime.ParseExact(txtDataNascimento.Text, "yyyy-MM-dd", null);
                command.Parameters.Add(new OracleParameter("dataNascimento", dataNascimento));

                command.Parameters.Add(new OracleParameter("cargoId", Convert.ToInt32(ddlCargoId.SelectedValue)));
                command.Parameters.Add(new OracleParameter("id", Convert.ToInt32(txtId.Text)));

                connection.Open();
                command.ExecuteNonQuery();
            }

            Response.Redirect("PessoaListagem.aspx?mensagem=editada");
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {

            Response.Redirect("PessoaListagem.aspx");
        }
    }
}