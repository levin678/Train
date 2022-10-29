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
    public partial class Form4 : Form
    {
        public static string connectString = "Provider = Microsoft.Jet.OLEDB.4.0;Data Source = DBtrain.mdb";
        private OleDbConnection myConnection;

        public static string login = "";
        public Form4()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            myConnection = new OleDbConnection(connectString);
            myConnection.Open();
            string query = "Select Поезд, Откуда, Куда, Отправление, Прибытие, Цена FROM Покупатели WHERE Логин ='" + login + "' AND Кто ='Покупатель'";
            OleDbDataAdapter command = new OleDbDataAdapter(query, myConnection);
            DataTable dt = new DataTable();
            command.Fill(dt);
            dataGridView1.DataSource = dt;
            myConnection.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form8 f8 = new Form8();
            f8.Owner = this;
            f8.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            myConnection = new OleDbConnection(connectString);
            myConnection.Open();
            string pa = "";
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
            pa += " AND Кто ='Покупатель'";
            string query1 = "Select Паспорт FROM Покупатели WHERE Логин ='" + login + "'" + pa;
            OleDbDataAdapter command1 = new OleDbDataAdapter(query1, myConnection);
            DataTable dt = new DataTable();
            command1.Fill(dt);
            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("Ошибка, у Вас нет такого билета!");
            }
            if (dt.Rows.Count == 1)
            {
                string query = "DELETE FROM Покупатели WHERE Логин ='" + login + "'" + pa;
                OleDbCommand command = new OleDbCommand(query, myConnection);
                command.ExecuteNonQuery();
            }
            if (dt.Rows.Count >1)
            {
                MessageBox.Show("Ошибка, по Вашим данным найдено несколько билетов, пожалуйста, внесите более точные данные!");
            }
            myConnection.Close();
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
