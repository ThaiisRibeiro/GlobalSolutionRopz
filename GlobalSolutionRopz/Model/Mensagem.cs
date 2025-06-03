using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;

namespace GlobalSolutionRopz.Model
{
    [Table("Api_Global_Dotnet_Mensagem")]
    public class Mensagem
    {
        
        [Key]
        // public int id { get; set; }
        public int tipo_mensagem { get; set; }
        [Column("descricao_mensagem")]
        [Display(Name = "descricao da mensagem: ")]
        [Required(ErrorMessage = "Campo descricao da mensagem Obrigátorio", AllowEmptyStrings = false)]
        public string descricao_mensagem { get; set; }
   

    }
}
