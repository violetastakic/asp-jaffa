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
    public class EfAddCategoryCommand : BaseEfCommand, IAddCategoryCommand
    {
       
        public EfAddCategoryCommand(DataContext context):base(context)
        {
           
        }

        public void Execute(CategoryDto request)
        {
            if (context.Categories.Any(c => c.Name == request.Name)) throw new EntityAlreadyExistsException("Category");


            context.Categories.Add(new Domain.Category
            {
                Name = request.Name
            });
            context.SaveChanges();



        }
    }
}
