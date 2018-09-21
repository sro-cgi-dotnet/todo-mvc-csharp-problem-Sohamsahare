using System.ComponentModel.DataAnnotations;
namespace TodoApi.Models{
    public class Label{
        public int Id {get; set;}
        [Required]
        public string Name { get; set; }
        public int NoteId { get; set; } 
    }
}