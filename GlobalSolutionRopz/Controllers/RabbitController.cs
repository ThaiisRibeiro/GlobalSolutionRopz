using GlobalSolutionRopz.Model;
using GlobalSolutionRopz.Repository.Interface;
using GlobalSolutionRopz.Services;
using Microsoft.AspNetCore.Mvc;

namespace GlobalSolutionRopz.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class RabbitController : ControllerBase
    {
        private readonly IUsuarioRepository _IUsuarioRepository;
        private static int _id = 0;


        private readonly WeatherService _WeatherService;

        private readonly IAlertaRepository _IAlertaRepository;

        private readonly IMensagemRepository _IMensagemRepository;

        private readonly ConsumerRabbit _ConsumerRabbit;

        private readonly PublisherRabbit _PublisherRabbit;




        public RabbitController(IUsuarioRepository IUsuarioRepository, IAlertaRepository IAlertaRepository, IMensagemRepository IMensagemRepository, WeatherService WeatherService, ConsumerRabbit consumerRabbit, PublisherRabbit publisherRabbit)
        {
            _IUsuarioRepository = IUsuarioRepository;
            _IAlertaRepository = IAlertaRepository;
            _IMensagemRepository = IMensagemRepository;
            _WeatherService = WeatherService;
            _ConsumerRabbit = consumerRabbit;
            _PublisherRabbit = publisherRabbit;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> PublisherMensagemRabbitMQ(int id)
        {
            var usuario = await _IUsuarioRepository.ObterPorId(id);
            if (usuario == null)
            {
                return NotFound(new { message = "usuario não encontrado." });
            }
            string tranformarId = id.ToString();
            _PublisherRabbit.PublicarMensagemAsync(tranformarId);

            return Ok(new { message = $"Mensagem com ID {id} enviada para a fila com sucesso." });

        }
        [HttpGet]
        public async Task<ActionResult> ConsumerMensagemRabbitMQ()
        {
            // Ler a mensagem Rabbit
            var idusuario =_ConsumerRabbit.ExecuteAsync();
            
            var mensagemSaida = "Não foram detectadas variações de temperatura nesta área.";

            if (idusuario != "")
            { // Obter dados do usuário
                int intId = int.Parse(idusuario);
                var usuario = await _IUsuarioRepository.ObterPorId(intId);
                if (usuario == null)
                    return NotFound(new { message = "Usuário não encontrado." });

                // Obter clima do estado do usuário
                var clima = await _WeatherService.ObterClimaAsync(usuario.estado);
                if (clima.temperatura == 0 || clima.temperatura == 0)
                    return BadRequest(new { message = "Não foi possível obter os dados do clima." });

                // Obter alertas e mensagens
                var alertas = await _IAlertaRepository.Listar();
                var mensagens = await _IMensagemRepository.Listar();

                
                foreach (var alerta in alertas)
                {
                    if (alerta.estado == usuario.estado && clima.temperatura > alerta.temperatura)
                    {
                        foreach (var mensagem in mensagens)
                        {
                            if (alerta.tipo_mensagem == mensagem.tipo_mensagem)
                            {
                                if ( mensagemSaida == "Não foram detectadas variações de temperatura nesta área.")
                                {
                                    mensagemSaida = "";

                                }

                                if (mensagemSaida == "")
                                {
                                    mensagemSaida = mensagemSaida + mensagem.descricao_mensagem;

                                }
                                else
                                {
                                    mensagemSaida = mensagemSaida + " , " + mensagem.descricao_mensagem;

                                }

                            }
                        }
                    }

                }

            }
            
            return Ok(mensagemSaida);
        }
    }
    
}
