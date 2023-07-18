# Projeto MyCollection

Este projeto foi criado com base em um desafio, onde é possível gerenciar a coleção de Livros, DVDs e CDs.

Link do projeto Frontend: https://github.com/JonathanAmarall/my-collection-frontend

# Índice

- [Funcionalidades](#funcionalidades)
- [Stack](#stack)
- [Rodando com Docker Compose](#rodando-com-docker-compose)
- [Rodando os testes](#rodando-os-testes)
- [Documentação da API](#documentação-da-api)
- [Demonstração](#demonstração)
- [Futuras Implementações](#futuras-implementações)

## Funcionalidades

- Cadastrar Livros, CDs, ou DVDs.
- Filtrar os itens, por tipo (Livro, CD ou DVD), Status (disponível ou indisponível) e também através de um filtro global.
- Emprestar um Item, informando o contato de quem o pegará emprestado.
- Visualizar para quem foi emprestado, exibindo nome completo, email e telefone de contato.
- Adicionar ou visualizar localização à um Item.
- A tabela de itens possui paginação e também pode ser ordenado por todas as colunas.
- Cadastro de localização em formato de árvore de dados.

## Stack

**Back-end:**

- ASP.NET 6 (backend)
- Angular (frontned)
- PostgreSQL (banco de dados)
- Github Actions (CI)
- Docker

## Rodando com Docker Compose

Clone o projeto

```bash
  git clone https://github.com/JonathanAmarall/my-collection-api
```

Entre no diretório do projeto

```bash
  cd docker
```

Execute o comando para a subir a stack do projeto

```bash
  docker-compose up
```

Abra o seguinte link no seu navegador

```bash
  http://localhost:4200
```

Para visualizar a documentação da API com Swagger

```bash
  http://localhost:5099/swagger
```

## Rodando os testes

Com o projeto já clonado, rode o seguinte comando na raíz do projeto:

```bash
  dotnet test
```

# Documentação da API

## CollectionItem

#### CollectionItem -Retorna todos os itens paginados

```http
  GET /api/v1/collection-items
```

#### CollectionItem - Também é possível filtrar os itens e ordená-los

```http
  GET /api/v1/collection-items?GlobalFilter=${globalFilter}&SortOrder=${sortOrder}&SortField=${sortField}&Status=${status}&Type=${type}&PageNumber=1&PageSize=5'
```

| Parâmetro      | Tipo      | Descrição                                                 |
| :------------- | :-------- | :-------------------------------------------------------- |
| `globalFilter` | `string`  | **Opcional**. Palavra-chave a ser utilizada no filtro     |
| `sortOrder`    | `string`  | **Opcional**. Utilizar "asc" ou "desc"                    |
| `sortField`    | `string`  | **Opcional**. Nome da coluna a ser ordenada               |
| `status`       | `integer` | **Opcional**. 1 Para "Disponível" - 2 Para "Indisponível" |
| `type`         | `integer` | **Opcional**. 1 Para "Livro" - 2 Para "DVD" - 3 Para "CD" |

#### CollectionItem - Cadastrar um Item

```http
  POST /api/v1/collection-items
```

Enviar no corpo da requisição:
| Parâmetro | Tipo | Descrição |
| :---------- | :--------- | :------------------------------------------ |
| `title` | `string` | **Obrigatório**. O ID do Item a ser emprestado |
| `autor` | `string` | **Obrigatório**. O ID do Contato |
| `quantity` | `integer` | **Obrigatório**. Quantidade do item, deve ser > 0 |
| `edition` | `string` | **Opcional**. Email do contato |
| `itemType` | `integer` | **Obrigatório**. Tipo de Item. 1 Para "Livro" - 2 Para "DVD" - 3 Para "CD" |

#

#### CollectionItem - Emprestar um Item

```http
  PUT /api/v1/collection-items/${id}/lend
```

| Parâmetro | Tipo   | Descrição                                      |
| :-------- | :----- | :--------------------------------------------- |
| `id`      | `uuid` | **Obrigatório**. O ID do item a ser emprestado |

Enviar no corpo da requisição:
| Parâmetro | Tipo | Descrição |
| :---------- | :--------- | :------------------------------------------ |
| `collectionItemId` | `uuid` | **Obrigatório**. O ID do Item a ser emprestado |
| `contactId` | `uuid` | **Opcional**. O ID do Contato |
| `fullName` | `string` | **Opcional**. Nome completo do Contato |
| `email` | `string` | **Opcional**. Email do contato |
| `phone` | `string` | **Opcional**. Telefone do contato |

\*\*Nota: Caso não seja informado o ID do Contato, será obrigatório o uso dos campos "fullName", "email", "phone".
Caso o ID do contato seja informado, não será preciso informar estes campos.

#

#### CollectionItem - Adicionar localização de um Item.

```http
  PUT /api/v1/collection-items/${id}
```

| Parâmetro | Tipo   | Descrição                     |
| :-------- | :----- | :---------------------------- |
| `id`      | `uuid` | **Obrigatório**. O ID do Item |

Enviar no corpo da requisição:
| Parâmetro | Tipo | Descrição |
| :---------- | :--------- | :------------------------------------------ |
| `collectionItemId` | `uuid` | **Obrigatório**. O ID do Item |
| `locationId` | `uuid` | **Obrigatório**. O ID da Localização |

#

#### CollectionItem - Obter localização de um Item.

```http
  GET /api/v1/collection-items/${id}/location
```

| Parâmetro | Tipo   | Descrição                     |
| :-------- | :----- | :---------------------------- |
| `id`      | `uuid` | **Obrigatório**. O ID do Item |

#

#### CollectionItem - Retorna lista paginada de Contatos

```http
  GET /api/v1/collection-items/contacts?GlobalFilter=${globalFilter}&&PageNumber=1&PageSize=5'
```

| Parâmetro      | Tipo     | Descrição                                             |
| :------------- | :------- | :---------------------------------------------------- |
| `globalFilter` | `string` | **Opcional**. Palavra-chave a ser utilizada no filtro |

#

## Location

#### Location - Retorna lista raíz das Localizações

```http
  GET /api/v1/locations
```

#### Location - Cadastra uma nova localização

```http
  POST /api/v1/locations
```

Enviar no corpo da requisição:
| Parâmetro | Tipo | Descrição |
| :---------- | :--------- | :------------------------------------------ |
| `initials` | `string` | **Obrigatório**. Sigla da localização |
| `description` | `string` | **Obrigatório**. Descrição da localização |
| `parentId` | `uuid` | **Opcional**. Id da localização Pai, caso possua |

#### Location - Retorna os filhos de uma localização Pai

```http
  POST /api/v1/locations/${id}/childrens
```

| Parâmetro | Tipo   | Descrição                            |
| :-------- | :----- | :----------------------------------- |
| `id`      | `uuid` | **Obrigatório**. O ID da Localização |

#### Location - Retorna toda localização de forma resumida

```http
  GET /api/v1/locations/${id}/full-location
```

| Parâmetro | Tipo   | Descrição                            |
| :-------- | :----- | :----------------------------------- |
| `id`      | `uuid` | **Obrigatório**. O ID da Localização |

#### Location - Deleta uma localização

```http
  DELETE /api/v1/locations/${id}
```

| Parâmetro | Tipo   | Descrição                            |
| :-------- | :----- | :----------------------------------- |
| `id`      | `uuid` | **Obrigatório**. O ID da Localização |


## Demonstração
### Demonstração - Cadastro de Localização

![](https://github.com/JonathanAmarall/my-collection-api/blob/criando-readme/images/localizacao-demonstracao.gif)

### Demonstração - Adicionando Localização à um Item

![](https://github.com/JonathanAmarall/my-collection-api/blob/criando-readme/images/localizacao-uso.gif)

\*\*Nota: O cadastro de Localização permite, de forma flexível, adicionar qualquer tipo de localização.
Podendo atender grandes e pequenos cenários.

## Futuras Implementações

- Delete e Update de Item
- Gerenciamento de Contatos
- Cadastro de usuário
- Autenticação via JWT
- Melhorias no frontend para responsividade mobile
- Logs de auditoria
- Job Background para enviar email lembrando contato de devolver o Item no momento do vencimento.
- HealthChecks
