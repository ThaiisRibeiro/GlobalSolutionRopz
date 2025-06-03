using GlobalSolutionRopz.Model;

namespace GlobalSolutionRopz.Repository.Interface
{
    public interface IUsuarioRepository
    {
        Task Adcionar(Usuario Objeto);

        Task Atualizar(Usuario Objeto);

        Task Excluir(Usuario Objeto);

        Task<Usuario> ObterPorId(int Id);

        Task<List<Usuario>> Listar();
    }
}
