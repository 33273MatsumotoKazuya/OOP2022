using System;
using System.Collections.Generic;
using System.Linq;
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

namespace NumberGame {
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window {
        int result;
        public MainWindow() {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            Button bt = (Button)sender;

            if ((int.Parse(bt.Content.ToString()) == result)) {
                infoDisp.Text = "正解!";
                bt.Background = Brushes.Red;
            } else {
                infoDisp.Text = "不正解";
                bt.Background = Brushes.Blue;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            Random rand = new Random();
            result = rand.Next(1, 25);
        }
    }
}
