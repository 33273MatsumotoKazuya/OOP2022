using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistanceConverter {

    public class Program {

        static void Main(string[] args) {
            if (args.Length >= 1 && args[0] == "-tom") {
                //フィートからメートルへの対応表を出力
                PrintMeterToFeet(1, 10);
            }
            else {
                //メートルからフィートへの対応表を出力
                PrintFeetToMeter(1, 10);
            }
        }

        //フィートからメートルへの対応表を出力
        static void PrintFeetToMeter(int start, int end) {
            for (int feet = start; feet <= end; feet++) {
                double meter = FeetConverter.FromMeter(feet);
                Console.WriteLine("{0}ft = {1:0.0000}m", feet, meter);
            }
        }

        //メートルからフィートへの対応表を出力
        static void PrintMeterToFeet(int start, int stop) {
            for (int meter = start; meter <= stop; meter++) {
                double feet = FeetConverter.Tometer(meter);
                Console.WriteLine("{0}m = {1:0.0000}ft", meter, feet);
            }
        }
    }
}
