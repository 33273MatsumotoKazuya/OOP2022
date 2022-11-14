using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WeatherApp {
    public partial class Form1 : Form {

        Dictionary<string, string> dic = new Dictionary<string, string>();

        public Form1() {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) {
            StreamReader sr = new StreamReader(@"areacode.csv", Encoding.GetEncoding("Shift_JIS"));
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    string[] values = line.Split(',');
                    dic.Add($"{values[0]}", $"{values[1]}");
                }
            }

        }

        private void areaTreeView_AfterSelect(object sender, TreeViewEventArgs e) {
            var value = dic.FirstOrDefault(x => x.Key.Equals(areaTreeView.SelectedNode.Text)).Value;
            if (value == null) return;

            var wc = new WebClient()
            {
                Encoding = Encoding.UTF8
            };

            var dString = wc.DownloadString($"https://www.jma.go.jp/bosai/forecast/data/overview_forecast/{value}.json");
            var json = JsonConvert.DeserializeObject<Rootobject>(dString);
            tbWeatherInfo.Text = json.text;
        }
    }
}
