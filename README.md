# Projeto: Gestão de Salários - Asp.Net Web Forms

Este projeto é uma aplicação web desenvolvida em **Asp.Net Web Forms**, cujo objetivo principal é gerenciar e calcular os salários das pessoas, armazenados na tabela `pessoa_salario`. O projeto utiliza **procedures** no banco de dados para calcular os salários e inclui um CRUD para gerenciamento de pessoas.

## Estrutura da Tabela `pessoa_salario`

A tabela `pessoa_salario` é a principal tabela de resultado deste projeto. Ela contém as seguintes colunas:

- **pessoa_id** (int): Identificador único da pessoa.
- **nome** (varchar): Nome da pessoa.
- **salario** (number): Salário calculado da pessoa.

## Funcionalidades Implementadas

### 1. Listagem e Cálculo/Recalculo de Salários
- A aplicação oferece uma tela para **listagem dos salários** das pessoas.
- Existe a opção de **calcular/recalcular os salários**, que preenche a tabela `pessoa_salario` com base nos dados das tabelas `pessoa` e `cargo`.

### 2. Cálculo de Salários no Banco de Dados
- A implementação do cálculo dos salários foi realizada no banco de dados, através de **procedures**.
- O script `CreateProcedure_CalcularSalarios.sql` foi criado para facilitar a execução da procedure de cálculo de salários.

### 3. CRUD de Pessoa
- O projeto inclui um **CRUD completo de pessoas**, que permite as seguintes operações:
  - **Criar** novas pessoas.
  - **Atualizar** informações de uma pessoa.
  - **Excluir** pessoas existentes.
  - **Listar** todas as pessoas cadastradas.

## Instruções de Execução

### 1. Requisitos

- **Visual Studio**: Certifique-se de ter o Visual Studio instalado.
- **Oracle Database**: A aplicação utiliza um banco de dados Oracle. Você precisará ter o Oracle instalado e configurado.

### 2. Configuração do Banco de Dados

1. **Criação das Tabelas**:
   - As tabelas `pessoa`, `cargo` e `pessoa_salario` podem ser criadas rodando as migrations no projeto.

2. **Inserção de Dados**:
   - Os dados nas tabelas `pessoa` e `cargo` foram inseridos utilizando o software SQL Developer da Oracle.

### 3. Executando o Projeto Localmente

1. Clone este repositório.
2. Abra o projeto no **Visual Studio**.
3. Atualize as configurações de conexão no arquivo `Web.config` para apontar para seu banco de dados Oracle.
4. No **Gerenciador de Pacotes NuGet**, certifique-se de que todas as dependências necessárias estão instaladas.
   - Se houver problemas com o Oracle ou Crystal Reports, pode ser necessário reinstalar os pacotes relevantes.
5. Compile e execute o projeto no Visual Studio (usando `Ctrl + F5`).

### 4. Recalculando os Salários
- Na página de listagem de salários, utilize o botão de **Recalcular Salários** para acionar o processamento assíncrono.
- A tabela `pessoa_salario` será atualizada com base nas tabelas `pessoa` e `cargo`.

### 5. Gerenciamento de Pessoas
- Acesse o CRUD de pessoas na interface para **incluir**, **atualizar** ou **excluir** registros.
