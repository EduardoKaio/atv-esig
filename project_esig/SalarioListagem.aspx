<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SalarioListagem.aspx.cs" Inherits="project_esig.SalarioListagem" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="Oracle.ManagedDataAccess.Client" %>

<!DOCTYPE html>
<html lang="pt-BR">
<head runat="server">
    <title>Listagem de Salários</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-QWTKZyjpPEjISv5WaRU9OFeRpok6YctnYmDr5pNlyT2bRjXh0JMhjY6hW+ALEwIH" crossorigin="anonymous">
<link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta3/css/all.min.css" rel="stylesheet">
</head>
<body>
    <form id="form1" runat="server">
        <div class="container mt-5">
            <h2 class="mb-4">Listagem de Salários</h2>

            <div id="mensagemContainer" runat="server" class="alert" style="display:none;width:auto; font-weight:600" role="alert">
                <asp:Label ID="lblMensagem" runat="server" Text=""></asp:Label>
            </div>
            <div class="d-flex justify-content-between">
                <a href="Default.aspx" class="btn btn-secondary">
                    <i class="fas fa-arrow-left"></i> 
                </a>
                <asp:Button ID="btnCalcularSalarios" runat="server" Text="Calcular Salários" CssClass="btn btn-success "
                OnClick="btnCalcularSalarios_Click" class="mt-3" />
            </div>
            <asp:GridView ID="GridViewSalarios" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-hover mt-2 table-striped"
                         PageSize="10">
                <Columns>
                    <asp:BoundField DataField="PessoaID" HeaderText="Id" />
                    <asp:BoundField DataField="Nome" HeaderText="Nome" />
                    <asp:BoundField DataField="Salario" HeaderText="Salário" DataFormatString="{0:C}" />
                </Columns>
                <PagerStyle CssClass="pagination" />
            </asp:GridView>

            <nav aria-label="Page navigation example">
                <ul class="pagination justify-content-center">
                    <!-- Link para a página anterior -->
                    <li class="page-item">
                        <asp:LinkButton ID="PreviousPageButton" runat="server" CssClass="page-link" OnClick="PreviousPageButton_Click" aria-label="Previous" Enabled="false">
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
                        <asp:LinkButton ID="NextPageButton" runat="server" CssClass="page-link" OnClick="NextPageButton_Click" aria-label="Next" Enabled="false">
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