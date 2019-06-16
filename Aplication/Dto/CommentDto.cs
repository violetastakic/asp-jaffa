using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Aplication.Dto
{
    public class CommentDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [MinLength(3, ErrorMessage = "This field must have at least 3 characters")]
        public string Content { get; set; }

        [Required]
        public int? UserId { get; set; }

        [Required]
        public int? PostId { get; set; }
        

    }
}
