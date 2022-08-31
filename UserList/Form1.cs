using System.Xml.Serialization;
using WF.Model;

namespace WF
{
    public partial class Form1 : Form
    {
        private string FilePath { get; set; } = string.Empty;
        private Users user { get; set; }
        public Form1()
        {
            InitializeComponent();
            user = new Users();
        }
        private void button1_Click(object sender, EventArgs e) // Add User
        {
            user.UserList.Add(new Users.User
            {
                Name = (this.textBox1.Text.Length != 0) ? textBox1.Text.ToString() : "%null string%",
                Sex = this.comboBox1.Text.ToString(),
                Age = Convert.ToInt16(this.numericUpDown1.Value)
            });
            ListViewItem LVI = new ListViewItem(user.UserList[user.UserList.Count - 1].Name);
            LVI.Tag = user.UserList[user.UserList.Count - 1];
            listView1.Items.Add(LVI);
            Clear();
        }

        private void Clear()
        {
            this.textBox1.Text = null;
            this.numericUpDown1.Value = 0;
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 1 && listView1.SelectedItems[0].Tag != null)
            {
                Users.User uSet = (Users.User)listView1.SelectedItems[0].Tag;
                textBox1.Text = uSet.Name;
                comboBox1.Text = uSet.Sex;
                numericUpDown1.Value = uSet.Age;
            }
            else
            {
                Clear();
            }
        }

        private void DeleteMembers()
        {

            if (listView1.SelectedItems.Count != 0)
            {
                foreach (ListViewItem item in listView1.SelectedItems)
                {
                    user.UserList.RemoveAt(listView1.Items.IndexOf(item));
                    item.Remove();
                }
            }
            else
            {
                MessageBox.Show("Не выбран ни один пользователь", "Warning");
            }
        }
        private void button5_Click(object sender, EventArgs e) => DeleteMembers(); // Delete users

        private void button2_Click(object sender, EventArgs e) // Serialize
        {
            XmlSerializer xmls = new XmlSerializer(typeof(Users));
            using (FileStream fs = new FileStream(FilePath, mode: FileMode.Truncate))
            {
                xmls.Serialize(fs, user);
            }
            MessageBox.Show("Успешно", "Log");
        }

        private void button6_Click_1(object sender, EventArgs e) // Select XML File
        {
            openFileDialog1.Filter = "XML Files (*.xml)|*.xml| TXT Files (*.txt)|*.txt";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.FilePath = openFileDialog1.FileName;
            }
        }

        private void openFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e) // Event for (De)serialize buttons
        {
            this.button2.Enabled = true;
            this.button3.Enabled = true;
        }

        private void button3_Click(object sender, EventArgs e) //Deserialize
        {
            XmlSerializer xmls = new XmlSerializer(typeof(Users));
            using (FileStream fs = new FileStream(FilePath, mode: FileMode.Open))
            {
                listView1.Items.Clear();
                user.UserList.RemoveAll(x => x.Equals(x));
                user = xmls.Deserialize(fs) as Users;
            }
            foreach (Users.User IUser in user.UserList)
            {
                ListViewItem LVI = new ListViewItem(IUser.Name);
                LVI.Tag = IUser;
                listView1.Items.Add(LVI);
            }
        }

    }
}