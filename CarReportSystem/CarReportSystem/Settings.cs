using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CarReportSystem {
    //設定情報
    public class Settings {
        [XmlIgnore]
        public Color MainFormColor { get; set; }

        [XmlElement("MainFormColorText")]
        public string MainFormColorText {
            get { return ColorTranslator.ToHtml(this.MainFormColor); }
            set { this.MainFormColor = ColorTranslator.FromHtml(value); }
        }
    }
}
