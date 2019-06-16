using Aplication.Commands.CategoryCommand;
using Aplication.Dto;
using Aplication.Exceptions;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EfCommands.EfCategoriesCommand
{
    public class EfEditCategoryCommand : BaseEfCommand, IEditCategoryCommand
    {
     

        public EfEditCategoryCommand(DataContext context):base(context)
        {

        }

        public void Execute(CategoryDto request)
        {
            var category = context.Categories.Find(request.Id);

            if (category == null) throw new EntityNotFoundException("Category");

            if (request.Name != category.Name)
            {
                if (context.Categories.Any(c => c.Name == request.Name)) throw new EntityAlreadyExistsException("Category");
            }
            category.Name = request.Name;
            category.ModifiedAt = DateTime.Now;


            context.SaveChanges();

        }
    }
}
