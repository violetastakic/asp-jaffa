using Aplication.Commands.RoleCommands;
using Aplication.Dto;
using Aplication.Search;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EfCommands.EfRolesCommand
{
    public class EfGetRolesWebCommand : BaseEfCommand, IGetRolesWebCommand
    {


        public EfGetRolesWebCommand(DataContext context) : base(context)
        {
        }

        public IEnumerable<RoleDto> Execute(RoleSearch request)
        {
            var roles = context.Roles.AsQueryable();


            if (request.Name != null)
            {
                roles = roles.Where(r => r.Name.ToLower().Contains(request.Name.ToLower()));
            }

            if (request.Deleted.HasValue)
            {
                roles = roles.Where(r => r.IsDeleted==request.Deleted);

            }



            return roles.Select(r => new RoleDto
            {
                Id = r.Id,
                Name = r.Name
            });

        }
    }
}
