﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
   public class Category:BaseEntity
    {
        public string Name { get; set; }

        public ICollection<PostCategory> CategoryPosts { get; set; }
    }
}
