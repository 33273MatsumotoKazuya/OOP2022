using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter15 {
    class Program {
        static void Main(string[] args) {

            IOrderedEnumerable<IGrouping<int, Book>> books = null;
            var years = new List<int>();
            int year;

            Console.WriteLine("出力したい西暦を入力(終了: -1)");
            year = int.Parse(Console.ReadLine());

            while (year != -1) {
                years.Add(year);
                year = int.Parse(Console.ReadLine());
            }

            Console.Write("[昇順:1 降順:2] ->");
            var odb = int.Parse(Console.ReadLine());

            if (odb == 1) {
                books = Library.Books
                           .Where(b => years.Contains(b.PublishedYear))
                           .GroupBy(b => b.PublishedYear)
                           .OrderBy(g => g.Key);
            }
            if (odb == 2) {
                books = Library.Books
                           .Where(b => years.Contains(b.PublishedYear))
                           .GroupBy(b => b.PublishedYear)
                           .OrderByDescending(g => g.Key);
            }

            foreach (var book in books) {
                Console.WriteLine($"{book.Key}年");
                foreach (var b in book) {
                    var category = Library.Categories.Where(c => c.Id == b.CategoryId).First();
                    Console.WriteLine($"  タイトル:{b.Title} 価格{b.Price} カテゴリ:{category.Name}");
                }
            }
        }
    }
}
