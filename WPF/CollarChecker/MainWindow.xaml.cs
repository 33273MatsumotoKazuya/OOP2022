using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ColorChecker {
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window {

        private List<MyColor> colorList = new List<MyColor>();

        public MainWindow() {
            InitializeComponent();
            DataContext = GetColorList();
        }

        /// <summary>
        /// 色と色名を保持するクラス
        /// </summary>
        public class MyColor {
            public Color Color { get; set; }
            public string Name { get; set; }
        }

        /// <summary>
        /// すべての色を取得するメソッド
        /// </summary>
        /// <returns></returns>
        private MyColor[] GetColorList() {
            return typeof(Colors).GetProperties(BindingFlags.Public | BindingFlags.Static)
                .Select(i => new MyColor() { Color = (Color)i.GetValue(null), Name = i.Name }).ToArray();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            setColor();
            EnableCheck();
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) {
            setColor();
        }

        private void Border_Loaded(object sender, RoutedEventArgs e) { }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            rSlider.Value = ((MyColor)((ComboBox)sender).SelectedItem).Color.R;
            gSlider.Value = ((MyColor)((ComboBox)sender).SelectedItem).Color.G;
            bSlider.Value = ((MyColor)((ComboBox)sender).SelectedItem).Color.B;
            setColor();
        }

        private void setColor() {
            colorArea.Background =
                new SolidColorBrush(Color.FromRgb((byte)rSlider.Value, (byte)gSlider.Value, (byte)bSlider.Value));
        }

        private void stockList_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (stockList.SelectedIndex == -1) return;
            rSlider.Value = colorList[stockList.SelectedIndex].Color.R;
            gSlider.Value = colorList[stockList.SelectedIndex].Color.G;
            bSlider.Value = colorList[stockList.SelectedIndex].Color.B;
            setColor();
        }

        private void Stock_Button_Click(object sender, RoutedEventArgs e) {
            var stColor = getMyColor(byte.Parse(rValue.Text), byte.Parse(gValue.Text), byte.Parse(bValue.Text));

            stockList.Items.Insert(0, stColor.Name ?? "R:" + stColor.Color.R + " G:" + stColor.Color.G + " B:" + stColor.Color.B);
            colorList.Insert(0, stColor);
            EnableCheck();
        }

        private MyColor getMyColor(byte r, byte g, byte b) {
            return new MyColor {
                Color = Color.FromRgb(r, g, b),
                Name = ((IEnumerable<MyColor>)DataContext)
                            .Where(c => c.Color.R == r &&
                                        c.Color.G == g &&
                                        c.Color.B == b)
                            .Select(c => c.Name).FirstOrDefault(),
            };
        }

        private void Delete_Button_Click(object sender, RoutedEventArgs e) {
            if (stockList.SelectedIndex == -1) return;
            colorList.RemoveAt(stockList.SelectedIndex);
            stockList.Items.RemoveAt(stockList.SelectedIndex);
            EnableCheck();
        }

        private void EnableCheck() {
            if (stockList.Items.Count == 0) {
                DeleteButton.IsEnabled = false;
            } else {
                DeleteButton.IsEnabled = true;
            }
        }
    }
}
