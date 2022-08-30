using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextFileProcessor;
using Microsoft.VisualBasic;

namespace Exercise1 {
    class ToHankakuProcessor : TextProcessor {
        protected override void Execute(string line) {
            Console.WriteLine(Strings.StrConv(line, VbStrConv.Narrow));
        }
    }
}
