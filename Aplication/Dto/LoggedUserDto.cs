﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Aplication.Dto
{
   public class LoggedUserDto
    {
       
     
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public bool IsLogged { get; set; }

    }
}
