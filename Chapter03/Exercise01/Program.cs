using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise01 {
    class Program {
        static void Main(string[] args) {
            var numbers = new List<int> { 12, 87, 94, 14, 53, 20, 40, 35, 76, 91, 31, 17, 48 };

            Execise1_1(numbers);

            Execise1_2(numbers);
        }

        private static void Execise1_1(List<int> numbers) {
            Console.WriteLine(numbers.Exists(s => s % 8 == 0 || s % 9 == 0));
        }

        private static void Execise1_2(List<int> numbers) {
            numbers.ForEach(s => Console.WriteLine(s / 2.0));
        }
    }
}
