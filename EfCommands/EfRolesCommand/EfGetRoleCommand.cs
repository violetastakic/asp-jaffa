using Aplication.Commands.RoleCommands;
using Aplication.Dto;
using Aplication.Exceptions;
using Aplication.Interfaces;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Text;


namespace EfCommands.EfRolesCommand
{
   public class EfGetRoleCommand:BaseEfCommand,IGetRoleCommand
    {

     

        public EfGetRoleCommand(DataContext context):base(context)
        {
           
        }

        public RoleDto Execute(int request)
        {
            var role = context.Roles.Find(request);

            if (role == null) throw new EntityNotFoundException("Role");
            if(role.IsDeleted == true) throw new EntityNotFoundException("Role");

            return new RoleDto
            {
                Id = role.Id,
                Name = role.Name
            };

        }
    }
}
