using LibraryManagementCourse.Data.Interfaces;
using LibraryManagementCourse.Data.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagementCourse.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;

        public BookController(IBookRepository bookRepository, IAuthorRepository authorRepository)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
        }

        public IActionResult List(int? authorId, int? borrowerId)
        {
            if(authorId == null && borrowerId == null)
            {
                // show all books
                var books = _bookRepository.GetAllWithAuthor();
                // check books
                return CheckBooks(books);
            }
            else if (authorId != null)
            {
                // filter by author id
                var author = _authorRepository
                    .GetWithBooks((int)authorId);

                // check author books
                if (author.Books.Count() == 0)
                {
                    return View("AuthorEmpty", author);
                }
                else
                {
                    return View(author.Books);
                }
            }
            else if (borrowerId != null)
            {
                // filter by borrower id
                var books = _bookRepository
                    .FindWithAuthorAndBorrower(book => book.BorrowerId == borrowerId);
                // check borrower books
                return CheckBooks(books);
            }
            else
            {
                // throw exception
                throw new ArgumentException();
            }
        }
        public IActionResult CheckBooks(IEnumerable<Book> books)
        {
            if (books.Count() == 0)
            {
                return View("Empty");
            }
            else
            {
                return View(books);
            }
        }
    }
}
