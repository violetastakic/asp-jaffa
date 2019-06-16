using System;
using System.Collections.Generic;
using System.Text;

namespace Aplication.Dto
{
    public class PostsDto
    {
        public PostsDto()
        {
    
            Categories = new List<CategoryPostDto>();
        }


        public int Id { get; set; }


        public string Title { get; set; }


        public string Subtitle { get; set; }


        public string Description { get; set; }
        public DateTime CreateDate { get; set; }

        public DateTime? ModifiedDate { get; set; }


        public int? UserId { get; set; }


        public string UserName { get; set; }


        public int? PictureId { get; set; }


        public string PicturePath { get; set; }


        public string Alt { get; set; }


        public int CommentsNumber { get; set; }
        public List<CategoryPostDto> Categories { get; set; }


    }
}
