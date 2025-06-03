using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GlobalSolutionRopz.Model
{
    [Table("Api_Global_Dotnet_Alerta")]
    public class Alerta
    {



        [Key]
        // public int id { get; set; }
        public int id_alerta { get; set; }
        [Column("tipo_mensagem  ")]
        [Display(Name = "tipo_mensagem : ")]
        [Required(ErrorMessage = "Campo Tipo de mensagem Obrigátoria", AllowEmptyStrings = false)]
        public int tipo_mensagem { get; set; }


        [Column("estado ")]
        [Display(Name = "estado: ")]
        [Required(ErrorMessage = "Campo estado Obrigátorio", AllowEmptyStrings = false)]
        public string estado { get; set; }

        [Column("temperatura ")]
        [Display(Name = " temperatura : ")]
        [Required(ErrorMessage = "Campo temperatura Obrigátorio", AllowEmptyStrings = false)]

        public int temperatura { get; set; }

       


    }
}
