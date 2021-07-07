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
    public partial class Journal : Form
    {
        public Journal()
        {
            InitializeComponent();
        }
        public static int n = -1, kod;
        private void Journal_Load(object sender, EventArgs e)
        {
            string sql = "SELECT Код_аттестации, Вид_аттестации, Студенты.ФИО AS Студент, Студенты.Группа AS Группа, Дисциплина, Преподаватели.ФИО AS Преподаватель, Оценка, Семестр, Дата_проведения FROM Аттестации, Студенты, Преподаватели, Дисциплины WHERE Преподаватели.Код_преподавателя = Аттестации.Код_преподавателя AND Студенты.Код_студента = Аттестации.Код_студента AND Дисциплины.Код_дисциплины = Аттестации.Код_дисциплины";
            Student_S.Table_Fill("ЖурналАттестаций", sql);
            dataGridView1.DataSource = Form1.ds.Tables["ЖурналАттестаций"];
            dataGridView1.Columns["Код_аттестации"].Visible = false;
            dataGridView1.CurrentCell = null;
            dataGridView1.BackgroundColor = SystemColors.Control;
            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.AutoResizeColumns();
            OleDbCommand command;
            OleDbDataReader dataReader;
            Form1.connection.Open();
            sql = "SELECT Семестр FROM Аттестации GROUP BY Семестр";
            command = new OleDbCommand(sql, Form1.connection);
            dataReader = command.ExecuteReader();
            while (dataReader.Read())
                comboBox1.Items.Add(dataReader[0]);
            sql = "SELECT Дисциплина FROM Дисциплины GROUP BY Дисциплина";
            command = new OleDbCommand(sql, Form1.connection);
            dataReader = command.ExecuteReader();
            while (dataReader.Read())
                comboBox2.Items.Add(dataReader[0]);
            sql = "SELECT ФИО FROM Преподаватели GROUP BY ФИО";
            command = new OleDbCommand(sql, Form1.connection);
            dataReader = command.ExecuteReader();
            while (dataReader.Read())
                comboBox3.Items.Add(dataReader[0]);
            sql = "SELECT ФИО FROM Студенты GROUP BY ФИО";
            command = new OleDbCommand(sql, Form1.connection);
            dataReader = command.ExecuteReader();
            while (dataReader.Read())
                comboBox4.Items.Add(dataReader[0]);
            sql = "SELECT Группа FROM Группы GROUP BY Группа";
            command = new OleDbCommand(sql, Form1.connection);
            dataReader = command.ExecuteReader();
            while (dataReader.Read())
                comboBox5.Items.Add(dataReader[0]);
            Form1.connection.Close();
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            n = dataGridView1.CurrentRow.Index;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            n = -1;
            OleDbCommand command = Form1.connection.CreateCommand();
            command.CommandText = "SELECT MAX(Код_аттестации) FROM Аттестации";
            Form1.connection.Open();
            OleDbDataReader reader = command.ExecuteReader();
            reader.Read();
            kod = Convert.ToInt32(reader[0].ToString()) + 1;
            Form1.connection.Close();
            dataGridView1.CurrentCell = null;
            Attestation att = new Attestation();
            att.Show();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (n == -1)
            {
                MessageBox.Show("Не указан редактируемый экземпляр!", "Ошибка");
                return;
            }
            dataGridView1.CurrentCell = null;
            Attestation att = new Attestation();
            att.Show();
        }
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            Form1.ds.Tables["ЖурналАттестаций"].DefaultView.RowFilter = "";
            dataGridView1.AutoResizeColumns();
            dataGridView1.CurrentCell = null;
        }
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text != "")
            {
                Form1.ds.Tables["ЖурналАттестаций"].DefaultView.RowFilter = "Семестр = '" + comboBox1.Text + "'";
                dataGridView1.AutoResizeColumns();
                dataGridView1.CurrentCell = null;
            }
        }
        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (comboBox2.Text != "")
            {
                Form1.ds.Tables["ЖурналАттестаций"].DefaultView.RowFilter = "Дисциплина = '" + comboBox2.Text + "'";
                dataGridView1.AutoResizeColumns();
                dataGridView1.CurrentCell = null;
            }
        }
        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (comboBox3.Text != "")
            {
                Form1.ds.Tables["ЖурналАттестаций"].DefaultView.RowFilter = "Преподаватель = '" + comboBox3.Text + "'";
                dataGridView1.AutoResizeColumns();
                dataGridView1.CurrentCell = null;
            }
        }
        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            if (comboBox4.Text != "")
            {
                Form1.ds.Tables["ЖурналАттестаций"].DefaultView.RowFilter = "Студент = '" + comboBox4.Text + "'";
                dataGridView1.AutoResizeColumns();
                dataGridView1.CurrentCell = null;
            }
        }
        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            if (comboBox5.Text != "")
            {
                Form1.ds.Tables["ЖурналАттестаций"].DefaultView.RowFilter = "Группа = '" + comboBox5.Text + "'";
                dataGridView1.AutoResizeColumns();
                dataGridView1.CurrentCell = null;
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (n == -1)
            {
                MessageBox.Show("Не указан удаляемый экземпляр!", "Ошибка");
                return;
            }
            OleDbCommand command = Form1.connection.CreateCommand();
            command.CommandText = "DELETE FROM Аттестации WHERE Код_аттестации = " + Form1.ds.Tables["ЖурналАттестаций"].Rows[n]["Код_аттестации"].ToString();
            Form1.connection.Open();
            command.ExecuteNonQuery();
            Form1.connection.Close();
            dataGridView1.Rows.RemoveAt(n);
            Form1.ds.Tables["ЖурналАттестаций"].Rows.RemoveAt(n);
        }
    }
}
