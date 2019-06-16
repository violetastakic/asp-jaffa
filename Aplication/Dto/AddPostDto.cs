using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Aplication.Dto
{
   public class AddPostDto

    {


        [Required(ErrorMessage = "This field is required")]
        [MinLength(3, ErrorMessage = "This field must have at least 3 characters")]
        public string Title { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [MinLength(3, ErrorMessage = "This field must have at least 3 characters")]
        public string Subtitle { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [MinLength(3, ErrorMessage = "This field must have at least 3 characters")]
        public string Description { get; set; }
        [Required]
        public int UserId { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [DataType(DataType.ImageUrl)]
        public string PicturePath { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [MinLength(3, ErrorMessage = "This field must have at least 3 characters")]
        public string Alt { get; set; }
       
        public List<int> CategoryId { get; set; }
      


    }
}
