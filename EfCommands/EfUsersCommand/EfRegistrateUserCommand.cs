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
    public class EfRegistrateUserCommand : BaseEfCommand, IRegistrateUserCommand
    {
        

        public EfRegistrateUserCommand(DataContext context):base(context)
        {
        }

        public void Execute(RegistrateUserDto request)
        {
            if (context.Users.Any(u => u.Email == request.Email)) throw new EntityAlreadyExistsException("Email");
            if (context.Users.Any(u => u.UserName == request.UserName)) throw new EntityAlreadyExistsException("Username");

            

            context.Users.Add(new Domain.User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.UserName,
                Email = request.Email,
                Password = request.Password,
                RoleId = 3


            });

            context.SaveChanges();
        }
    }
}
