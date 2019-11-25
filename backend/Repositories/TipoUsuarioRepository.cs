using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Domains;
using backend.Insterfaces;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories
{
    public class TipoUsuarioRepository : ITipoUsuario
    {
        public async Task<TipoUsuario> Alterar(TipoUsuario tipousuario)
        {
            using(GufosContext _contexto=new GufosContext()){
                //Comparamos os atributos que foram modificados através do EF
                _contexto.Entry(tipousuario).State=EntityState.Modified;
                await _contexto.SaveChangesAsync();
                return tipousuario;
            }
        }

        public async Task<TipoUsuario> BuscarPorId(int id)
        {
            using(GufosContext _contexto=new GufosContext()){
                return await _contexto.TipoUsuario.FindAsync(id);
            }
        }

        public async Task<TipoUsuario> Excluir(TipoUsuario tipousuario)
        {
            using(GufosContext _contexto=new GufosContext()){
                _contexto.TipoUsuario.Remove(tipousuario);
                await _contexto.SaveChangesAsync();
                return tipousuario;
            } 
        }

        public async Task<List<TipoUsuario>> Listar()
        {
            using(GufosContext _contexto= new GufosContext()){
                return await _contexto.TipoUsuario.ToListAsync();
            } 
        }

        public async Task<TipoUsuario> Salvar(TipoUsuario tipousuario)
        {
             using(GufosContext _contexto=new GufosContext()){
                //Tratamos contra ataques de SQL Injection
                await _contexto.AddAsync(tipousuario);
                //Salvamos efetivamente o nosso objeto no banco de dados
                await _contexto.SaveChangesAsync();
                return tipousuario;
            } 
        }
    }
}