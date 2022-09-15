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
        Settings settings = Settings.getInstance();

        public Form1() {
            InitializeComponent();
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
            DataRow newRow = infosys202224DataSet.CarReportDB.NewRow();
            newRow[1] = dateTimePicker.Value;
            newRow[2] = cbAuther.Text;
            newRow[3] = GetCarMaker();
            newRow[4] = cbCarName.Text;
            newRow[5] = tbReport.Text;
            if (pbPicture.Image != null) {
                newRow[6] = ImageToByteArray(pbPicture.Image);
            }

            //データセットへ新しいレコードを追加
            infosys202224DataSet.CarReportDB.Rows.Add(newRow);
            //データベース更新
            this.carReportDBTableAdapter.Update(this.infosys202224DataSet.CarReportDB);
            setCheckBox();
            EnabledCheck();
        }

        private void setCheckBox() {
            if (!cbAuther.Items.Contains(cbAuther.Text)) {
                cbAuther.Items.Add(cbAuther.Text);
            }
            if (!cbCarName.Items.Contains(cbCarName.Text)) {
                cbCarName.Items.Add(cbCarName.Text);
            }
        }

        //修正ボタン
        private void btReportCorrect_Click(object sender, EventArgs e) {
            carReportDBDataGridView.CurrentRow.Cells[1].Value = dateTimePicker.Value;
            carReportDBDataGridView.CurrentRow.Cells[2].Value = cbAuther.Text;
            carReportDBDataGridView.CurrentRow.Cells[3].Value = GetCarMaker();
            carReportDBDataGridView.CurrentRow.Cells[4].Value = cbCarName.Text;
            carReportDBDataGridView.CurrentRow.Cells[5].Value = tbReport.Text;
            carReportDBDataGridView.CurrentRow.Cells[6].Value = ImageToByteArray(pbPicture.Image);

            this.Validate();
            this.carReportDBBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.infosys202224DataSet);
        }

        private string GetCarMaker() {
            if (rbToyota.Checked) {
                return "トヨタ";
            }
            if (rbNissan.Checked) {
                return "日産";
            }
            if (rbHonda.Checked) {
                return "ホンダ";
            }
            if (rbSubaru.Checked) {
                return "スバル";
            }
            if (rbForeignCar.Checked) {
                return "外国車";
            } else {
                return"その他";
            }
        }

        private void rbChecked() {
            switch (carReportDBDataGridView.CurrentRow.Cells[3].Value) {
                case "トヨタ":
                    rbToyota.Checked = true;
                    break;
                case "日産":
                    rbNissan.Checked = true;
                    break;
                case "ホンダ":
                    rbHonda.Checked = true;
                    break;
                case "スバル":
                    rbSubaru.Checked = true;
                    break;
                case "外国車":
                    rbForeignCar.Checked = true;
                    break;
                case "その他":
                    rbOther.Checked = true;
                    break;
                default:
                    break;
            }
        }

        //削除ボタン
        private void btReportDelete_Click(object sender, EventArgs e) {
            infosys202224DataSet.CarReportDB.Rows.RemoveAt(carReportDBDataGridView.CurrentRow.Index);
            EnabledCheck();
        }

        private void EnabledCheck() {
            if (carReportDBDataGridView.RowCount > 0) {
                //マスク解除
                btReportDelete.Enabled = true;
                btReportUpdate.Enabled = true;
            } else {
                //マスク設定
                btReportDelete.Enabled = false;
                btReportUpdate.Enabled = false;
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
            try {
                using (var reader = XmlReader.Create("settings.xml")) {
                    var serializer = new XmlSerializer(typeof(Settings));
                    settings = serializer.Deserialize(reader) as Settings;
                    BackColor = settings.MainFormColor;
                }
            }
            catch (Exception ex) {
                Console.WriteLine("例外メッセージ --> " + ex.Message);
                Console.WriteLine("スタックトレース ");
                Console.WriteLine(ex.StackTrace);
            }
        }

        private void carReportDBBindingNavigatorSaveItem_Click(object sender, EventArgs e) {
            this.Validate();
            this.carReportDBBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.infosys202224DataSet);
        }

        private void btConnect_Click(object sender, EventArgs e) {
            // TODO: このコード行はデータを 'infosys202224DataSet.CarReportDB' テーブルに読み込みます。必要に応じて移動、または削除をしてください。
            this.carReportDBTableAdapter.Fill(this.infosys202224DataSet.CarReportDB);
            EnabledCheck();
        }

        // バイト配列をImageオブジェクトに変換
        public static Image ByteArrayToImage(byte[] b) {
            ImageConverter imgconv = new ImageConverter();
            Image img = (Image)imgconv.ConvertFrom(b);
            return img;
        }

        // Imageオブジェクトをバイト配列に変換
        public static byte[] ImageToByteArray(Image img) {
            ImageConverter imgconv = new ImageConverter();
            byte[] b = (byte[])imgconv.ConvertTo(img, typeof(byte[]));
            return b;
        }

        private void carReportDBDataGridView_Click(object sender, EventArgs e) {
            if (carReportDBDataGridView.CurrentRow == null) { return; }

            //データグリッドビューの選択レコードを各テキストボックスへ設定
            dateTimePicker.Value = (DateTime)carReportDBDataGridView.CurrentRow.Cells[1].Value;
            cbAuther.Text = carReportDBDataGridView.CurrentRow.Cells[2].Value.ToString();
            rbChecked();
            cbCarName.Text = carReportDBDataGridView.CurrentRow.Cells[4].Value.ToString();
            tbReport.Text = carReportDBDataGridView.CurrentRow.Cells[5].Value.ToString();
            if (!DBNull.Value.Equals(carReportDBDataGridView.CurrentRow.Cells[6].Value)) {
                pbPicture.Image = ByteArrayToImage((byte[])carReportDBDataGridView.CurrentRow.Cells[6].Value);
            } else {
                pbPicture.Image = null;
            }
        }

        private void btSearchNameClear_Click(object sender, EventArgs e) {
            btSearchName.Text = null;
            this.carReportDBTableAdapter.Fill(infosys202224DataSet.CarReportDB);
        }

        private void btSearchName_Click(object sender, EventArgs e) {
            this.carReportDBTableAdapter.SearchName(this.infosys202224DataSet.CarReportDB, tbSearchName.Text);
        }

        private void carReportDBDataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e) { }
        
    }
}
