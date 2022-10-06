using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter15 {
    class Program {
        static void Main(string[] args) {

            var years = new List<int>();
            int year;

            Console.WriteLine("出力したい西暦を入力（終了：-1）");
            year = int.Parse(Console.ReadLine());
            while (year != -1) {
                years.Add(year);
                year = int.Parse(Console.ReadLine());
            }

            //int sort;

            Console.WriteLine();
            //Console.Write("[昇順:1 降順:2] -> ");
            //sort = int.Parse(Console.ReadLine());

            //IEnumerable<Book> books;
            //if (sort == 1) {
            //    books = Library.Books
            //        .Where(b => years.Contains(b.PublishedYear))
            //        .OrderBy(b => b.PublishedYear);
            //} else {
            //    books = Library.Books
            //        .Where(b => years.Contains(b.PublishedYear))
            //        .OrderByDescending(b => b.PublishedYear);
            //}

            //foreach (var book in books) {
            //    Console.WriteLine(book);
            //}

            //Console.WriteLine();

            var books2 = Library.Books
                .Where(b => years.Contains(b.PublishedYear))
                .Join(Library.Categories,
                      book => book.CategoryId,
                      category => category.Id,
                      (book, category) => new {
                          Title = book.Title,
                          Category = category.Name,
                          PublishedYear = book.PublishedYear,
                          Price = book.Price
                      }
                ).ToList();

            foreach (var book in books2.OrderByDescending(b => b.PublishedYear).ThenBy(b => b.Category)) {
                Console.WriteLine($"{book.PublishedYear}年,{book.Title},{book.Category},{book.Price}円");
            }
            Console.WriteLine($"金額の合計 {books2.Sum(b => b.Price)}円");
        }
    }
}
