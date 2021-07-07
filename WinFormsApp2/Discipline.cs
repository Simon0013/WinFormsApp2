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
    public partial class Discipline : Form
    {
        public Discipline()
        {
            InitializeComponent();
        }
        public string department, id;
        public void FiledsForm_Fill()
        {
            textBox1.Text = Form1.ds.Tables["Дисциплины"].Rows[n]["Код_дисциплины"].ToString();
            textBox2.Text = Form1.ds.Tables["Дисциплины"].Rows[n]["Дисциплина"].ToString();
            id = Form1.ds.Tables["Дисциплины"].Rows[n]["Код_кафедры"].ToString();
            OleDbCommand command = Form1.connection.CreateCommand();
            command.CommandText = "SELECT * FROM Кафедры";
            Form1.connection.Open();
            OleDbDataReader reader = command.ExecuteReader();
            while (reader.Read())
                if (reader["Код_кафедры"].ToString() == id)
                    department = reader["Кафедра"].ToString();
            Form1.connection.Close();
            comboBox1.Text = department;
            Form1.connection.Open();
            OleDbDataAdapter da1 = new OleDbDataAdapter("SELECT Преподаватели.Код_преподавателя AS Код_преподавателя, ФИО, Код_кафедры FROM Преподаватели, Преподаватели_дисциплины WHERE Преподаватели_дисциплины.Код_преподавателя = Преподаватели.Код_преподавателя AND Преподаватели_дисциплины.Код_дисциплины = " + textBox1.Text, Form1.connection);
            if (Form1.ds.Tables["Преподаватели_дисциплины"] != null) Form1.ds.Tables["Преподаватели_дисциплины"].Clear();
            da1.Fill(Form1.ds, "Преподаватели_дисциплины");
            da1 = new OleDbDataAdapter("SELECT Специальности.Код_специальности AS Код_специальности, Специальность, Код_кафедры FROM Специальности, Дисциплины_специальности WHERE Дисциплины_специальности.Код_специальности = Специальности.Код_специальности AND Дисциплины_специальности.Код_дисциплины = " + textBox1.Text, Form1.connection);
            if (Form1.ds.Tables["Дисциплины_специальности"] != null) Form1.ds.Tables["Дисциплины_специальности"].Clear();
            da1.Fill(Form1.ds, "Дисциплины_специальности");
            dataGridView1.DataSource = Form1.ds.Tables["Преподаватели_дисциплины"];
            dataGridView1.Columns["Код_преподавателя"].Visible = false;
            dataGridView1.AutoResizeColumns();
            dataGridView2.DataSource = Form1.ds.Tables["Дисциплины_специальности"];
            dataGridView2.Columns["Код_специальности"].Visible = false;
            dataGridView2.AutoResizeColumns();
            Form1.connection.Close();
            textBox1.Enabled = false;
        }
        public void FiledsForm_Clear()
        {
            textBox1.Text = "0";
            textBox2.Text = "";
            comboBox1.Text = "";
            Form1.ds.Tables["Преподаватели_дисциплины"].Clear();
            Form1.ds.Tables["Дисциплины_специальности"].Clear();
            textBox1.Enabled = true;
            textBox1.Focus();
            textBox1.ReadOnly = false;
        }
        public static int n = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            OleDbCommand command = Form1.connection.CreateCommand();
            command.CommandText = "SELECT * FROM Кафедры";
            Form1.connection.Open();
            OleDbDataReader reader = command.ExecuteReader();
            while (reader.Read())
                if (reader["Кафедра"].ToString() == comboBox1.Text)
                    id = reader["Код_кафедры"].ToString();
            if (n == Form1.ds.Tables["Дисциплины"].Rows.Count)
            {
                command = Form1.connection.CreateCommand();
                command.CommandText = "INSERT INTO Дисциплины VALUES (" + textBox1.Text + ", '" + textBox2.Text + "', " + id + ")";
                Form1.connection.Open();
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
                Form1.ds.Tables["Дисциплины"].Rows.Add(new object[] { textBox1.Text, textBox2.Text, id });
            }
            else
            {
                command = Form1.connection.CreateCommand();
                command.CommandText = "UPDATE Дисциплины SET Дисциплина = '" + textBox2.Text + "', Код_кафедры = " + id + " WHERE Код_дисциплины = " + textBox1.Text;
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
                Form1.ds.Tables["Дисциплины"].Rows.Add(new object[] { textBox1.Text, textBox2.Text, id });
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            string message = "Вы точно хотите удалить из картотеки дисциплину " + textBox1.Text + "?";
            string caption = "Удаление";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(message, caption, buttons);
            if (result == DialogResult.No) { return; }
            string sql = "DELETE FROM Дисциплины WHERE Код_дисциплины = " + textBox1.Text;
            OleDbCommand command = new OleDbCommand(sql, Form1.connection);
            Form1.connection.Open();
            command.ExecuteNonQuery();
            Form1.connection.Close();
            try
            {
                Form1.ds.Tables["Дисциплины"].Rows.RemoveAt(n);
            }
            catch (IndexOutOfRangeException)
            {
                MessageBox.Show("Удаление не было выполнено из-за указания несуществующего экземпляра", "Ошибка");
                return;
            }
            if (Form1.ds.Tables["Дисциплины"].Rows.Count > n)
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
            if (n < Form1.ds.Tables["Дисциплины"].Rows.Count) n++;
            if (Form1.ds.Tables["Дисциплины"].Rows.Count > n)
                FiledsForm_Fill();
            else
                FiledsForm_Clear();
        }
        private void button5_Click(object sender, EventArgs e)
        {
            n = 0;
            if (Form1.ds.Tables["Дисциплины"].Rows.Count > n)
            {
                FiledsForm_Fill();
            }
        }
        private void button6_Click(object sender, EventArgs e)
        {
            n = Form1.ds.Tables["Дисциплины"].Rows.Count;
            FiledsForm_Clear();
        }
        private void button7_Click(object sender, EventArgs e)
        {
            Form4 form = new Form4();
            form.Show();
        }
        private void button8_Click(object sender, EventArgs e)
        {
            Form5 form = new Form5();
            form.Show();
        }
        private void Discipline_Load(object sender, EventArgs e)
        {
            Form1.connection.Open();
            OleDbDataAdapter da1 = new OleDbDataAdapter("SELECT * FROM Дисциплины ORDER BY Код_дисциплины", Form1.connection);
            if (Form1.ds.Tables["Дисциплины"] != null) Form1.ds.Tables["Дисциплины"].Clear();
            da1.Fill(Form1.ds, "Дисциплины");
            OleDbCommand command = Form1.connection.CreateCommand();
            command.CommandText = "SELECT * FROM Кафедры";
            OleDbDataReader reader = command.ExecuteReader();
            while (reader.Read())
                comboBox1.Items.Add(reader["Кафедра"]);
            Form1.connection.Close();
            if (Form1.ds.Tables["Дисциплины"].Rows.Count > n)
            {
                FiledsForm_Fill();
            }
        }
    }
}
