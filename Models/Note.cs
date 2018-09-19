using System.ComponentModel.DataAnnotations;
namespace TodoApi.Models {
    public class Note{
         [Required]
         public int? Id {get; set;}
         [Required]
         public string Title {get; set;}
         public string PlainText {get; set;}
        
    }
}