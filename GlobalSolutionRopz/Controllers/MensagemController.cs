using GlobalSolutionRopz.Model;
using GlobalSolutionRopz.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GlobalSolutionRopz.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MensagemController : ControllerBase
    {
        private readonly IMensagemRepository _IMensagemRepository;
        private static int _id = 0; // Controla o ID

        public MensagemController(IMensagemRepository IMensagemRepository)
        {
            _IMensagemRepository = IMensagemRepository;
        }

        /// <summary>
        /// Retorna todas as mensagens cadastradas no sistema.
        /// </summary>
        /// <returns>Lista de mensagem.</returns>
        /// <response code="200">mensagem retornadas com sucesso.</response>
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            return Ok(await _IMensagemRepository.Listar());
        }

        /// <summary>
        /// Retorna uma mensagem específico pelo ID.
        /// </summary>
        /// <param name="id">ID do mensagem.</param>
        /// <returns>Dados do mensagem.</returns>
        /// <response code="200">mensagem encontrada com sucesso.</response>
        /// <response code="404">mensagem não encontrada.</response>
        [HttpGet("{id}")]
        public async Task<ActionResult> ObterPorId(int id)
        {
            var mensagem = await _IMensagemRepository.ObterPorId(id);
            if (mensagem == null)
            {
                return NotFound(new { message = "Mensagem não encontrada." });
            }
            return Ok(mensagem);
        }

        /// <summary>
        /// Adiciona uma nova mensagem ao sistema.
        /// </summary>
        /// <param name="mensagem">Objeto contendo os dados da mensagem.</param>
        /// <returns>mensagem cadastrada.</returns>
        /// <response code="200">mensagem cadastrada com sucesso.</response>
        /// <response code="400">Dados inválidos.</response>
        [HttpPost("adicionar")]
        public async Task<ActionResult> Adicionar([FromBody] Mensagem mensagem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Dados inválidos." });
            }

            mensagem.tipo_mensagem = ++_id;
            await _IMensagemRepository.Adcionar(mensagem);

            return Ok(new { message = "mensagem cadastrado!", data = mensagem });
        }

        /// <summary>
        /// Atualiza os dados de uma mensagem existente.
        /// </summary>
        /// <param name="id">ID da mensagem.</param>
        /// <param name="mensagem">Objeto contendo os novos dados da mensagem.</param>
        /// <returns>mensagem atualizada.</returns>
        /// <response code="200">mensagem atualizada com sucesso.</response>
        /// <response code="400">ID inválido ou dados incorretos.</response>
        /// <response code="409">Erro de concorrência ao atualizar a mensagem.</response>
        [HttpPut("atualizar/{id}")]
        public async Task<ActionResult> Atualizar(int id, [FromBody] Mensagem mensagem)
        {
            if (id != mensagem.tipo_mensagem)
            {
                return BadRequest(new { message = "O ID informado não corresponde a mensagem." });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Dados inválidos." });
            }

            try
            {
                await _IMensagemRepository.Atualizar(mensagem);
            }
            catch (DbUpdateConcurrencyException)
            {
                return Conflict(new { message = "Erro de concorrência ao atualizar a mensagem." });
            }

            return Ok(new { message = "Mensagem atualizado!", data = mensagem });
        }

        /// <summary>
        /// Exclui uma mensagem do sistema com base no ID.
        /// </summary>
        /// <param name="id">ID da mensagem.</param>
        /// <returns>Mensagem de sucesso.</returns>
        /// <response code="200">mensagem excluída com sucesso.</response>
        /// <response code="404">mensagem não encontrada.</response>
        [HttpDelete("excluir/{id}")]
        public async Task<ActionResult> Excluir(int id)
        {
            var mensagem = await _IMensagemRepository.ObterPorId(id);
            if (mensagem == null)
            {
                return NotFound(new { message = "Mensagem não encontrado." });
            }

            await _IMensagemRepository.Excluir(mensagem);

            return Ok(new { message = "Mensagem excluído com sucesso!" });
        }
    }

}

