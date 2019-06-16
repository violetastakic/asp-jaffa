using Api.Helpers;
using Aplication;
using Aplication.Commands.CategoryCommand;
using Aplication.Dto;
using Aplication.Exceptions;
using Aplication.Search;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {

        private readonly IGetCategoriesCommand _IGetCategoriesCommand;
        private readonly IGetCategoryCommand _IGetCategoryCommand;
        private readonly IAddCategoryCommand _IAddCategoryCommand;
        private readonly IEditCategoryCommand _IEditCategoryCommand;
        private readonly IDeleteCategoryCommand _IDeleteCategoryCommand;


        private readonly LoggedUser _user;

        public CategoriesController(IGetCategoriesCommand IGetCategoriesCommand, IGetCategoryCommand IGetCategoryCommand, IAddCategoryCommand IAddCategoryCommand, IEditCategoryCommand IEditCategoryCommand, IDeleteCategoryCommand IDeleteCategoryCommand, LoggedUser user)
        {
            _IGetCategoriesCommand = IGetCategoriesCommand;
            _IGetCategoryCommand = IGetCategoryCommand;
            _IAddCategoryCommand = IAddCategoryCommand;
            _IEditCategoryCommand = IEditCategoryCommand;
            _IDeleteCategoryCommand = IDeleteCategoryCommand;
            _user = user;
        }






        /// <summary>
        /// Return all categories
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/categories
        ///
        /// </remarks>
        /// <param name="search"></param>
        /// <response code="200">Returns all categories</response>
       
        [LoggedIn("Admin")]
        [HttpGet]
        public ActionResult<IEnumerable<CategoryDto>> Get([FromQuery] CategorySearch search)
        {
            var categories = _IGetCategoriesCommand.Execute(search);
            return Ok(categories);

        }





        /// <summary>
        /// Return a specific Category.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/categories/id
        ///
        /// </remarks>
        /// <param name="id"></param>   
        /// <response code="200">Return category</response>
        /// <response code="404">Category not found</response>
        /// <response code="500">An error has occured</response>

        [HttpGet("{id}")]
        public ActionResult<CategoryDto> Get(int id)
        {
            try
            {
                var category = _IGetCategoryCommand.Execute(id);
                return Ok(category);
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error has occured.");
            }
        }





        /// <summary>
        /// Create a Category.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/categories
        ///     {
        ///        "name": "Category"
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Created Category</response>
        /// <response code="409">Category already exists</response>
        /// <response code="500">An error has occured</response>
        [ProducesResponseType(201)]
        [ProducesResponseType(409)]
        [ProducesResponseType(500)]
        [HttpPost]
        public ActionResult<CategoryDto> Post([FromBody] CategoryDto dto)
        {
            try
            {
                _IAddCategoryCommand.Execute(dto);
                return StatusCode(201);
            }
            catch (EntityAlreadyExistsException e)
            {
                return Conflict(e.Message);

            }
            catch (Exception)
            {
                return StatusCode(500, "An error has occured.");
            }



        }

        /// <summary>
        /// Edit a specific Category
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /api/categories/id
        ///     {
        ///        "id":1
        ///        "name": "Category"
        ///     }
        ///
        /// </remarks>
        /// <param name="id"></param>   
        /// <param name="dto"></param>
        /// <response code="204">Updated category</response>
        /// <response code="404">Category not found</response>
        /// <response code="409">Category already exists</response>
        /// <response code="500">An error has occured</response>
        [HttpPut("{id}")]
        public ActionResult<CategoryDto> Put(int id, [FromBody] CategoryDto dto)
        {
            try
            {
                _IEditCategoryCommand.Execute(dto);
                return NoContent();
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (EntityAlreadyExistsException e)
            {
                return Conflict(e.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error has occured.");
            }
        }

        /// <summary>
        /// Delete category
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE /api/categories/id
        ///
        /// </remarks>
        /// <response code="204">Deleted category</response>
        /// <response code="404">Category not found</response>
        /// <response code="500">An error has occured</response>
        /// <param name="id"></param>  
        [HttpDelete("{id}")]
        public ActionResult<CategoryDto> Delete(int id)
        {

            try
            {
                _IDeleteCategoryCommand.Execute(id);
                return NoContent();


            }
            catch (EntityNotFoundException e)

            {
                return NotFound(e.Message);

            }
            catch (Exception)
            {
                return StatusCode(500, "An error has occured.");
            }
        }
    }
}
