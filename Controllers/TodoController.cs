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
        IDataRepo dataRepo ;
        // intialise this repo with a dependency injection
        public TodoController(IDataRepo _ListRepo){
            this.dataRepo = _ListRepo;
        }
        // GET api/todo
        [HttpGet]
        public ActionResult<IEnumerable<Note>> Get()
        {
            var notes = dataRepo.GetAllNotes();
            if(notes.Count > 0){
                return Ok(notes);
            }
            else{
                return Ok("No Entries Available. Database is Empty");
            }
        }

        // GET api/todo/5
        [HttpGet("{id}")]
        public ActionResult<Note> Get(int id)
        {
            var noteById = dataRepo.GetNote(id);
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
        public IActionResult Post([FromBody] Note note)
        {
            if(ModelState.IsValid){
                bool result = dataRepo.PostNote(note);
                if (result)
                {
                    return Created($"/todo/{note.Id}",note);
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
        public IActionResult Put(int id, [FromBody] Note note)
        {
            if(ModelState.IsValid){
                bool result = dataRepo.PutNote(id, note);
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
            bool result = dataRepo.DeleteNote(id);
            if(result){
                return Ok($"note with id : {id} deleted succesfully");
            }
            else{
                return NotFound($"Note with {id} not found.");
            }
        }
    }
}
