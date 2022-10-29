using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace TrainForm
{
    public partial class Form3 : Form
    {
        public static string connectString = "Provider = Microsoft.Jet.OLEDB.4.0;Data Source = DBtrain.mdb";
        private OleDbConnection myConnection;
        public Form3()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 f1 = new Form1();
            f1.Owner = this;
            f1.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 f2 = new Form2();
            f2.Owner = this;
            f2.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            myConnection = new OleDbConnection(connectString);
            myConnection.Open();
            string login1 = textBox1.Text;
            string password1 = textBox2.Text;
            string query1 = "Select Логин, Пароль FROM Пользователь WHERE Логин ='" + login1 + "'";
            OleDbDataAdapter command1 = new OleDbDataAdapter(query1, myConnection);
            DataTable dt = new DataTable();
            command1.Fill(dt);
            if (dt.Rows.Count == 0)
            {
                int kod;
                if (textBox6.Text != "")
                {
                    kod = Convert.ToInt32(textBox6.Text);
                }
                else
                {
                    kod = 0;
                }
                string Name = textBox3.Text;
                string Surname = textBox4.Text;
                string Position = textBox5.Text;
                if ((login1 != "") && (password1 != "") && (kod > 99999) && (Surname != "") && (Position != "") && (Name != "") && (kod <= 999999))
                {
                    string query2 = "Select Паспорт FROM Пользователь WHERE Паспорт =" + kod + "";
                    OleDbDataAdapter command2 = new OleDbDataAdapter(query2, myConnection);
                    DataTable dt2 = new DataTable();
                    command2.Fill(dt2);
                    if (dt.Rows.Count == 0)
                    {
                        string query = "INSERT INTO Пользователь (Логин, Пароль, Паспорт, Имя, Фамилия, Отчество) VALUES ('" + login1 + "','" + password1 + "'," + kod + ",'" + Name + "','" + Surname + "', '" + Position + "')";
                        OleDbCommand command = new OleDbCommand(query, myConnection);
                        command.ExecuteNonQuery();
                        this.Hide();
                        Form4.login = textBox1.Text;
                        Form4 f4 = new Form4();
                        f4.Owner = this;
                        f4.Show();
                        myConnection.Close();
                    }
                    else
                    {
                        MessageBox.Show("Ошибка, такой паспорт существует в другом логине!");
                    }
                }
                else
                {
                    MessageBox.Show("Ошибка при заполнении данных!");
                }
            }
            else
            {
                MessageBox.Show("Ошибка, такой логин существует!");
            }
        }
    }
}
