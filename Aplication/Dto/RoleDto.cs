using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Aplication.Dto
{
    public class RoleDto
    {

        public int Id { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [MinLength(3,ErrorMessage = "This field must have at least 3 characters")]
        public string Name { get; set; }

    }
}
