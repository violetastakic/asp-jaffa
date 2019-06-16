using Aplication.Commands.PictureCommand;
using Aplication.Dto;
using Aplication.Exceptions;
using EfDataAccess;
using System.Linq;

namespace EfCommands.EfPictureCommand
{
    public class EfAddPictureCommand : BaseEfCommand, IAddPictureCommand
    {
        

        public EfAddPictureCommand(DataContext context):base(context)
        {
           
        }

        public void Execute(PictureDto request)
        {

            if (context.Pictures.Any(p => p.Path == request.Path)) throw new EntityAlreadyExistsException("Picture");
            if (context.Pictures.Any(p => p.Alt == request.Alt)) throw new EntityAlreadyExistsException("Picture");

            context.Pictures.Add(new Domain.Picture
            {
                Path = request.Path,
                Alt = request.Alt

            });
            context.SaveChanges();
        }
    }
}
