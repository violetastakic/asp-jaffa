using Aplication.Commands.PostCommands;
using Aplication.Dto;
using Aplication.Search;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EfCommands.EfPostsCommand
{
    public class EfGetPostsWebCommand : BaseEfCommand, IGetPostsWebCommand
    {
      

        public EfGetPostsWebCommand(DataContext context) : base(context)
        {
           
        }

        public IEnumerable<PostsDto> Execute(PostSearch request)
        {
            var posts = context.Posts.AsQueryable();

            if (request.Title != null)
            {
                posts = posts.Where(p => p.Title.ToLower().Contains(request.Title.ToLower()));
            }

            if (request.Subtitle != null)
            {
                posts = posts.Where(p => p.Title.ToLower().Contains(request.Title.ToLower()));
            }
            if (request.UserId != 0)
            {
                posts = posts.Where(u => u.Id == request.UserId);
            }
            if (request.CategoryId != 0)
            {
                posts = posts.Where(c => c.Id == request.CategoryId);
            }
            if (request.PictureId != 0)
            {
                posts = posts.Where(p => p.Id == request.PictureId);
            }


            return posts.Select(p => new PostsDto
            {

               Id=p.Id,
               Title=p.Title,
               Subtitle=p.Subtitle,
               Description=p.Description,
               UserId=p.UserId,
               UserName=p.User.UserName,
               PictureId=p.PictureId,
               PicturePath=p.Picture.Path,
               Alt=p.Picture.Alt,
               CommentsNumber=p.Comments.Count(),
               CreateDate=p.CreatedAt,
               ModifiedDate=p.ModifiedAt,
                Categories = p.PostCategories.Select(pc => new CategoryPostDto
                {
                    CategoryId = pc.CategoryId,
                    Name = pc.Category.Name
                }).ToList(),

            });

        }
    }
}
