using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Domains;
using backend.Insterfaces;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories
{
    public class UsuarioRepository : IUsuario
    {
        public async Task<Usuario> Alterar(Usuario usuario)
        {
            using(GufosContext _contexto=new GufosContext()){
                //Comparamos os atributos que foram modificados através do EF
                _contexto.Entry(usuario).State=EntityState.Modified;
                await _contexto.SaveChangesAsync();
                return usuario;
            }
        }

        public async Task<Usuario> BuscarPorId(int id)
        {
            using(GufosContext _contexto=new GufosContext()){
                return await _contexto.Usuario.FindAsync(id);
            }
        }

        public async Task<Usuario> Excluir(Usuario usuario)
        {
            using(GufosContext _contexto=new GufosContext()){
                _contexto.Usuario.Remove(usuario);
                await _contexto.SaveChangesAsync();
                return usuario;
            } 
        }

        public async Task<List<Usuario>> Listar()
        {
            using(GufosContext _contexto= new GufosContext()){
                return await _contexto.Usuario.ToListAsync();
            } 
        }

        public async Task<Usuario> Salvar(Usuario usuario)
        {
             using(GufosContext _contexto=new GufosContext()){
                //Tratamos contra ataques de SQL Injection
                await _contexto.AddAsync(usuario);
                //Salvamos efetivamente o nosso objeto no banco de dados
                await _contexto.SaveChangesAsync();
                return usuario;
            } 
        }
    }
}