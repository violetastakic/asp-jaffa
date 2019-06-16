using System;
using System.Collections.Generic;
using System.Text;

namespace Aplication.Search
{
    public class CategorySearch
    {
        public string Name { get; set; }
        public bool? Deleted { get; set; }

        public int PerPage { get; set; } = 2;
        public int PageNumber { get; set; } = 1;

    }
}
