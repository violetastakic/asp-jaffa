using System;
using System.Collections.Generic;
using System.Text;

namespace Aplication.Search
{
    public class UserSearch
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }

        public bool? Deleted { get; set; }


        public int RoleId { get; set; }


        public int PerPage { get; set; } = 2;
        public int PageNumber { get; set; } = 1;

    }
}
