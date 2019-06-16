using Aplication.Commands.PictureCommand;
using Aplication.Exceptions;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace EfCommands.EfPictureCommand
{
    public class EfDeletePictureCommand : BaseEfCommand, IDeletePictureCommand
    {
      

        public EfDeletePictureCommand(DataContext context):base(context)
        {
            
        }

        public void Execute(int request)
        {
            var picture = context.Pictures.Find(request);

            if (picture == null) throw new EntityNotFoundException("Picture");
            if (picture.IsDeleted) throw new EntityNotFoundException("Picture");


            picture.IsDeleted = true;
            context.SaveChanges();
        }
    }
}
