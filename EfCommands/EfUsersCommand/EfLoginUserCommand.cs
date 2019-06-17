using Aplication.Commands.UserCommands;
using Aplication.Dto;
using Aplication.Exceptions;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EfCommands.EfUsersCommand
{
    public class EfLoginUserCommand : BaseEfCommand, ILoginUserCommand
    {
       

        public EfLoginUserCommand(DataContext context):base(context)
        {
            this.context = context;
        }

        public LoginUserDto Execute(LoggedUserDto request)
        {
            if(request.UserName == null)
            {
                throw new EntityNotFoundException("User");
            }
            if (request.Password == null)
            {
                throw new EntityNotFoundException("User");
            }


            if(context.Users.Where(u=>u.UserName == request.UserName &&  u.Password == request.Password).First() == null)
            {
                throw new EntityNotFoundException("User");
            }


            return context.Users.Select(x => new LoginUserDto
            {
                Id=x.Id,
                UserName=x.UserName,
                Password=x.Password,
                Role=x.Role.Name
            }
                ).Where(u=>u.UserName == request.UserName && u.Password == request.Password).First();
        }
    }
}
