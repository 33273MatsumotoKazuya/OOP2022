using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarReportSystem {
    public partial class Form1 : Form {

        //レポートデータ管理用リスト
        BindingList<CarReport> listCarReport = new BindingList<CarReport>();

        public Form1() {
            InitializeComponent();
            dgvCarReports.DataSource = listCarReport;
        }

        //終了ボタン
        private void btSystemExit_Click(object sender, EventArgs e) {
            //アプリケーションを終了
            Application.Exit();
        }

        //画像：開くボタン
        private void btPictureOpen_Click(object sender, EventArgs e) {
            if (ofbFileOpenDialog.ShowDialog() == DialogResult.OK) {
                pbPicture.Image = Image.FromFile(ofbFileOpenDialog.FileName);
            }
            btPictureDelete.Enabled = true;
        }

        //画像：削除ボタン
        private void btPictureDelete_Click(object sender, EventArgs e) {
            pbPicture.Image = null;
            btPictureDelete.Enabled = false;
        }

        //追加ボタン
        private void btAddReport_Click(object sender, EventArgs e) {
            if (string.IsNullOrEmpty(cbAuther.Text)) {
                MessageBox.Show("記録者が選択されていません");
                return;
            }

            CarReport newCarReport = new CarReport {
                Date = dateTimePicker.Value,
                Auther = cbAuther.Text,
                Maker = GetCarMaker(),
                CarName = cbCarName.Text,
                Report = tbReport.Text,
                Picture = pbPicture.Image,
            };

            listCarReport.Add(newCarReport);
        }

        //修正ボタン
        private void btReportCorrect_Click(object sender, EventArgs e) {
            listCarReport[dgvCarReports.CurrentRow.Index].Date = dateTimePicker.Value;
            listCarReport[dgvCarReports.CurrentRow.Index].Auther = cbAuther.Text;
            listCarReport[dgvCarReports.CurrentRow.Index].Maker = GetCarMaker();
            listCarReport[dgvCarReports.CurrentRow.Index].CarName = cbCarName.Text;
            listCarReport[dgvCarReports.CurrentRow.Index].Report = tbReport.Text;
            listCarReport[dgvCarReports.CurrentRow.Index].Picture = pbPicture.Image;
            dgvCarReports.Refresh();
        }

        private CarReport.MakerGroup GetCarMaker() {
            if (rbToyota.Checked) {
                return CarReport.MakerGroup.トヨタ;
            }
            if (rbNissan.Checked) {
                return CarReport.MakerGroup.日産;
            }
            if (rbHonda.Checked) {
                return CarReport.MakerGroup.ホンダ;
            }
            if (rbSubaru.Checked) {
                return CarReport.MakerGroup.スバル;
            }
            if (rbForeignCar.Checked) {
                return CarReport.MakerGroup.外国車;
            } else {
                return CarReport.MakerGroup.その他;
            }
        }
    }
}
