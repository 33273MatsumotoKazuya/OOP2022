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
            if (int.TryParse(strNum, out var num)) {
                Console.WriteLine("{0:#.#}", num);
            } else {
                Console.WriteLine("不適な数字文字列です");
            }
        }
    }
}
