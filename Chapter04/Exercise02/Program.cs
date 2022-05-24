using Exercise01;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise02 {
    class Program {
        static void Main(string[] args) {

            //4.2.1
            var ymCollection = new YearMonth[] {
                new YearMonth(2002, 11),
                new YearMonth(2022, 5),
                new YearMonth(1989, 7),
                new YearMonth(1999, 12),
                new YearMonth(2200, 1)
            };

            //4.2.2
            Exercise2_2(ymCollection);
            Console.WriteLine("-------------");

            //4.2.4
            Exercise2_4(ymCollection);
            Console.WriteLine("-------------");

            //4.2.5
            Exercise2_5(ymCollection);
            Console.WriteLine("-------------");

            Exercise2_6(ymCollection);
        }

        //4.2.3
        static YearMonth FindFirst21C(YearMonth[] yms) {
            foreach (var ffc in yms) {
                if (ffc.Is21Century) {
                    return ffc;
                }
            }
            return null;
        }

        private static void Exercise2_2(YearMonth[] ymCollection) {
            foreach (var ym in ymCollection) {
                Console.WriteLine(ym);
            }
        }

        private static void Exercise2_4(YearMonth[] ymCollectoin) {
            var ym = FindFirst21C(ymCollectoin);
            if (ym != null) {
                Console.WriteLine(ym);
            } else {
                Console.WriteLine("21世紀のデータはありません");
            }
        }

        private static void Exercise2_5(YearMonth[] ymCollectoin) {
            foreach (var ym in ymCollectoin.Select(ym => ym.addOneMonth()).ToArray()) {
                Console.WriteLine(ym);
            }
        }

        private static void Exercise2_6(YearMonth[] ymCollection) {
            foreach (var ym in ymCollection.OrderByDescending(ym => ym.Year)) {
                Console.WriteLine(ym);
            }
        }
    }
}
