using System.Threading.Tasks;
using backend.Domains;
using backend.ViewModels;

namespace backend.Insterfaces
{
    public interface ILogin
    {
        Usuario Logar(LoginViewModel login);
    }
}