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
    public partial class Teacher_S : Form
    {
        public Teacher_S()
        {
            InitializeComponent();
        }
        private void Teacher_S_Load(object sender, EventArgs e)
        {
            string sql;
            OleDbCommand command;
            OleDbDataReader dataReader;
            Form1.connection.Open();
            sql = "SELECT Кафедра FROM Кафедры GROUP BY Кафедра";
            command = new OleDbCommand(sql, Form1.connection);
            dataReader = command.ExecuteReader();
            while (dataReader.Read())
                comboBox1.Items.Add(dataReader[0]);
            Form1.connection.Close();
            sql = "SELECT Код_преподавателя, ФИО, Дата_рождения, Пол, Кафедра FROM Преподаватели INNER JOIN Кафедры ON Преподаватели.Код_кафедры = Кафедры.Код_кафедры";
            Student_S.Table_Fill("ПоискПреподавателей", sql);
            dataGridView1.DataSource = Form1.ds.Tables["ПоискПреподавателей"];
            dataGridView1.Columns["Код_преподавателя"].Visible = false;
            dataGridView1.BackgroundColor = SystemColors.Control;
            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.AutoResizeColumns();
            dataGridView1.Enabled = false;
        }
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            Form1.ds.Tables["ПоискПреподавателей"].DefaultView.RowFilter = "";
            dataGridView1.CurrentCell = null;
        }
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text != "")
            {
                Form1.ds.Tables["ПоискПреподавателей"].DefaultView.RowFilter = "Кафедра = '" + comboBox1.Text + "'";
                dataGridView1.CurrentCell = null;
            }
        }
    }
}
