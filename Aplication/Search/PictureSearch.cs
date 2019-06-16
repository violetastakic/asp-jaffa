using System;
using System.Collections.Generic;
using System.Text;

namespace Aplication.Search
{
    public class PictureSearch
    {

        public string Path { get; set; }
        public string Alt { get; set; }


        public int PerPage { get; set; } = 2;
        public int PageNumber { get; set; } = 1;
    }
}
