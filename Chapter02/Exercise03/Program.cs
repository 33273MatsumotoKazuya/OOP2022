using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise03 {
    class Program {
        static void Main(string[] args) {
            int judge = 0;
            var sales = new SalesCounter("sales.csv");

            while (true) {
                Console.WriteLine("売上高の表示選択（終了は999）");
                Console.WriteLine("1.店舗別");
                Console.WriteLine("2.商品カテゴリー別");
                Console.Write(">");
                judge = int.Parse(Console.ReadLine());

                if (judge == 1) {
                    outPut(sales.GetPerStoreSales());
                }
                else if (judge == 2) {
                    outPut(sales.GetPerProductSales());
                }
                else if (judge == 999) {
                    break;
                }
            }
        }

        private static void outPut(IDictionary<string, int> amountPerStore) {
            foreach (var obj in amountPerStore) {
                Console.WriteLine("{0}{1}", obj.Key, obj.Value);
            }
        }
    }
}
