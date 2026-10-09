namespace DotNet10.WebApi.Domain
{
    public class Cliente
    {
        public uint Id { get; set; }
        public string Nome { get; set; } = "";
        public string Email { get; set; } = string.Empty;
        public bool Deletado { get; set; }
        public bool Ativo { get; set; }
    }
}
