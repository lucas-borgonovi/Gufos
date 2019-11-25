using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Domains;

namespace backend.Insterfaces
{
    public interface IUsuario
    {
        Task<List<Usuario>> Listar();

        Task<Usuario> BuscarPorId(int id);

        Task<Usuario> Salvar(Usuario usuario);

        Task<Usuario> Alterar(Usuario usuario);

        Task<Usuario> Excluir(Usuario usuario);
    }
}