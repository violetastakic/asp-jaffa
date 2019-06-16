using Aplication.Dto;
using Aplication.Interfaces;
using Aplication.PageResponse;
using Aplication.Search;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplication.Commands.CommentCommand
{
   public interface IGetCommentsCommand:ICommand<CommentSearch,PageResponse<CommentDto>>
    {
    }
}
