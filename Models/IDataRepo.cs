using System.Collections.Generic;

namespace TodoApi.Models{
    public interface IDataRepo{
        Note GetNote(int Id);

        List<Note> GetAllNotes();

        bool PostNote(Note note);

        bool PutNote(int id, Note note);
        bool DeleteNote(int id);
    }
}