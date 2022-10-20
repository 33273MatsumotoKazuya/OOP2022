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
        private int ans;
        public MainWindow() {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            Button bt = (Button)sender;

            if ((int.Parse((string)bt.Content) == ans)) {
                infoDisp.Text = "正解!";
                bt.Background = Brushes.Red;
            } else {
                infoDisp.Text 
                    = (int.Parse((string)bt.Content)) < ans
                        ? "もっと大きい" : "もっと小さい";
                bt.Background = int.Parse((string)bt.Content) < ans
                                    ? Brushes.Blue : Brushes.Green;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            infoDisp.Text = "ゲームスタート";
            Random rand = new Random();
            ans = rand.Next(25) + 1;
        }
    }
}
