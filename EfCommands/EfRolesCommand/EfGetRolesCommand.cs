using Aplication.Commands.RoleCommands;
using Aplication.Dto;
using Aplication.PageResponse;
using Aplication.Search;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EfCommands.EfRolesCommand
{
    public class EfGetRolesCommand : BaseEfCommand, IGetRolesCommand
    {
        

        public EfGetRolesCommand(DataContext context):base(context)
        {
        
        }

        public PageResponse<RoleDto> Execute(RoleSearch request)
        {

            var roles = context.Roles.AsQueryable();

            if(request.Name != null)
            {
                roles = roles.Where(r => r.Name.ToLower().Contains(request.Name.ToLower()));
            }

            if (request.Deleted.HasValue)
            {
                roles = roles.Where(r => r.IsDeleted == request.Deleted);
            }



            var totalCount = roles  .Count();

            roles = roles.Skip((request.PageNumber - 1) * request.PerPage).Take(request.PerPage);

            var pagesCount = (int)Math.Ceiling((double)totalCount / request.PerPage);


            var response = new PageResponse<RoleDto>
            {
                CurrentPage = request.PageNumber,
                TotalCount = totalCount,
                PagesCount = pagesCount,
                Data = roles.Select(c => new RoleDto
                {

                    Id = c.Id,
                    Name = c.Name,
                    
                }


           )
            };
            return response;
        }
    }
}
