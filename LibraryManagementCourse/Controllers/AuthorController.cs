using LibraryManagementCourse.Data.Interfaces;
using LibraryManagementCourse.Data.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagementCourse.Controllers
{
    public class AuthorController : Controller
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorController(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        [Route("Author")]
        public IActionResult List()
        {
            var authors = _authorRepository.GetAllWithBooks();

            if (authors.Count() == 0) return View("Empty");

            return View(authors);
        }

        public IActionResult Update(int id)
        {
            var author = _authorRepository.GetById(id);

            if (author == null) return NotFound();

            return View(author);
        }

        [HttpPost] 
        public IActionResult Update (Author author)
        {
            if (!ModelState.IsValid)
            {
                return View(author);
            }

            _authorRepository.Update(author);

            return RedirectToAction("List");
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Author author)
        {
            if (!ModelState.IsValid)
            {
                return View(author);
            }

            _authorRepository.Create(author);

            return RedirectToAction("List");
        }

        public IActionResult Delete(int id)
        {
            var author = _authorRepository.GetById(id);

            _authorRepository.Delete(author);

            return RedirectToAction("List");
        }
    }
}
