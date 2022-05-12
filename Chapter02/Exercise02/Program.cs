using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise02 {

    public class Program {

        static void Main(string[] args) {
            PrintInchToMeter(1, 10);
        }

        //インチからメートルへの対応表を出力
        static void PrintInchToMeter(int start, int end) {
            for (var inch = start; inch <= end; inch++) {
                var meter = InchConverter.Tometer(inch);
                Console.WriteLine("{0}in = {1:0.0000}m", inch, meter);
            }
        }
    }
}
