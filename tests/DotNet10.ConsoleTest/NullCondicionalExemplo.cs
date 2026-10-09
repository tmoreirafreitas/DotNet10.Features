namespace DotNet10.ConsoleTest
{
    public class ConfiguracaoSistema
    {
        public int TimeoutSegundos { get; set; }
    }

    public class NullCondicionalExemplo
    {
        public static void Executar()
        {
            Console.WriteLine("***** C# 14 | Null-Conditional Assignment *****");
            Console.WriteLine();

            // --- Cenario 1: O objeto existe ---
            Console.WriteLine("*** Cenário 1: Configuração disponível ***");

            ConfiguracaoSistema? config = ObterConfiguracaoOpcional(disponivel: true);

            Console.WriteLine($"Timeout inicial: {config?.TimeoutSegundos} segundos");

            config?.TimeoutSegundos = 60;

            Console.WriteLine($"Timeout após atribuição: {config?.TimeoutSegundos} segundos");
            Console.WriteLine();

            // --- Cenario 2: O objeto e nulo ---
            Console.WriteLine("*** Cenário 2: Configuração indisponível ***");

            config = ObterConfiguracaoOpcional(disponivel: false);

            Console.WriteLine($"Configuração encontrada: {config is not null}");

            config?.TimeoutSegundos = 60;

            Console.WriteLine($"Timeout após tentativa de atribuição: {config?.TimeoutSegundos.ToString() ?? "Não definido"}");

            Console.WriteLine();
            Console.WriteLine("Conclusão: quando o objeto é nulo, a atribuição não acontece e nenhuma exceção é lançada.");
        }

        private static ConfiguracaoSistema? ObterConfiguracaoOpcional(bool disponivel)
        {
            return disponivel ? new ConfiguracaoSistema { TimeoutSegundos = 30 } : null;
        }
    }
}