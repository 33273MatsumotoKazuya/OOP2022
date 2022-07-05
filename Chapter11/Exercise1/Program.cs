using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.IO;

namespace Exercise1 {
    class Program {
        static void Main(string[] args) {
            var file = "sample.xml";
            Exercise1_1(file);
            Console.WriteLine();
            Exercise1_2(file);
            Console.WriteLine();
            Exercise1_3(file);
            Console.WriteLine();

            var newfile = "sports.xml";
            Exercise1_4(file, newfile);

            var text = File.ReadAllText(newfile);
            Console.WriteLine(text);
        }

        private static void Exercise1_1(string file) {
            var xdoc = XDocument.Load(file);
            var xelements = xdoc.Root.Elements()
                                     .Select(x => new {
                                     Name = (string)x.Element("name"),
                                     TeamMember = (int)x.Element("teammembers")
                                     });

            foreach (var item in xelements) {
                Console.WriteLine("{0} {1}", item.Name, item.TeamMember);
            }
        }

        private static void Exercise1_2(string file) {
            var xdoc = XDocument.Load(file);
            var xelements = xdoc.Root.Elements()
                                     .OrderBy(x => (int)x.Element("firstplayed"))
                                     .Select(x => new {
                                         Name = (string)x.Element("name").Attribute("kanji"),
                                         FirstPlayed = (int)x.Element("firstplayed")
                                     });

            foreach (var item in xelements) {
                Console.WriteLine("{0}({1})", item.Name, item.FirstPlayed);
            }
        }

        private static void Exercise1_3(string file) {
            var xdoc = XDocument.Load(file);
            var sport = xdoc.Root.Elements()
                                 .Select(x => new {
                                     Name = x.Element("name").Value,
                                     Teammembers = x.Element("teammembers").Value
                                 })
                                 .OrderByDescending(x => int.Parse(x.Teammembers)).FirstOrDefault();
            Console.WriteLine("{0}({1}人)", sport.Name, sport.Teammembers);
        }

        private static void Exercise1_4(string file, string newfile) {
            var element = new XElement("ballsport",
                            new XElement("name", "サッカー", new XAttribute("kanji", "蹴球")),
                            new XElement("teammembers", "11"),
                            new XElement("firstplayed", "1863")
                            );
            var xdoc = XDocument.Load(file);
            xdoc.Root.Add(element);
            xdoc.Save(newfile);
        }
    }
}
