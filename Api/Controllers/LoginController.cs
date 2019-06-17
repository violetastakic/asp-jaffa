using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Helpers;
using Aplication;
using Aplication.Commands.UserCommands;
using Aplication.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {

         private readonly Encryption _enc;
        private readonly ILoginUserCommand _iLogginUser;

        public LoginController(Encryption enc, ILoginUserCommand iLogginUser)
        {
            _enc = enc;
            _iLogginUser = iLogginUser;
        }


        /// <summary>
        /// Login
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/login
        ///     {
        ///        "username": "violetastakic",
        ///        "password":"password"
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Return token</response>

        [HttpPost]
        public ActionResult Post([FromBody] LoggedUserDto dto )
        {
            try
            {

                var user = _iLogginUser.Execute(dto);


                var stringObject = JsonConvert.SerializeObject(user);

                var encrypted = _enc.EncryptString(stringObject);

                return Ok(new { token = encrypted });
            }
            catch(EntityNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500,e.Message);
            }

        }


        [HttpGet("decode")]
       
        public IActionResult Decode(string value)
        {
          

            
            var decodedString = _enc.DecryptString(value);
            decodedString = decodedString.Substring(0, decodedString.LastIndexOf("}") + 1);

            var user = JsonConvert.DeserializeObject<LoggedUserDto>(decodedString);

            return null;
        }

    }
}
