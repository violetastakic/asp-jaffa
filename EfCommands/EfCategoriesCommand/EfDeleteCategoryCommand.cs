using Aplication.Commands.CategoryCommand;
using Aplication.Exceptions;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace EfCommands.EfCategoriesCommand
{
    public class EfDeleteCategoryCommand : BaseEfCommand, IDeleteCategoryCommand
    {
        

        public EfDeleteCategoryCommand(DataContext context):base(context)
        {
           
        }

        public void Execute(int request)
        {

            var category = context.Categories.Find(request);

            if (category == null) throw new EntityNotFoundException("Category");
            if (category.IsDeleted ==true) throw new EntityNotFoundException("Category");


            category.IsDeleted = true;
            
            context.SaveChanges();


        }


    }
}
