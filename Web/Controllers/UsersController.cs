using System;
using Aplication.Commands.RoleCommands;
using Aplication.Commands.UserCommands;
using Aplication.Dto;
using Aplication.Exceptions;
using Aplication.Search;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class UsersController : Controller
    {

        private readonly IGetUsersWebCommand _iGetUsersWebCommand;
        private readonly IGetUserCommand _iGetUserCommand;
        private readonly IDeleteUserCommand _iDeleteUserCommand;
        private readonly IEditUserCommand _iEditUserCommand;
        private readonly IAddUserCommand _iAddUserCommand;
        private readonly IGetRolesWebCommand _iGetRolesWebCommand;

        public UsersController(IGetUsersWebCommand iGetUsersWebCommand, IGetUserCommand iGetUserCommand, IDeleteUserCommand iDeleteUserCommand, IEditUserCommand iEditUserCommand, IAddUserCommand iAddUserCommand, IGetRolesWebCommand iGetRolesWebCommand)
        {
            _iGetUsersWebCommand = iGetUsersWebCommand;
            _iGetUserCommand = iGetUserCommand;
            _iDeleteUserCommand = iDeleteUserCommand;
            _iEditUserCommand = iEditUserCommand;
            _iAddUserCommand = iAddUserCommand;
            _iGetRolesWebCommand = iGetRolesWebCommand;
        }






        // GET: Users
        public ActionResult Index(UserSearch search)
        {
            var users = _iGetUsersWebCommand.Execute(search);

            return View(users);
        }

        // GET: Users/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                var user = _iGetUserCommand.Execute(id);
                return View(user);

            }
            catch (EntityNotFoundException){

                TempData["error"] = "User not found";
                return View();

             }
            catch (Exception)
            {
                return View();
            }
        }

        // GET: Users/Create
        public ActionResult Create(RoleSearch search)
        {
            ViewBag.Roles = _iGetRolesWebCommand.Execute(search);

            return View();
        }

        // POST: Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserDto dto)
        {

            if (!ModelState.IsValid)
            {
                return View(dto);
            }
            try
            {
                _iAddUserCommand.Execute(dto);
               
                return RedirectToAction(nameof(Index));
            }
            catch (EntityAlreadyExistsException)
            {
                TempData["error"] = "User already exist";
                return View();
            }
                
            catch (Exception)
            {
                TempData["error"] = "An error has occured.";
                return View();
            }
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int id)
        {
           
            try
            {
                var user = _iGetUserCommand.Execute(id);

                return View(user);
            }
            catch (EntityNotFoundException)
            {
                TempData["error"] = "User not found";
                return View();
            }
            catch (Exception)
            {

                return RedirectToAction("index");
            }

           
        }



        // POST: Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, UserDto dto)
        {

            try
            {
                _iEditUserCommand.Execute(dto);
                return RedirectToAction(nameof(Index));
            }
            catch(EntityNotFoundException){
                TempData["error"] = "User not found";
                return RedirectToAction(nameof(Index));
            }
            catch (EntityAlreadyExistsException)
            {
                TempData["error"] = "User already exists";
                return View(dto);
            }

        }




        // GET: Users/Edit/5
        public ActionResult Delete(int id)
        {
            try
            {
                var user = _iGetUserCommand.Execute(id);

                return View(user);
            }
            catch (EntityNotFoundException)
            {
                TempData["error"] = "User not found";
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
        public ActionResult Delete(int id,UserDto dto)
        {
            try
            {
                _iDeleteUserCommand.Execute(id);
                return RedirectToAction(nameof(Index));
            }
            catch (EntityNotFoundException)
            {
                TempData["error"] = "User not found";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return RedirectToAction(nameof(Index));
            }
        }
    }
}