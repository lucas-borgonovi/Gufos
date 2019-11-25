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
    public class TipoUsuarioController:ControllerBase
    {
    //    GufosContext _contexto=new GufosContext();
        TipoUsuarioRepository _repositorio=new TipoUsuarioRepository();

       // GET: api/TipoUsuario
       [HttpGet]
       public async Task<ActionResult<List<TipoUsuario>>> Get()
       {
           var tipousuarios = await _repositorio.Listar();

           if(tipousuarios==null){
               return NotFound();
           }

           return tipousuarios;
       }

       // GET: api/TipoUsuario/2
       [HttpGet("{id}")]
       public async Task<ActionResult<TipoUsuario>> Get(int id)
       {
           var tipousuario = await _repositorio.BuscarPorId(id);

           if(tipousuario==null){
               return NotFound();
           }

           return tipousuario;
       }

       //POST api/TipoUsuario
       [HttpPost]
       public async Task<ActionResult<TipoUsuario>> Post(TipoUsuario tipousuario){

           try{
               await _repositorio.Salvar(tipousuario);
           }catch(DbUpdateConcurrencyException){
               throw;
           }

           return tipousuario;
       }

       [HttpPut("{id}")]
       public async Task<ActionResult> Put(int id,TipoUsuario tipousuario){

           // Se o Id do objeto não existir 
           //ele retorna 400
           if(id !=tipousuario.TipoUsuarioId){
               return BadRequest();
           }

           try{
               await _repositorio.Alterar(tipousuario);

           }catch(DbUpdateConcurrencyException){

               //verificamos se o objeto inserido realmente existe no banco
               var tipousuario_valido =await _repositorio.BuscarPorId(id);

               if(tipousuario_valido==null){
                   return NotFound();
               }
               else{
                   throw;
               }
              
           }
           //Nocontent =Retorna 204, sem nada
           return NoContent();
       }

        //DELETE api/tipousuario/id
        [HttpDelete("{id}")]
        public async Task<ActionResult<TipoUsuario>> Delete(int id){
            var tipousuario = await _repositorio.BuscarPorId(id);
            if(tipousuario==null){
                return NotFound();
            }
            await _repositorio.Excluir(tipousuario);
            return tipousuario;
        }
    }
}