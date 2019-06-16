using System;
using System.Collections.Generic;
using System.Text;

namespace Aplication.Dto
{
   public class OnePostCommentsDto
    {
        public int Id { get; set; }
        public string Comments { get; set; }
        public string UserComment { get; set; }
        public DateTime CommentAt { get; set; }
    }
}
