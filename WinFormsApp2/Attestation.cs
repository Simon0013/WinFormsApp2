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
    public partial class Attestation : Form
    {
        public Attestation()
        {
            InitializeComponent();
        }
        public void FiledsForm_Fill()
        {
            textBox1.Text = Form1.ds.Tables["ЖурналАттестаций"].Rows[Journal.n]["Код_аттестации"].ToString();
            comboBox1.Text = Form1.ds.Tables["ЖурналАттестаций"].Rows[Journal.n]["Вид_аттестации"].ToString();
            comboBox2.Text = Form1.ds.Tables["ЖурналАттестаций"].Rows[Journal.n]["Студент"].ToString();
            comboBox3.Text = Form1.ds.Tables["ЖурналАттестаций"].Rows[Journal.n]["Дисциплина"].ToString();
            comboBox4.Text = Form1.ds.Tables["ЖурналАттестаций"].Rows[Journal.n]["Преподаватель"].ToString();
            textBox2.Text = Form1.ds.Tables["ЖурналАттестаций"].Rows[Journal.n]["Оценка"].ToString();
            textBox3.Text = Form1.ds.Tables["ЖурналАттестаций"].Rows[Journal.n]["Семестр"].ToString();
            dateTimePicker1.Text = Form1.ds.Tables["ЖурналАттестаций"].Rows[Journal.n]["Дата_проведения"].ToString();
        }
        private void Attestation_Load(object sender, EventArgs e)
        {
            Form1.connection.Open();
            textBox1.Enabled = false;
            OleDbCommand command = Form1.connection.CreateCommand();
            command.CommandText = "SELECT * FROM Студенты";
            OleDbDataReader reader = command.ExecuteReader();
            while (reader.Read())
                comboBox2.Items.Add(reader["ФИО"]);
            command = Form1.connection.CreateCommand();
            command.CommandText = "SELECT * FROM Дисциплины";
            reader = command.ExecuteReader();
            while (reader.Read())
                comboBox3.Items.Add(reader["Дисциплина"]);
            command = Form1.connection.CreateCommand();
            command.CommandText = "SELECT * FROM Преподаватели";
            reader = command.ExecuteReader();
            while (reader.Read())
                comboBox4.Items.Add(reader["ФИО"]);
            comboBox1.Items.Add("Экзамен");
            comboBox1.Items.Add("Дифференциальный зачёт");
            comboBox1.Items.Add("Зачёт");
            Form1.connection.Close();
            if (Journal.n >= 0)
            {
                FiledsForm_Fill();
            }
            else textBox1.Text = Journal.kod.ToString();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            OleDbCommand command = Form1.connection.CreateCommand();
            command.CommandText = "SELECT Код_студента FROM Студенты WHERE ФИО = '" + comboBox2.Text + "'";
            Form1.connection.Open();
            OleDbDataReader reader = command.ExecuteReader();
            reader.Read();
            string student_kod = reader[0].ToString();
            command = Form1.connection.CreateCommand();
            command.CommandText = "SELECT Код_дисциплины FROM Дисциплины WHERE Дисциплина = '" + comboBox3.Text + "'";
            reader = command.ExecuteReader();
            reader.Read();
            string discipline_kod = reader[0].ToString();
            command = Form1.connection.CreateCommand();
            command.CommandText = "SELECT Код_преподавателя FROM Преподаватели WHERE ФИО = '" + comboBox4.Text + "'";
            reader = command.ExecuteReader();
            reader.Read();
            string teacher_kod = reader[0].ToString();
            Form1.connection.Close();
            command = Form1.connection.CreateCommand();
            Form1.connection.Open();
            if (Journal.n >= 0)
                command.CommandText = "UPDATE Аттестации SET Вид_аттестации = '" + comboBox1.Text + "', Код_студента = " + student_kod + ", Код_дисциплины = " + discipline_kod + ", Код_преподавателя = " + teacher_kod + ", Оценка = '" + textBox2.Text + "', Семестр = '" + textBox3.Text + "', Дата_проведения = '" + dateTimePicker1.Value.ToShortDateString() + "' WHERE Код_аттестации = " + textBox1.Text;
            else
                command.CommandText = "INSERT INTO Аттестации VALUES (" + textBox1.Text + ", '" + comboBox1.Text + "', " + student_kod + ", " + discipline_kod + ", " + teacher_kod + ", '" + textBox2.Text + "', '" + textBox3.Text + "', '" + dateTimePicker1.Value.ToShortDateString() + "')";
            try
            {
                command.ExecuteNonQuery();
            }
            catch (OleDbException ex)
            {
                MessageBox.Show(ex.Message);
                Form1.connection.Close();
                return;
            }
            Form1.connection.Close();
            Form1.ds.Tables["ЖурналАттестаций"].Rows.Add(new object[] { textBox1.Text, comboBox1.Text, comboBox2.Text, comboBox3.Text, comboBox4.Text, textBox2.Text, textBox3.Text, dateTimePicker1.Value.ToShortDateString() });
            if (Journal.n >= 0) Form1.ds.Tables["ЖурналАттестаций"].Rows.RemoveAt(Journal.n);
        }
        private void Attestation_FormClosed(object sender, FormClosedEventArgs e)
        {
            Journal.n = -1;
        }
    }
}
