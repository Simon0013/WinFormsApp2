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
    public partial class Profile : Form
    {
        public Profile()
        {
            InitializeComponent();
        }
        public string speciality, id;
        public void FiledsForm_Fill()
        {
            textBox1.Text = Form1.ds.Tables["Профили"].Rows[n]["Код_профиля"].ToString();
            textBox2.Text = Form1.ds.Tables["Профили"].Rows[n]["Профиль"].ToString();
            id = Form1.ds.Tables["Профили"].Rows[n]["Код_специальности"].ToString();
            OleDbCommand command = Form1.connection.CreateCommand();
            command.CommandText = "SELECT * FROM Специальности";
            Form1.connection.Open();
            OleDbDataReader reader = command.ExecuteReader();
            while (reader.Read())
                if (reader["Код_специальности"].ToString() == id)
                    speciality = reader["Специальность"].ToString();
            Form1.connection.Close();
            comboBox1.Text = speciality;
        }
        public void FiledsForm_Clear()
        {
            textBox1.Text = "0";
            textBox2.Text = "";
            comboBox1.Text = "";
            OleDbCommand command = Form1.connection.CreateCommand();
            command.CommandText = "SELECT MAX(Код_профиля) FROM Профили";
            Form1.connection.Open();
            OleDbDataReader reader = command.ExecuteReader();
            reader.Read();
            textBox1.Text = (Convert.ToInt32(reader[0].ToString()) + 1).ToString();
            Form1.connection.Close();
        }
        int n = 0;
        private void Profile_Load(object sender, EventArgs e)
        {
            textBox1.Enabled = false;
            Form1.connection.Open();
            OleDbDataAdapter da1 = new OleDbDataAdapter("SELECT * FROM Профили ORDER BY Код_профиля", Form1.connection);
            if (Form1.ds.Tables["Профили"] != null) Form1.ds.Tables["Профили"].Clear();
            da1.Fill(Form1.ds, "Профили");
            OleDbCommand command = Form1.connection.CreateCommand();
            command.CommandText = "SELECT * FROM Специальности";
            OleDbDataReader reader = command.ExecuteReader();
            while (reader.Read())
                comboBox1.Items.Add(reader["Специальность"]);
            Form1.connection.Close();
            if (Form1.ds.Tables["Профили"].Rows.Count > n)
            {
                FiledsForm_Fill();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            OleDbCommand command = Form1.connection.CreateCommand();
            command.CommandText = "SELECT * FROM Специальности";
            Form1.connection.Open();
            OleDbDataReader reader = command.ExecuteReader();
            while (reader.Read())
                if (reader["Специальность"].ToString() == comboBox1.Text)
                    id = reader["Код_специальности"].ToString();
            if (n == Form1.ds.Tables["Профили"].Rows.Count)
            {
                command = Form1.connection.CreateCommand();
                command.CommandText = "INSERT INTO Профили VALUES (" + textBox1.Text + ", '" + textBox2.Text + "', " + id + ")";
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
                Form1.ds.Tables["Профили"].Rows.Add(new object[] { textBox1.Text, textBox2.Text, id });
            }
            else
            {
                command = Form1.connection.CreateCommand();
                command.CommandText = "UPDATE Профили SET Профиль = '" + textBox2.Text + "', Код_специальности = " + id + " WHERE Код_профиля = " + textBox1.Text;
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
                Form1.ds.Tables["Профили"].Rows.Add(new object[] { textBox1.Text, textBox2.Text, id });
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            string message = "Вы точно хотите удалить из картотеки профиль " + textBox1.Text + "?";
            string caption = "Удаление";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(message, caption, buttons);
            if (result == DialogResult.No) { return; }
            string sql = "DELETE FROM Профили WHERE Код_профиля = " + textBox1.Text;
            OleDbCommand command = new OleDbCommand(sql, Form1.connection);
            Form1.connection.Open();
            command.ExecuteNonQuery();
            Form1.connection.Close();
            try
            {
                Form1.ds.Tables["Профили"].Rows.RemoveAt(n);
            }
            catch (IndexOutOfRangeException)
            {
                MessageBox.Show("Удаление не было выполнено из-за указания несуществующего экземпляра", "Ошибка");
                return;
            }
            if (Form1.ds.Tables["Профили"].Rows.Count > n)
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
            if (n < Form1.ds.Tables["Профили"].Rows.Count) n++;
            if (Form1.ds.Tables["Профили"].Rows.Count > n)
                FiledsForm_Fill();
            else
                FiledsForm_Clear();
        }
        private void button5_Click(object sender, EventArgs e)
        {
            n = 0;
            if (Form1.ds.Tables["Профили"].Rows.Count > n)
            {
                FiledsForm_Fill();
            }
        }
        private void button6_Click(object sender, EventArgs e)
        {
            n = Form1.ds.Tables["Профили"].Rows.Count;
            FiledsForm_Clear();
        }
    }
}
