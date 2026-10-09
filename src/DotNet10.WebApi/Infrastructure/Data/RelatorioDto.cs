namespace DotNet10.WebApi.Infrastructure.Data
{
    public record RelatorioDto
    {
        public string NomeCliente { get; init; } = "";
        public uint IdPedido { get; init; }
        public decimal ValorPedido { get; init; }
    }
}
