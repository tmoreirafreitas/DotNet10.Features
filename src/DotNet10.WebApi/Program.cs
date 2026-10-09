using DotNet10.WebApi.Infrastructure.Data;
using DotNet10.WebApi.Infrastructure.Data.Context;
using DotNet10.WebApi.ViewModel;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using System.Net.ServerSentEvents;
using System.Runtime.CompilerServices;

var builder = WebApplication.CreateBuilder(args);

// 1. Suporte Nativo a OpenAPI 3.1 (suporta JSON e YAML nativamente no .NET 10)
builder.Services.AddOpenApi();

// 2. Novo recurso de Validação Integrada para Minimal APIs (substitui FluentValidation para cenários padrão)
builder.Services.AddValidation();

// Registro do EF Core InMemory
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseInMemoryDatabase("DotNet10DemoDb"));

var app = builder.Build();

// ====================================================================
// CHAME O SEEDER EXATAMENTE AQUI (ANTES DAS ROTAS E DO app.Run)
// ====================================================================
using (var scope = app.Services.CreateScope())
{
    // Recupera o contexto de banco cadastrado no container de Injeção de Dependência
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    // Alimenta o banco em memória com os dados simulados (Faker)
    DataSeeder.PopularDadosFaker(context);
}
// ====================================================================

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.MapOpenApi("/openapi/{documentName}.yaml");
    // Substitui o Swagger tradicional pelo Scalar UI
    app.MapScalarApiReference(options =>
    {
        options.Title = "API Demo Novas Feature DotNet10";
        options.Theme = ScalarTheme.BluePlanet;
        options.DarkMode = true;
    });
}

app.UseHttpsRedirection();

// 3. Endpoint usando a nova API de Validação Automática
// Se os dados forem inválidos, retorna 400 Bad Request estruturado automaticamente antes de entrar no método
app.MapPost("/v1/produtos", (ProdutoInput input) =>
{
    return TypedResults.Ok(new { Mensagem = "Produto criado com sucesso!", input.Nome });
})
.WithName("CriarProduto")
.Produces(StatusCodes.Status200OK)
.Produces(StatusCodes.Status400BadRequest);

// 4. Endpoint usando Server-Sent Events (SSE) nativo do .NET 10 (TypedResults.ServerSentEvents)
app.MapGet("/v1/notificacoes/json-item", (CancellationToken cancellationToken) =>
{
    async IAsyncEnumerable<HeartRateRecord> GetHeartRate([EnumeratorCancellation] CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            var heartRate = Random.Shared.Next(60, 100);
            yield return HeartRateRecord.Create(heartRate);
            await Task.Delay(2000, cancellationToken);
        }
    }

    return TypedResults.ServerSentEvents(GetHeartRate(cancellationToken), eventType: "heartRate");
}).WithName("ObterNotificacoesJsonSse");

app.MapGet("/v1/notificacoes/sse-item", (CancellationToken cancellationToken) =>
{
    async IAsyncEnumerable<SseItem<int>> GetHeartRate([EnumeratorCancellation] CancellationToken cancellationToken)
    {
        var eventId = 0;

        while (!cancellationToken.IsCancellationRequested)
        {
            var heartRate = Random.Shared.Next(60, 100);

            // Cada evento leva tipo, id e intervalo de reconexão
            yield return new SseItem<int>(heartRate, eventType: "heartRate")
            {
                EventId = (++eventId).ToString(),
                ReconnectionInterval = TimeSpan.FromMinutes(1)

            };

            await Task.Delay(2000, cancellationToken);
        }
    }

    return TypedResults.ServerSentEvents(GetHeartRate(cancellationToken));
}).WithName("ObterNotificacoesSseComIdentificacao");

