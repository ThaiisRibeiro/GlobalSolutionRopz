using GlobalSolutionRopz.Model;
using GlobalSolutionRopz.Repository;
using GlobalSolutionRopz.Repository.Interface;
using GlobalSolutionRopz.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;



namespace GlobalSolutionRopz.Controllers
{



    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepository _IUsuarioRepository;
        private static int _id = 0;


        private readonly WeatherService _WeatherService;

        private readonly IAlertaRepository _IAlertaRepository;

        private readonly IMensagemRepository _IMensagemRepository;





        public UsuarioController(IUsuarioRepository IUsuarioRepository, IAlertaRepository IAlertaRepository, IMensagemRepository IMensagemRepository, WeatherService WeatherService)
        {
            _IUsuarioRepository = IUsuarioRepository;
            _IAlertaRepository = IAlertaRepository;
            _IMensagemRepository = IMensagemRepository;
            _WeatherService = WeatherService;
        }

        /// <summary>
        /// Retorna todos os usuarios cadastrados.
        /// </summary>
        /// <returns>Lista de usuarios.</returns>
        /// <response code="200">Retorna a lista de usuarios</response>
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            return Ok(await _IUsuarioRepository.Listar());
        }

        /// <summary>
        /// Retorna os dados de um usuario específico pelo ID.
        /// </summary>
        /// <param name="id">ID do usuario</param>
        /// <returns>Dados do usuario</returns>
        /// <response code="200">usuario encontrado</response>
        /// <response code="404">usuario não encontrado</response>
        [HttpGet("{id}")]
        public async Task<ActionResult> ObterPorId(int id)
        {
            var usuario = await _IUsuarioRepository.ObterPorId(id);
            if (usuario == null)
            {
                return NotFound(new { message = "usuario não encontrado." });
            }
            return Ok(usuario);
        }

 

        /// <summary>
        /// Verifica a temperatura do estado do usuário e retorna uma mensagem de alerta.
        /// </summary>
        /// <param name="id">ID do usuário</param>
        /// <returns>Mensagem de alerta baseada na temperatura</returns>
        [HttpPost("AlertaTemperatura/{id}")]
        public async Task<ActionResult> AlertaTemperatura(int id)
        {
            // Obter dados do usuário
            var usuario = await _IUsuarioRepository.ObterPorId(id);
            if (usuario == null)
                return NotFound(new { message = "Usuário não encontrado." });

            // Obter clima do estado do usuário
            var clima = await _WeatherService.ObterClimaAsync(usuario.estado);
            if (clima.temperatura == 0 || clima.temperatura == 0)
                return BadRequest(new { message = "Não foi possível obter os dados do clima." });

            // Obter alertas e mensagens
            var alertas = await _IAlertaRepository.Listar();
            var mensagens = await _IMensagemRepository.Listar();

            var mensagemSaida = "";
            foreach (var alerta in alertas)
            {
                if (alerta.estado == usuario.estado && clima.temperatura > alerta.temperatura )
                {
                    foreach(var mensagem in mensagens)
                    {
                        if (alerta.tipo_mensagem == mensagem.tipo_mensagem)
                        {
                            if(mensagemSaida == "")
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



            /*
            // Verificar se há um alerta compatível com o estado e temperatura
            var alertaCorrespondente = alertas.FirstOrDefault(a =>
                a.estado.Equals(usuario.estado, StringComparison.OrdinalIgnoreCase) &&
                clima.temperatura > a.temperatura);

            if (alertaCorrespondente != null)
            {
                var mensagemCorrespondente = mensagens.FirstOrDefault(m =>
                    m.tipo_mensagem == alertaCorrespondente.tipo_mensagem);

                if (mensagemCorrespondente != null)
                {
                    return Ok(new
                    {
                        estado = usuario.estado,
                        temperaturaAtual = clima.temperatura,
                        alerta = alertaCorrespondente.tipo_mensagem,
                        mensagem = mensagemCorrespondente.descricao_mensagem
                    });
                }
            }

            
            */

            if (mensagemSaida != "")
            {
                return Ok(new
                {
                    estado = usuario.estado,
                    temperaturaAtual = clima.temperatura,
                    mensagem = mensagemSaida
                });

            }
            else
            {// Retorno padrão se nenhum alerta for necessário
                return Ok(new
                {
                    estado = usuario.estado,
                    temperaturaAtual = clima.temperatura,
                    mensagem = "Nenhum alerta necessário no momento."
                });

            }

            
        }






        /// <summary>
        /// Cadastra um novo usuario.
        /// </summary>
        /// <param name="usuario">Objeto usuario a ser cadastrado</param>
        /// <returns>usuario cadastrado</returns>
        /// <response code="200">usuario cadastrado com sucesso</response>
        /// <response code="400">Dados inválidos</response>
        [HttpPost("adicionar")]
        public async Task<ActionResult> Adicionar([FromBody] Usuario usuario)
        {
            // Verificar se os dados enviados são válidos
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Dados inválidos." });
            }

            // Realiza a consulta ao ViaCEP para obter os detalhes do endereço
            using var httpClient = new HttpClient();
            var response = await httpClient.GetAsync($"https://viacep.com.br/ws/{usuario.cep}/json/");

            // Se a requisição for bem-sucedida
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var dadosCep = JsonSerializer.Deserialize<ViaCepResponse>(json);

                // Se o CEP for válido (sem erro)
                if (dadosCep != null && !dadosCep.erro)
                {
                    // Monta o endereço completo utilizando os dados do ViaCEP
                    usuario.endereco = $"{dadosCep.logradouro}, {dadosCep.bairro}, {dadosCep.localidade},{dadosCep.uf}";
                 

                }
                else
                {
                    return BadRequest(new { message = "CEP inválido." });
                }
            }
            else
            {
                return BadRequest(new { message = "Erro ao consultar o CEP." });
            }

            // Incrementa o ID do usuario (caso não tenha sido passado) e salva no repositório
           // usuario.id_usuario = ++_id;
            await _IUsuarioRepository.Adcionar(usuario);

            // Retorna uma resposta indicando que o usuario foi cadastrado com sucesso
            return Ok(new { message = "usuario cadastrado!", data = usuario });
        }


        /// <summary>
        /// Atualiza os dados de um usuario existente.
        /// </summary>
        /// <param name="id">ID do usuario</param>
        /// <param name="usuario">Dados atualizados do usuario</param>
        /// <returns>usuario atualizado</returns>
        /// <response code="200">usuario atualizado com sucesso</response>
        /// <response code="400">ID inválido ou dados incorretos</response>
        /// <response code="409">Erro de concorrência</response>
        [HttpPut("atualizar/{id}")]
        public async Task<ActionResult> Atualizar(int id, [FromBody] Usuario usuario)
        {
            if (id != usuario.id_usuario)
            {
                return BadRequest(new { message = "O ID informado não corresponde ao usuario." });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Dados inválidos." });
            }

            try
            {
                await _IUsuarioRepository.Atualizar(usuario);
            }
            catch (DbUpdateConcurrencyException)
            {
                return Conflict(new { message = "Erro de concorrência ao atualizar o usuario." });
            }

            return Ok(new { message = "usuario atualizado!", data = usuario });
        }

        /// <summary>
        /// Exclui um usuario pelo ID.
        /// </summary>
        /// <param name="id">ID do usuario</param>
        /// <returns>Status da exclusão</returns>
        /// <response code="200">usuario excluído com sucesso</response>
        /// <response code="404">usuario não encontrado</response>
        [HttpDelete("excluir/{id}")]
        public async Task<ActionResult> Excluir(int id)
        {
            var usuario = await _IUsuarioRepository.ObterPorId(id);
            if (usuario == null)
            {
                return NotFound(new { message = "usuario não encontrado." });
            }

            await _IUsuarioRepository.Excluir(usuario);
            return Ok(new { message = "usuario excluído com sucesso!" });
        }
    }

}
