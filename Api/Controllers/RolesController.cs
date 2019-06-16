using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aplication.Commands.RoleCommands;
using Aplication.Dto;
using Aplication.Exceptions;
using Aplication.Search;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IGetRolesCommand _iGetRolesCommand;
        private readonly IGetRoleCommand _iGetRoleCommand;
        private readonly IAddRoleCommand _iAddRoleCommand;
        private readonly IEditRoleCommand _iEditRoleCommand;
        private readonly IDeleteRoleCommand _iDeleteRoleCommand;

        public RolesController(IGetRolesCommand iGetRolesCommand, IGetRoleCommand iGetRoleCommand, IAddRoleCommand iAddRoleCommand, IEditRoleCommand iEditRoleCommand, IDeleteRoleCommand iDeleteRoleCommand)
        {
            _iGetRolesCommand = iGetRolesCommand;
            _iGetRoleCommand = iGetRoleCommand;
            _iAddRoleCommand = iAddRoleCommand;
            _iEditRoleCommand = iEditRoleCommand;
            _iDeleteRoleCommand = iDeleteRoleCommand;
        }





        /// <summary>
        /// Return all roles
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/roles
        ///
        /// </remarks>
        /// <param name="search"></param>
        /// <response code="200">Returns all roles</response>
        [HttpGet]
        public ActionResult<IEnumerable<RoleDto>> Get([FromQuery] RoleSearch search)
        {
            var roles = _iGetRolesCommand.Execute(search);
            return Ok(roles);
        }






        /// <summary>
        /// Return a specific Role.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/roles/id
        ///
        /// </remarks>
        /// <param name="id"></param>   
        /// <response code="200">Return role</response>
        /// <response code="404">Role not found</response>
        /// <response code="500">An error has occured</response>
        [HttpGet("{id}")]
        public ActionResult<RoleDto> Get(int id)
        {
            try
            {
                var role = _iGetRoleCommand.Execute(id);
                return Ok(role);

            }catch(EntityNotFoundException e){
                return NotFound(e.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error has occured.");
            }
        }








        /// <summary>
        /// Create a Role.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/roles
        ///     {
        ///        "name": "Role name"
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Created Role</response>
        /// <response code="409">Role already exists</response>
        /// <response code="500">An error has occured</response>
        [ProducesResponseType(201)]
        [ProducesResponseType(409)]
        [ProducesResponseType(500)]
        [HttpPost]
        public ActionResult<RoleDto> Post([FromBody] RoleDto dto)
        {
            try
            {
                _iAddRoleCommand.Execute(dto);
                return StatusCode(201);

            }
            catch(EntityAlreadyExistsException e)
            {
                return Conflict(e.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error has occured.");
            }

        }







        /// <summary>
        /// Edit a specific Role
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /api/roles/id
        ///     {
        ///        "id":1,
        ///        "name": "New role name"
        ///     }
        ///
        /// </remarks>
        /// <param name="id"></param>   
        /// <param name="dto"></param>
        /// <response code="204">Updated role</response>
        /// <response code="404">Role not found</response>
        /// <response code="409">Role already exists</response>
        /// <response code="500">An error has occured</response>
        [HttpPut("{id}")]
        public ActionResult<RoleDto> Put(int id, [FromBody] RoleDto dto)
        {
            try
            {
                _iEditRoleCommand.Execute(dto);
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
        /// Delete role
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE /api/roles/id
        ///
        /// </remarks>
        /// <response code="204">Delete role</response>
        /// <response code="404">Role not found</response>
        /// <response code="500">An error has occured</response>
        /// <param name="id"></param>  
        [HttpDelete("{id}")]
        public ActionResult<RoleDto> Delete(int id)
        {

            try
            {
               _iDeleteRoleCommand.Execute(id);
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
