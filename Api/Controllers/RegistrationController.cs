using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Aplication.Commands.UserCommands;
using Aplication.Dto;
using Aplication.Exceptions;
using Aplication.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly IRegistrateUserCommand _iRegistrateUserCommand;

        private readonly IEmailSender sender;

        public RegistrationController(IRegistrateUserCommand iRegistrateUserCommand, IEmailSender sender)
        {
            _iRegistrateUserCommand = iRegistrateUserCommand;
            this.sender = sender;
        }






        /// <summary>
        /// Registration.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/registration
        ///     {
        ///        "FirstName": "Violeta",
        ///        "LastName":"Stakic",
        ///        "UserName":"violetastakic",
        ///        "Email":"violeta.stakic@gmail.com",
        ///        "Password":"violeta"
        ///     }
        ///
        /// </remarks>

        /// <response code="201">Registrate</response>
        /// <response code="409">Email or Username already exists</response>
        /// <response code="500">An error has occured</response>
        [HttpPost]
        public ActionResult<RegistrateUserDto> Post([FromBody] RegistrateUserDto dto)
        {

            try
            {


                sender.Subject = "Uspesna registracija";
                sender.ToEmail = dto.Email;
                sender.Body = "Uspešno ste se registrovali." +
                    "Korisničko ime: "+dto.UserName+" , Password:"+dto.Password;

                sender.Send();

                _iRegistrateUserCommand.Execute(dto);
               

               

                return StatusCode(201,"Email is sent");
            }
            catch (EntityAlreadyExistsException e)
            {
                return Conflict(e.Message);
            }
            catch (Exception)
            {
                return StatusCode(500,"An error has occured");
            }

           
        }


    }
}
