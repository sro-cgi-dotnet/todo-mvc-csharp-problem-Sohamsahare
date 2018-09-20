using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TodoApi.Models{
    public class DatabaseRepo : IDataRepo {

        TodoContext db = null;
        public DatabaseRepo(TodoContext _db){
            this.db = _db;
        }

        public Note GetNote(int Id){
            using (db){
                return db.Notes.FirstOrDefault(n => n.NoteId == Id);
            }
        }

        public List<Note> GetAllNotes(){
            using (db){
                return db.Notes.Include(n=> n.CheckList).ToList();
            }
        }

        public bool PostNote(Note note){
            using (db)
            {
                if(db.Notes.FirstOrDefault(n => n.NoteId == note.NoteId) == null){
                    db.Notes.Add(note);
                    PostChecklist(note);
                    db.SaveChanges();
                    return true;
                }
                else{
                    return false;
                }
            }
        }

        void PostChecklist(Note note){
            foreach(CheckListItem cl in note.CheckList){
                db.CheckLists.Add(cl);
            }
            db.SaveChanges();
        }

        public bool PutNote(int id, Note note){
            using (db){
                Note retrievedNote = db.Notes.FirstOrDefault(n => n.NoteId == id);
                if(retrievedNote != null){
                    db.Notes.Remove(retrievedNote);
                    db.Notes.Add(note);
                    db.SaveChanges();
                    return true;
                }
                else{
                    return false;
                }
            }
        }

        public bool DeleteNote(int id){
            using ( db){
                Note retrievedNote = db.Notes.FirstOrDefault(n => n.NoteId == id);
                if(retrievedNote != null){
                    db.Notes.Remove(retrievedNote);
                    db.SaveChanges();
                    return true;
                }
                else{
                    return false;
                }
            }
        }

        ~DatabaseRepo(){
            db.Dispose();
        }
    }
}
