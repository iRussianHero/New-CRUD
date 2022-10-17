using System.ComponentModel.DataAnnotations;

namespace New_CRUD.Models
{
    public class Post
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "This field empty...")]
        [MaxLength(255)]
        [MinLength(3, ErrorMessage = "To short...")]
        public string Title { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Content { get; set; }
        public DateTime Date { get; set; }

        [Display(Name = "Image path : ")]
        public string ImageUrl { get; set; }
    }
}
