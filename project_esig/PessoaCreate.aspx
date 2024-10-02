<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PessoaCreate.aspx.cs" Inherits="project_esig.PessoaCreate" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="Oracle.ManagedDataAccess.Client" %>

<!DOCTYPE html>
<html lang="pt-BR">
<head runat="server">
    <title>Criar Nova Pessoa</title>
    <link href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container mt-5">
            <h2 class="mb-4">Criar Nova Pessoa</h2>

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
                 <label for="txtCEP">CEP:</label>
                 <asp:TextBox ID="txtCEP" runat="server" CssClass="form-control"></asp:TextBox>
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

            <asp:Button ID="btnSalvar" runat="server" Text="Salvar" CssClass="btn btn-primary" OnClick="btnSalvar_Click" />
        </div>
    </form>
</body>
</html>