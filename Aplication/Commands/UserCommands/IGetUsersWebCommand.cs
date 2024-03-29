﻿using Aplication.Dto;
using Aplication.Interfaces;
using Aplication.Search;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplication.Commands.UserCommands
{
   public interface IGetUsersWebCommand:ICommand<UserSearch,IEnumerable<UserDto>>
    {
    }
}
