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
    public class PresencaController:ControllerBase
    {
    //    GufosContext _contexto=new GufosContext();
        PresencaRepository _repositorio=new PresencaRepository();

       // GET: api/Presenca
       [HttpGet]
       public async Task<ActionResult<List<Presenca>>> Get()
       {
           var presencas = await _repositorio.Listar();

           if(presencas==null){
               return NotFound();
           }

           return presencas;
       }

       // GET: api/Presenca/2
       [HttpGet("{id}")]
       public async Task<ActionResult<Presenca>> Get(int id)
       {
           var presenca = await _repositorio.BuscarPorId(id);

           if(presenca==null){
               return NotFound();
           }

           return presenca;
       }

       //POST api/Presenca
       [HttpPost]
       public async Task<ActionResult<Presenca>> Post(Presenca presenca){

           try{
               await _repositorio.Salvar(presenca);
           }catch(DbUpdateConcurrencyException){
               throw;
           }

           return presenca;
       }

       [HttpPut("{id}")]
       public async Task<ActionResult> Put(int id,Presenca presenca){

           // Se o Id do objeto não existir 
           //ele retorna 400
           if(id !=presenca.PresencaId){
               return BadRequest();
           }

           try{
               await _repositorio.Alterar(presenca);

           }catch(DbUpdateConcurrencyException){

               //verificamos se o objeto inserido realmente existe no banco
               var presenca_valido =await _repositorio.BuscarPorId(id);

               if(presenca_valido==null){
                   return NotFound();
               }
               else{
                   throw;
               }
              
           }
           //Nocontent =Retorna 204, sem nada
           return NoContent();
       }

        //DELETE api/presenca/id
        [HttpDelete("{id}")]
        public async Task<ActionResult<Presenca>> Delete(int id){
            var presenca = await _repositorio.BuscarPorId(id);
            if(presenca==null){
                return NotFound();
            }
            await _repositorio.Excluir(presenca);
            return presenca;
        }
    }
}