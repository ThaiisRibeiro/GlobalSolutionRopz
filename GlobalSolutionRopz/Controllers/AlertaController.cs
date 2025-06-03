using GlobalSolutionRopz.Model;
using GlobalSolutionRopz.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GlobalSolutionRopz.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlertaController : ControllerBase
    {
        private readonly IAlertaRepository _IAlertaRepository;
        private static int _id = 0;

        public AlertaController(IAlertaRepository AlertaRepository)
        {
            _IAlertaRepository = AlertaRepository;
        }

        /// <summary>
        /// Retorna todos os alertas cadastradas no sistema.
        /// </summary>
        /// <returns>Lista de alertas.</returns>
        /// <response code="200">alertas retornados com sucesso.</response>
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            return Ok(await _IAlertaRepository.Listar());
        }

        /// <summary>
        /// Retorna um alerta específica pelo ID.
        /// </summary>
        /// <param name="id">ID do alerta.</param>
        /// <returns>Dados do alerta.</returns>
        /// <response code="200">alerta encontrado com sucesso.</response>
        /// <response code="404">alerta não encontrado.</response>
        [HttpGet("{id}")]
        public async Task<ActionResult> ObterPorId(int id)
        {
            var alerta = await _IAlertaRepository.ObterPorId(id);
            if (alerta == null)
            {
                return NotFound(new { message = "alerta não encontrado." });
            }
            return Ok(alerta);
        }

        /// <summary>
        /// Adiciona um novo alerta ao sistema.
        /// </summary>
        /// <param name="alerta">Objeto contendo os dados do alerta.</param>
        /// <returns>alerta cadastrado.</returns>
        /// <response code="200">alerta cadastrado com sucesso.</response>
        /// <response code="400">Dados inválidos.</response>
        [HttpPost("adicionar")]
        public async Task<ActionResult> Adicionar([FromBody] Alerta alerta)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Dados inválidos." });
            }

            alerta.id_alerta = ++_id;
            await _IAlertaRepository.Adcionar(alerta);

            return Ok(new { message = "Alerta cadastrada!", data = alerta });
        }

        /// <summary>
        /// Atualiza os dados de um alerta existente.
        /// </summary>
        /// <param name="id">ID do alerta.</param>
        /// <param name="alerta">Objeto contendo os novos dados do alerta.</param>
        /// <returns>alerta atualizado.</returns>
        /// <response code="200">alerta atualizado com sucesso.</response>
        /// <response code="400">ID inválido ou dados incorretos.</response>
        /// <response code="409">Erro de concorrência ao atualizar o alerta.</response>
        [HttpPut("atualizar/{id}")]
        public async Task<ActionResult> Atualizar(int id, [FromBody] Alerta alerta)
        {
            if (id != alerta.id_alerta)
            {
                return BadRequest(new { message = "O ID informado não corresponde ao alerta." });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Dados inválidos." });
            }

            try
            {
                await _IAlertaRepository.Atualizar(alerta);
            }
            catch (DbUpdateConcurrencyException)
            {
                return Conflict(new { message = "Erro de concorrência ao atualizar a alerta." });
            }

            return Ok(new { message = "alerta atualizada!", data = alerta });
        }

        /// <summary>
        /// Exclui um alerta do sistema com base no ID.
        /// </summary>
        /// <param name="id">ID do alerta.</param>
        /// <returns>Mensagem de sucesso.</returns>
        /// <response code="200">alerta excluído com sucesso.</response>
        /// <response code="404">alerta não encontrado.</response>
        [HttpDelete("excluir/{id}")]
        public async Task<ActionResult> Excluir(int id)
        {
            var alerta = await _IAlertaRepository.ObterPorId(id);
            if (alerta == null)
            {
                return NotFound(new { message = "alerta não encontrada." });
            }

            await _IAlertaRepository.Excluir(alerta);

            return Ok(new { message = "alerta excluída com sucesso!" });
        }
    }

}


