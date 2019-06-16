using System;
using System.Collections.Generic;
using System.Text;

namespace Aplication.Search
{
   public class PostSearch
    {

        public string Title { get; set; }
        public string Subtitle { get; set; }

        public int UserId { get; set; }
        public int CategoryId { get; set; }
        public int PictureId { get; set; }


        public int PerPage { get; set; } = 2;
        public int PageNumber { get; set; } = 1;
    }
}
