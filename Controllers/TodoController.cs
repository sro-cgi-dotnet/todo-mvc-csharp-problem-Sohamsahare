using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        // Reposition responsible for fetchin and adding to 
        // database / collection
        ITodoRepo todoRepo ;
        // intialise this repo with a dependency injection
        public TodoController(ITodoRepo _todoRepo){
            this.todoRepo = _todoRepo;
        }
        // GET api/todo
        [HttpGet]
        public ActionResult<IEnumerable<TodoNote>> Get()
        {
            var notes = todoRepo.GetAllNotes();
            if(notes.Count > 0){
                return Ok(notes);
            }
            else{
                return Ok("No Entries Available. Database is Empty");
            }
        }

        // GET api/todo/5
        [HttpGet("{id}")]
        public ActionResult<TodoNote> Get(int id)
        {
            var noteById = todoRepo.GetNote(id);
            if (noteById != null)
            {
                return Ok(noteById);
            }
            else
            {
                return NotFound($"Note with {id} not found.");
            }
        }

        // POST api/todo
        [HttpPost]
        public IActionResult Post([FromBody] TodoNote note)
        {
            if(ModelState.IsValid){
                bool result = todoRepo.PostNote(note);
                if (result)
                {
                    return Created("/api/todo",note);
                }
                else
                {
                    return BadRequest("Note already exists, please try again.");
                }
            }
            return BadRequest("Invalid Format");
        }

        // PUT api/todo/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] TodoNote note)
        {
            if(ModelState.IsValid){
                bool result = todoRepo.PutNote(id, note);
                if(result){
                    return Created("/api/todo", note);
                }
                else{
                    return NotFound($"Note with {id} not found.");
                }
            }
            return BadRequest("Invalid Format");
        }

        // DELETE api/todo/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            bool result = todoRepo.DeleteNote(id);
            if(result){
                return Ok($"note with id : {id} deleted succesfully");
            }
            else{
                return NotFound($"Note with {id} not found.");
            }
        }
    }
}
