using System.ComponentModel.DataAnnotations;

namespace TodoApi.Models
{
    public class CheckListItem {
        public int Id {get; set;}
        public bool IsChecked {get; set;}
        public string Text {get; set;}
        public int NoteId{get; set;}

    }
}