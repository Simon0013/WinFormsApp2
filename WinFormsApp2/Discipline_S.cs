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
    public partial class Discipline_S : Form
    {
        public Discipline_S()
        {
            InitializeComponent();
        }
        private void Discipline_S_Load(object sender, EventArgs e)
        {
            string sql;
            OleDbCommand command;
            OleDbDataReader dataReader;
            Form1.connection.Open();
            sql = "SELECT ФИО FROM Преподаватели GROUP BY ФИО";
            command = new OleDbCommand(sql, Form1.connection);
            dataReader = command.ExecuteReader();
            while (dataReader.Read())
                comboBox1.Items.Add(dataReader[0]);
            sql = "SELECT Специальность FROM Специальности GROUP BY Специальность";
            command = new OleDbCommand(sql, Form1.connection);
            dataReader = command.ExecuteReader();
            while (dataReader.Read())
                comboBox2.Items.Add(dataReader[0]);
            sql = "SELECT Кафедра FROM Кафедры GROUP BY Кафедра";
            command = new OleDbCommand(sql, Form1.connection);
            dataReader = command.ExecuteReader();
            while (dataReader.Read())
                comboBox3.Items.Add(dataReader[0]);
            Form1.connection.Close();
            sql = "SELECT Код_дисциплины, Дисциплина, Кафедра FROM Дисциплины, Кафедры WHERE Кафедры.Код_кафедры = Дисциплины.Код_кафедры";
            Student_S.Table_Fill("ПоискДисциплин", sql);
            dataGridView1.DataSource = Form1.ds.Tables["ПоискДисциплин"];
            dataGridView1.Columns["Код_дисциплины"].Visible = false;
            dataGridView1.BackgroundColor = SystemColors.Control;
            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.AutoResizeColumns();
            dataGridView1.Enabled = false;
        }
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = Form1.ds.Tables["ПоискДисциплин"];
            Form1.ds.Tables["ПоискДисциплин"].DefaultView.RowFilter = "";
            dataGridView1.CurrentCell = null;
        }
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text != "")
            {
                string sql = "SELECT Код_преподавателя FROM Преподаватели WHERE ФИО = '" + comboBox1.Text + "'";
                OleDbCommand command = new OleDbCommand(sql, Form1.connection);
                Form1.connection.Open();
                OleDbDataReader dataReader = command.ExecuteReader();
                dataReader.Read();
                string kodteacher = dataReader[0].ToString();
                Form1.connection.Close();
                sql = "SELECT Дисциплины.Код_дисциплины, Дисциплина, Кафедра FROM Дисциплины, Преподаватели_дисциплины, Кафедры WHERE Кафедры.Код_кафедры = Дисциплины.Код_кафедры AND Дисциплины.Код_дисциплины = Преподаватели_дисциплины.Код_дисциплины AND Преподаватели_дисциплины.Код_преподавателя = " + kodteacher;
                Student_S.Table_Fill("Преподаватели_дисциплины", sql);
                dataGridView1.DataSource = Form1.ds.Tables["Преподаватели_дисциплины"];
                dataGridView1.CurrentCell = null;
            }
        }
        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (comboBox2.Text != "")
            {
                string sql = "SELECT Код_специальности FROM Специальности WHERE Специальность = '" + comboBox2.Text + "'";
                OleDbCommand command = new OleDbCommand(sql, Form1.connection);
                Form1.connection.Open();
                OleDbDataReader dataReader = command.ExecuteReader();
                dataReader.Read();
                string kodspec = dataReader[0].ToString();
                Form1.connection.Close();
                sql = "SELECT Дисциплины.Код_дисциплины, Дисциплина, Кафедра FROM Дисциплины, Дисциплины_специальности, Кафедры WHERE Кафедры.Код_кафедры = Дисциплины.Код_кафедры AND Дисциплины.Код_дисциплины = Дисциплины_специальности.Код_дисциплины AND Дисциплины_специальности.Код_специальности = " + kodspec;
                Student_S.Table_Fill("Дисциплины_специальности", sql);
                dataGridView1.DataSource = Form1.ds.Tables["Дисциплины_специальности"];
                dataGridView1.CurrentCell = null;
            }
        }
        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (comboBox3.Text != "")
            {
                dataGridView1.DataSource = Form1.ds.Tables["ПоискДисциплин"];
                Form1.ds.Tables["ПоискДисциплин"].DefaultView.RowFilter = "Кафедра = '" + comboBox3.Text + "'";
                dataGridView1.CurrentCell = null;
            }
        }
    }
}
