using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise04 {
    class Program {
        static void Main(string[] args) {
            var text = "Novelist=谷崎潤一郎;BestWork=春琴抄;Born=1886";

            Exercise4(text);
        }

        private static void Exercise4(string text) {
            var array = text.Split(';');
            foreach (var word in array) {
                var str1 = ToJapanese(word.Substring(0, word.IndexOf('=')));
                var str2 = word.Substring(word.IndexOf("="));
                var str3 = str2.Remove(0, 1);
                Console.WriteLine(str1 + "：" + str3);
            }
        }

        static string ToJapanese(string key) {
            switch (key) {
                case "Novelist":
                    return "作家　";
                case "BestWork":
                    return "代表作";
                case "Born":
                    return "誕生年";
            }
            throw new ArgumentException("引数keyは、正しい値ではありません");
        }
    }
}
