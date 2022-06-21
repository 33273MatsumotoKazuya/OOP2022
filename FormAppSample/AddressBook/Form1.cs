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

namespace AddressBook {
    public partial class Form1 : Form {

        //住所データ管理用リスト
        BindingList<Person> listPerson = new BindingList<Person>();

        public Form1() {
            InitializeComponent();
            dgvPersons.DataSource = listPerson;
        }

        //フォームがロードされた時の処理
        private void Form1_Load(object sender, EventArgs e) {
            EnabledCheck();
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

            if (string.IsNullOrEmpty(tbName.Text)) {
                MessageBox.Show("氏名が入力されていません");
                return;
            }

            Person newPerson = new Person {
                Name = tbName.Text,
                MailAddress = tbMailAddress.Text,
                Address = tbAddress.Text,
                Company = cbCompany.Text,
                listGroup = GetCheckBoxGroup(),
                Registration = dateTimePicker.Value,
                KindNumber = GetKindNumber(),
                TelNumber = tbTelNumber.Text,
                Picture = pbPicture.Image,
            };

            listPerson.Add(newPerson);
            dgvPersons.Rows[dgvPersons.RowCount - 1].Selected = true;

            EnabledCheck();
            setCbCompany(cbCompany.Text);
        }

        

        //更新ボタンが押された時の処理
        private void btUpdate_Click(object sender, EventArgs e) {

            listPerson[dgvPersons.CurrentRow.Index].Name = tbName.Text;
            listPerson[dgvPersons.CurrentRow.Index].MailAddress = tbMailAddress.Text;
            listPerson[dgvPersons.CurrentRow.Index].Address = tbAddress.Text;
            listPerson[dgvPersons.CurrentRow.Index].Company = cbCompany.Text;
            listPerson[dgvPersons.CurrentRow.Index].listGroup = GetCheckBoxGroup();
            listPerson[dgvPersons.CurrentRow.Index].Registration = dateTimePicker.Value;
            listPerson[dgvPersons.CurrentRow.Index].TelNumber = tbTelNumber.Text;
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

            EnabledCheck();
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

            dateTimePicker.Value = 
                listPerson[indexRow].Registration.Year > 1900 ? listPerson[indexRow].Registration : DateTime.Today;

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

        //保存ボタンのイベントハンドラ
        private void btSave_Click(object sender, EventArgs e) {
            if (sfdSaveDialog.ShowDialog() == DialogResult.OK) {
                try {
                    //バイナリ形式でシリアル化
                    var bf = new BinaryFormatter();

                    using (FileStream fs = File.Open(sfdSaveDialog.FileName, FileMode.Create)) {
                        bf.Serialize(fs, listPerson);
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
                        listPerson = (BindingList<Person>)bf.Deserialize(fs);
                        dgvPersons.DataSource = null;
                        dgvPersons.DataSource = listPerson;
                    }
                }
                catch (Exception ex) {
                    MessageBox.Show(ex.Message);
                }

                cbCompany.Items.Clear();

                foreach (var item in listPerson.Select(p => p.Company)) {
                    setCbCompany(item);
                }

                EnabledCheck();
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

        private Person.KindNumberType GetKindNumber() {
            if (rbHome.Checked) {
                return Person.KindNumberType.自宅;
            } else {
                return Person.KindNumberType.携帯;
            }
        }

        //チェックボックスの初期化
        private void allClear() {
            cbFamily.Checked =
            cbFriend.Checked =
            cbWork.Checked =
            cbOther.Checked = false;
        }

        //項目の有無を判断し、マスク処理の呼び出しを行う
        private void EnabledCheck() {
            if (listPerson.Count() > 0) {
                //マスク解除
                btDelete.Enabled = true;
                btUpdate.Enabled = true;
            } else {
                //マスク設定
                btDelete.Enabled = false;
                btUpdate.Enabled = false;
            }
        }

        //コンボボックスに会社名を登録する(重複なし)
        private void setCbCompany(string company) {
            
            if (!cbCompany.Items.Contains(company)) {
                //未登録であれば登録処理
                cbCompany.Items.Add(company);
            }
        }
    }
}
