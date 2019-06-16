using Aplication.Commands.PictureCommand;
using Aplication.Dto;
using Aplication.Exceptions;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace EfCommands.EfPictureCommand
{
    public class EfGetPictureCommand : BaseEfCommand, IGetPictureCommand
    {
        

        public EfGetPictureCommand(DataContext context):base(context)
        {
            
        }

        public PictureDto Execute(int request)
        {

            var picture = context.Pictures.Find(request);

            if (picture == null) throw new EntityNotFoundException("Picture");
            if (picture.IsDeleted) throw new EntityNotFoundException("Picture");


            return new PictureDto
            {
                Id = picture.Id,
                Alt = picture.Alt,
                Path = picture.Path
            };
        }
    }
}
