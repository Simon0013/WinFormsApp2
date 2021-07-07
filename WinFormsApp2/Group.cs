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
    public partial class Group : Form
    {
        public Group()
        {
            InitializeComponent();
        }
        public string speciality, id;
        public void FiledsForm_Fill()
        {
            textBox1.Text = Form1.ds.Tables["Группы"].Rows[n]["Группа"].ToString();
            textBox2.Text = Form1.ds.Tables["Группы"].Rows[n]["Курс"].ToString();
            id = Form1.ds.Tables["Группы"].Rows[n]["Код_профиля"].ToString();
            OleDbCommand command = Form1.connection.CreateCommand();
            command.CommandText = "SELECT * FROM Профили";
            Form1.connection.Open();
            OleDbDataReader reader = command.ExecuteReader();
            while (reader.Read())
                if (reader["Код_профиля"].ToString() == id)
                    speciality = reader["Профиль"].ToString();
            Form1.connection.Close();
            comboBox1.Text = speciality;
            textBox1.Enabled = false;
        }
        public void FiledsForm_Clear()
        {
            textBox1.Text = "0";
            textBox2.Text = "";
            comboBox1.Text = "";
            textBox1.Enabled = true;
            textBox1.Focus();
            textBox1.ReadOnly = false;
        }
        int n = 0;
        private void Group_Load(object sender, EventArgs e)
        {
            Form1.connection.Open();
            OleDbDataAdapter da1 = new OleDbDataAdapter("SELECT * FROM Группы ORDER BY Группа", Form1.connection);
            if (Form1.ds.Tables["Группы"] != null) Form1.ds.Tables["Группы"].Clear();
            da1.Fill(Form1.ds, "Группы");
            OleDbCommand command = Form1.connection.CreateCommand();
            command.CommandText = "SELECT * FROM Профили";
            OleDbDataReader reader = command.ExecuteReader();
            while (reader.Read())
                comboBox1.Items.Add(reader["Профиль"]);
            Form1.connection.Close();
            if (Form1.ds.Tables["Группы"].Rows.Count > n)
            {
                FiledsForm_Fill();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            OleDbCommand command = Form1.connection.CreateCommand();
            command.CommandText = "SELECT * FROM Профили";
            Form1.connection.Open();
            OleDbDataReader reader = command.ExecuteReader();
            while (reader.Read())
                if (reader["Профиль"].ToString() == comboBox1.Text)
                    id = reader["Код_профиля"].ToString();
            if (n == Form1.ds.Tables["Группы"].Rows.Count)
            {
                command = Form1.connection.CreateCommand();
                command.CommandText = "INSERT INTO Группы VALUES ('" + textBox1.Text + "', " + textBox2.Text + ", " + id + ")";
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
                Form1.ds.Tables["Группы"].Rows.Add(new object[] { textBox1.Text, textBox2.Text, id });
            }
            else
            {
                command = Form1.connection.CreateCommand();
                command.CommandText = "UPDATE Группы SET Курс = " + textBox2.Text + ", Код_профиля = " + id + " WHERE Группа = '" + textBox1.Text + "'";
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
                Form1.ds.Tables["Группы"].Rows.Add(new object[] { textBox1.Text, textBox2.Text, id });
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            string message = "Вы точно хотите удалить из картотеки группы " + textBox1.Text + "?";
            string caption = "Удаление";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(message, caption, buttons);
            if (result == DialogResult.No) { return; }
            string sql = "DELETE FROM Группы WHERE Группа = '" + textBox1.Text + "'";
            OleDbCommand command = new OleDbCommand(sql, Form1.connection);
            Form1.connection.Open();
            command.ExecuteNonQuery();
            Form1.connection.Close();
            try
            {
                Form1.ds.Tables["Группы"].Rows.RemoveAt(n);
            }
            catch (IndexOutOfRangeException)
            {
                MessageBox.Show("Удаление не было выполнено из-за указания несуществующего экземпляра", "Ошибка");
                return;
            }
            if (Form1.ds.Tables["Группы"].Rows.Count > n)
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
            if (n < Form1.ds.Tables["Группы"].Rows.Count) n++;
            if (Form1.ds.Tables["Группы"].Rows.Count > n)
                FiledsForm_Fill();
            else
                FiledsForm_Clear();
        }
        private void button5_Click(object sender, EventArgs e)
        {
            n = 0;
            if (Form1.ds.Tables["Группы"].Rows.Count > n)
            {
                FiledsForm_Fill();
            }
        }
        private void button6_Click(object sender, EventArgs e)
        {
            n = Form1.ds.Tables["Группы"].Rows.Count;
            FiledsForm_Clear();
        }
    }
}
