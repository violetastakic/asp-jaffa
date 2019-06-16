using Aplication.Dto;
using Aplication.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplication.Commands.CategoryCommand
{
    public interface IGetCategoryCommand:ICommand<int,CategoryDto>
    {


    }
}
