using Aplication.Commands.UserCommands;
using Aplication.Dto;
using Aplication.Exceptions;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace EfCommands.EfUsersCommand
{
    public class EfGetUserCommand : BaseEfCommand, IGetUserCommand
    {

        public EfGetUserCommand(DataContext context):base(context)
        {
           
        }

        public UserDto Execute(int request)
        {

            var user = context.Users.Find(request);
            if (user == null) throw new EntityNotFoundException("User");
            if(user.IsDeleted == true) throw new EntityNotFoundException("User");


            return new UserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Email = user.Email,
                Password = user.Password,
                RoleId = user.RoleId
            };


        }
    }
}
