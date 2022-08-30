using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistanceConverter {
    static class ConverterFactory {
        //あらかじめインスタンスを生成し、配列に入れておく
        private static ConverterBase[] _converters = new ConverterBase[] {
            new MeterConvarter(),
            new FeetConvarter(),
            new YardConvarter(),
            new InchConvarter(),
            new KiloMeterConvarter(),
            new MileConvarter(),
        };

        public static ConverterBase GetInstance(string name) {
            return _converters.FirstOrDefault(x => x.IsMyUnit(name));
        }
    }
}
