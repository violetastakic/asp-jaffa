using Aplication.Commands.CategoryCommand;
using Aplication.Dto;
using Aplication.Exceptions;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace EfCommands.EfCategoriesCommand
{
    public class EfGetCategoryCommand : BaseEfCommand, IGetCategoryCommand
    {
       

        public EfGetCategoryCommand(DataContext context):base(context)
        {
          
        }

        public CategoryDto Execute(int request)
        {
            var category = context.Categories.Find(request);

            if (category == null) throw new EntityNotFoundException("Category");
            if (category.IsDeleted == true) throw new EntityNotFoundException("Category");


            return new CategoryDto
            {
                Id = category.Id,
                Name = category.Name
            };

        }
    }
}
