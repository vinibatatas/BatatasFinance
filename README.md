# BatatasFinance

API REST para controle financeiro pessoal. O projeto permite cadastrar usuários, contas bancárias, cartões de crédito, despesas e receitas, além de consultar despesas fixas e resumos de faturas.

## Tecnologias e arquitetura

- .NET 10 e ASP.NET Core Web API
- PostgreSQL
- Dapper para acesso a dados
- Organização em quatro projetos:
  - `BatatasFinance.Domain`: entidades, enums e contratos dos repositórios.
  - `BatatasFinance.Application`: casos de uso e contratos de entrada e saída.
  - `BatatasFinance.Infrastructure`: repositórios SQL e conexão PostgreSQL.
  - `BatatasFinance.API`: controllers, configuração e inicialização da API.

O acesso a dados registrado atualmente usa Dapper e `Npgsql`. Embora existam referências a Entity Framework Core nos projetos, o `DbContext` está vazio e não há migrations configuradas. O script [init.sql](./init.sql) define o esquema inicial do banco.

## Requisitos

- .NET 10 SDK
- Docker com Docker Compose, ou uma instância PostgreSQL compatível

## Configuração e execução

Na raiz do repositório, inicie o banco de dados:

```powershell
docker compose up -d db
```

O serviço do Compose cria o banco `batatas_db`, com usuário `postgres` e senha `postgres`, expõe a porta `5432` e executa `init.sql` na inicialização. O script só é executado automaticamente quando o volume de dados é criado pela primeira vez.

Configure a conexão da API. O valor definido em `appsettings.Development.json` é um placeholder e precisa ser substituído por uma senha válida. Para usar as credenciais locais do Compose, forneça a conexão por variável de ambiente no PowerShell:

```powershell
$env:ConnectionStrings__DefaultConnection = "Host=localhost;Port=5432;Database=batatas_db;Username=postgres;Password=postgres"
```

Execute a API usando o perfil HTTP:

```powershell
dotnet run --project .\BatatasFinance.API\BatatasFinance.API.csproj --launch-profile http
```

A API ficará disponível em `http://localhost:5019`. O perfil `https` também disponibiliza `https://localhost:7004` e `http://localhost:5019`.

Para compilar a solução:

```powershell
dotnet build .\BatatasFinance.sln
```

Não há projeto de testes automatizados na solução. O endpoint de exemplo presente em `BatatasFinance.API.http` ainda aponta para `/weatherforecast`, que não está implementado.

## Endpoints

Todas as rotas usam o prefixo `/api`. Os identificadores de usuários, contas e cartões são GUIDs. Os corpos e respostas usam JSON.

### Usuários

| Método | Rota | Corpo | Resposta |
|---|---|---|---|
| `POST` | `/api/Users` | `{"userName":"Ana"}` | `201 Created` com `id` e `name`; `400 Bad Request` para nome inválido. |

### Contas

| Método | Rota | Corpo ou parâmetro | Resposta |
|---|---|---|---|
| `POST` | `/api/Account` | Cadastro de conta, descrito abaixo. | `201 Created`. |
| `GET` | `/api/Account/users/{userId}` | `userId` na rota. | `200 OK` com lista de contas ou `204 No Content` quando vazia. |

Corpo de cadastro:

```json
{
  "accountName": "Conta principal",
  "bankName": "Banco",
  "balance": 1500.00,
  "rentableTax": null,
  "userId": "00000000-0000-0000-0000-000000000000"
}
```

`rentableTax` é opcional, mas não é utilizado pelo caso de uso de cadastro atual.

### Cartões de crédito

| Método | Rota | Corpo ou parâmetro | Resposta |
|---|---|---|---|
| `POST` | `/api/CreditCard` | Cadastro de cartão, descrito abaixo. | `201 Created`; `400 Bad Request` para dados rejeitados pelo domínio. |
| `GET` | `/api/CreditCard/{userId}` | `userId` na rota. | `200 OK` com lista de cartões ou `204 No Content` quando vazia. |
| `GET` | `/api/CreditCard/{creditCardId}/summary/{userId}` | IDs do cartão e do usuário na rota. | `200 OK` com limite total, limite disponível e valor da fatura; `404 Not Found` se o cartão não existir ou não pertencer ao usuário. |
| `GET` | `/api/CreditCard/expenses/{creditCardId}/summary/{userId}` | IDs do cartão e do usuário na rota. | `200 OK` com despesas do período da fatura; `400 Bad Request` em caso de exceção. |

