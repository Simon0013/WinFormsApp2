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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
        public static string login, password, type = "";
        private void Form3_Load(object sender, EventArgs e)
        {
            
        }
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
                type = "admin";
        }
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
                type = "employee";
        }
        private void Form3_Closed(object sender, FormClosedEventArgs e)
        {
            login = "";
            password = "";
            type = "";
        }
        private void button1_Click(object sender, EventArgs e)
        {
            OleDbCommand command = Form1.connection.CreateCommand();
            command.CommandText = "INSERT INTO Пользователи VALUES ('" + login + "', '" + password + "', '" + type + "')";
            Form1.connection.Open();
            try
            {
                command.ExecuteNonQuery();
            }
            catch (OleDbException oledbe)
            {
                MessageBox.Show(oledbe.Message);
            }
            Form1.connection.Close();
            Form2 form = new Form2();
            form.ShowDialog();
            Close();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Form1 form = new Form1();
            form.ShowDialog();
            Close();
        }
    }
}
