using Aplication.Dto;
using Aplication.Interfaces;
using Aplication.PageResponse;
using Aplication.Search;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplication.Commands.UserCommands
{
    public interface IGetUsersCommand:ICommand<UserSearch,PageResponse<UserDto>>
    {
    }
}
