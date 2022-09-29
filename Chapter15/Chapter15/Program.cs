using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter15 {
    class Program {
        static void Main(string[] args) {
            var query = Library.Books
                .OrderBy(b => b.CategoryId)
                .ThenByDescending(b => b.PublishedYear);

            foreach (var n in query) {
                Console.WriteLine(n);
            }
        }
    }
}
