using DotNet10.WebApi.Domain;
using Microsoft.EntityFrameworkCore;

namespace DotNet10.WebApi.Infrastructure.Data.Context
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // EF Core 10: Filtros de Consulta Nomeados (Múltiplos e desativáveis individualmente)
            modelBuilder.Entity<Cliente>()
                .HasQueryFilter("FiltroExclusaoLogica", c => !c.Deletado)
                .HasQueryFilter("FiltroInadimplentes", c => c.Ativo);
        }
    }
}
