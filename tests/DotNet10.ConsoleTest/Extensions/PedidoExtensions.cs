namespace DotNet10.ConsoleTest.Extensions
{
    public static class PedidoExtensions
    {
        // Podemos criar um método de extensão de forma direta e limpa
        public static decimal CalcularImpostoTotal(this Pedido pedido) => pedido.Total * 1.1m;

        // E a grande novidade: PODEMOS CRIAR PROPRIEDADES DE EXTENSÃO!
        extension(Pedido pedido)
        {            
            public bool ValorValido => pedido.Total > 0;            
            public decimal TotalComImposto => pedido.Total * 1.1m;
        }
    }
}
