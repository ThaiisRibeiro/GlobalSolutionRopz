using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace GlobalSolutionRopz.Model
{
    [Table("Api_Global_Dotnet_Usuario")]
    public class Usuario
    {
    
        private string _cep;
        private string _telefone;
        private string _email;
        private string _nome;
        private string _senha;
        private string _estado;
        private string _endereco;



        [Key]
        // public int id { get; set; }
        public int id_usuario { get; set; }
       


        [Column("nome")]
        [Display(Name = "Nome usuario: ")]
        [Required(ErrorMessage = "Campo Nome Obrigátorio", AllowEmptyStrings = false)]
        public string nome { get; set; } = string.Empty;
        [Column("estado")]
        [Display(Name = "Estado usuario: ")]
        [Required(ErrorMessage = "Campo Estado Obrigátorio", AllowEmptyStrings = false)]
        public string estado { get; set; } = string.Empty;

        [Column("endereco")]
        [Display(Name = "Endereco usuario: ")]
        [Required(ErrorMessage = "Campo Endereco Obrigátorio", AllowEmptyStrings = false)]
        public string endereco { get; set; } = string.Empty;



        [Column("cep")]
        [Display(Name = "CEP do usuario: ")]
        [Required(ErrorMessage = "Campo CEP obrigatório", AllowEmptyStrings = false)]
        public string cep
        {
            get => _cep;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("CEP é obrigatório.");

                if (!Regex.IsMatch(value, @"^\d{5}-?\d{3}$"))
                    throw new ArgumentException("CEP inválido. Formato: 00000-000");

                _cep = value;
            }
        }

        [Display(Name = "Telefone usuario: ")]
        [Required(ErrorMessage = "Campo Telefone Obrigátorio", AllowEmptyStrings = false)]
        public string telefone
        {
            get => _telefone;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Telefone é obrigatório.");

                if (!Regex.IsMatch(value, @"^\d{10,11}$"))
                    throw new ArgumentException("Telefone inválido. Use apenas números com DDD.");

                _telefone = value;
            }
        }

        [Display(Name = "Email usuario: ")]
        [Required(ErrorMessage = "Campo email Obrigátorio", AllowEmptyStrings = false)]
        public string email
        {
            get => _email;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Email é obrigatório.");

                if (!new EmailAddressAttribute().IsValid(value))
                    throw new ArgumentException("Email inválido.");

                _email = value;
            }
        }


        [Display(Name = "Senha paciente: ")]
        [Required(ErrorMessage = "Campo Senha Obrigátorio", AllowEmptyStrings = false)]
        public string senha
        {
            get => _senha;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Senha é obrigatória.");

                _senha = value;
            }
        }
      
    }
}
