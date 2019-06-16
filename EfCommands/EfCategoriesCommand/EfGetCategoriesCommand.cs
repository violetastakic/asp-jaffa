using Aplication.Commands.CategoryCommand;
using Aplication.Dto;
using Aplication.PageResponse;
using Aplication.Search;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EfCommands.EfCategoriesCommand
{
    public class EfGetCategoriesCommand : BaseEfCommand, IGetCategoriesCommand
    {


        public EfGetCategoriesCommand(DataContext context):base(context)
        {
            
        }

        public PageResponse<CategoryDto> Execute(CategorySearch request)
        {
            var categories = context.Categories.AsQueryable();

            if(request.Name != null)
            {
                categories = categories.Where(c => c.Name.ToLower().Contains(request.Name.ToLower()));

            }

            if (request.Deleted.HasValue)
            {
                categories = categories.Where(c => c.IsDeleted == request.Deleted);
            }


            var totalCount = categories.Count();

            categories = categories.Skip((request.PageNumber - 1) * request.PerPage).Take(request.PerPage);

            var pagesCount = (int)Math.Ceiling((double)totalCount / request.PerPage);

            var response = new PageResponse<CategoryDto>
            {
                CurrentPage = request.PageNumber,
                TotalCount = totalCount,
                PagesCount = pagesCount,
                Data = categories.Select(c => new CategoryDto
                {
                    Id = c.Id,
                    Name=c.Name
                }


           )
            };
            return response;
        }
    }
}
