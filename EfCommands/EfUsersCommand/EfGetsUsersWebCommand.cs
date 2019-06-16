using Aplication.Commands.UserCommands;
using Aplication.Dto;
using Aplication.Search;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EfCommands.EfUsersCommand
{
    public class EfGetsUsersWebCommand : BaseEfCommand, IGetUsersWebCommand

    {
        

        public EfGetsUsersWebCommand(DataContext contex):base(contex)
        {
          
        }

        public IEnumerable<UserDto> Execute(UserSearch request)
        {
            var users = context.Users.AsQueryable();


            if (request.FirstName != null)
            {
                users = users.Where(p => p.FirstName.ToLower().Contains(request.FirstName.ToLower()));
            }

            if (request.LastName != null)
            {
                users = users.Where(p => p.LastName.ToLower().Contains(request.LastName.ToLower()));
            }
            if (request.UserName != null)
            {
                users = users.Where(p => p.UserName.ToLower().Contains(request.UserName.ToLower()));

            }
            if (request.Email != null)
            {
                users = users.Where(p => p.Email.ToLower().Contains(request.Email.ToLower()));
            }


            if (request.RoleId != 0)
            {
                users = users.Where(r => r.Id == request.RoleId);
            }

            if (request.Deleted.HasValue)
            {

                users = users.Where(r => r.IsDeleted == request.Deleted);

            }


            return users.Select(u => new UserDto
            {
                Id = u.Id,
                FirstName=u.FirstName,
                LastName=u.LastName,
                UserName=u.UserName,
                Email=u.Email,
                Password=u.Password,
                RoleId=u.Role.Id

            });




        }
    }
}
