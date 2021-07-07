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
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }
        public static int n = -1;
        private void Form5_Load(object sender, EventArgs e)
        {
            Form1.connection.Open();
            OleDbDataAdapter da1 = new OleDbDataAdapter("SELECT * FROM Специальности", Form1.connection);
            if (Form1.ds.Tables["Специальности"] != null) Form1.ds.Tables["Специальности"].Clear();
            da1.Fill(Form1.ds, "Специальности");
            Form1.connection.Close();
            dataGridView1.DataSource = Form1.ds.Tables["Специальности"];
            dataGridView1.CurrentCell = null;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Form1.ds.Tables["Дисциплины_специальности"].Rows.Count; i++)
                if (Form1.ds.Tables["Специальности"].Rows[n]["Код_специальности"].ToString() == Form1.ds.Tables["Дисциплины_специальности"].Rows[i]["Код_специальности"].ToString())
                {
                    MessageBox.Show("Данная специальность уже присутствует в таблице!!!", "Ошибка");
                    dataGridView1.CurrentCell = null;
                    return;
                }
            if (n >= 0)
            {
                OleDbCommand command = Form1.connection.CreateCommand();
                command.CommandText = "INSERT INTO Дисциплины_специальности VALUES (" + Form1.ds.Tables["Дисциплины"].Rows[Discipline.n]["Код_дисциплины"].ToString() + ", " + Form1.ds.Tables["Специальности"].Rows[n]["Код_специальности"].ToString() + ")";
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
                Form1.ds.Tables["Дисциплины_специальности"].Rows.Add(new object[] { Form1.ds.Tables["Специальности"].Rows[n]["Код_специальности"].ToString(), Form1.ds.Tables["Специальности"].Rows[n]["Специальность"].ToString(), Form1.ds.Tables["Специальности"].Rows[n]["Код_кафедры"].ToString() });
            }
            else
            {
                MessageBox.Show("Не указан редактируемый экземпляр!!!", "Ошибка");
                return;
            }
            dataGridView1.CurrentCell = null;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (n >= 0)
            {
                string message = "Вы точно хотите удалить из картотеки специальности " + Form1.ds.Tables["Специальности"].Rows[n]["Код_специальности"].ToString() + "?";
                string caption = "Удаление";
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult result = MessageBox.Show(message, caption, buttons);
                if (result == DialogResult.No) { return; }
                string sql = "DELETE FROM Дисциплины_специальности WHERE Код_специальности = " + Form1.ds.Tables["Специальности"].Rows[n]["Код_специальности"].ToString() + " AND Код_дисциплины = " + Form1.ds.Tables["Дисциплины"].Rows[Discipline.n]["Код_дисциплины"].ToString();
                OleDbCommand command = new OleDbCommand(sql, Form1.connection);
                Form1.connection.Open();
                command.ExecuteNonQuery();
                Form1.connection.Close();
                int kod = -1;
                for (int i = 0; i < Form1.ds.Tables["Дисциплины_специальности"].Rows.Count; i++)
                {
                    if (Form1.ds.Tables["Специальности"].Rows[n]["Код_специальности"].ToString() == Form1.ds.Tables["Дисциплины_специальности"].Rows[i]["Код_специальности"].ToString())
                    {
                        kod = i;
                        break;
                    }
                }
                try
                {
                    Form1.ds.Tables["Дисциплины_специальности"].Rows.RemoveAt(kod);
                }
                catch (IndexOutOfRangeException)
                {
                    MessageBox.Show("Удаление не было выполнено из-за указания несуществующего экземпляра", "Ошибка");
                    dataGridView1.CurrentCell = null;
                    return;
                }
            }
            else
            {
                MessageBox.Show("Не указан редактируемый экземпляр!!!", "Ошибка");
                dataGridView1.CurrentCell = null;
                return;
            }
            dataGridView1.CurrentCell = null;
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            n = dataGridView1.CurrentRow.Index;
        }
    }
}
