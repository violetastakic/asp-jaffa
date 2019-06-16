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
    public class EfEditRoleCommand : BaseEfCommand, IEditRoleCommand
    {


        public EfEditRoleCommand(DataContext context) : base(context)
        {
        }

        public void Execute(RoleDto request)
        {
            var role = context.Roles.Find(request.Id);

            if (role == null) throw new EntityNotFoundException("Role");

            if (request.Name != role.Name)
            {
                if (context.Roles.Any(r => r.Name == request.Name)) throw new EntityAlreadyExistsException("Role");
            }


            role.Name = request.Name;
            role.ModifiedAt = DateTime.Now;

            context.SaveChanges();
        

    }
    }
}
