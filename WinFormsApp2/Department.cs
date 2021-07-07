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
    public partial class Department : Form
    {
        public Department()
        {
            InitializeComponent();
        }
        public void FiledsForm_Fill()
        {
            textBox1.Text = Form1.ds.Tables["Кафедры"].Rows[n]["Код_кафедры"].ToString();
            textBox2.Text = Form1.ds.Tables["Кафедры"].Rows[n]["Кафедра"].ToString();
            textBox3.Text = Form1.ds.Tables["Кафедры"].Rows[n]["ФИО_заведующего"].ToString();
        }
        public void FiledsForm_Clear()
        {
            textBox2.Text = "";
            textBox3.Text = "";
            OleDbCommand command = Form1.connection.CreateCommand();
            command.CommandText = "SELECT MAX(Код_кафедры) FROM Кафедры";
            Form1.connection.Open();
            OleDbDataReader reader = command.ExecuteReader();
            reader.Read();
            textBox1.Text = (Convert.ToInt32(reader[0].ToString()) + 1).ToString();
            Form1.connection.Close();
        }
        int n = 0;
        private void Department_Load(object sender, EventArgs e)
        {
            textBox1.Enabled = false;
            Form1.connection.Open();
            OleDbDataAdapter da1 = new OleDbDataAdapter("SELECT * FROM Кафедры ORDER BY Код_кафедры", Form1.connection);
            if (Form1.ds.Tables["Кафедры"] != null) Form1.ds.Tables["Кафедры"].Clear();
            da1.Fill(Form1.ds, "Кафедры");
            Form1.connection.Close();
            if (Form1.ds.Tables["Кафедры"].Rows.Count > n)
            {
                FiledsForm_Fill();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (n == Form1.ds.Tables["Кафедры"].Rows.Count)
            {
                OleDbCommand command = Form1.connection.CreateCommand();
                command.CommandText = "INSERT INTO Кафедры VALUES (" + textBox1.Text + ", '" + textBox2.Text + "', '" + textBox3.Text + "')";
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
                Form1.ds.Tables["Кафедры"].Rows.Add(new object[] { textBox1.Text, textBox2.Text, textBox3.Text });
            }
            else
            {
                OleDbCommand command = Form1.connection.CreateCommand();
                command.CommandText = "UPDATE Кафедры SET Кафедра = '" + textBox2.Text + "', ФИО_заведующего = '" + textBox3.Text + "' WHERE Код_кафедры = " + textBox1.Text;
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
                Form1.ds.Tables["Кафедры"].Rows.Add(new object[] { textBox1.Text, textBox2.Text, textBox3.Text });
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            string message = "Вы точно хотите удалить из картотеки кафедру " + textBox1.Text + "?";
            string caption = "Удаление";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(message, caption, buttons);
            if (result == DialogResult.No) { return; }
            string sql = "DELETE FROM Кафедры WHERE Код_кафедры = " + textBox1.Text;
            OleDbCommand command = new OleDbCommand(sql, Form1.connection);
            Form1.connection.Open();
            command.ExecuteNonQuery();
            Form1.connection.Close();
            try
            {
                Form1.ds.Tables["Кафедры"].Rows.RemoveAt(n);
            }
            catch (IndexOutOfRangeException)
            {
                MessageBox.Show("Удаление не было выполнено из-за указания несуществующего экземпляра", "Ошибка");
                return;
            }
            if (Form1.ds.Tables["Кафедры"].Rows.Count > n)
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
            if (n < Form1.ds.Tables["Кафедры"].Rows.Count) n++;
            if (Form1.ds.Tables["Кафедры"].Rows.Count > n)
                FiledsForm_Fill();
            else
                FiledsForm_Clear();
        }
        private void button5_Click(object sender, EventArgs e)
        {
            n = 0;
            if (Form1.ds.Tables["Кафедры"].Rows.Count > n)
            {
                FiledsForm_Fill();
            }
        }
        private void button6_Click(object sender, EventArgs e)
        {
            n = Form1.ds.Tables["Кафедры"].Rows.Count;
            FiledsForm_Clear();
        }
    }
}
