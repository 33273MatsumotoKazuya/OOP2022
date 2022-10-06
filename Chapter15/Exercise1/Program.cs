using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise1 {
    class Program {
        static void Main(string[] args) {
            Exercise1_2();
            Console.WriteLine();
            Exercise1_3();
            Console.WriteLine();
            Exercise1_4();
            Console.WriteLine();
            Exercise1_5();
            Console.WriteLine();
            Exercise1_6();
            Console.WriteLine();
            Exercise1_7();
            Console.WriteLine();
            Exercise1_8();

            Console.ReadLine();
        }

        private static void Exercise1_2() {
            var max = Library.Books
                             .OrderByDescending(b => b.Price)
                             .First();
            Console.WriteLine(max);
        }

        private static void Exercise1_3() {
            var count = Library.Books
                               .GroupBy(b => b.PublishedYear);
            foreach (var book in count) {
                Console.WriteLine($"{book.Key}年 {book.Count()}冊");
            }
        }

        private static void Exercise1_4() {
            var books = Library.Books
                               .OrderByDescending(b => b.PublishedYear)
                               .ThenByDescending(b => b.Price)
                               .Join(Library.Categories,
                                     book => book.CategoryId,
                                     category => category.Id,
                                     (book, category) => new {
                                         PublishedYear = book.PublishedYear,
                                         Price = book.Price,
                                         Title = book.Title,
                                         Category = category.Name
                                     }
                               );
            foreach (var book in books) {
                Console.WriteLine($"{book.PublishedYear}年 {book.Price}円 {book.Title} ({book.Category})");
            }
        }

        private static void Exercise1_5() {
            var names = Library.Books
                               .Where(b => b.PublishedYear == 2016)
                               .Join(Library.Categories,
                                     book => book.CategoryId,
                                     category => category.Id,
                                     (book, category) => category.Name)
                               .Distinct();
            foreach (var name in names) {
                Console.WriteLine(name);
            }
        }

        private static void Exercise1_6() {
            var groups = Library.Books
                                .GroupBy(b => b.CategoryId)
                                .Join(Library.Categories,
                                     book => book.Key,
                                     category => category.Id,
                                     (book, category) => new {
                                         Category = category.Name, 
                                         Books = book 
                                     }
                                ).OrderBy(b => b.Category);
            foreach (var group in groups) {
                Console.WriteLine($"#{group.Category}");
                foreach (var book in group.Books) {
                    Console.WriteLine($" {book.Title}");
                }
            }
        }

        private static void Exercise1_7() {
            var books2 = Library.Books
                                .Where(b => b.CategoryId == 1)
                                .GroupBy(b => b.PublishedYear)
                                .OrderBy(b => b.Key);
            foreach (var book in books2) {
                Console.WriteLine($"#{book.Key}年");
                foreach (var b in book) {
                    Console.WriteLine($" {b.Title}");
                }
            }
        }

        private static void Exercise1_8() {
            var countBook = Library.Categories
                                   .GroupJoin(Library.Books,
                                             c => c.Id,
                                             b => b.CategoryId,
                                             (c, books) => new {
                                                 Category = c.Name,
                                                 Count = books.Count()
                                             }
                                   ).Where(b => b.Count >= 4);
            foreach (var book in countBook) {
                Console.WriteLine(book.Category);
            }
        }
    }
}
