using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aplication.Commands.CommentCommand;
using Aplication.Dto;
using Aplication.Exceptions;
using Aplication.Search;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly IGetCommentsCommand _iGetCommentsCommand;
        private readonly IAddCommentsCommand _iAddCommentCommand;
        private readonly IDeleteCommentsCommand _iDeleteCommentCommand;


        public CommentsController(IGetCommentsCommand iGetCommentsCommand, IAddCommentsCommand iAddCommentCommand, IDeleteCommentsCommand iDeleteCommentCommand)
        {
            _iGetCommentsCommand = iGetCommentsCommand;
            _iAddCommentCommand = iAddCommentCommand;
            _iDeleteCommentCommand = iDeleteCommentCommand;
        }




        /// <summary>
        /// Return all comments
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/comments
        ///
        /// </remarks>
        /// <param name="search"></param>
        /// <response code="200">Returns all comments</response>
        [HttpGet]
        public ActionResult<IEnumerable<CommentDto>> Get([FromQuery] CommentSearch search)
        {

            var comments = _iGetCommentsCommand.Execute(search);

            return Ok(comments);

        }



        /// <summary>
        /// Create a Comment.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/comments
        ///     {
        ///        "content": "Great!",
        ///        "UserId":1,
        ///        "PostId":5
        ///        
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Created Comment</response>
        /// <response code="404">Comment not found</response>
        /// <response code="500">An error has occured</response>
        [ProducesResponseType(201)]
        [ProducesResponseType(409)]
        [HttpPost]
        public ActionResult Post([FromBody] CommentDto dto)
        {

            try
            {
                _iAddCommentCommand.Execute(dto);
                return StatusCode(201);
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
        /// Delete comment
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE /api/comments/id
        ///
        /// </remarks>
        /// <response code="204">Delete comment</response>
        /// <response code="400">Comment not found</response>
        /// <response code="500">An error has occured</response>
        /// <param name="id"></param>  
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try{
                _iDeleteCommentCommand.Execute(id);
                return NoContent();

            }
            catch (EntityNotFoundException e)
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
