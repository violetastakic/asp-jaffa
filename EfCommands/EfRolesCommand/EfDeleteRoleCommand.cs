using Aplication.Commands.RoleCommands;
using Aplication.Exceptions;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace EfCommands.EfRolesCommand
{
    public class EfDeleteRoleCommand : BaseEfCommand, IDeleteRoleCommand
    {
       

        public EfDeleteRoleCommand(DataContext context):base(context)
        {
           
        }

        public void Execute(int request)
        {
            var role = context.Roles.Find(request);

            if (role == null) throw new EntityNotFoundException("Role");
            if(role.IsDeleted == true) throw new EntityNotFoundException("Role");


            role.IsDeleted = true;
            context.SaveChanges();

        }
    }
}