Corpo de cadastro:

```json
{
  "name": "Cartão principal",
  "creditLimit": 5000.00,
  "closingDay": 10,
  "dueDay": 17,
  "userId": "00000000-0000-0000-0000-000000000000",
  "creditFlag": 1
}
```

`creditFlag`: `1` para Mastercard e `2` para Visa. Os dias de fechamento e vencimento devem estar entre 1 e 31; o limite não pode ser negativo.

O resumo da fatura considera a soma das despesas associadas ao cartão. A rota de despesas delimita o período com base no dia de fechamento do cartão.

### Despesas

| Método | Rota | Corpo ou parâmetro | Resposta |
|---|---|---|---|
| `POST` | `/api/Expenses` | Cadastro de despesa, descrito abaixo. | `201 Created`; `400 Bad Request` quando o cadastro falha. |
| `GET` | `/api/Expenses/{userId}` | `userId` na rota. | `200 OK` com lista de despesas ou `204 No Content` quando vazia. |
| `GET` | `/api/Expenses/fixed/{userId}` | `userId` na rota. | `200 OK` com despesas fixas. |
| `GET` | `/api/Expenses/fixed/total/{userId}` | `userId` na rota. | `200 OK` com o total numérico ou `204 No Content` quando o total for zero ou negativo. |

Corpo de cadastro:

```json
{
  "description": "Mercado",
  "amount": 250.00,
  "category": 1,
  "paymentMethod": 3,
  "isFixed": false,
  "userId": "00000000-0000-0000-0000-000000000000",
  "creditCardId": null,
  "accountId": "00000000-0000-0000-0000-000000000000",
  "installments": 1
}
```

Enums aceitos:

- `category`: `1` Food, `2` Clothing, `3` Transport, `4` Transportation, `5` Other.
- `paymentMethod`: `1` Pix, `2` CreditCard, `3` DebitCard.

O cadastro exige cartão para pagamento por crédito e conta para pagamentos sem crédito. O número de parcelas deve estar entre 1 e 12.

### Receitas

| Método | Rota | Corpo ou parâmetro | Resposta |
|---|---|---|---|
| `POST` | `/api/Incomes` | Cadastro de receita, descrito abaixo. | `201 Created`. |
| `GET` | `/api/Incomes/users/{userId}` | `userId` na rota. | `200 OK` com lista de receitas. |
| `PUT` | `/api/Incomes/users/{userId}/incomes/{incomeId}` | IDs na rota e os campos de atualização no corpo. | `200 OK`. |

Corpo para cadastrar uma receita:

```json
{
  "description": "Salário",
  "amount": 4000.00,
  "isRecurring": true,
  "userId": "00000000-0000-0000-0000-000000000000"
}
```

O corpo do `PUT` aceita `description`, `amount`, `isRecurring` e `userId`. A receita é localizada pelo par `incomeId` e `userId`.

## Formato das respostas

As consultas retornam os campos definidos pelos respectivos casos de uso:

- Contas: `id`, `accountName`, `bankName`, `balance`, `dateCreated` e `userId`.
- Cartões: `id`, `name`, `creditLimit`, `closingDay`, `dueDay`, `userId`, `creditFlag` e `createdAt`.
- Despesas: `id`, `description`, `amount`, `category`, `paymentMethod`, `isFixed`, `userId`, `creditCardId`, `installments` e `createdAt`.
- Receitas: `id`, `description`, `amount`, `isRecurring`, `userId` e `createdAt`.
- Resumo de cartão: `userId`, `creditCardId`, `totalLimit`, `availableLimit` e `currentInvoiceAmount`.

Enums são serializados como números inteiros. Valores monetários são decimais.

## Limitações conhecidas do esquema atual

- A entidade e o contrato de resposta de conta incluem `AccountName` e data de criação, mas a tabela `Accounts` em `init.sql` não tem essas colunas e o repositório não as grava. Assim, o nome enviado no cadastro não é persistido e a resposta da consulta não pode ser preenchida integralmente pelo esquema atual.
- `Expenses.AccountId` está definido como obrigatório em `init.sql`, mas despesas pagas com cartão são registradas sem conta. Nesse esquema, o cadastro de despesas no crédito pode falhar no banco.
- Não há autenticação ou autorização configuradas na API.
- OpenAPI/Swagger não está habilitado em `Program.cs`.
