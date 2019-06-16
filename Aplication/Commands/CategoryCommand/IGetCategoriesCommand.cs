using Aplication.Dto;
using Aplication.Interfaces;
using Aplication.PageResponse;
using Aplication.Search;

namespace Aplication.Commands.CategoryCommand
{
    public interface IGetCategoriesCommand:ICommand<CategorySearch,PageResponse<CategoryDto>>
    {


    }
}
