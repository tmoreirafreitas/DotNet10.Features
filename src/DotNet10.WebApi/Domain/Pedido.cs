namespace DotNet10.WebApi.Domain
{
    public class Pedido
    {
        public uint Id { get; set; }
        public uint ClienteId { get; set; }
        public decimal Total { get; set; }
        public DateTime DataPedido { get; set; }
    }
}
