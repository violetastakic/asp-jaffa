using Aplication.Commands.PostCommands;
using Aplication.Dto;
using Aplication.PageResponse;
using Aplication.Search;
using EfDataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EfCommands.EfPostsCommand
{
    public class EfGetPostsCommand : BaseEfCommand, IGetPostsCommand
    {
        
        public EfGetPostsCommand(DataContext context):base(context)
        {
         
        }

        public PageResponse<PostsDto> Execute(PostSearch request)
        {

            var posts = context.Posts.AsQueryable();

            if (request.Title != null)
            {
                posts = posts.Where(p => p.Title.ToLower().Contains(request.Title.ToLower()));

            }
            if (request.Subtitle != null)
            {
                posts = posts.Where(p => p.Subtitle.ToLower().Contains(request.Subtitle.ToLower()));

            }


            //USER
            if (request.UserId != 0)
            {
                posts = posts.Where(u => u.Id == request.UserId);
            }

            //CATEGORIES

            if (request.CategoryId != 0)
            {

                posts = posts.Where(c => c.Id == request.CategoryId);
            }


            //PICTURE

            if (request.PictureId != 0)
            {
                posts = posts.Where(p => p.Id == request.PictureId);
            }




            var totalCount = posts.Count();

            posts = posts.Skip((request.PageNumber - 1) * request.PerPage).Take(request.PerPage);

            var pagesCount = (int)Math.Ceiling((double)totalCount / request.PerPage);




            var response = new PageResponse<PostsDto>
            {
                CurrentPage = request.PageNumber,
                TotalCount = totalCount,
                PagesCount = pagesCount,
                Data = posts.Include(p => p.PostCategories)
                            .ThenInclude(pc => pc.Category)
                            .Include(p => p.Picture)

                     .Select(p => new PostsDto
                     {
                         Id = p.Id,
                         Title = p.Title,
                         Subtitle = p.Subtitle,
                         Description = p.Description,
                         CreateDate = p.CreatedAt,
                         ModifiedDate = p.ModifiedAt,
                         UserId = p.UserId,
                         UserName = p.User.UserName,
                         PictureId=p.PictureId,
                         PicturePath=p.Picture.Path,
                         Alt=p.Picture.Alt,
                         Categories = p.PostCategories.Select(pc => new CategoryPostDto
                         {
                             CategoryId = pc.CategoryId,
                             Name = pc.Category.Name
                         }).ToList(),
                         CommentsNumber = p.Comments.Count()


                     }).ToList()
            };
            return response;
        }
    }
}
