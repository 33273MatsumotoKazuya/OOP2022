using System;
using System.Linq;
using System.Text;

namespace Exercise03 {
    class Program {
        static void Main(string[] args) {
            var text = "Jackdaws love my big sphinx of quartz";

            Exercise3_1(text);
            Console.WriteLine("-----");

            Exercise3_2(text);
            Console.WriteLine("-----");

            Exercise3_3(text);
            Console.WriteLine("-----");

            Exercise3_4(text);
            Console.WriteLine("-----");

            Exercise3_5(text);
        }

        private static void Exercise3_1(string text) {
            var space = text.Count(s => s == ' ');
            Console.WriteLine("空白数：{0}", space);
        }

        private static void Exercise3_2(string text) {
            var replaced = text.Replace("big", "small");
            Console.WriteLine(replaced);
        }

        private static void Exercise3_3(string text) {
            var count = text.Split(' ').Length;
            Console.WriteLine("単語数：{0}", count);
        }

        private static void Exercise3_4(string text) {
            text.Split(' ').Where(word => word.Length <= 4)
                .ToList().ForEach(word => Console.WriteLine(word));
        }

        private static void Exercise3_5(string text) {
            var words = text.Split(' ');
            if (words.Length > 0) {
                var sb = new StringBuilder(words[0]);
                foreach (var word in words.Skip(1)) {
                    sb.Append(" ");
                    sb.Append(word);
                }
                var str = sb.ToString();
                Console.WriteLine(str);
            }
        }
    }
}
