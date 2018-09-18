using System.Collections.Generic;
using System.Linq;

namespace TodoApi.Models {
    public class TodoRepo : ITodoRepo{
        // initialize the list with at least one note
        static List<TodoNote> notes=new List<TodoNote>{
                new TodoNote{ Id = 1, Title = "Sample", PlainText = "This is a sample Note"},
                new TodoNote{ Id = 2, Title = "Test", PlainText = "This is a test Note"}
            };

        public TodoNote GetNote(int id){
            return notes.FirstOrDefault(note => note.Id == id);
        }

        public List<TodoNote> GetAllNotes(){
            return notes;
        }

        public bool PostNote(TodoNote note){
            if(notes.Find(n => n.Id == note.Id) == null){
                notes.Add(note);
                return true;
            }
            else{
                return false;
            }
        }

        public bool PutNote(int id, TodoNote note){
            TodoNote retrievedNote = notes.Find(n => n.Id == note.Id);
            if( retrievedNote != null){
                notes.Remove(retrievedNote);
                notes.Add(note);
                return true;
            }
            else{
                return false;
            }
        }

        public bool DeleteNote(int id){
            TodoNote retrievedNote = notes.Find(n => n.Id == id);
            if (retrievedNote != null){
                notes.Remove(retrievedNote);
                return true;
            }
            else{
                return false;
            }
        }
    }
}