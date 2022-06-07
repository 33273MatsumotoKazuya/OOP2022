using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sample0603 {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        //ボタンクリックイベントハンドラ
        private void btPush_Click(object sender, EventArgs e) {
            var Answer = int.Parse(tbNum1.Text) + int.Parse(tbNum2.Text);
            tbAns.Text = Answer.ToString();
            nudAns.Value = nudNum1.Value + nudNum2.Value;
        }
    }
}
