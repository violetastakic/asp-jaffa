using System;
using System.Collections.Generic;
using System.Text;

namespace Aplication.Dto
{
    public class LoginUserDto
    { 
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
