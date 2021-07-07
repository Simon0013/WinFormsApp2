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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            Student student = new Student();
            student.Show();
        }
        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            Teacher teacher = new Teacher();
            teacher.Show();
        }
        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            Specialty specialty = new Specialty();
            specialty.Show();
        }
        private void toolStripMenuItem8_Click(object sender, EventArgs e)
        {
            Department department = new Department();
            department.Show();
        }
        private void toolStripMenuItem9_Click(object sender, EventArgs e)
        {
            Discipline discipline = new Discipline();
            discipline.Show();
        }
        private void toolStripMenuItem10_Click(object sender, EventArgs e)
        {
            Student_S sstudent = new Student_S();
            sstudent.Show();
        }
        private void toolStripMenuItem11_Click(object sender, EventArgs e)
        {
            Teacher_S steacher = new Teacher_S();
            steacher.Show();
        }
        private void toolStripMenuItem12_Click(object sender, EventArgs e)
        {
            Specialty_S sspecialty = new Specialty_S();
            sspecialty.Show();
        }
        private void toolStripMenuItem13_Click(object sender, EventArgs e)
        {
            Discipline_S sdiscipline = new Discipline_S();
            sdiscipline.Show();
        }
        private void toolStripMenuItem14_Click(object sender, EventArgs e)
        {
            AboutSystem about = new AboutSystem();
            about.Show();
        }
        private void toolStripMenuItem15_Click(object sender, EventArgs e)
        {
            Info info = new Info();
            info.Show();
        }
        private void toolStripMenuItem16_Click(object sender, EventArgs e)
        {
            Settings settings = new Settings();
            settings.Show();
        }
        private void toolStripMenuItem17_Click(object sender, EventArgs e)
        {
            string message = "Вы точно хотите хотите выйти из системы?", caption = "Выход";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            if (MessageBox.Show(message, caption, buttons) == DialogResult.No) return;
            Close();
        }
        private void toolStripMenuItem18_Click(object sender, EventArgs e)
        {
            string message = "Вы точно хотите хотите удалить свою учётную запись?", caption = "Удаление профиля";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            if (MessageBox.Show(message, caption, buttons) == DialogResult.No) return;
            OleDbCommand command = Form1.connection.CreateCommand();
            command.CommandText = "DELETE FROM Пользователи WHERE Логин = '" + Form1.login + "'";
            Form1.connection.Open();
            command.ExecuteNonQuery();
            MessageBox.Show("Учётная запись удалена!", "Удаление профиля");
            Close();
        }
        private void toolStripMenuItem19_Click(object sender, EventArgs e)
        {
            Journal journal = new Journal();
            journal.Show();
        }
        private void toolStripMenuItem20_Click(object sender, EventArgs e)
        {
            Group group = new Group();
            group.Show();
        }
        private void toolStripMenuItem21_Click(object sender, EventArgs e)
        {
            Profile profile = new Profile();
            profile.Show();
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            if (Form1.root != "admin")
            {
                toolStripMenuItem7.Enabled = false;
                toolStripMenuItem8.Enabled = false;
            }
        }
    }
}
