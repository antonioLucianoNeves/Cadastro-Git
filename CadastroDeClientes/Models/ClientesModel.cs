using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CadastroDeClientes.Models
{
    public class ClientesModel
    {
        //Data Anotation
        [Key]
        [Required]
        public int Id { get; set; }

        [Display(Name = "Nome do Cliente")]
        [Required(ErrorMessage = "O {0} é obrigatorio", AllowEmptyStrings = false)]
        [MinLength(3, ErrorMessage = "Nome inválido, informe no mínimo 3 caracteres")]
        [MaxLength(100, ErrorMessage = "Nome excedeu o tamanho permitido")]
        public string Nome { get; set; }

        [Display(Name = "CPF/CNPJ")]
        [Required(ErrorMessage = "O {0} é obrigatorio", AllowEmptyStrings = false)]
        [MaxLength(14, ErrorMessage = "CPF/CNPJ contém 11 digitos e CNPJ somente 14 digitos")]
        [Range(00000000000, 99999999999999)]
        public string Cpf { get; set; }
        
        //[MinLength(18), MaxLength(100)]
        [Display(Name = "Data de Nascimento", Description = "A idade deve ser acima de 18.")]
        [DataType(DataType.DateTime, ErrorMessage = "Data invalida")]
        [Required(ErrorMessage = "O {0} é obrigatorio", AllowEmptyStrings = false)]
        public DateTime? DataNascimento { get; set; }

        [Display(Name = "Tipo")]
        public int Tipo { get; set; }
    }
}
