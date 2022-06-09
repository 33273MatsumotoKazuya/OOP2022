using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NumberGame {
    public partial class Form1 : Form {

        private Random rand = new Random();
        private int randomNumber;

        public Form1() {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) {
            getRandom();
        }

        private void btInput_Click(object sender, EventArgs e) {
            if (nudNum.Value == randomNumber) {
                tbAns.Text = "正解！";
            } else if (nudNum.Value > randomNumber) {
                tbAns.Text = $"{nudNum.Value}より小さい値です";
            } else if (nudNum.Value < randomNumber) {
                tbAns.Text = $"{nudNum.Value}より大きい値です";
            } 
        }

        private void nudMax_ValueChanged(object sender, EventArgs e) {
            getRandom();
        }

        private void getRandom() {
            randomNumber = rand.Next(1, (int)nudMax.Value + 1);
        }
    }
}
