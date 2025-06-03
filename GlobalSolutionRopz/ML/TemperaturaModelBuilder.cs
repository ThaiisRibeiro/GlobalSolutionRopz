using Microsoft.ML.Data;
using Microsoft.ML;

namespace GlobalSolutionRopz.ML
{
    public class TemperaturaModelBuilder
    {
        private readonly MLContext mlContext;
        private readonly string caminhoCsv = Path.Combine(Environment.CurrentDirectory, "ML", "historico_temperatura_completo.csv");
        private readonly string caminhoModelo = Path.Combine(Environment.CurrentDirectory, "ML", "modelo_alerta_temperatura.zip");

        public TemperaturaModelBuilder()
        {
            mlContext = new MLContext(seed: 0);
        }

        public class TemperaturaData
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
            public bool alertaEmitido { get; set; }
        }

        public class TemperaturaPrediction
        {
            [ColumnName("PredictedLabel")]
            public bool AlertaPrevisto { get; set; }

            public float Probability { get; set; }
        }

        public void TreinarModelo()
        {
            if (!File.Exists(caminhoCsv))
            {
                Console.WriteLine("❌ Arquivo CSV não encontrado em: " + caminhoCsv);
                return;
            }

            IDataView dados = mlContext.Data.LoadFromTextFile<TemperaturaData>(
                caminhoCsv,
                hasHeader: true,
                separatorChar: ',');

            var pipeline = mlContext.Transforms.Conversion.MapValueToKey("estado")
                .Append(mlContext.Transforms.Concatenate("Features", "temperatura", "estado", "mes", "diaDaSemana", "ano"))
                .Append(mlContext.Transforms.NormalizeMinMax("Features"))
                .Append(mlContext.Transforms.CopyColumns("Label", nameof(TemperaturaData.alertaEmitido)))
                .Append(mlContext.BinaryClassification.Trainers.FastTree());

            var modelo = pipeline.Fit(dados);

            mlContext.Model.Save(modelo, dados.Schema, caminhoModelo);

            Console.WriteLine("✅ Modelo treinado e salvo em: " + caminhoModelo);
        }

        public TemperaturaPrediction Prever(TemperaturaData entrada)
        {
            if (!File.Exists(caminhoModelo))
            {
                Console.WriteLine("❌ Modelo ainda não foi treinado.");
                return null;
            }

            var modelo = mlContext.Model.Load(caminhoModelo, out var schema);

            var engine = mlContext.Model.CreatePredictionEngine<TemperaturaData, TemperaturaPrediction>(modelo);

            return engine.Predict(entrada);
        }
    }
}

