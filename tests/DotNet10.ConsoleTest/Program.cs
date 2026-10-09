namespace DotNet10.ConsoleTest;

internal class Program
{
    private static void Main(string[] args)
    {
        bool continuar = true;

        while (continuar)
        {
            Console.Clear();
            ExibirMenu();

            Console.Write("Escolha uma opcao: ");
            string? opcao = Console.ReadLine();

            Console.WriteLine();

            switch (opcao)
            {
                case "1":
                    ISOWeekExemplo.Executar();
                    break;

                case "2":
                    NullCondicionalExemplo.Executar();
                    break;

                case "3":
                    ExtensionMemberExemplo.Executar();
                    break;

                case "0":
                    continuar = false;
                    Console.WriteLine("Encerrando a aplicacao...");
                    continue;

                default:
                    Console.WriteLine("Opcao invalida. Tente novamente.");
                    break;
            }

            Console.WriteLine();
            Console.WriteLine("----------------------------------------");
            Console.WriteLine("Pressione qualquer tecla para continuar...");
            Console.ReadKey(intercept: true);
        }
    }

    private static void ExibirMenu()
    {
        Console.WriteLine("========================================");
        Console.WriteLine("       Exemplos .NET 10 / C# 14");
        Console.WriteLine("========================================");
        Console.WriteLine();
        Console.WriteLine("  1 - ISOWeek");
        Console.WriteLine("  2 - Null Conditional Assignment");
        Console.WriteLine("  3 - Extension Members");
        Console.WriteLine();
        Console.WriteLine("  0 - Sair");
        Console.WriteLine();
    }
}