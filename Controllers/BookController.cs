using Library.Data;
using Library.Models;
using Library.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Library.Controllers
{
    public class BookController : Controller
    {
        private BookRepository bookRepository;

        public BookController(ApplicationDbContext dbContext)
        {
            this.bookRepository = new BookRepository(dbContext);
        }

        // GET: BookController
        public ActionResult Index()
        {
            var allBooks = bookRepository.GetAllBooks();
            return View(allBooks);
        }

        // GET: BookController/Details/5
        public ActionResult Details(Guid id)
        {
            var model = bookRepository.GetBookByID(id);
            return View(model);
        }

        // GET: BookController/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: BookController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                BookModel model = new BookModel();

                var task = TryUpdateModelAsync(model);
                task.Wait();

                if (task.Result)
                {
                    bookRepository.InsertBook(model);
                }

                return RedirectToAction(nameof(Details), new {id = model.Idbook});
            }
            catch
            {
                return View();
            }
        }

        // GET: BookController/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(Guid id)
        {
            var model = bookRepository.GetBookByID(id);
            return View(model);
        }

        // POST: BookController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(Guid id, IFormCollection collection)
        {
            try
            {
                var model = new BookModel();
                var task = TryUpdateModelAsync(model);
                task.Wait();

                if (task.Result)
                {
                    bookRepository.UpdateBook(model);

                }

                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: BookController/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(Guid id)
        {
            var model = bookRepository.GetBookByID(id);
            return View(model);
        }

        // POST: BookController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(Guid id, IFormCollection collection)
        {
            try
            {
                bookRepository.DeleteBook(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewBag.ErrorMessage = "This book is still being borrowed and cannot be deleted.";
                return View(bookRepository.GetBookByID(id));
            }
        }
    }
}
