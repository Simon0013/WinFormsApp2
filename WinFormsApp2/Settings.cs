using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WinFormsApp2
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
        }
        private string password;
        private void Settings_Load(object sender, EventArgs e)
        {
            textBox1.Text = Form1.login;
            textBox2.Text = Form1.type;
            OleDbCommand command = Form1.connection.CreateCommand();
            command.CommandText = "SELECT Пароль FROM Пользователи WHERE Логин = '" + Form1.login + "'";
            Form1.connection.Open();
            OleDbDataReader reader = command.ExecuteReader();
            reader.Read();
            password = reader[0].ToString();
            Form1.connection.Close();
            button2.Visible = false;
            label4.Visible = false;
            label5.Visible = false;
            label6.Visible = false;
            label7.Visible = false;
            label8.Visible = false;
            label9.Visible = false;
            textBox3.Visible = false;
            comboBox1.Visible = false;
            textBox4.Visible = false;
            textBox5.Visible = false;
            textBox6.Visible = false;
            Size = new Size(412, 215);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Size = new Size(412, 444);
            button2.Visible = true;
            label4.Visible = true;
            label5.Visible = true;
            label6.Visible = true;
            label7.Visible = true;
            label8.Visible = true;
            textBox3.Visible = true;
            comboBox1.Visible = true;
            textBox4.Visible = true;
            textBox5.Visible = true;
            textBox6.Visible = true;
            textBox4.UseSystemPasswordChar = true;
            textBox5.UseSystemPasswordChar = true;
            textBox6.UseSystemPasswordChar = true;
            comboBox1.Items.Add("admin");
            comboBox1.Items.Add("employee");
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox4.Text != password)
            {
                MessageBox.Show("Текущий пароль неверен!", "Ошибка");
                return;
            }
            if (textBox5.Text != textBox6.Text)
            {
                MessageBox.Show("Не совпадают новый пароль и его повтор!", "Ошибка");
                return;
            }
            OleDbCommand command = Form1.connection.CreateCommand();
            command.CommandText = "SELECT Пароль FROM Пользователи";
            Form1.connection.Open();
            OleDbDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                if (reader[0].ToString() == textBox5.Text)
                {
                    MessageBox.Show("Пароль занят!", "Ошибка");
                    Form1.connection.Close();
                    return;
                }
            }
            Form1.connection.Close();
            try
            {
                command = Form1.connection.CreateCommand();
                command.CommandText = "UPDATE Пользователи SET Логин = '" + textBox3.Text + "', Тип = '" + comboBox1.Text + "', Пароль = '" + textBox5.Text + "' WHERE Пароль = '" + textBox4.Text + "'";
                Form1.connection.Open();
                command.ExecuteNonQuery();
            }
            catch (OleDbException)
            {
                MessageBox.Show("Все поля должны быть заполненными!", "Ошибка");
                Form1.connection.Close();
                return;
            }
            Form1.connection.Close();
            Size = new Size(412, 472);
            label9.Visible = true;
        }
    }
}
