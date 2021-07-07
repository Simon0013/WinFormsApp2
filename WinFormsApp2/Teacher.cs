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
    public partial class Teacher : Form
    {
        public Teacher()
        {
            InitializeComponent();
        }
        public string department, id;
        public void FiledsForm_Fill()
        {
            textBox1.Text = Form1.ds.Tables["Преподаватели"].Rows[n]["Код_преподавателя"].ToString();
            textBox2.Text = Form1.ds.Tables["Преподаватели"].Rows[n]["ФИО"].ToString();
            dateTimePicker1.Text = Form1.ds.Tables["Преподаватели"].Rows[n]["Дата_рождения"].ToString();
            comboBox1.Text = Form1.ds.Tables["Преподаватели"].Rows[n]["Пол"].ToString();
            id = Form1.ds.Tables["Преподаватели"].Rows[n]["Код_кафедры"].ToString();
            OleDbCommand command = Form1.connection.CreateCommand();
            command.CommandText = "SELECT * FROM Кафедры";
            Form1.connection.Open();
            OleDbDataReader reader = command.ExecuteReader();
            while (reader.Read())
                if (reader["Код_кафедры"].ToString() == id)
                    department = reader["Кафедра"].ToString();
            Form1.connection.Close();
            comboBox2.Text = department;
        }
        public void FiledsForm_Clear()
        {
            textBox2.Text = "";
            dateTimePicker1.Text = DateTime.Now.ToString();
            comboBox1.Text = "";
            comboBox2.Text = "";
            OleDbCommand command = Form1.connection.CreateCommand();
            command.CommandText = "SELECT MAX(Код_преподавателя) FROM Преподаватели";
            Form1.connection.Open();
            OleDbDataReader reader = command.ExecuteReader();
            reader.Read();
            textBox1.Text = (Convert.ToInt32(reader[0].ToString()) + 1).ToString();
            Form1.connection.Close();
        }
        int n = 0;
        private void Teacher_Load (object sender, EventArgs e)
        {
            textBox1.Enabled = false;
            Form1.connection.Open();
            OleDbDataAdapter da1 = new OleDbDataAdapter("SELECT * FROM Преподаватели ORDER BY Код_преподавателя", Form1.connection);
            if (Form1.ds.Tables["Преподаватели"] != null) Form1.ds.Tables["Преподаватели"].Clear();
            da1.Fill(Form1.ds, "Преподаватели");
            OleDbCommand command = Form1.connection.CreateCommand();
            command.CommandText = "SELECT * FROM Кафедры";
            OleDbDataReader reader = command.ExecuteReader();
            while (reader.Read())
                comboBox2.Items.Add(reader["Кафедра"]);
            comboBox1.Items.Add("Муж");
            comboBox1.Items.Add("Жен");
            Form1.connection.Close();
            if (Form1.ds.Tables["Преподаватели"].Rows.Count > n)
            {
                FiledsForm_Fill();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            OleDbCommand command = Form1.connection.CreateCommand();
            command.CommandText = "SELECT * FROM Кафедры";
            Form1.connection.Open();
            OleDbDataReader reader = command.ExecuteReader();
            while (reader.Read())
                if (reader["Кафедра"].ToString() == comboBox2.Text)
                    id = reader["Код_кафедры"].ToString();
            if (n == Form1.ds.Tables["Преподаватели"].Rows.Count)
            {
                command = Form1.connection.CreateCommand();
                command.CommandText = "INSERT INTO Преподаватели VALUES (" + textBox1.Text + ", '" + textBox2.Text + "', '" + dateTimePicker1.Value.ToShortDateString() + "', '" + comboBox1.Text + "', " + id + ")";
                try
                {
                    command.ExecuteNonQuery();
                }
                catch (OleDbException)
                {
                    MessageBox.Show("Добавление экземпляра не было успешно проведено из-за неуказания его данных или" + " несоответствия их типов или попытки добавить экземпляр с уже используемым кодом!!!", "Ошибка");
                    Form1.connection.Close();
                    return;
                }
                Form1.connection.Close();
                textBox1.Enabled = false;
                Form1.ds.Tables["Преподаватели"].Rows.Add(new object[] { textBox1.Text, textBox2.Text, dateTimePicker1.Value.ToShortDateString(), comboBox1.Text, id });
            }
            else
            {
                command = Form1.connection.CreateCommand();
                command.CommandText = "UPDATE Преподаватели SET ФИО = '" + textBox2.Text + "', Дата_рождения = '" + dateTimePicker1.Value.ToShortDateString() + "', Пол = '" + comboBox1.Text + "', Код_кафедры = " + id + " WHERE Код_преподавателя = " + textBox1.Text;
                try
                {
                    command.ExecuteNonQuery();
                }
                catch (OleDbException)
                {
                    MessageBox.Show("Изменения не были успешно сохранены из-за несовпадения типов значений!!!", "Ошибка");
                    Form1.connection.Close();
                    return;
                }
                Form1.connection.Close();
                Form1.ds.Tables["Преподаватели"].Rows.Add(new object[] { textBox1.Text, textBox2.Text, dateTimePicker1.Value.ToShortDateString(), comboBox1.Text, id });
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            string message = "Вы точно хотите удалить из картотеки преподавателя " + textBox1.Text + "?";
            string caption = "Удаление";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(message, caption, buttons);
            if (result == DialogResult.No) { return; }
            string sql = "DELETE FROM Преподаватели WHERE Код_преподавателя = " + textBox1.Text;
            OleDbCommand command = new OleDbCommand(sql, Form1.connection);
            Form1.connection.Open();
            command.ExecuteNonQuery();
            Form1.connection.Close();
            try
            {
                Form1.ds.Tables["Преподаватели"].Rows.RemoveAt(n);
            }
            catch (IndexOutOfRangeException)
            {
                MessageBox.Show("Удаление не было выполнено из-за указания несуществующего экземпляра", "Ошибка");
                return;
            }
            if (Form1.ds.Tables["Преподаватели"].Rows.Count > n)
            {
                FiledsForm_Fill();
            }
            else
            {
                FiledsForm_Clear();
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (n > 0)
            {
                n--;
                FiledsForm_Fill();
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            if (n < Form1.ds.Tables["Преподаватели"].Rows.Count) n++;
            if (Form1.ds.Tables["Преподаватели"].Rows.Count > n)
                FiledsForm_Fill();
            else
                FiledsForm_Clear();
        }
        private void button5_Click(object sender, EventArgs e)
        {
            n = 0;
            if (Form1.ds.Tables["Преподаватели"].Rows.Count > n)
            {
                FiledsForm_Fill();
            }
        }
        private void button6_Click(object sender, EventArgs e)
        {
            n = Form1.ds.Tables["Преподаватели"].Rows.Count;
            FiledsForm_Clear();
        }
    }
}
