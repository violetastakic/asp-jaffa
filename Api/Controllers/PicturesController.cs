using System;
using System.IO;
using System.Linq;
using Api.Models;
using Aplication.Commands.PictureCommand;
using Aplication.Dto;
using Aplication.Exceptions;
using Aplication.Heplpers;
using Aplication.Search;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PicturesController : ControllerBase
    {
        private readonly IAddPictureCommand _iAddPictureCommand;
        private readonly IGetPictureCommand _iGetPictureCommand;
        private readonly IGetPicturesCommand _iGetPicturesCommand;
        private readonly IEditPictureCommand _iEditPictureCommand;
        private readonly IDeletePictureCommand _iDeletePictureCommand;

        public PicturesController(IAddPictureCommand iAddPictureCommand, IGetPictureCommand iGetPictureCommand, IGetPicturesCommand iGetPicturesCommand, IEditPictureCommand iEditPictureCommand, IDeletePictureCommand iDeletePictureCommand)
        {
            _iAddPictureCommand = iAddPictureCommand;
            _iGetPictureCommand = iGetPictureCommand;
            _iGetPicturesCommand = iGetPicturesCommand;
            _iEditPictureCommand = iEditPictureCommand;
            _iDeletePictureCommand = iDeletePictureCommand;
        }



        /// <summary>
        /// Return all pictures
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/pictures
        ///
        /// </remarks>
        /// <param name="search"></param>
        /// <response code="200">Returns all pictures</response>
        [HttpGet]
        public ActionResult<PictureDto> Get([FromQuery] PictureSearch search)
        {
            var pictures = _iGetPicturesCommand.Execute(search);
            return Ok(pictures);
        }







        /// <summary>
        /// Return a specific Picture.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/pictures/id
        ///
        /// </remarks>
        /// <param name="id"></param>   
        /// <response code="200">Return picture</response>
        /// <response code="404">Picture not found</response>
        /// <response code="500">An error has occured</response>
        [HttpGet("{id}")]
        public ActionResult<PictureDto> Get(int id)
        {
            try
            {
                var picture = _iGetPictureCommand.Execute(id);
                return Ok(picture);

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
        /// Create a Picture.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/pictures
        ///     {
        ///        "Path": "image.jpg",
        ///        "Alt!":"Alt"
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Created Picture</response>
        /// <response code="409">Picture already exists</response>
        /// <response code="500">An error has occured</response>
        [ProducesResponseType(201)]
        [ProducesResponseType(409)]
        [ProducesResponseType(500)]
        [HttpPost]
        public ActionResult<PictureDto> Post([FromForm] Picture p)
        {
          
            var ext = Path.GetExtension(p.Path.FileName);
            if (!FileUpload.AllowedExtensions.Contains(ext))
            {
                return UnprocessableEntity("Image extension is not allowed.");
            }

            try
            {
                var newFileName = Guid.NewGuid().ToString() + "_" + p.Path.FileName;

                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", newFileName);

                p.Path.CopyTo(new FileStream(filePath, FileMode.Create));

                var dto = new PictureDto
                {
                    Path = newFileName,
                    Alt = p.Alt
                };

                _iAddPictureCommand.Execute(dto);
              

            }
            catch (EntityAlreadyExistsException e)
            {
                return Conflict(e.Message);
            }
            catch (Exception)
            {
                return StatusCode(500,"An error has occured");
            }
            return NoContent();

        }








        /// <summary>
        /// Edit a specific Picture
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /api/pictures/id
        ///     {
        ///        "id":1
        ///        "Path": "image1.jpg",
        ///        "Alt":"New Alt"
        ///     }
        ///
        /// </remarks>
        /// <param name="id"></param>   
        /// <param name="p"></param>
        /// <response code="204">Updated picture</response>
        /// <response code="404">Picture not found</response>
        /// <response code="409">Picture already exists</response>
        /// <response code="500">An error has occured</response>
        [HttpPut("{id}")]
        public ActionResult<PictureDto> Put(int id, [FromForm] Picture p)
        {
            var ext = Path.GetExtension(p.Path.FileName);
            if (!FileUpload.AllowedExtensions.Contains(ext))
            {
                return UnprocessableEntity("Image extension is not allowed.");
            }

            try
            {
                var newFileName = Guid.NewGuid().ToString() + "_" + p.Path.FileName;

                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", newFileName);

                p.Path.CopyTo(new FileStream(filePath, FileMode.Create));

                var dto = new PictureDto
                {
                    Id=id,
                    Path = newFileName,
                    Alt = p.Alt
                };

                _iEditPictureCommand.Execute(dto);


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
                return StatusCode(500,"An error has occured");
            }
            return NoContent();


        }


        /// <summary>
        /// Delete Picture
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE /api/pictures/id
        ///
        /// </remarks>
        /// <response code="204">Delete picture</response>
        /// <response code="400">Picture not found</response>
        /// <response code="500">An error has occured</response>
        /// <param name="id"></param>  
        [HttpDelete("{id}")]
        public ActionResult<PictureDto> Delete(int id)
        {

            try
            {
                _iDeletePictureCommand.Execute(id);
                return NoContent();
            }
            catch(EntityNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception)
            {
                return StatusCode(500,"An error has occured");
            }
        }
    }
}
