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
    public partial class Form6 : Form
    {
        public static string connectString = "Provider = Microsoft.Jet.OLEDB.4.0;Data Source = DBtrain.mdb";
        private OleDbConnection myConnection;
        public Form6()
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

        private void button1_Click(object sender, EventArgs e)
        {
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
            if (textBox7.Text != "")
            {
                pa += " AND Касса =" + textBox7.Text;
            }
            if (textBox8.Text != "")
            {
                pa += " AND Кто ='" + textBox8.Text + "'";
            }
            if (pa != "")
            {
                pa = pa.Substring(4, pa.Length - 4);
                myConnection = new OleDbConnection(connectString);
                myConnection.Open();
                string query = "Select Поезд, Откуда, Куда, Отправление, Прибытие, Касса, Кто, Цена FROM Покупатели WHERE" + pa;
                OleDbDataAdapter command = new OleDbDataAdapter(query, myConnection);
                DataTable dt = new DataTable();
                command.Fill(dt);
                dataGridView1.DataSource = dt;
                myConnection.Close();
            }
            else
            {
                myConnection = new OleDbConnection(connectString);
                myConnection.Open();
                string query = "Select Поезд, Откуда, Куда, Отправление, Прибытие, Касса, Кто, Цена FROM Покупатели";
                OleDbDataAdapter command = new OleDbDataAdapter(query, myConnection);
                DataTable dt = new DataTable();
                command.Fill(dt);
                dataGridView1.DataSource = dt;
                myConnection.Close();
            }
        }
    }
}
