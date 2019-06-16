using Aplication.Commands.CategoryCommand;
using Aplication.Dto;
using Aplication.Interfaces;
using Aplication.Search;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EfCommands.EfCategoriesCommand
{
    public class EfGetCategoriesWebCommand : BaseEfCommand, IGetCategoriesWebCommand
    {
      

        public EfGetCategoriesWebCommand(DataContext context):base(context)
        {
           
        }

        public IEnumerable<CategoryDto> Execute(CategorySearch request)
        {
            var categories = context.Categories.AsQueryable();

            if (request.Name != null)
            {
                categories = categories.Where(c => c.Name.ToLower().Contains(request.Name.ToLower()));

            }

            if (request.Deleted.HasValue)
            {
                categories = categories.Where(c => c.IsDeleted == request.Deleted);
            }



            return categories.Select(c => new CategoryDto
            {
                Id = c.Id,
                Name = c.Name
            });
        }
    }
    }

