using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AddressBook {
    public partial class Form1 : Form {

        //住所データ管理用リスト
        BindingList<Person> listPerson = new BindingList<Person>();

        public Form1() {
            InitializeComponent();
            dgvPersons.DataSource = listPerson;
        }

        private void btPictureOpen_Click(object sender, EventArgs e) {
            if (ofbFileOpenDialog.ShowDialog() == DialogResult.OK) {
                pbPicture.Image = Image.FromFile(ofbFileOpenDialog.FileName);
            }
            btPictureClear.Enabled = true;
        }

        private void btPictureClear_Click(object sender, EventArgs e) {
            pbPicture.Image = null;
            btPictureClear.Enabled = false;
        }

        private void btAddPerson_Click(object sender, EventArgs e) {

            if(string.IsNullOrEmpty(tbName.Text)) {
                MessageBox.Show("氏名が入力されていません");
                return;
            }

            Person newPerson = new Person {
                Name = tbName.Text,
                MailAddress = tbMailAddress.Text,
                Address = tbAddress.Text,
                Company = cbCompany.Text,
                Picture = pbPicture.Image,
                listGroup = GetCheckBoxGroup(),
            };

            listPerson.Add(newPerson);
            dgvPersons.Rows[dgvPersons.RowCount - 1].Selected = true;

            if (listPerson.Count() > 0) {
                btDelete.Enabled = true;
                btUpdate.Enabled = true;
            }

            if (!cbCompany.Items.Contains(cbCompany.Text)) {
                cbCompany.Items.Add(cbCompany.Text);
            }
        }

        //更新ボタンが押された時の処理
        private void btUpdate_Click(object sender, EventArgs e) {

            listPerson[dgvPersons.CurrentRow.Index].Name = tbName.Text;
            listPerson[dgvPersons.CurrentRow.Index].MailAddress = tbMailAddress.Text;
            listPerson[dgvPersons.CurrentRow.Index].Address = tbAddress.Text;
            listPerson[dgvPersons.CurrentRow.Index].Company = cbCompany.Text;
            listPerson[dgvPersons.CurrentRow.Index].listGroup = GetCheckBoxGroup();
            listPerson[dgvPersons.CurrentRow.Index].Picture = pbPicture.Image;
            dgvPersons.Refresh();   //データグリッドビュー更新
        }

        

        //削除ボタンが押された時の処理
        private void btDelete_Click(object sender, EventArgs e) {

            DialogResult result = MessageBox.Show("本当に削除してよいですか？",
                "確認",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Exclamation);

            if (result == DialogResult.OK) {
                dgvPersons.Rows.RemoveAt(dgvPersons.CurrentRow.Index);
                dgvPersons.Refresh();
            }

            enabled();
        }

        //データグリッドビューをクリックしたときのイベントハンドラ
        private void dgvPersons_Click(object sender, EventArgs e) {

            //選択した行のインデックスを取得
            if (dgvPersons.CurrentRow == null) return;

            var indexRow = dgvPersons.CurrentRow.Index;

            tbName.Text = listPerson[indexRow].Name;
            tbMailAddress.Text = listPerson[indexRow].MailAddress;
            tbAddress.Text = listPerson[indexRow].Address;
            cbCompany.Text = listPerson[indexRow].Company;
            pbPicture.Image = listPerson[indexRow].Picture;

            allClear();

            foreach (var group in listPerson[indexRow].listGroup) {

                switch (group) {
                    case Person.GroupType.家族:
                        cbFamily.Checked = true;
                        break;
                    case Person.GroupType.友人:
                        cbFriend.Checked = true;
                        break;
                    case Person.GroupType.仕事:
                        cbWork.Checked = true;
                        break;
                    case Person.GroupType.その他:
                        cbOther.Checked = true;
                        break;
                    default:
                        break;
                }
            }
        }

        //チェックボックスにセットされている値をリストとして取り出す
        private List<Person.GroupType> GetCheckBoxGroup() {
            var listGroup = new List<Person.GroupType>();
            if (cbFamily.Checked) {
                listGroup.Add(Person.GroupType.家族);
            }
            if (cbFriend.Checked) {
                listGroup.Add(Person.GroupType.友人);
            }
            if (cbWork.Checked) {
                listGroup.Add(Person.GroupType.仕事);
            }
            if (cbOther.Checked) {
                listGroup.Add(Person.GroupType.その他);
            }
            return listGroup;
        }

        private void allClear() {
            cbFamily.Checked =
            cbFriend.Checked =
            cbWork.Checked =
            cbOther.Checked = false;
        }

        private void enabled() {
            if (listPerson.Count() == 0) {
                btDelete.Enabled = false;
                btUpdate.Enabled = false;
                pbPicture.Enabled = false;
            }
        }
    }
}
