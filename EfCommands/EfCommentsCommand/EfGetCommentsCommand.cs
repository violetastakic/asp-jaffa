using Aplication.Commands.CommentCommand;
using Aplication.Dto;
using Aplication.PageResponse;
using Aplication.Search;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EfCommands.EfCommentsCommand
{
    public class EfGetCommentsCommand : BaseEfCommand, IGetCommentsCommand
    {
        

        public EfGetCommentsCommand(DataContext context):base(context)
        {
         
        }

        public PageResponse<CommentDto> Execute(CommentSearch request)
        {
            var comments = context.Comments.AsQueryable();

            if(request.Content != null)
            {
                comments = comments.Where(c => c.Content.ToLower().Contains(request.Content.ToLower()));
            }

            if(request.UserId != 0)
            {

                comments = comments.Where(c => c.UserId == request.UserId);
            }
            if (request.PostId != 0)
            {

                comments = comments.Where(c => c.PostId == request.PostId);
            }

            var totalCount = comments.Count();

            comments = comments.Skip((request.PageNumber - 1) * request.PerPage).Take(request.PerPage);

            var pagesCount = (int)Math.Ceiling((double)totalCount / request.PerPage);




            var response = new PageResponse<CommentDto>
            {
                CurrentPage = request.PageNumber,
                TotalCount = totalCount,
                PagesCount = pagesCount,
                Data = comments.Select(c => new CommentDto
                {
                    Id = c.Id,
                    Content = c.Content,
                    UserId = c.UserId,
                    PostId = c.PostId
                }


           )
            };
            return response;






        }
    }
}
