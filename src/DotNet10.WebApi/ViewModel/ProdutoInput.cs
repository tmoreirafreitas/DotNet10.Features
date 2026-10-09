using System.ComponentModel.DataAnnotations;

namespace DotNet10.WebApi.ViewModel
{
    // Modelo validável do .NET 10 usando Data Annotations    
    public class ProdutoInput
    {
        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "O nome deve ter entre 3 e 50 caracteres.")]
        public string Nome { get; set; } = string.Empty;

        [Range(0.01, 10000.0, ErrorMessage = "O preço deve ser entre 0.01 e 10.000,00.")]
        public decimal Preco { get; set; }
    }
}
