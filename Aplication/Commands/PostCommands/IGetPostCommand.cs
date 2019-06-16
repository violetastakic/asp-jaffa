using Aplication.Dto;
using Aplication.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplication.Commands.PostCommands
{
    public interface IGetPostCommand:ICommand<int,PostDto>
    {
    }
}
