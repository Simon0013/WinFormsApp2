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
    public partial class Specialty_S : Form
    {
        public Specialty_S()
        {
            InitializeComponent();
        }
        private void Specialty_S_Load(object sender, EventArgs e)
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
            sql = "SELECT Код_специальности, Специальность, Кафедра FROM Специальности INNER JOIN Кафедры ON Специальности.Код_кафедры = Кафедры.Код_кафедры";
            Student_S.Table_Fill("ПоискСпециальностей", sql);
            dataGridView1.DataSource = Form1.ds.Tables["ПоискСпециальностей"];
            dataGridView1.Columns["Код_специальности"].Visible = false;
            dataGridView1.BackgroundColor = SystemColors.Control;
            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.AutoResizeColumns();
            dataGridView1.Enabled = false;
        }
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            Form1.ds.Tables["ПоискСпециальностей"].DefaultView.RowFilter = "";
            dataGridView1.CurrentCell = null;
        }
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text != "")
            {
                Form1.ds.Tables["ПоискСпециальностей"].DefaultView.RowFilter = "Кафедра = '" + comboBox1.Text + "'";
                dataGridView1.CurrentCell = null;
            }
        }
    }
}
