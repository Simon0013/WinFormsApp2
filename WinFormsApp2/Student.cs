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
    public partial class Student : Form
    {
        public Student()
        {
            InitializeComponent();
        }
        public void FiledsForm_Fill()
        {
            textBox1.Text = Form1.ds.Tables["Студенты"].Rows[n]["Код_студента"].ToString();
            textBox2.Text = Form1.ds.Tables["Студенты"].Rows[n]["ФИО"].ToString();
            comboBox1.Text = Form1.ds.Tables["Студенты"].Rows[n]["Группа"].ToString();
            dateTimePicker1.Text = Form1.ds.Tables["Студенты"].Rows[n]["Дата_рождения"].ToString();
            comboBox2.Text = Form1.ds.Tables["Студенты"].Rows[n]["Пол"].ToString();
            textBox3.Text = Form1.ds.Tables["Студенты"].Rows[n]["Адрес"].ToString();
            textBox4.Text = Form1.ds.Tables["Студенты"].Rows[n]["Зачетная_книжка"].ToString();
        }
        public void FiledsForm_Clear()
        {
            textBox2.Text = "";
            comboBox1.Text = "";
            dateTimePicker1.Text = DateTime.Now.ToString();
            comboBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            OleDbCommand command = Form1.connection.CreateCommand();
            command.CommandText = "SELECT MAX(Код_студента) FROM Студенты";
            Form1.connection.Open();
            OleDbDataReader reader = command.ExecuteReader();
            reader.Read();
            textBox1.Text = (Convert.ToInt32(reader[0].ToString()) + 1).ToString();
            Form1.connection.Close();
        }
        int n = 0;
        private void Student_Load(object sender, EventArgs e)
        {
            textBox1.Enabled = false;
            Form1.connection.Open();
            OleDbDataAdapter da1 = new OleDbDataAdapter("SELECT * FROM Студенты ORDER BY Код_студента", Form1.connection);
            if (Form1.ds.Tables["Студенты"] != null) Form1.ds.Tables["Студенты"].Clear();
            da1.Fill(Form1.ds, "Студенты");
            OleDbCommand command = Form1.connection.CreateCommand();
            command.CommandText = "SELECT * FROM Группы";
            OleDbDataReader reader = command.ExecuteReader();
            while (reader.Read())
                comboBox1.Items.Add(reader["Группа"]);
            comboBox2.Items.Add("Муж");
            comboBox2.Items.Add("Жен");
            Form1.connection.Close();
            if (Form1.ds.Tables["Студенты"].Rows.Count > n)
            {
                FiledsForm_Fill();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (n == Form1.ds.Tables["Студенты"].Rows.Count)
            {
                OleDbCommand command = Form1.connection.CreateCommand();
                command.CommandText = "INSERT INTO Студенты VALUES (" + textBox1.Text + ", '" + textBox2.Text + "', '" + comboBox1.Text + "', '" + dateTimePicker1.Value.ToShortDateString() + "', '" + comboBox2.Text + "', '" + textBox3.Text + "', " + textBox4.Text + ")";
                Form1.connection.Open();
                try
                {
                    command.ExecuteNonQuery();
                }
                catch (OleDbException)
                {
                    MessageBox.Show("Добавление экземпляра не было успешно проведено из-за неуказания его данных или несоответствия их типов или попытки добавить экземпляр с уже используемым кодом!!!", "Ошибка");
                    Form1.connection.Close();
                    return;
                }
                Form1.connection.Close();
                textBox1.Enabled = false;
                Form1.ds.Tables["Студенты"].Rows.Add(new object[] { textBox1.Text, textBox2.Text, comboBox1.Text, dateTimePicker1.Value.ToShortDateString(), comboBox2.Text, textBox3.Text, textBox4.Text });
            }
            else
            {
                OleDbCommand command = Form1.connection.CreateCommand();
                command.CommandText = "UPDATE Студенты SET ФИО = '" + textBox2.Text + "', Группа = '" + comboBox1.Text + "', Дата_рождения = '" + dateTimePicker1.Value.ToShortDateString() + "', Пол = '" + comboBox2.Text + "', Адрес = '" + textBox3.Text + "', Зачетная_книжка = " + textBox4.Text + " WHERE Код_студента = " + textBox1.Text;
                Form1.connection.Open();
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
                Form1.ds.Tables["Студенты"].Rows.Add(new object[] { textBox1.Text, textBox2.Text, comboBox1.Text, dateTimePicker1.Value.ToShortDateString(), comboBox2.Text, textBox3.Text, textBox4.Text });
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            string message = "Вы точно хотите удалить из картотеки студента " + textBox1.Text + "?";
            string caption = "Удаление";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(message, caption, buttons);
            if (result == DialogResult.No) { return; }
            string sql = "DELETE FROM Студенты WHERE Код_студента = " + textBox1.Text;
            OleDbCommand command = new OleDbCommand(sql, Form1.connection);
            Form1.connection.Open();
            command.ExecuteNonQuery();
            Form1.connection.Close();
            try
            {
                Form1.ds.Tables["Студенты"].Rows.RemoveAt(n);
            }
            catch (IndexOutOfRangeException)
            {
                MessageBox.Show("Удаление не было выполнено из-за указания несуществующего экземпляра", "Ошибка");
                return;
            }
            if (Form1.ds.Tables["Студенты"].Rows.Count > n)
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
            if (n < Form1.ds.Tables["Студенты"].Rows.Count) n++;
            if (Form1.ds.Tables["Студенты"].Rows.Count > n)
                FiledsForm_Fill();
            else
                FiledsForm_Clear();
        }
        private void button5_Click(object sender, EventArgs e)
        {
            n = 0;
            if (Form1.ds.Tables["Студенты"].Rows.Count > n)
            {
                FiledsForm_Fill();
            }
        }
        private void button6_Click(object sender, EventArgs e)
        {
            n = Form1.ds.Tables["Студенты"].Rows.Count;
            FiledsForm_Clear();
        }
    }
}
