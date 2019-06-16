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
    public class EfEditUserCommand : BaseEfCommand, IEditUserCommand
    {
     

        public EfEditUserCommand(DataContext context):base(context)
        {
          
        }

        public void Execute(UserDto request)
        {
            var user = context.Users.Find(request.Id);

            if (user == null) throw new EntityNotFoundException("User");


            if (request.Email != user.Email)
            {
                if (context.Users.Any(u => u.Email == request.Email))
                {
                    throw new EntityAlreadyExistsException("Email");

                }
            }



            
            if (!context.Roles.Any(r => r.Id == request.RoleId)) throw new EntityNotFoundException("Role");


            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.UserName = request.UserName;
            user.Email = request.Email;
            user.Password = request.Password;
            user.RoleId = request.RoleId;

            user.ModifiedAt = DateTime.Now;

            context.SaveChanges();


        }
    }
}
