using System.Globalization;
using System.Runtime.InteropServices;

namespace DotNet10.ConsoleTest
{
    public class ISOWeekExemplo
    {
        public static void Executar()
        {
            Console.WriteLine("***** Testes com .NET 10 | Novos overloads na classe estática ISOWeek *****");
            Console.WriteLine($"Versão do .NET em uso: {RuntimeInformation
                .FrameworkDescription} - Ambiente: {Environment.MachineName} - Kernel: {Environment
                .OSVersion.VersionString}");

            var currentDate = DateOnly.FromDateTime(DateTime.Now);
            Console.WriteLine();
            Console.WriteLine($"Data atual: {currentDate}");
            Console.WriteLine($"Semana do ano: {ISOWeek.GetWeekOfYear(currentDate)}");
            Console.WriteLine($"Ano: {ISOWeek.GetYear(currentDate)}");

            var firstFriday2026 = ISOWeek.ToDateOnly(2026, 1, DayOfWeek.Friday);
            Console.WriteLine($"Primeira sexta-feira de 2026: {firstFriday2026}");
        }
    }
}
