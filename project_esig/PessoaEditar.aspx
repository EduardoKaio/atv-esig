<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PessoaEditar.aspx.cs" Inherits="project_esig.PessoaEditar" %>

<!DOCTYPE html>
<html lang="pt-BR">
<head>
    <meta charset="utf-8" />
    <title>Edição de Pessoa</title>
    <!-- Importando o Bootstrap CSS -->
    <link href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container mt-5">
            <h2 class="mb-4">Edição de Pessoa</h2>
            
            <!-- ID (Somente leitura) -->
            <div class="form-group">
                <label for="txtId">ID:</label>
                <asp:TextBox ID="txtId" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
            </div>
            
            <!-- Nome -->
            <div class="form-group">
                <label for="txtNome">Nome:</label>
                <asp:TextBox ID="txtNome" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            
            <!-- Cidade -->
            <div class="form-group">
                <label for="txtCidade">Cidade:</label>
                <asp:TextBox ID="txtCidade" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            
            <!-- Email -->
            <div class="form-group">
                <label for="txtEmail">Email:</label>
                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            
            <!-- CEP -->
            <div class="form-group">
                <label for="txtCep">CEP:</label>
                <asp:TextBox ID="txtCep" runat="server" CssClass="form-control"></asp:TextBox>
            </div>

            <!-- Endereço -->
            <div class="form-group">
                <label for="txtEndereco">Endereço:</label>
                <asp:TextBox ID="txtEndereco" runat="server" CssClass="form-control"></asp:TextBox>
            </div>

            <!-- País -->
            <div class="form-group">
                <label for="txtPais">País:</label>
                <asp:TextBox ID="txtPais" runat="server" CssClass="form-control"></asp:TextBox>
            </div>

            <!-- Usuário -->
            <div class="form-group">
                <label for="txtUsuario">Usuário:</label>
                <asp:TextBox ID="txtUsuario" runat="server" CssClass="form-control"></asp:TextBox>
            </div>

            <!-- Telefone -->
            <div class="form-group">
                <label for="txtTelefone">Telefone:</label>
                <asp:TextBox ID="txtTelefone" runat="server" CssClass="form-control"></asp:TextBox>
            </div>

            <!-- Data de Nascimento -->
            <div class="form-group">
                <label for="txtDataNascimento">Data de Nascimento:</label>
                <asp:TextBox ID="txtDataNascimento" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
            </div>

            <!-- Cargo ID (Select) -->
            <div class="form-group">
                <label for="ddlCargoId">Cargo:</label>
                <asp:DropDownList ID="ddlCargoId" runat="server" CssClass="form-control">
                    <asp:ListItem Value="1" Text="Estagiário"></asp:ListItem>
                    <asp:ListItem Value="2" Text="Técnico"></asp:ListItem>
                    <asp:ListItem Value="3" Text="Analista"></asp:ListItem>
                    <asp:ListItem Value="4" Text="Coordenador"></asp:ListItem>
                    <asp:ListItem Value="5" Text="Gerente"></asp:ListItem>
                </asp:DropDownList>
            </div>

            <!-- Botões -->
            <div class="form-group">
                <asp:Button ID="btnSalvar" runat="server" Text="Salvar" CssClass="btn btn-primary" OnClick="btnSalvar_Click" />
                <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-secondary" OnClick="btnCancelar_Click" />
            </div>
        </div>
    </form>

    <!-- Importando o Bootstrap JS (opcional, para funcionalidades como modais) -->
    <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@4.5.2/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>