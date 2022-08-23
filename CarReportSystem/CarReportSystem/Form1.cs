using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace CarReportSystem {
    public partial class Form1 : Form {

        //設定保存用オブジェクト
        Settings settings = new Settings();

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
            setCheckBox(newCarReport);
            EnabledCheck();
        }

        private void setCheckBox(CarReport newCarReport) {
            if (!cbAuther.Items.Contains(cbAuther.Text)) {
                cbAuther.Items.Add(cbAuther.Text);
            }
            if (!cbCarName.Items.Contains(cbCarName.Text)) {
                cbCarName.Items.Add(cbCarName.Text);
            }
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

        private void rbChecked(int index) {
            switch (listCarReport[index].Maker) {
                case CarReport.MakerGroup.トヨタ:
                    rbToyota.Checked = true;
                    break;
                case CarReport.MakerGroup.日産:
                    rbNissan.Checked = true;
                    break;
                case CarReport.MakerGroup.ホンダ:
                    rbHonda.Checked = true;
                    break;
                case CarReport.MakerGroup.スバル:
                    rbSubaru.Checked = true;
                    break;
                case CarReport.MakerGroup.外国車:
                    rbForeignCar.Checked = true;
                    break;
                case CarReport.MakerGroup.その他:
                    rbOther.Checked = true;
                    break;
                default:
                    break;
            }
        }

        //削除ボタン
        private void btReportDelete_Click(object sender, EventArgs e) {
            DialogResult result = MessageBox.Show("本当に削除してよいですか？",
               "確認",
               MessageBoxButtons.OKCancel,
               MessageBoxIcon.Exclamation);

            if (result == DialogResult.OK) {
                dgvCarReports.Rows.RemoveAt(dgvCarReports.CurrentRow.Index);
                dgvCarReports.Refresh();
            }

            EnabledCheck();
        }

        private void dgvCarReports_Click(object sender, EventArgs e) {
            if (dgvCarReports.CurrentRow == null) return;

            var index = dgvCarReports.CurrentRow.Index;

            dateTimePicker.Value = listCarReport[index].Date.Year > 1900 ? listCarReport[index].Date : DateTime.Today;
            cbAuther.Text = listCarReport[index].Auther;
            rbChecked(index);
            cbCarName.Text = listCarReport[index].CarName;
            tbReport.Text = listCarReport[index].Report;
            pbPicture.Image = listCarReport[index].Picture;
        }

        private void btSave_Click(object sender, EventArgs e) {
            if (sfdSaveDialog.ShowDialog() == DialogResult.OK) {
                try {
                    //バイナリ形式でシリアル化
                    var bf = new BinaryFormatter();

                    using (FileStream fs = File.Open(sfdSaveDialog.FileName, FileMode.Create)) {
                        bf.Serialize(fs, listCarReport);
                    }
                }
                catch (Exception ex) {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btOpen_Click(object sender, EventArgs e) {
            if (ofbFileOpenDialog.ShowDialog() == DialogResult.OK) {
                try {
                    //バイナリ形式で逆シリアル化
                    var bf = new BinaryFormatter();

                    using (FileStream fs = File.Open(ofbFileOpenDialog.FileName, FileMode.Open, FileAccess.Read)) {
                        //逆シリアルして読み込む
                        listCarReport = (BindingList<CarReport>)bf.Deserialize(fs);
                        dgvCarReports.DataSource = null;
                        dgvCarReports.DataSource = listCarReport;
                    }
                }
                catch (Exception ex) {
                    MessageBox.Show(ex.Message);
                }

                cbAuther.Items.Clear();
                cbCarName.Items.Clear();

                foreach (var item in listCarReport) {
                    setCheckBox(item);
                }

                EnabledCheck();
            }
        }

        private void EnabledCheck() {
            if (listCarReport.Count() > 0) {
                //マスク解除
                btReportDelete.Enabled = true;
                btReportCorrect.Enabled = true;
            } else {
                //マスク設定
                btReportDelete.Enabled = false;
                btReportCorrect.Enabled = false;
            }
        }

        private void 色設定ToolStripMenuItem_Click(object sender, EventArgs e) {
            if (cdColorSelect.ShowDialog() == DialogResult.OK) {
                BackColor = cdColorSelect.Color;
                settings.MainFormColor = cdColorSelect.Color;
            }  
        }

        private void btChangeSizeMode_Click(object sender, EventArgs e) {
            if (pbPicture.SizeMode == PictureBoxSizeMode.StretchImage) {
                pbPicture.SizeMode = PictureBoxSizeMode.AutoSize;
                return;
            }
            if (pbPicture.SizeMode == PictureBoxSizeMode.AutoSize) {
                pbPicture.SizeMode = PictureBoxSizeMode.CenterImage;
                return;
            }
            if (pbPicture.SizeMode == PictureBoxSizeMode.CenterImage) {
                pbPicture.SizeMode = PictureBoxSizeMode.Zoom;
                return;
            }
            if (pbPicture.SizeMode == PictureBoxSizeMode.Zoom) {
                pbPicture.SizeMode = PictureBoxSizeMode.StretchImage;
                return;
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e) {
            //設定ファイルをシリアル化
            using (var writer = XmlWriter.Create("settings.xml")) {
                var serializer = new XmlSerializer(settings.GetType());
                serializer.Serialize(writer, settings);
            }
        }

        private void Form1_Load(object sender, EventArgs e) {
            EnabledCheck();
            using (var reader = XmlReader.Create("settings.xml")) {
                var serializer = new XmlSerializer(typeof(Settings));
                settings = serializer.Deserialize(reader) as Settings;
                BackColor = settings.MainFormColor;
            }
        }
    }
}
