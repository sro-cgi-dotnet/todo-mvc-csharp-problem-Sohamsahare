using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TodoApi.Models {
    public class Note{
         [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
         public int? Id {get; set;}
         [Required]
         public virtual string Title {get; set;}
        public string PlainText {get; set;}
        
    }
}