using GlobalSolutionRopz.DTO;
using GlobalSolutionRopz.ML;
using GlobalSolutionRopz.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Transforms;

namespace GlobalSolutionRopz.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TemperaturaMLController : ControllerBase
    {
        private readonly string caminhoCsv = Path.Combine(Directory.GetCurrentDirectory(), "ML", "historico_temperatura_completo.csv");
        private readonly string caminhoModelo = Path.Combine(Directory.GetCurrentDirectory(), "ML", "modelo_alerta_temperatura.zip");
        private readonly MLContext mlContext = new();

        [HttpGet("treinar")]
        public IActionResult TreinarModelo()
        {
            if (!System.IO.File.Exists(caminhoCsv))
                return NotFound("❌ Arquivo CSV de treinamento não encontrado.");

            var dados = mlContext.Data.LoadFromTextFile<HistoricoTemperaturaML>(
                path: caminhoCsv,
                hasHeader: true,
                separatorChar: ',');


            var pipeline = mlContext.Transforms.Categorical.OneHotEncoding("estado")
     .Append(mlContext.Transforms.Concatenate("Features", "temperatura", "estado", "mes", "diaDaSemana", "ano"))
     .Append(mlContext.Transforms.NormalizeMinMax("Features"))
     .Append(mlContext.BinaryClassification.Trainers.FastTree());




            var modelo = pipeline.Fit(dados);

            mlContext.Model.Save(modelo, dados.Schema, caminhoModelo);

            return Ok("✅ Modelo treinado e salvo com sucesso!");
           
        }

        [HttpPost("verificar")]
        public IActionResult Verificar([FromBody] VerificacaoAlertaDTO entrada)
        {
            if (!System.IO.File.Exists(caminhoModelo))
                return BadRequest("❌ O modelo ainda não foi treinado. Acesse /api/temperaturaml/treinar primeiro.");

            var model = mlContext.Model.Load(caminhoModelo, out _);
            var engine = mlContext.Model.CreatePredictionEngine<HistoricoTemperaturaML, ResultadoAlerta>(model);

            var entradaML = new HistoricoTemperaturaML
            {
                temperatura = entrada.temperatura,
                estado = entrada.estado,
                mes = entrada.mes,
                diaDaSemana = entrada.diaDaSemana,
                ano = entrada.ano
            };

            var resultado = engine.Predict(entradaML);

            return Ok(new
            {
                AlertaPrevisto = resultado.AlertaPrevisto,
                Probabilidade = resultado.Probability
            });
        }
    }
}
