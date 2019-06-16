using Aplication.Commands.PictureCommand;
using Aplication.Dto;
using Aplication.Exceptions;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EfCommands.EfPictureCommand
{
    public class EfEditPictureCommand : BaseEfCommand, IEditPictureCommand
    {

        public EfEditPictureCommand(DataContext context):base(context)
        {
           
        }

        public void Execute(PictureDto request)
        {
            var picture = context.Pictures.Find(request.Id);

            if (picture == null) throw new EntityNotFoundException("Picture");

            if (picture.Path != request.Path)
            {
                if (context.Pictures.Any(p => p.Path.Contains(request.Path))) throw new EntityAlreadyExistsException("Picture");
            }


            picture.Alt = request.Alt;
            picture.Path = request.Path;
            picture.ModifiedAt = DateTime.Now;

            context.SaveChanges();
        }
    }
}
