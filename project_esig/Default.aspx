<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="project_esig._Default" %>

<!DOCTYPE html>
<html lang="pt-BR">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>Página Inicial - Atividade Técnica ESIG</title>
    <!-- Link para o Bootstrap CDN -->
    <link href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
    <form runat="server">
        <div class="container mt-5">
            <div class="jumbotron text-center">
                <img width="30%" src="./esig-group.png" />
                <h1 class="display-4">Atividade Técnica - ESIG</h1>
                <p class="lead">Bem-vindo à aplicação para gerenciamento de Pessoas e Salários.</p>
                <hr class="my-4" />

                <p>Clique nos botões abaixo para acessar as funcionalidades:</p>
                
                <div class="row justify-content-center mt-4">
                    <div class="col-md-6">
                        <!-- Botão para Listagem de Pessoas -->
                        <a href="PessoaListagem.aspx" class="btn btn-primary btn-lg btn-block" role="button">
                            Listagem de Pessoas
                        </a>
                    </div>
                    <div class="col-md-6">
                        <!-- Botão para Listagem de Salários -->
                        <a href="SalarioListagem.aspx" class="btn btn-success btn-lg btn-block" role="button">
                            Listagem de Salários
                        </a>
                    </div>
                </div>
            </div>
        </div>
    </form>

    <!-- Bootstrap JS e dependências (Opcional para funcionalidades JS do Bootstrap) -->
    <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.9.3/dist/umd/popper.min.js"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
</body>
</html>