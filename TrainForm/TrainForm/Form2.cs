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
    public partial class Form2 : Form
    {
        public static string connectString = "Provider = Microsoft.Jet.OLEDB.4.0;Data Source = DBtrain.mdb";
        private OleDbConnection myConnection;
        public Form2()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 f1 = new Form1();
            f1.Owner = this;
            f1.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form3 f3 = new Form3();
            f3.Owner = this;
            f3.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string login1 = textBox1.Text;
            string password1 = textBox2.Text;
            if ((login1 != "") && (password1 != ""))
            {
                if (radioButton2.Checked)
                {
                    myConnection = new OleDbConnection(connectString);
                    myConnection.Open();
                    string query1 = "Select * FROM Кассир WHERE Логин ='" + login1 + "'";
                    OleDbDataAdapter command1 = new OleDbDataAdapter(query1, myConnection);
                    DataTable dt = new DataTable();
                    command1.Fill(dt);
                    if (dt.Rows.Count == 0)
                    {
                        string query = "INSERT INTO Кассир (Логин, Пароль) VALUES ('" + login1 + "','" + password1 + "')";
                        OleDbCommand command = new OleDbCommand(query, myConnection);
                        command.ExecuteNonQuery();
                        this.Hide();
                        Form5.login = textBox1.Text;
                        Form5 f5 = new Form5();
                        f5.Owner = this;
                        f5.Show();
                        myConnection.Close();
                    }
                    else
                    {
                        MessageBox.Show("Ошибка, такой логин существует!");
                    }
                }
                if (radioButton3.Checked)
                {
                    myConnection = new OleDbConnection(connectString);
                    myConnection.Open();
                    string query1 = "Select * FROM Бухгалтер WHERE Логин ='" + login1 + "'";
                    OleDbDataAdapter command1 = new OleDbDataAdapter(query1, myConnection);
                    DataTable dt = new DataTable();
                    command1.Fill(dt);
                    if (dt.Rows.Count == 0)
                    {
                        string query = "INSERT INTO Бухгалтер (Логин, Пароль) VALUES ('" + login1 + "','" + password1 + "')";
                        OleDbCommand command = new OleDbCommand(query, myConnection);
                        command.ExecuteNonQuery();
                        this.Hide();
                        Form6 f6 = new Form6();
                        f6.Owner = this;
                        f6.Show();
                        myConnection.Close();
                    }
                    else
                    {
                        MessageBox.Show("Ошибка, такой логин существует!");
                    }
                }
                if ((!radioButton2.Checked) && (!radioButton3.Checked))
                {
                    MessageBox.Show("Ошибка, Вы не отметили за кого будете регистрироваться!");
                }
            }
            else
            {
                MessageBox.Show("Ошибка при заполнении данных!");
            }
        }
    }
}
