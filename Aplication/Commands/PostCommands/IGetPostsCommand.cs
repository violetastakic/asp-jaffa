using Aplication.Dto;
using Aplication.Interfaces;
using Aplication.PageResponse;
using Aplication.Search;


namespace Aplication.Commands.PostCommands
{
    public interface IGetPostsCommand:ICommand<PostSearch,PageResponse<PostsDto>>
    {
    }
}
