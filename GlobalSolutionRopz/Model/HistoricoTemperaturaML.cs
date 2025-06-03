using Microsoft.ML.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlobalSolutionRopz.Model
{
   

    public class HistoricoTemperaturaML
    {


        [LoadColumn(0)]

        public float temperatura { get; set; }


        [LoadColumn(1)]

        public string estado { get; set; }
        
        [LoadColumn(2)]

        public float mes { get; set; }

        [LoadColumn(3)]
        public float diaDaSemana { get; set; }
        [LoadColumn(4)]
        public float ano { get; set; }


        [LoadColumn(5)]
        [ColumnName("Label")]
        public bool alertaEmitido { get; set; } 

    }
}
