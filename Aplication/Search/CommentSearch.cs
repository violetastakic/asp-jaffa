using System;
using System.Collections.Generic;
using System.Text;

namespace Aplication.Search
{
    public class CommentSearch
    {
        public string Content { get; set; }
        public int UserId { get; set; }
        public int PostId { get; set; }

        public int PerPage { get; set; } = 2;
        public int PageNumber { get; set; } = 1;

    }
}
