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
            foreach (var item in text.Split(';')) {
                var array = item.Split('=');
                Console.WriteLine("{0}：{1}", ToJapanese(array[0]), array[1]);
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
