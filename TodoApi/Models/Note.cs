using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace TodoApi.Models {
    public class Note{
        [Required,Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int? NoteId {get; set;}
        [Required]
        public string Title {get; set;}
        public string PlainText {get; set;}
        public bool IsPinned { get; set; }
        public List<CheckListItem> CheckList {get; set;}

        public List<Label> Labels { get; set; }
        
    }
}


/*
    NoteId:
        type: "integer"
        format: "int64"
    Title:
        type: "string"
        example: "Sample Note Title"
    PlainText:
        type: "string"
        example: "This is sample for the plaintext field."
    IsPinned:
        type: "bool"
*/
