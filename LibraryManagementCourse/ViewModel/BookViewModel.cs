using LibraryManagementCourse.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagementCourse.ViewModel
{
    public class BookViewModel
    {
        public Book Book { get; set; }
        public IEnumerable<Author> Authors { get; set; }
    }
}
