using Microsoft.ML.Data;

namespace GlobalSolutionRopz.DTO
{
    public class ResultadoAlerta
    {
        [ColumnName("PredictedLabel")]
        public bool AlertaPrevisto { get; set; }

        public float Probability { get; set; }
    }
}
