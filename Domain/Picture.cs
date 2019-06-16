using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
   public class Picture:BaseEntity
    {
        public string Path { get; set; }
        public string Alt { get; set; }

      
    }
}
