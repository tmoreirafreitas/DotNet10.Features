# .NET 10 na prática — Do Console à Web API

Repositório complementar à apresentação **Novidades do .NET 10 & EF Core 10**. Aqui, os conceitos são explorados por meio de exemplos em uma aplicação Console e em uma Web API.

A proposta é demonstrar os recursos em código, explicar seus cenários de uso e facilitar a experimentação durante ou após a apresentação.

## Conteúdo

- [Apresentação](./novidades_dotnet10.pptx)
- Exemplos práticos em uma aplicação Console.
- Exemplos de desenvolvimento web com ASP.NET Core e EF Core 10.

## Demonstrações

### Console App

- **Extension Members (C# 14):** demonstração de membros de extensão e de como ampliar a forma de expor funcionalidades associadas a tipos existentes.
- **ISOWeek:** uso de semanas no padrão ISO para trabalhar com datas e cenários de relatórios temporais.
- **Null-Conditional Assignment (C# 14):** atribuição condicional com `?.`, evitando a necessidade de uma verificação explícita de nulidade em determinados cenários. O exemplo também permite discutir quando essa sintaxe é apropriada e quando uma atribuição ignorada pode ocultar um problema de lógica.

### Web API

- **OpenAPI 3.1:** geração e exposição da descrição da API.
- **Scalar:** interface para explorar a documentação e testar os endpoints.
- **Server-Sent Events (SSE):** envio de atualizações do servidor para o cliente por meio de um fluxo de eventos.
- **EF Core 10 — filtros de consulta nomeados:** configuração e desativação seletiva de filtros, como filtros de exclusão lógica ou multitenancy.
- **LINQ — `LeftJoin()` e `RightJoin()`:** demonstração dos operadores de junção e de seus cenários de consulta.

## Requisitos

- SDK do **.NET 10**.
- Um editor ou IDE compatível com .NET, como Visual Studio ou Visual Studio Code.
- Para executar as demonstrações de persistência, os serviços e as configurações de banco de dados exigidos pelo projeto.

## Como executar

Na raiz do repositório, restaure e compile a solução:

```bash
dotnet restore <caminho-da-solucao.slnx>
dotnet build <caminho-da-solucao.slnx>
```

Execute cada demonstração informando o caminho do respectivo arquivo `.csproj`:

```bash
# Aplicação Console
dotnet run --project <caminho-do-projeto-console.csproj>

# Web API
dotnet run --project <caminho-do-projeto-webapi.csproj>
```

> **Nota:** substitua os caminhos entre `<` e `>` pelos nomes e diretórios reais dos projetos neste repositório. Caso a solução não tenha um arquivo `.slnx`, execute os comandos a partir do diretório de cada projeto.

Ao iniciar a Web API, consulte a saída do terminal para identificar o endereço local utilizado. A interface do Scalar e o documento OpenAPI estarão disponíveis conforme as rotas configuradas na aplicação.

## Objetivo da demonstração

O foco é apresentar exemplos pequenos e contextualizados, conectando as novidades da plataforma a situações reais de desenvolvimento. Os exemplos devem ser avaliados conforme o contexto de uso; nem todo recurso precisa ser adotado em todos os projetos.

## Referências oficiais

- [.NET 10 — novidades](https://learn.microsoft.com/pt-br/dotnet/core/whats-new/dotnet-10/overview)
- [C# 14 — novidades da linguagem](https://learn.microsoft.com/pt-br/dotnet/csharp/whats-new/csharp-14)
- [EF Core 10 — novidades](https://learn.microsoft.com/pt-br/ef/core/what-is-new/ef-core-10.0/whatsnew)
- [Scalar — documentação](https://scalar.com/products/api-references/integrations/aspnetcore/integration)

---

Material de apoio para estudo e demonstração técnica. Consulte a documentação oficial para detalhes, limitações e orientações de uso de cada recurso.
