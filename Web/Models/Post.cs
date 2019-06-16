﻿
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{
    public class Post
    {
        public string Title { get; set; }

        public string Subtitle { get; set; }

        public string Description { get; set; }

        public int UserId { get; set; }

        public IFormFile PicturePath { get; set; }

        public string Alt { get; set; }

        public List<int> CategoryId { get; set; }

    }
}
