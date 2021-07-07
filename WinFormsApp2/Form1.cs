using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public static string conn = "Provider = Microsoft.ACE.OLEDB.12.0; Data Source = Database1.mdb";
        public static OleDbConnection connection = new OleDbConnection(conn);
        public static DataSet ds = new DataSet();
        public static string root = "";
        public static string login, type;
        private void Form1_Load(object sender, EventArgs e)
        {
            textBox2.UseSystemPasswordChar = true;
            textBox4.UseSystemPasswordChar = true;
            login = ""; type = "";
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
                textBox2.UseSystemPasswordChar = false;
            else
                textBox2.UseSystemPasswordChar = true;
        }
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
                textBox4.UseSystemPasswordChar = false;
            else
                textBox4.UseSystemPasswordChar = true;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Логин не может быть пустым!", "Ошибка");
                return;
            }
            OleDbCommand command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM Пользователи WHERE Логин = '" + textBox1.Text + "'";
            connection.Open();
            OleDbDataReader reader = command.ExecuteReader();
            reader.Read();
            if (textBox2.Text == reader["Пароль"].ToString())
            {
                if (reader["Тип"].ToString() == "admin")
                {
                    root = "admin";
                }
                login = textBox1.Text;
                type = reader["Тип"].ToString();
                connection.Close();
                Hide();
                Form2 form = new Form2();
                form.ShowDialog();
                Close();
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль!", "Ошибка");
            }
            connection.Close();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            OleDbCommand command = connection.CreateCommand();
            command.CommandText = "SELECT Пароль FROM Пользователи";
            connection.Open();
            OleDbDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                if (reader[0].ToString() == textBox4.Text)
                {
                    MessageBox.Show("Пароль занят!", "Ошибка");
                    return;
                }
            }
            if ((textBox3.Text != "") && (textBox4.Text != ""))
            {
                Form3 form = new Form3();
                form.Show();
                Form3.login = textBox3.Text;
                Form3.password = textBox4.Text;
            }
            else
            {
                MessageBox.Show("У вас пустой логин или пароль!", "Ошибка");
            }
        }
    }
}
