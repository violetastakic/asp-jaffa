using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Helpers;
using Aplication;
using Aplication.Commands.UserCommands;
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
        private readonly GetUsernameAndPassword _getUsernameAndPassword;

        public LoginController(Encryption enc, GetUsernameAndPassword getUsernameAndPassword) : this(enc)
        {
            _getUsernameAndPassword = getUsernameAndPassword;
        }

        public LoginController(Encryption enc)
        {
            _enc = enc;
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
        public ActionResult Post([FromBody] string username,string password)
        {

            var user = new LoggedUser();
      

            _getUsernameAndPassword.Execute(user);

            var stringObject = JsonConvert.SerializeObject(user);

            var encrypted = _enc.EncryptString(stringObject);

            return Ok(new { token=encrypted});

        }


        [HttpGet("decode")]
       
        public IActionResult Decode(string value)
        {
          

            
            var decodedString = _enc.DecryptString(value);
            decodedString = decodedString.Substring(0, decodedString.LastIndexOf("}") + 1);

            var user = JsonConvert.DeserializeObject<LoggedUser>(decodedString);

            return null;
        }

    }
}
