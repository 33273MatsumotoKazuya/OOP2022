﻿using SampleEntityFramework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleEntityFramework {
    class Program {
        static void Main(string[] args) {
            //InsertBooks();

            //13-1
            //AddAuthors();
            //AddBooks();

            //13-2
            //var books = GetAllBooks();
            //foreach (var book in books) {
            //    Console.WriteLine($"タイトル:{book.Title} 発行年:{book.PublishedYear}年 著者:{book.Author.Name}");
            //}

            //13-3
            //var books = Exercise13_3();
            //foreach (var book in books) {
            //    Console.WriteLine($"タイトル:{book.Title}");
            //}

            //13-4
            //var books = Exercise13_4();
            //foreach (var book in books) {
            //    Console.WriteLine($"タイトル:{book.Title} 著者:{book.Author.Name}");
            //}

            //13-5
            var authers = Exercise13_5_1();
            foreach (var auther in authers) {
                Console.WriteLine($"{auther.Name} {auther.Birthday.ToString("yyyy/MM/dd")}");

                var books = Exercise13_5_2(auther.Name);
                foreach (var book in books) {
                    Console.WriteLine($"  {book.Title} {book.PublishedYear}");
                }
                Console.WriteLine();
            }
        }

        // List 13-5
        static void InsertBooks() {
            using (var db = new BooksDbContext()) {
                var book1 = new Book {
                    Title = "坊ちゃん",
                    PublishedYear = 2003,
                    Author = new Author {
                        Birthday = new DateTime(1867, 2, 9),
                        Gender = "M",
                        Name = "夏目漱石",
                    }
                };

                db.Books.Add(book1);

                var book2 = new Book {
                    Title = "人間失格",
                    PublishedYear = 1990,
                    Author = new Author {
                        Birthday = new DateTime(1909, 6, 19),
                        Gender = "M",
                        Name = "太宰治",
                    }
                };

                db.Books.Add(book2);
                db.SaveChanges(); //データベースの更新
            }
        }

        // List 13-9
        private static void AddAuthors() {
            using (var db = new BooksDbContext()) {
                var author1 = new Author {
                    Birthday = new DateTime(1888, 12, 26),
                    Gender = "M",
                    Name = "菊池寛"
                };

                db.Authors.Add(author1);

                var author2 = new Author {
                    Birthday = new DateTime(1899, 6, 14),
                    Gender = "M",
                    Name = "川端康成"
                };

                db.Authors.Add(author2);
                db.SaveChanges();
            }
    }

        // List 13-10
        private static void AddBooks() {
            using (var db = new BooksDbContext()) {
                var author1 = db.Authors.Single(a => a.Name == "夏目漱石");
                var book1 = new Book {
                    Title = "こころ",
                    PublishedYear = 1991,
                    Author = author1,
                };
                db.Books.Add(book1);
                var author2 = db.Authors.Single(a => a.Name == "川端康成");
                var book2 = new Book {
                    Title = "伊豆の踊子",
                    PublishedYear = 2003,
                    Author = author2,
                };
                db.Books.Add(book2);
                var author3 = db.Authors.Single(a => a.Name == "菊池寛");
                var book3 = new Book {
                    Title = "真珠夫人",
                    PublishedYear = 2002,
                    Author = author3,
                };
                db.Books.Add(book3);
                var author4 = db.Authors.Single(a => a.Name == "宮沢賢治");
                var book4 = new Book {
                    Title = "注文の多い料理店",
                    PublishedYear = 2000,
                    Author = author4,
                };
                db.Books.Add(book4);
                db.SaveChanges();
            }
        }

        static IEnumerable<Book> GetAllBooks() {
            using (var db = new BooksDbContext()) {
                return db.Books.Include(nameof(Author)).ToList();
            }
        }

        static IEnumerable<Book> Exercise13_3() {
            using (var db = new BooksDbContext()) {
                return db.Books
                    .Where(len => len.Title.Length == db.Books.Max(b => b.Title.Length))
                    .ToList();
            }
        }

        static IEnumerable<Book> Exercise13_4() {
            using (var db = new BooksDbContext()) {
                return db.Books
                    .Include(nameof(Author))
                    .OrderBy(b => b.PublishedYear).Take(3)
                    .ToList();
            }
        }

        static IEnumerable<Author> Exercise13_5_1() {
            using (var db = new BooksDbContext()) {
                return db.Authors
                    .OrderByDescending(a => a.Birthday)
                    .ToList();
            }
        }

        static IEnumerable<Book> Exercise13_5_2(string name) {
            using (var db = new BooksDbContext()) {
                return db.Books
                    .Where(b => b.Author.Name == name)
                    .ToList();
            }
        }
    }
}
