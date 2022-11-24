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
                while (!sr.EndOfStream) {
                    string line = sr.ReadLine();
                    string[] values = line.Split(',');
                    dic.Add($"{values[0]}", $"{values[1]}");
                }
            }
        }

        private void areaTreeView_AfterSelect(object sender, TreeViewEventArgs e) {
            var value = dic.FirstOrDefault(x => x.Key.Equals(areaTreeView.SelectedNode.Text)).Value;
            if (value == null) return;

            var wc = new WebClient() { Encoding = Encoding.UTF8 };

            var wString = wc.DownloadString($"https://www.jma.go.jp/bosai/forecast/data/forecast/{value}.json");
            var wString_3days = wc.DownloadString($"https://www.jma.go.jp/bosai/forecast/data/overview_forecast/{value}.json");

            var json_weather = JsonConvert.DeserializeObject<Class1[]>(wString);
            var json_weather_3days = JsonConvert.DeserializeObject<Rootobject_3days>(wString_3days);

            var weatherCodeToday = json_weather[0].timeSeries[0].areas[0].weatherCodes[0];
            var weatherCodeTomorrow = json_weather[0].timeSeries[0].areas[0].weatherCodes[1];
            var weatherCodeDayAfterTomorrow = json_weather[0].timeSeries[0].areas[0].weatherCodes[2];

            tbWeather.Text = json_weather_3days.text;
            tbToday.Text = json_weather[0].timeSeries[0].areas[0].weathers[0];
            tbTomorrow.Text = json_weather[0].timeSeries[0].areas[0].weathers[1];
            tbDayAfterTomorrow.Text = json_weather[0].timeSeries[0].areas[0].weathers[2];
            pbToday.ImageLocation = $"https://www.jma.go.jp/bosai/forecast/img/{weatherCodeToday}.png";
            pbTomorrow.ImageLocation = $"https://www.jma.go.jp/bosai/forecast/img/{weatherCodeTomorrow}.png";
            pbDayAfterTomorrow.ImageLocation = $"https://www.jma.go.jp/bosai/forecast/img/{weatherCodeDayAfterTomorrow}.png";
            TempMin1.Text = json_weather[1].timeSeries[1].areas[0].tempsMin[0];
            TempMax1.Text = json_weather[1].timeSeries[1].areas[0].tempsMax[0];
            TempMin2.Text = json_weather[1].timeSeries[1].areas[0].tempsMin[1];
            TempMax2.Text = json_weather[1].timeSeries[1].areas[0].tempsMax[1];
            TempMin3.Text = json_weather[1].timeSeries[1].areas[0].tempsMin[2];
            TempMax3.Text = json_weather[1].timeSeries[1].areas[0].tempsMax[2];
        }
    }
}
