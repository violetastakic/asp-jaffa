using System;
using System.Collections.Generic;
using System.Text;

namespace Aplication.Dto
{
   public  class PostDto
    {

        public PostDto()
        {
            OnePostComments = new List<OnePostCommentsDto>();

            CategoriesPostsDto = new List<CategoriesPostsDto>();
        }


        public int Id { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Description { get; set; }

        public string UserName { get; set; }

        public DateTime CreatedAt { get; set; }

        public string PicturePath { get; set; }


        public string Alt { get; set; }


        public List<OnePostCommentsDto> OnePostComments { get; set; }
       

        public List<CategoriesPostsDto> CategoriesPostsDto { get; set; }





    }
}
