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
    public partial class Form5 : Form
    {
        public static string login  = "";
        public static int kas = 0;

        public static string connectString = "Provider = Microsoft.Jet.OLEDB.4.0;Data Source = DBtrain.mdb";
        private OleDbConnection myConnection;

        public Form5()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            myConnection = new OleDbConnection(connectString);
            myConnection.Open();
            string query = "Select Паспорт, Имя, Фамилия, Отчество, Поезд, Откуда, Куда, Отправление, Прибытие, Цена, Кто FROM Покупатели";
            OleDbDataAdapter command = new OleDbDataAdapter(query, myConnection);
            DataTable dt = new DataTable();
            command.Fill(dt);
            dataGridView1.DataSource = dt;
            myConnection.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            myConnection = new OleDbConnection(connectString);
            myConnection.Open();
            string pa = "";
            if (textBox9.Text != "")
            {
                pa += " AND Имя ='" + textBox9.Text + "'";
            }
            if (textBox8.Text != "")
            {
                pa += " AND Фамилия ='" + textBox8.Text + "'";
            }
            if (textBox10.Text != "")
            {
                pa += " AND Отчество ='" + textBox10.Text + "'";
            }
            if (textBox1.Text != "")
            {
                pa += " AND Поезд =" + textBox1.Text;
            }
            if (textBox2.Text != "")
            {
                pa += " AND Откуда ='" + textBox2.Text + "'";
            }
            if (textBox3.Text != "")
            {
                pa += " AND Куда ='" + textBox3.Text + "'";
            }
            if (textBox4.Text != "")
            {
                pa += " AND Отправление ='" + textBox4.Text + "'";
            }
            if (textBox5.Text != "")
            {
                pa += " AND Прибытие ='" + textBox5.Text + "'";
            }
            if (textBox6.Text != "")
            {
                pa += " AND Цена =" + textBox6.Text;
            }
            if (textBox11.Text != "")
            {
                pa += " AND Кто ='" + textBox11.Text + "'";
            }
            if (textBox7.Text != "")
            {
                if ((Convert.ToInt32(textBox7.Text) > 99999) && (Convert.ToInt32(textBox7.Text) <= 999999))
                {
                    pa += " AND Паспорт =" + textBox7.Text;
                }
                else
                {
                    MessageBox.Show("Ошибка, паспорт должен состоять из 6 цифр!");
                    pa = "";
                }
            }
            if (pa != "")
            {
                pa = pa.Substring(4, pa.Length - 4);
                string query1 = "Select Паспорт FROM Покупатели WHERE" + pa;
                OleDbDataAdapter command1 = new OleDbDataAdapter(query1, myConnection);
                DataTable dt = new DataTable();
                command1.Fill(dt);
                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("Ошибка, у Вас нет такого билета!");
                }
                if (dt.Rows.Count == 1)
                {
                    string query = "DELETE FROM Покупатели WHERE" + pa;
                    OleDbCommand command = new OleDbCommand(query, myConnection);
                    command.ExecuteNonQuery();
                }
                if (dt.Rows.Count > 1)
                {
                    MessageBox.Show("Ошибка, по Вашим данным найдено несколько билетов, пожалуйста, внесите более точные данные!");
                }
                myConnection.Close();
            }
            else
            {
                if (textBox7.Text == "")
                {
                    MessageBox.Show("Ошибка, нет данных!");
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form7 f7 = new Form7();
            f7.Owner = this;
            f7.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 f1 = new Form1();
            f1.Owner = this;
            f1.Show();
        }
    }
}
