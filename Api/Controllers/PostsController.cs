using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Api.Models;
using Aplication.Commands.PostCommands;
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
    public class PostsController : ControllerBase
    {
        private readonly IGetPostsCommand _iGetPostsCommand;
        private readonly IGetPostCommand _iGetPostCommand;
        private readonly IAddPostCommand _iAddPostCommand;
        private readonly IEditPostCommand _iEditPostCommand;
        private readonly IDeletePostCommand _iDeletePostCommand;

        public PostsController(IGetPostsCommand iGetPostsCommand, IGetPostCommand iGetPostCommand, IAddPostCommand iAddPostCommand, IEditPostCommand iEditPostCommand, IDeletePostCommand iDeletePostCommand)
        {
            _iGetPostsCommand = iGetPostsCommand;
            _iGetPostCommand = iGetPostCommand;
            _iAddPostCommand = iAddPostCommand;
            _iEditPostCommand = iEditPostCommand;
            _iDeletePostCommand = iDeletePostCommand;
        }





        /// <summary>
        /// Return all posts
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/posts
        ///
        /// </remarks>
        /// <param name="search"></param>
        /// <response code="200">Returns all posts</response>
        [HttpGet]
        public ActionResult<IEnumerable<PostDto>> Get([FromQuery] PostSearch search)
        {
            var posts = _iGetPostsCommand.Execute(search);
            return Ok(posts);
        }







        /// <summary>
        /// Return a specific Post.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/posts/id
        ///
        /// </remarks>
        /// <param name="id"></param>   
        /// <response code="200">Return post</response>
        /// <response code="404">Post not found</response>
        /// <response code="500">An error has occured</response>
        [HttpGet("{id}")]
        public ActionResult<PostDto> Get(int id)
        {
            try
            {
                var post = _iGetPostCommand.Execute(id);
                return Ok(post);
            }
            catch(EntityNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch(Exception e)
            {
                return StatusCode(500,e.Message);
            }
        }




        /// <summary>
        /// Create a Post.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/posts
        ///     {
        ///        "title": "New post",
        ///        "subtitle":"New post subtitle",
        ///        "description":"New post description",
        ///        "picturePath":"image.jpg",
        ///        "alt":"alt",
        ///         "userId":1,
        ///         "categoryId":5
        ///     }
        ///
        /// </remarks>

        /// <response code="201">Created Post</response>
        /// <response code="404">Entity not found</response>
        /// <response code="409">Post already exists</response>
        /// <response code="500">An error has occured</response>
        [ProducesResponseType(201)]
        [ProducesResponseType(404)]
        [ProducesResponseType(409)]
        [ProducesResponseType(500)]
        [HttpPost]
        public ActionResult<AddPostDto> Post([FromForm] Post p)
        {
            var ext = Path.GetExtension(p.PicturePath.FileName);
            if (!FileUpload.AllowedExtensions.Contains(ext))
            {
                return UnprocessableEntity("Image extension is not allowed.");
            }

            try
            {
                var newFileName = Guid.NewGuid().ToString() + "_" + p.PicturePath.FileName;

                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", newFileName);

                p.PicturePath.CopyTo(new FileStream(filePath, FileMode.Create));

                var dto = new AddPostDto
                {
                    Title = p.Title,
                    Subtitle = p.Subtitle,
                    Description = p.Description,
                    PicturePath = newFileName,
                    Alt = p.Alt,
                    UserId = p.UserId,
                    CategoryId = p.CategoryId
                };

                _iAddPostCommand.Execute(dto);



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

            return StatusCode(201, "Created");


        }





        /// <summary>
        /// Edit a specific Post
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /api/posts/id
        ///     {
        ///        "id":1,
        ///        "title": "New post",
        ///        "subtitle":"New post subtitle",
        ///        "description":"New post description",
        ///        "picturePath":"image.jpg",
        ///        "alt":"alt",
        ///         "userId":1,
        ///         "categoryId":5
        ///     }
        ///
        /// </remarks>
        /// <param name="id"></param>   
        /// <param name="p"></param>
        /// <response code="204">Updated post</response>
        /// <response code="404">Post not found</response>
        /// <response code="409">Post already exists</response>
        /// <response code="500">An error has occured</response>
        [HttpPut("{id}")]
        public ActionResult<EditPostDto> Put(int id, [FromForm] Post p)
        {
            var ext = Path.GetExtension(p.PicturePath.FileName);
            if (!FileUpload.AllowedExtensions.Contains(ext))
            {
                return UnprocessableEntity("Image extension is not allowed.");
            }

            try
            {
                var newFileName = Guid.NewGuid().ToString() + "_" + p.PicturePath.FileName;

                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", newFileName);

                p.PicturePath.CopyTo(new FileStream(filePath, FileMode.Create));

                var dto = new EditPostDto
                {
                    Id = id,
                    Title = p.Title,
                    Subtitle = p.Subtitle,
                    Description = p.Description,
                    PicturePath = newFileName,
                    Alt = p.Alt,
                    UserId = p.UserId,
                    CategoryId = p.CategoryId
                };

                _iEditPostCommand.Execute(dto);



            }
            catch (EntityNotFoundException e)
            {

                return NotFound(e.Message);
            }
            catch (EntityAlreadyExistsException e)
            {

                return Conflict(e.Message);
            }
            catch (Exception e)
            {

                return StatusCode(500,e.Message);
            }
            return NoContent();


        }




        /// <summary>
        /// Delete post
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE /api/posts/id
        ///
        /// </remarks>
        /// <response code="204">Delete post</response>
        /// <response code="400">Post not found</response>
        /// <response code="500">An error has occured</response>
        /// <param name="id"></param>  
        [HttpDelete("{id}")]
        public ActionResult<PostDto> Delete(int id)
        {

            try
            {
                _iDeletePostCommand.Execute(id);
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
