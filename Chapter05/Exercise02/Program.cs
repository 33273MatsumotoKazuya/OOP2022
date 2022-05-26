using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise02 {
    class Program {
        static void Main(string[] args) {

            Console.Write("数字文字列：");
            var strNum = Console.ReadLine();
            if (int.TryParse(strNum, out var result)) {
                Console.WriteLine("{0:N0}", result);
            } else {
                Console.WriteLine("不適な数字文字列です");
            }
        }
    }
}
