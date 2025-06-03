using GlobalSolutionRopz.Model;

namespace GlobalSolutionRopz.Repository.Interface
{
    public interface IAlertaRepository
    {
        Task Adcionar(Alerta Objeto);

        Task Atualizar(Alerta Objeto);

        Task Excluir(Alerta Objeto);

        Task<Alerta> ObterPorId(int Id);

        Task<List<Alerta>> Listar();
    }
}
