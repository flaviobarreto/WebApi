using ApiCatalogo.Validacao;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogo.Models
{
    [Table("Produtos")]
    public class Produto : IValidatableObject
    {
        [Key]
        public int ProdutoId { get; set; }
        [Required(ErrorMessage = "O nome é obrigatorio")]
        [MaxLength(300)]
        public string Nome { get; set; }
        [Required]
        [StringLength(80, ErrorMessage = "O nome deve ter no maximo {1} e no minimo{2 caracteres}")]
       // [PrimeiraLetraMaiuscula]
        public string Descricao { get; set; }
        [Required]
        public decimal Preco { get; set; }
        [Required]
        [MaxLength(300)]
        public string ImagemUrl { get; set; }
        public float Estoque { get; set; }
        public DateTime DataCadastro { get; set; }
        public Categoria Categoria { get; set; }
        public int CategoriaId { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!string.IsNullOrEmpty(this.Nome))
            {
                var primeiraLetra = this.Nome[0].ToString();
                if (primeiraLetra != primeiraLetra.ToUpper())
                {
                    yield return new
                        ValidationResult("A primiera ledra do produto deve ser maiuscula",
                        new[] { nameof(this.Nome) });
                }
            }
            if(this.Estoque <= 0)
            {
                yield return new
                       ValidationResult("O estoque deve ser maior que zero",
                       new[] { nameof(this.Estoque) });
            }
        }
    }
}
