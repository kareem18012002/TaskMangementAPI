using System.ComponentModel.DataAnnotations;

namespace TaskMangementAPI.Models
{
    public class CreateTaskDto
    {
       
        [Required(ErrorMessage = "Title is required.")]
        public string Title { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }
  
    }
}