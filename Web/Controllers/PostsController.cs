using System;
using System.IO;
using System.Linq;
using Aplication.Commands.PostCommands;
using Aplication.Dto;
using Aplication.Exceptions;
using Aplication.Heplpers;
using Aplication.Search;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web.Models;

namespace Web.Controllers
{
    public class PostsController : Controller
    {

        private readonly IGetPostsWebCommand _iGetPostsWebCommand;
        private readonly IGetPostCommand _iGetPostCommand;
        private readonly IAddPostCommand _iAddPostCommand;
        private readonly IEditPostCommand _iEditPostCommand;
        private readonly IDeletePostCommand _iDeletePostCommand;

        public PostsController(IGetPostsWebCommand iGetPostsWebCommand, IGetPostCommand iGetPostCommand, IAddPostCommand iAddPostCommand, IEditPostCommand iEditPostCommand, IDeletePostCommand iDeletePostCommand)
        {
            _iGetPostsWebCommand = iGetPostsWebCommand;
            _iGetPostCommand = iGetPostCommand;
            _iAddPostCommand = iAddPostCommand;
            _iEditPostCommand = iEditPostCommand;
            _iDeletePostCommand = iDeletePostCommand;
        }






        // GET: Posts
        public ActionResult Index(PostSearch search)
        {

            var posts = _iGetPostsWebCommand.Execute(search);
            return View(posts);
        }

        // GET: Posts/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                var post = _iGetPostCommand.Execute(id);
                return View(post);
            }
            catch (EntityNotFoundException)
            {
                TempData["error"] = "Post not found";
                return View();
            }
            catch (Exception)
            {
                TempData["error"] = "An error has occured";
                return View();
            }
        }

        // GET: Posts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Posts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Post p)
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
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
            return RedirectToAction("index");

        }

        // GET: Posts/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                var dto = _iGetPostCommand.Execute(id);
                return View(dto);
            }
            catch (EntityNotFoundException)
            {
                TempData["error"] = "Category not found";
                return View();
            }
            catch (Exception)
            {

                return RedirectToAction("index");
            }
        }

        // POST: Posts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Post p)
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
                    Id=id,
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
            catch (EntityNotFoundException)
            {
                TempData["error"] = "Category not found";
                return View();
            }
            catch (Exception)
            {

                return RedirectToAction("index");
            }
            return NoContent();
        }



        // GET: posts/Edit/5
        public ActionResult Delete(int id)
        {
            try
            {
                var post = _iGetPostCommand.Execute(id);

                return View(post);
            }
            catch (EntityNotFoundException)
            {
                TempData["error"] = "Post not found";
                return View();
            }
            catch (Exception)
            {

                return RedirectToAction("index");
            }


        }




        // POST: Users/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, UserDto dto)
        {
            try
            {
                _iDeletePostCommand.Execute(id);
                return RedirectToAction(nameof(Index));
            }
            catch (EntityNotFoundException)
            {
                TempData["error"] = "Category not found";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {

                return RedirectToAction("index");
            }
        }
    }
}