using Aplication.Commands.UserCommands;
using Aplication.Dto;
using Aplication.PageResponse;
using Aplication.Search;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EfCommands.EfUsersCommand
{
    public class EfGetUsersCommand : BaseEfCommand, IGetUsersCommand
    {
       

        public EfGetUsersCommand(DataContext context):base(context)
        {
            
        }

        public PageResponse<UserDto> Execute(UserSearch request)
        {
            var users = context.Users.AsQueryable();

          if(request.FirstName != null)
            {
                users = users.Where(u => u.FirstName.ToLower().Contains(request.FirstName.ToLower()));
            }

          if(request.LastName != null)
            {
                users = users.Where(u => u.LastName.ToLower().Contains(request.LastName.ToLower()));
            }

          if(request.UserName != null)
            {
                users = users.Where(u => u.UserName.ToLower().Contains(request.UserName.ToLower()));
            }

            if (request.Email != null)
            {
                users = users.Where(u => u.Email.ToLower().Contains(request.Email.ToLower()));
            }

            if (request.RoleId != 0)
            {
               
                users = users.Where(r => r.RoleId == request.RoleId);

            }

            if (request.Deleted.HasValue)
            {

                users = users.Where(r => r.IsDeleted == request.Deleted);

            }


            var totalCount = users.Count();

            users = users.Skip((request.PageNumber - 1) * request.PerPage).Take(request.PerPage);

            var pagesCount = (int)Math.Ceiling((double)totalCount / request.PerPage);

            var response = new PageResponse<UserDto>
            {
                CurrentPage = request.PageNumber,
                TotalCount = totalCount,
                PagesCount = pagesCount,
                Data = users.Select(u => new UserDto
                {
                    Id=u.Id,
                    FirstName=u.FirstName,
                    LastName=u.LastName,
                    UserName=u.UserName,
                    Email=u.Email,
                    Password=u.Password,
                    RoleId=u.Role.Id

                }


           )
            };
            return response;

        }
    }
}
