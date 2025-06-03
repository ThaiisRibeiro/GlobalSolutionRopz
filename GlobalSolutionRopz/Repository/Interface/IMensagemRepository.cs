using GlobalSolutionRopz.Model;

namespace GlobalSolutionRopz.Repository.Interface
{
    public interface IMensagemRepository
    {
        Task Adcionar(Mensagem Objeto);

        Task Atualizar(Mensagem Objeto);

        Task Excluir(Mensagem Objeto);

        Task<Mensagem> ObterPorId(int Id);

        Task<List<Mensagem>> Listar();
    }
}
