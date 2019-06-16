using Aplication.Commands.UserCommands;
using Aplication.Exceptions;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace EfCommands.EfUsersCommand
{
    public class EfDeleteUserCommand : BaseEfCommand, IDeleteUserCommand
    {
        
        public EfDeleteUserCommand(DataContext context):base(context)
        {
          
        }

        public void Execute(int request)
        {
            var user = context.Users.Find(request);

            if (user == null) throw new EntityNotFoundException("User");
            if(user.IsDeleted == true) throw new EntityNotFoundException("User");

            user.IsDeleted = true;
            user.RoleId = null;
            

            context.SaveChanges();
        }
        }
}
