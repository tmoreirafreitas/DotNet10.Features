using DotNet10.ConsoleTest.Extensions;

namespace DotNet10.ConsoleTest;

// 1. Criamos a classe Pedido comum (sem métodos de imposto nela)
public class Pedido
{
    public int Id { get; set; }
    public decimal Total { get; set; }
}

public static class ExtensionMemberExemplo
{
    public static void Executar()
    {
        Console.WriteLine("=== Extension Members no C# 14 ===");
        Console.WriteLine();

        var pedido = new Pedido
        {
            Id = 1001,
            Total = 250.00m
        };

        // Utilizacao do metodo de extensao.
        decimal impostoCalculado = pedido.CalcularImpostoTotal();

        // Utilizacao da propriedade de extensao.
        decimal totalFinal = pedido.TotalComImposto;

        Console.WriteLine($"Pedido ID: {pedido.Id}");
        Console.WriteLine($"Valor Original: {pedido.Total:C}");
        Console.WriteLine($"Valor com Método de Extensão: {impostoCalculado:C}");
        Console.WriteLine($"Valor via Propriedade de Extensão: {totalFinal:C}");
    }
}