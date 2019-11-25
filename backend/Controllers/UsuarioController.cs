using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Domains;
using backend.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers
{
    //Definimos nossa rota do controller e dizemos que é um controller de API
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController:ControllerBase
    {
    //    GufosContext _contexto=new GufosContext();
        UsuarioRepository _repositorio=new UsuarioRepository();

       // GET: api/Usuario
       [HttpGet]
       public async Task<ActionResult<List<Usuario>>> Get()
       {
           var usuarios = await _repositorio.Listar();

           if(usuarios==null){
               return NotFound();
           }

           return usuarios;
       }

       // GET: api/Usuario/2
       [HttpGet("{id}")]
       public async Task<ActionResult<Usuario>> Get(int id)
       {
           var usuario = await _repositorio.BuscarPorId(id);

           if(usuario==null){
               return NotFound();
           }

           return usuario;
       }

       //POST api/Usuario
       [HttpPost]
       public async Task<ActionResult<Usuario>> Post(Usuario usuario){

           try{
               await _repositorio.Salvar(usuario);
           }catch(DbUpdateConcurrencyException){
               throw;
           }

           return usuario;
       }

       [HttpPut("{id}")]
       public async Task<ActionResult> Put(int id,Usuario usuario){

           // Se o Id do objeto não existir 
           //ele retorna 400
           if(id !=usuario.UsuarioId){
               return BadRequest();
           }

           try{
               await _repositorio.Alterar(usuario);

           }catch(DbUpdateConcurrencyException){

               //verificamos se o objeto inserido realmente existe no banco
               var usuario_valido =await _repositorio.BuscarPorId(id);

               if(usuario_valido==null){
                   return NotFound();
               }
               else{
                   throw;
               }
              
           }
           //Nocontent =Retorna 204, sem nada
           return NoContent();
       }

        //DELETE api/usuario/id
        [HttpDelete("{id}")]
        public async Task<ActionResult<Usuario>> Delete(int id){
            var usuario = await _repositorio.BuscarPorId(id);
            if(usuario==null){
                return NotFound();
            }
            await _repositorio.Excluir(usuario);
            return usuario;
        }
    }
}