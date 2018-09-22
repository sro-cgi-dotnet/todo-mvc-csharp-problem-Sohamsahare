// using System.Collections.Generic;
// using System.Linq;

// namespace TodoApi.Models {
//     public class ListRepo : IDataRepo{
//         // initialize the list with at least one note
//         static List<Note> notes=new List<Note>{
//                 new Note{ NoteId = 1, Title = "Sample", PlainText = "This is a sample Note"},
//                 new Note{ NoteId = 2, Title = "Test", PlainText = "This is a test Note"}
//             };

//         public Note GetNote(int id){
//             return notes.FirstOrDefault(note => note.NoteId == id);
//         }

//         public List<Note> GetAllNotes(){
//             return notes;
//         }

//         public bool PostNote(Note note){
//             if(notes.Find(n => n.NoteId == note.NoteId) == null){
//                 notes.Add(note);
//                 return true;
//             }
//             else{
//                 return false;
//             }
//         }

//         public bool PutNote(int id, Note note){
//             Note retrievedNote = notes.Find(n => n.NoteId == id);
//             if( retrievedNote != null){
//                 notes.Remove(retrievedNote);
//                 notes.Add(note);
//                 return true;
//             }
//             else{
//                 return false;
//             }
//         }

//         public bool DeleteNote(int id){
//             Note retrievedNote = notes.Find(n => n.NoteId == id);
//             if (retrievedNote != null){
//                 notes.Remove(retrievedNote);
//                 return true;
//             }
//             else{
//                 return false;
//             }
//         }
//     }
// }