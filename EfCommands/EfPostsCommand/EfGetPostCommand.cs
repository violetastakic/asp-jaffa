using Aplication.Commands.PostCommands;
using Aplication.Dto;
using Aplication.Exceptions;
using EfDataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EfCommands.EfPostsCommand
{
    public class EfGetPostCommand : BaseEfCommand, IGetPostCommand
    {
     

        public EfGetPostCommand(DataContext context):base(context)
        {
            
        }

        public PostDto Execute(int request)
        {
            var post = context.Posts
                .Include(p => p.User)
                .Include(p => p.Picture)
                .Include(p => p.Comments)
                .Include(p => p.PostCategories)
                .ThenInclude(pc => pc.Category)
                .LastOrDefault();







            if (post == null) throw new EntityNotFoundException("Post");
          


            return new PostDto
            {
                Id = post.Id,
                Title = post.Title,
                Subtitle = post.Subtitle,
                Description = post.Description,
                UserName = post.User.UserName,
                CreatedAt = post.CreatedAt,
                OnePostComments = post.Comments.Select(c => new OnePostCommentsDto
                {
                    Id = c.Id,
                    Comments = c.Content,
                    CommentAt = c.CreatedAt,
                    UserComment = c.User.UserName
                }).ToList(),
                CategoriesPostsDto = post.PostCategories.Select(pc => new CategoriesPostsDto
                {
                    CategoryId=pc.CategoryId,
                    Name=pc.Category.Name
                }).ToList(),
                PicturePath=post.Picture.Path,
                Alt=post.Picture.Alt,
                

            };


        }
    }
}