// ==========================================
// DEMONSTRAÇÃO 1: NATIVE LINQ LEFT JOIN (.NET 10)
// ==========================================
app.MapGet("/v1/relatorios/left-join", async (ApplicationDbContext context, CancellationToken cancellationToken = default) =>
{
    // O operador LeftJoin nativo inclui todos os clientes, mesmo aqueles sem pedidos (ex: Daniela Lima)
    var resultado = await context.Clientes.AsNoTracking()
        .LeftJoin(
            context.Pedidos.AsNoTracking(),
            cliente => cliente.Id,
            pedido => pedido.ClienteId,
            (cliente, pedido) => new RelatorioDto
            {
                NomeCliente = cliente.Nome,
                IdPedido = pedido != null ? pedido.Id : 0,
                ValorPedido = pedido != null ? pedido.Total : 0m
            }
        ).ToListAsync(cancellationToken);

    return TypedResults.Ok(resultado);
}).WithName("ObterRelatorioLeftJoin");

// ==========================================
// DEMONSTRAÇÃO 2: NATIVE LINQ RIGHT JOIN (.NET 10)
// ==========================================
app.MapGet("/v1/relatorios/right-join", async (ApplicationDbContext context, CancellationToken cancellationToken = default) =>
    {
        var clientes = await context.Clientes            
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        var pedidos = await context.Pedidos
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        var resultado = clientes
            .RightJoin(
                pedidos,
                cliente => cliente.Id,
                pedido => pedido.ClienteId,
                (cliente, pedido) => new RelatorioDto
                {
                    NomeCliente = cliente?.Nome ?? "Cliente Não Identificado",
                    IdPedido = pedido.Id,
                    ValorPedido = pedido.Total
                })
            .ToList();

        return TypedResults.Ok(resultado);
    })
.WithName("ObterRelatorioRightJoin");

// =======================================================================
// CENÁRIO A: CONSULTA PADRÃO (Aplica TODOS os Filtros Nomeados Automaticamente)
// =======================================================================
app.MapGet("/v1/clientes/ativos", async (ApplicationDbContext context, CancellationToken cancellationToken = default) =>
{
    // Por padrão, o EF Core 10 aplica o "FiltroExclusaoLogica" E o "FiltroInadimplentes".
    // Retornará apenas os IDs de 1 a 4. Roberto (inativo) e Marta (excluída) ficam de fora.
    var clientes = await context.Clientes.ToListAsync(cancellationToken);
    return TypedResults.Ok(clientes);
}).WithName("ObterClientesAtivos");

// =======================================================================
// CENÁRIO B: DESATIVAÇÃO SELETIVA (Ignora apenas UM filtro específico)
// =======================================================================
app.MapGet("/v1/clientes/incluir-inativos", async (ApplicationDbContext context, CancellationToken cancellationToken = default) =>
{
    // O .NET 10 permite ignorar um filtro nomeado específico mantendo os outros ativos!
    // Aqui desativamos o filtro de inativos, então o "Roberto Inativo" vai aparecer,
    // mas a "Marta Excluída" continuará oculta pelo filtro de exclusão lógica.
    var clientes = await context.Clientes
        .IgnoreQueryFilters(["FiltroInadimplentes"])
        .ToListAsync(cancellationToken);

    return TypedResults.Ok(clientes);
}).WithName("ObterClientesIncluindoInativos");

// =======================================================================
// CENÁRIO C: CONTROLE TOTAL (Ignora múltiplos filtros de forma cirúrgica)
// =======================================================================
app.MapGet("/v1/clientes/auditoria-geral", async (ApplicationDbContext context, CancellationToken cancellationToken = default) =>
{
    // Caso precise de um relatório administrativo completo, você pode encadear as desativações
    // ou usar o método tradicional para ignorar tudo se preferir.
    var todosOsClientes = await context.Clientes
        .IgnoreQueryFilters(["FiltroExclusaoLogica", "FiltroInadimplentes"])
        .ToListAsync(cancellationToken);

    return TypedResults.Ok(todosOsClientes);
}).WithName("ObterClientesParaAuditoria");

await app.RunAsync();

// Registro com o valor do batimento e o horário da leitura
public record HeartRateRecord(int HeartRate, DateTime Timestamp)
{
    public static HeartRateRecord Create(int heartRate) => new(heartRate, DateTime.UtcNow);
}