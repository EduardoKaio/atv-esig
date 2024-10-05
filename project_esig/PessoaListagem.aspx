<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PessoaListagem.aspx.cs" Inherits="project_esig.PessoaListagem" %>

<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="Oracle.ManagedDataAccess.Client" %>

<!DOCTYPE html>
<html lang="pt-BR">
<head runat="server">
    <title>Lista de Pessoas</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-QWTKZyjpPEjISv5WaRU9OFeRpok6YctnYmDr5pNlyT2bRjXh0JMhjY6hW+ALEwIH" crossorigin="anonymous">
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta3/css/all.min.css" rel="stylesheet">
</head>
<body>

    <form id="form1" runat="server">
    <div class="container mt-5">
        <h2 class="mb-4">Lista de Pessoas</h2>

        <!-- Mensagens de sucesso ou erro -->
        <div id="mensagemContainer" runat="server" class="alert" style="display:none;width:auto; font-weight:600" role="alert">
            <asp:Label ID="lblMensagem" runat="server" Text=""></asp:Label>
        </div>
        <div class="d-flex justify-content-between"> 
            <a href="Default.aspx" class="btn btn-secondary">
                <i class="fas fa-arrow-left"></i> 
            </a>
            <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn btn-success" OnClick="btnNovaPessoa_Click">
                <i class="fas fa-plus"></i> Adicionar pessoa
            </asp:LinkButton>
        </div>
        
        <!-- GridView para listagem de pessoas -->
        <asp:GridView ID="GridViewPessoas" runat="server" AutoGenerateColumns="False" CssClass="table mt-2 table-bordered table-striped table-hover" pagesize="10"
                      OnRowCommand="GridViewPessoas_RowCommand">
            <Columns>
                <asp:BoundField DataField="Id" HeaderText="Id" />
                <asp:BoundField DataField="Nome" HeaderText="Nome" />
                <asp:BoundField DataField="Email" HeaderText="Email" />
                <asp:BoundField DataField="Telefone" HeaderText="Telefone" />
                <asp:BoundField DataField="Cargo" HeaderText="Cargo" />
                <asp:TemplateField>
                 <ItemTemplate>
                     <div class="d-flex justify-content-evenly">
                         <div>
                            <a href='PessoaEditar.aspx?id=<%# Eval("Id") %>' class="btn btn-primary btn-circle">
                            <i class="fas fa-pen"></i> <!-- Ícone de lápis (editar) -->
                            </a>
                         </div>
                         <div>
                            <asp:LinkButton ID="lnkExcluir" runat="server" CommandName="Excluir" CommandArgument='<%# Eval("Id") %>' OnClientClick="return confirm('Tem certeza que deseja excluir?');" CssClass="btn btn-danger btn-circle">
                            <i class="fas fa-trash"></i>
                        </asp:LinkButton>
                         </div>
                     </div>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>

        <!-- Container para paginação -->
        <nav aria-label="Page navigation example">
            <ul class="pagination justify-content-center">
                <!-- Link para a página anterior -->
                <li class="page-item">
                    <asp:LinkButton ID="PreviousPageButton" runat="server" CommandArgument="Previous" CssClass="page-link" OnClick="PageLinkButton_Click" aria-label="Previous">
                        <span aria-hidden="true">&laquo;</span>
                    </asp:LinkButton>
                </li>

                <!-- Páginas numéricas -->
                <asp:Repeater ID="PagerRepeater" runat="server">
                    <ItemTemplate>
                        <li class='<%# Eval("CssClass") %>'>
                            <asp:LinkButton ID="PageLinkButton" runat="server" CommandArgument='<%# Eval("PageNumber") %>' OnClick="PageLinkButton_Click" CssClass="page-link">
                                <%# Eval("Text") %>
                            </asp:LinkButton>
                        </li>
                    </ItemTemplate>
                </asp:Repeater>

                <!-- Link para a próxima página -->
                <li class="page-item">
                    <asp:LinkButton ID="NextPageButton" runat="server" CommandArgument="Next" CssClass="page-link" OnClick="PageLinkButton_Click" aria-label="Next">
                        <span aria-hidden="true">&raquo;</span>
                    </asp:LinkButton>
                </li>
            </ul>
        </nav>
    </div>
</form>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js" integrity="sha384-YvpcrYf0tY3lHB60NNkmXc5s9fDVZLESaAA55NDzOxhy9GkcIdslK1eN7N6jIeHz" crossorigin="anonymous"></script>
</body>
</html>