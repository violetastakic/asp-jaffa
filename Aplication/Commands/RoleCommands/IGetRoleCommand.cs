﻿using Aplication.Dto;
using Aplication.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplication.Commands.RoleCommands
{
    public interface IGetRoleCommand:ICommand<int,RoleDto>
    {

    }
}
