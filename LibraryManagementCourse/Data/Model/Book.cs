using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagementCourse.Data.Model
{
    public class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; }

        public virtual Author Author { get; set; }
        public int AuthorId { get; set; }

        public virtual Customer Borrower { get; set; }
        public int BorrowerId { get; set; }
    }
}
