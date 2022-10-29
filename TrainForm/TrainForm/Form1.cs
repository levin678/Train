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
    public partial class Form1 : Form
    {
        public static string connectString = "Provider = Microsoft.Jet.OLEDB.4.0;Data Source = DBtrain.mdb";
        private OleDbConnection myConnection;

        public Form1()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 f2 = new Form2();
            f2.Owner = this;
            f2.Show();
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
            if (radioButton1.Checked)
            {
                myConnection = new OleDbConnection(connectString);
                myConnection.Open();
                string logi = textBox1.Text;
                string pass = textBox2.Text;
                string query = "Select Логин, Пароль FROM Пользователь WHERE Логин ='" + logi + "' AND Пароль ='" + pass + "'";
                OleDbDataAdapter command = new OleDbDataAdapter(query, myConnection);
                DataTable dt = new DataTable();
                command.Fill(dt);
                if (dt.Rows.Count == 1)
                {
                    Form4.login = textBox1.Text;
                    this.Hide();
                    Form4 f4 = new Form4();
                    f4.Owner = this;
                    f4.Show();
                    myConnection.Close();
                }
                else
                {
                    MessageBox.Show("Ошибка, такого логина и пароля не существует!");
                }
            }
            if (radioButton2.Checked)
            {
                myConnection = new OleDbConnection(connectString);
                myConnection.Open();
                string logi = textBox1.Text;
                string pass = textBox2.Text;
                string query = "Select * FROM Кассир WHERE Логин ='" + logi + "' AND Пароль ='" + pass + "'";
                OleDbDataAdapter command = new OleDbDataAdapter(query, myConnection);
                DataTable dt = new DataTable();
                command.Fill(dt);
                if (dt.Rows.Count == 1)
                {
                    Form5.login = textBox1.Text;
                    this.Hide();
                    Form5 f5 = new Form5();
                    f5.Owner = this;
                    f5.Show();
                    myConnection.Close();
                }
                else
                {
                    MessageBox.Show("Ошибка, такого логина и пароля не существует!");
                }
            }
            if (radioButton3.Checked)
            {
                myConnection = new OleDbConnection(connectString);
                myConnection.Open();
                string logi = textBox1.Text;
                string pass = textBox2.Text;
                string query = "Select * FROM Бухгалтер WHERE Логин ='" + logi + "' AND Пароль ='" + pass + "'";
                OleDbDataAdapter command = new OleDbDataAdapter(query, myConnection);
                DataTable dt = new DataTable();
                command.Fill(dt);
                if (dt.Rows.Count == 1)
                {
                    this.Hide();
                    Form6 f6 = new Form6();
                    f6.Owner = this;
                    f6.Show();
                    myConnection.Close();
                }
                else
                {
                    MessageBox.Show("Ошибка, такого логина и пароля не существует!");
                }
            }
            if ((!radioButton1.Checked) && (!radioButton2.Checked) && (!radioButton3.Checked))
            {
                MessageBox.Show("Ошибка, Вы не отметили за кого будете авторизироваться!");
            }
        }
    }
}
