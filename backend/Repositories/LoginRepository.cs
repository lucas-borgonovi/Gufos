using System.Linq;
using System.Threading.Tasks;
using backend.Domains;
using backend.ViewModels;

namespace backend.Repositories
{
    public class LoginRepository
    {
      public Usuario Logar(LoginViewModel login)
        {
            using(GufosContext _contexto=new GufosContext()){
                //Comparamos os atributos que foram modificados através do EF
                return _contexto.Usuario.FirstOrDefault(u => u.Email == login.Email && u.Senha == login.Senha);
                  
            }
        }  
    }
}