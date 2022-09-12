using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AddressBook {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) {
            pbImage.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void btConnect_Click(object sender, EventArgs e) {
            // TODO: このコード行はデータを 'infosys202224DataSet.AddressTable' テーブルに読み込みます。必要に応じて移動、または削除をしてください。
            this.addressTableTableAdapter.Fill(this.infosys202224DataSet.AddressTable);
        }

        private void addressTableDataGridView_Click(object sender, EventArgs e) {
            if (addressTableDataGridView.CurrentRow == null) return;

            //データグリッドビューの選択レコードを各テキストボックスへ設定
            tbName.Text = addressTableDataGridView.CurrentRow.Cells[1].Value.ToString();
            tbAddress.Text = addressTableDataGridView.CurrentRow.Cells[2].Value.ToString();
            tbTel.Text = addressTableDataGridView.CurrentRow.Cells[3].Value.ToString();
            tbMail.Text = addressTableDataGridView.CurrentRow.Cells[4].Value.ToString();
            tbMemo.Text = addressTableDataGridView.CurrentRow.Cells[5].Value.ToString();
            if (!DBNull.Value.Equals(addressTableDataGridView.CurrentRow.Cells[6].Value)) {
                pbImage.Image = ByteArrayToImage((byte[])addressTableDataGridView.CurrentRow.Cells[6].Value);
            } else {
                pbImage.Image = null;
            }
        }

        private void btUpdate_Click(object sender, EventArgs e) {
            //各テキストボックスからデータグリッドビューへ設定
            addressTableDataGridView.CurrentRow.Cells[1].Value = tbName.Text;
            addressTableDataGridView.CurrentRow.Cells[2].Value = tbAddress.Text;
            addressTableDataGridView.CurrentRow.Cells[3].Value = tbTel.Text;
            addressTableDataGridView.CurrentRow.Cells[4].Value = tbMail.Text;
            addressTableDataGridView.CurrentRow.Cells[5].Value = tbMemo.Text;
            addressTableDataGridView.CurrentRow.Cells[6].Value = ImageToByteArray(pbImage.Image);
            //データセットの中をデータベースへ反映
            this.Validate();
            this.addressTableBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.infosys202224DataSet);
        }

        private void btImageOpen_Click(object sender, EventArgs e) {
            if (ofdImage.ShowDialog() == DialogResult.OK) {
                pbImage.Image = System.Drawing.Image.FromFile(ofdImage.FileName);
            }
        }

        private void btImageClose_Click(object sender, EventArgs e) {
            pbImage.Image = null;
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

        private void btAdd_Click(object sender, EventArgs e) {
            DataRow newRow = infosys202224DataSet.AddressTable.NewRow();
            newRow[1] = tbName.Text;
            newRow[2] = tbAddress.Text;
            newRow[3] = tbTel.Text;
            newRow[4] = tbMail.Text;
            newRow[5] = tbMemo.Text;
            newRow[6] = ImageToByteArray(pbImage.Image);

            //データセットへ新しいレコードを追加
            infosys202224DataSet.AddressTable.Rows.Add(newRow);
            //データベース更新
            this.addressTableTableAdapter.Update(this.infosys202224DataSet.AddressTable);
        }

        private void addressTableDataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e) { }

        private void btSearchName_Click(object sender, EventArgs e) {
            this.addressTableTableAdapter.SearchName(this.infosys202224DataSet.AddressTable, tbSearchName.Text);
        }

        private void btClear_Click(object sender, EventArgs e) {
            tbName.Text = null;
            tbAddress.Text = null;
            tbTel.Text = null;
            tbMail.Text = null;
            tbMemo.Text = null;
            pbImage.Image = null;
        }

        private void btSearchNameClear_Click(object sender, EventArgs e) {
            tbSearchName.Text = null;
            this.addressTableTableAdapter.Fill(this.infosys202224DataSet.AddressTable);
        }

        private void 終了ToolStripMenuItem_Click(object sender, EventArgs e) {
            Application.Exit();
        }

        private void バージョン情報ToolStripMenuItem_Click(object sender, EventArgs e) {
            new Version().ShowDialog();
        }
    }
}
