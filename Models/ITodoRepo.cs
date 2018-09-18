using System.Collections.Generic;

namespace TodoApi.Models{
    public interface ITodoRepo{
        TodoNote GetNote(int Id);

        List<TodoNote> GetAllNotes();

        bool PostNote(TodoNote note);

        bool PutNote(int id, TodoNote note);
        bool DeleteNote(int id);
    }
}