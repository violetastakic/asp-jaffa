using Aplication.Commands.RoleCommands;
using Aplication.Dto;
using Aplication.Exceptions;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EfCommands.EfRolesCommand
{
    public class EfAddRoleCommand : BaseEfCommand, IAddRoleCommand
    {
      

        public EfAddRoleCommand(DataContext context):base(context)
        {
            
        }

        public void Execute(RoleDto request)
        {
            if (context.Roles.Any(r => r.Name == request.Name)) throw new EntityAlreadyExistsException();

            context.Roles.Add(new Domain.Role
            {
                Name = request.Name
            });
            context.SaveChanges();
        } 
    }
}
