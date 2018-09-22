using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace TodoApi.Models {
    public class Note{
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int? NoteId {get; set;}
        [Required]
        public string Title {get; set;}
        public string PlainText {get; set;}
        public bool IsPinned { get; set; }
        public List<CheckListItem> CheckList {get; set;}

        public List<Label> Labels { get; set; }
        
    }
}