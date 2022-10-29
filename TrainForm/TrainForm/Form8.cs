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
    public partial class Form8 : Form
    {
        public static string connectString = "Provider = Microsoft.Jet.OLEDB.4.0;Data Source = DBtrain.mdb";
        private OleDbConnection myConnection;
        public Form8()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form4 f4 = new Form4();
            f4.Owner = this;
            f4.Show();
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
            if (pa != "")
            {
                myConnection = new OleDbConnection(connectString);
                myConnection.Open();
                pa = pa.Substring(4, pa.Length - 4);
                string query1 = "Select * FROM Расписание WHERE" + pa;
                OleDbDataAdapter command1 = new OleDbDataAdapter(query1, myConnection);
                DataTable dt = new DataTable();
                command1.Fill(dt);
                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("Ошибка, билетов с такими данными нет!");
                }
                if (dt.Rows.Count == 1)
                {
                    string query2 = "Select Паспорт, Имя, Фамилия, Отчество FROM Пользователь WHERE Логин ='" + Form4.login + "'";
                    OleDbDataAdapter command2 = new OleDbDataAdapter(query2, myConnection);
                    DataTable dt2 = new DataTable();
                    command2.Fill(dt2);

                    StringBuilder csv1 = new StringBuilder();
                    foreach (DataRow row in dt2.Rows)
                        csv1.AppendLine(String.Join("','", row.ItemArray));
                    pa = csv1.ToString();
                    pa = pa.Substring(0, pa.Length - 2) + "',";
                    string w;
                    w = pa.Split(',')[0];
                    w = w.Substring(0, w.Length - 1);
                    pa = w + pa.Substring(7, pa.Length - 7);

                    StringBuilder csv = new StringBuilder();
                    foreach (DataRow row in dt.Rows)
                        csv.AppendLine(String.Join("','", row.ItemArray));
                    string pa1;
                    pa1 = csv.ToString();
                    pa1 = pa1.Substring(0, pa1.Length - 2);
                    w = pa1.Split(',')[0];
                    w = w.Substring(0, w.Length - 1);
                    pa1 = w + pa1.Substring(w.Length + 1, pa1.Length - w.Length - 1);
                    w = pa1.Split(',')[5];
                    w = w.Substring(1, w.Length - 1);
                    pa1 = pa1.Substring(0, pa1.Length - w.Length - 1) + w;

                    string l = pa + pa1;
                    string[] m = new string[10];
                    m[0] = "Паспорт =";
                    m[1] = "Имя =";
                    m[2] = "Фамилия =";
                    m[3] = "Отчество =";
                    m[4] = "Поезд =";
                    m[5] = "Откуда =";
                    m[6] = "Куда =";
                    m[7] = "Отправление =";
                    m[8] = "Прибытие =";
                    m[9] = "Цена =";
                    int h = 0;
                    string s = "";
                    l = m[0] + l;
                    for (int i = 0; i < l.Length; i++)
                    {
                        if (l[i] == ',')
                        {
                            h++;
                            s += " AND " + m[h];
                        }
                        else
                        {
                            s += l[i];
                        }
                    }

                    string query3 = "Select Паспорт FROM Покупатели WHERE " + s;
                    OleDbDataAdapter command3 = new OleDbDataAdapter(query3, myConnection);
                    DataTable dt3 = new DataTable();
                    command3.Fill(dt3);
                    if (dt3.Rows.Count == 0)
                    {
                        string query = "INSERT INTO Покупатели (Паспорт, Имя, Фамилия, Отчество, Поезд, Откуда, Куда, Отправление, Прибытие, Цена, Касса, Логин, Кто) VALUES (" + pa + pa1 + "," + 0 + ",'" + Form4.login + "','Покупатель')";
                        OleDbCommand command = new OleDbCommand(query, myConnection);
                        command.ExecuteNonQuery();
                        this.Hide();
                        Form4 f4 = new Form4();
                        f4.Owner = this;
                        f4.Show();
                    }
                    else
                    {
                        MessageBox.Show("Ошибка, у Вас уже есть билет на данный рейс!");
                    }
                }
                if (dt.Rows.Count > 1)
                {
                    MessageBox.Show("Ошибка, по Вашим данным найдено несколько рейсов, пожалуйста, внесите более точные данные!");
                }
                myConnection.Close();
            }
            else
            {
                MessageBox.Show("Ошибка, нет данных!");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            myConnection = new OleDbConnection(connectString);
            myConnection.Open();
            string query = "Select * FROM Расписание";
            OleDbDataAdapter command = new OleDbDataAdapter(query, myConnection);
            DataTable dt = new DataTable();
            command.Fill(dt);
            dataGridView1.DataSource = dt;
            myConnection.Close();
        }
    }
}
