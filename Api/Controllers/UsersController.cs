using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aplication.Commands.UserCommands;
using Aplication.Dto;
using Aplication.Exceptions;
using Aplication.Search;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly IGetUsersCommand _iGetUsersCommand;
        private readonly IAddUserCommand _iAddUserCommand;
        private readonly IEditUserCommand _iEditUserCommand;
        private readonly IGetUserCommand _iGetUserCommand;
        private readonly IDeleteUserCommand _iDelteUserCommand;

        public UsersController(IGetUsersCommand iGetUsersCommand, IAddUserCommand iAddUserCommand, IEditUserCommand iEditUserCommand, IGetUserCommand iGetUserCommand, IDeleteUserCommand iDelteUserCommand)
        {
            _iGetUsersCommand = iGetUsersCommand;
            _iAddUserCommand = iAddUserCommand;
            _iEditUserCommand = iEditUserCommand;
            _iGetUserCommand = iGetUserCommand;
            _iDelteUserCommand = iDelteUserCommand;
        }





        /// <summary>
        /// Return all users
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/users
        ///
        /// </remarks>
        /// <param name="search"></param>
        /// <response code="200">Returns all users</response>
        [HttpGet]
        public ActionResult<IEnumerable<UserDto>> Get([FromQuery] UserSearch search)
        {
            var users = _iGetUsersCommand.Execute(search);
            return Ok(users);
        }





        /// <summary>
        /// Return a specific User.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/users/id
        ///
        /// </remarks>
        /// <param name="id"></param>   
        /// <response code="200">Return user</response>
        /// <response code="404">User not found</response>
        /// <response code="500">An error has occured</response>
        [HttpGet("{id}")]
        public ActionResult<UserDto> Get(int id)
        {
            try
            {
                var user = _iGetUserCommand.Execute(id);
                return Ok(user);

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
        /// Create a User.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/users
        ///     {
        ///       
        ///        "FirstName": "Violeta",
        ///        "LastName":"Stakic",
        ///        "UserName":"violetastakic",
        ///        "Email":"violeta.stakic@gmail.com",
        ///        "Password":"violeta",
        ///        "RoleId":1
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Created User</response>
        /// <response code="409">User already exists</response>
        /// <response code="409">User not found</response>
        /// <response code="500">An error has occured</response>
        [ProducesResponseType(201)]
        [ProducesResponseType(409)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [HttpPost]
        public ActionResult<UserDto> Post([FromBody] UserDto dto)
        {

            try
            {
                _iAddUserCommand.Execute(dto);
                return StatusCode(201);
            }
            catch (EntityAlreadyExistsException e)
            {
                return Conflict(e.Message);
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
        /// Edit a specific User
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /api/users/id
        ///     {
        ///        "id":1,
        ///        "FirstName": "Violeta",
        ///        "LastName":"Stakic",
        ///        "UserName":"violetastakic",
        ///        "Email":"violeta.stakic@gmail.com",
        ///        "Password":"violeta",
        ///        "RoleId":1
        ///     }
        ///
        /// </remarks>
        /// <param name="id"></param>   
        /// <param name="dto"></param>
        /// <response code="204">Updated user</response>
        /// <response code="400">User not found</response>
        /// <response code="409">User already exists</response>
        /// <response code="500">An error has occured</response>
        [HttpPut("{id}")]
        public ActionResult<UserDto> Put(int id, [FromBody] UserDto dto)
        {

            try
            {
                _iEditUserCommand.Execute(dto);
                return NoContent();
            }
            catch (EntityAlreadyExistsException e)
            {
                return Conflict(e.Message);
            }
            catch(EntityNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error has occured.");
            }

        }


        /// <summary>
        /// Delete user
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE /api/user/id
        ///
        /// </remarks>
        /// <response code="204">Delete user</response>
        /// <response code="404">User not found</response>
        /// <response code="500">An error has occured</response>
        /// <param name="id"></param>  
        [HttpDelete("{id}")]
        public ActionResult<UserDto> Delete(int id)
        {

            try
            {
                _iDelteUserCommand.Execute(id);
                return NoContent();
            }
            catch(EntityNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch(Exception)
            {
                return StatusCode(500, "An error has occured.");
            }

            
        }
    }
}
