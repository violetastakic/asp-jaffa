using Aplication.Dto;
using Aplication.Interfaces;
using Aplication.PageResponse;
using Aplication.Search;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplication.Commands.PictureCommand
{
    public interface IGetPicturesCommand:ICommand<PictureSearch,PageResponse<PictureDto>>
    {
    }
}
