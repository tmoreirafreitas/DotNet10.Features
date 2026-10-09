using DotNet10.WebApi.Domain;

namespace DotNet10.WebApi.Infrastructure.Data.Context
{
    public static class DataSeeder
    {
        public static void PopularDadosFaker(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Clientes.Any()) return;

            var clientes = new List<Cliente>
        {
            new() { Id = 1, Nome = "André Silva", Email = "andre@email.com", Ativo = true, Deletado = false },
            new() { Id = 2, Nome = "Beatriz Souza", Email = "beatriz@email.com", Ativo = true, Deletado = false },
            new() { Id = 3, Nome = "Carlos Eduardo", Email = "carlos@email.com", Ativo = true, Deletado = false },
            new() { Id = 4, Nome = "Daniela Lima", Email = "daniela@email.com", Ativo = true, Deletado = false },
            
            // Dados específicos para testar os múltiplos Filtros Nomeados:
            new() { Id = 5, Nome = "Roberto Inativo", Email = "roberto@email.com", Ativo = false, Deletado = false },
            new() { Id = 6, Nome = "Marta Excluída", Email = "marta@email.com", Ativo = true, Deletado = true }
        };

            var pedidos = new List<Pedido>
        {
            new() { Id = 101, ClienteId = 1, Total = 250.50m, DataPedido = DateTime.Now.AddDays(-5) },
            new() { Id = 102, ClienteId = 1, Total = 120.00m, DataPedido = DateTime.Now.AddDays(-2) },
            new() { Id = 103, ClienteId = 2, Total = 550.00m, DataPedido = DateTime.Now.AddDays(-1) },
            new() { Id = 104, ClienteId = 3, Total = 89.90m,  DataPedido = DateTime.Now.AddDays(-10) }
        };

            context.Clientes.AddRange(clientes);
            context.Pedidos.AddRange(pedidos);
            context.SaveChanges();
        }
    }
}
