using LibraryManagement.Data.Interfaces;
using LibraryManagement.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.Controllers
{
    public class LendController : Controller
    {
        private readonly IBookRepository _bookRepository;
        private readonly ICustomerRepository _customerRepository;

        public LendController(IBookRepository bookRepository, ICustomerRepository customerRepository)
        {
            _bookRepository = bookRepository;
            _customerRepository = customerRepository;
        }

        [Route("Lend")]
        public IActionResult List()
        {
            // load all available books
            var availableBooks = _bookRepository.FindWithAuthor(x => x.BorrowerId == 0);
            // check collection
            if (availableBooks.Count() == 0)
            {
                return View("Empty");
            }
            else
            {
                return View(availableBooks);
            }
        }

        public IActionResult LendBook(int bookId)
        {
            // load current book and all customers
            var lendVM = new LendViewModel()
            {
                Book = _bookRepository.GetById(bookId),
                Customers = _customerRepository.GetAll()
            };
            // Send data to the Lend view
            return View(lendVM);
        }

        [HttpPost]
        public IActionResult LendBook(LendViewModel lendViewModel)
        {
            // update the database 
            var book = _bookRepository.GetById(lendViewModel.Book.BookId);

            var customer = _customerRepository.GetById(lendViewModel.Book.BorrowerId);

            book.Borrower = customer;

            _bookRepository.Update(book);

            // redirect to the list view
            return RedirectToAction("List");
        }
    }
}
