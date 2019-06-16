using Aplication.Commands.CategoryCommand;
using Aplication.Dto;
using Aplication.Exceptions;
using Aplication.Search;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Web.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly IGetCategoriesWebCommand _iGetCategoriesWebCommand;
        private readonly IAddCategoryCommand _iAddCategoryCommand;
        private readonly IGetCategoryCommand _iGetCategoryCommand;
        private readonly IEditCategoryCommand _iEditCategoryCommand;
        private readonly IDeleteCategoryCommand _iDeleteCategoryCommand;

        public CategoriesController(IGetCategoriesWebCommand iGetCategoriesWebCommand, IAddCategoryCommand iAddCategoryCommand, IGetCategoryCommand iGetCategoryCommand, IEditCategoryCommand iEditCategoryCommand, IDeleteCategoryCommand iDeleteCategoryCommand)
        {
            _iGetCategoriesWebCommand = iGetCategoriesWebCommand;
            _iAddCategoryCommand = iAddCategoryCommand;
            _iGetCategoryCommand = iGetCategoryCommand;
            _iEditCategoryCommand = iEditCategoryCommand;
            _iDeleteCategoryCommand = iDeleteCategoryCommand;
        }









        // GET: Categories
        public ActionResult Index(CategorySearch search)
        {
            var categories = _iGetCategoriesWebCommand.Execute(search);
           

            return View(categories);
        }




        // GET: Categories/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                var category = _iGetCategoryCommand.Execute(id);
                return View(category);
            }
            catch (EntityNotFoundException)
            {
                TempData["error"] = "Category not found";
                return View();
            }
            catch (Exception)
            {

                TempData["error"] = "An error has occured";
                return View();
            }

        }

        // GET: Categories/Create
        public ActionResult Create()
        {
            return View();
        }




        // POST: Categories/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CategoryDto dto)
        {

            if (!ModelState.IsValid)
            {
                return View(dto);
            }
            try
            {
                _iAddCategoryCommand.Execute(dto);

                return RedirectToAction(nameof(Index));
            }
            catch (EntityAlreadyExistsException)
            {
                TempData["error"] = "Category already exists";
                return View();
            }
            catch (Exception)
            {
                TempData["error"] = "An error has occured.";
                return View();
                 
            }
           
        }

        // GET: Categories/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                var dto = _iGetCategoryCommand.Execute(id);
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

        // POST: Categories/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CategoryDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            try
            {
                _iEditCategoryCommand.Execute(dto);
                return RedirectToAction(nameof(Index));
            }
            catch (EntityNotFoundException)
            {
                TempData["error"] = "Category not found";
                return RedirectToAction(nameof(Index));
            }
            catch (EntityAlreadyExistsException)
            {
                TempData["error"] = "Category already exists";
                return View(dto);
            }
        }



        // GET: categories/Edit/5
        public ActionResult Delete(int id)
        {
            try
            {
                var category = _iGetCategoryCommand.Execute(id);

                return View(category);
            }
            catch (EntityNotFoundException)
            {
                TempData["error"] = "cAtegory not found";
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
                _iDeleteCategoryCommand.Execute(id);
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