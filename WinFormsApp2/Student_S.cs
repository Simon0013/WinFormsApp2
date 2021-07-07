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
    public partial class Student_S : Form
    {
        public Student_S()
        {
            InitializeComponent();
        }
        public static void Table_Fill(string name, string sql)
        {
            Form1.connection.Open();
            if (Form1.ds.Tables[name] != null)
                Form1.ds.Tables[name].Clear();
            OleDbDataAdapter da;
            da = new OleDbDataAdapter(sql, Form1.connection);
            da.Fill(Form1.ds, name);
            Form1.connection.Close();
        }
        private void Student_S_Load(object sender, EventArgs e)
        {
            string sql;
            OleDbCommand command;
            OleDbDataReader dataReader;
            Form1.connection.Open();
            sql = "SELECT Группа FROM Группы GROUP BY Группа";
            command = new OleDbCommand(sql, Form1.connection);
            dataReader = command.ExecuteReader();
            while (dataReader.Read())
                comboBox1.Items.Add(dataReader[0]);
            sql = "SELECT Курс FROM Группы GROUP BY Курс";
            command = new OleDbCommand(sql, Form1.connection);
            dataReader = command.ExecuteReader();
            while (dataReader.Read())
                comboBox2.Items.Add(dataReader[0]);
            sql = "SELECT Специальность FROM Специальности GROUP BY Специальность";
            command = new OleDbCommand(sql, Form1.connection);
            dataReader = command.ExecuteReader();
            while (dataReader.Read())
                comboBox3.Items.Add(dataReader[0]);
            sql = "SELECT Профиль FROM Профили GROUP BY Профиль";
            command = new OleDbCommand(sql, Form1.connection);
            dataReader = command.ExecuteReader();
            while (dataReader.Read())
                comboBox4.Items.Add(dataReader[0]);
            Form1.connection.Close();
            sql = "SELECT Код_студента, ФИО, Студенты.Группа, Курс, Специальность, Профиль, Дата_рождения, Пол, Адрес, Зачетная_книжка FROM Студенты, Группы, Специальности, Профили WHERE Профили.Код_специальности = Специальности.Код_специальности AND Студенты.Группа = Группы.Группа AND Профили.Код_профиля = Группы.Код_профиля";
            Table_Fill("ПоискСтудентов", sql);
            dataGridView1.DataSource = Form1.ds.Tables["ПоискСтудентов"];
            dataGridView1.Columns["Код_студента"].Visible = false;
            dataGridView1.BackgroundColor = SystemColors.Control;
            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.AutoResizeColumns();
            dataGridView1.Enabled = false;
        }
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            Form1.ds.Tables["ПоискСтудентов"].DefaultView.RowFilter = "";
            dataGridView1.CurrentCell = null;
        }
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text != "")
            {
                Form1.ds.Tables["ПоискСтудентов"].DefaultView.RowFilter = "Группа = '" + comboBox1.Text + "'";
                dataGridView1.CurrentCell = null;
            }
        }
        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (comboBox2.Text != "")
            {
                Form1.ds.Tables["ПоискСтудентов"].DefaultView.RowFilter = "Курс = '" + comboBox2.Text + "'";
                dataGridView1.CurrentCell = null;
            }
        }
        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (comboBox3.Text != "")
            {
                Form1.ds.Tables["ПоискСтудентов"].DefaultView.RowFilter = "Специальность = '" + comboBox3.Text + "'";
                dataGridView1.CurrentCell = null;
            }
        }
        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            if (comboBox4.Text != "")
            {
                Form1.ds.Tables["ПоискСтудентов"].DefaultView.RowFilter = "Профиль = '" + comboBox4.Text + "'";
                dataGridView1.CurrentCell = null;
            }
        }
    }
}
