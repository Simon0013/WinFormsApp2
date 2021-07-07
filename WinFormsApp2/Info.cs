using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WinFormsApp2
{
    public partial class Info : Form
    {
        public Info()
        {
            InitializeComponent();
        }
        DataGridViewColumn column1 = new DataGridViewColumn();
        DataGridViewColumn column2 = new DataGridViewColumn();
        DataGridViewColumn column3 = new DataGridViewColumn();
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Rows.Add("Чем отличается администраторская учётная запись от учётной записи обычного сотрудника?", "Для сотрудника недоступны для редактирования данные о специальностях и кафедрах учебного заведения. Администратор имеет права на все данные базы", "Для просмотра данных о специальностях в режиме сотрудника воспользуетесь разделом \"Запросы\"");
            dataGridView1.Rows.Add("Можно ли сменить тип своей учётной записи?", "Да, как и все остальные данные профиля", "Для этого необходимо открыть окно \"Настройка профиля\" в разделе \"Профиль\"");
            dataGridView1.Rows.Add("Можно ли восстановить учётную запись после её удаления?", "При удалении учётной записи её логин и пароль удаляются из базы данных системы", "Чтобы вернуть доступ к системе, необходимо заново пройти регистрацию");
            dataGridView1.Rows.Add("Имеет ли доступ к учётной записи кто-то ещё помимо её владельца?", "Все логины и пароли сохраняются в специальной таблице базы данных, к которой не имеют доступ пользователи системы", "Для наибольшей безопасности придумывайте надёжные логин и пароль при регистрации");
            dataGridView1.AutoResizeColumns();
            dataGridView1.CurrentCell = null;
        }
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Rows.Add("В картотеках студентов, преподавателей и кафедр поле для кода недоступно для редактирования", "Это поле в данном случае служит лишь для визуального представления порядка записи в таблице базы данных. Для перечисленных картотек код генерируется автоматически базой данных путём прибавления единицы к коду последней записи", "-");
            dataGridView1.Rows.Add("Почему нельзя добавить запись с другими данными, но с кодом, который уже есть в картотеке?", "Для всех объектов картотеки код служит для связи между ними в базе данных, если коды разных записей в одной таблице будут совпадать, система не сможет отличить их друг от друга, что ведёт к некорректному представлению данных", "-");
            dataGridView1.AutoResizeColumns();
            dataGridView1.CurrentCell = null;
        }
        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Rows.Add("Можно ли удалить одновременно несколько записей из журнала?", "Нет, такая возможность в этой системе не предусмотрена", "Лучше удалять записи по одной, чем незаметно для себя удалить то, что не нужно было удалять");
            dataGridView1.Rows.Add("Если удалить из картотеки студента, дисциплину или преподавателя, то нужно ли будет вручную после этого корректировать журнал?", "Нет. В таких случаях система автоматичеки удаляет из журнала все записи, которые связаны с удалёнными записями в картотеке", "-");
            dataGridView1.Rows.Add("Можно ли указать вид аттестации, не представленный в выпадающем списке?", "Вообще можно, но, скорее всего, это не будет соответствовать внутренней документации учебного заведения", "Перед тем, как добавлять вид аттестации, не представленный в выпадающем списке, согласуйте этот шаг с руководством");
            dataGridView1.AutoResizeColumns();
            dataGridView1.CurrentCell = null;
        }
        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Rows.Add("Почему в данных профиля не показывается пароль?", "Это сделано из соображений безопасности. Если Ваша учётная запись по какой-то причине будет взломана, злоумышленник не увидит Ваш пароль. Именно пароль является наиболее конфиденциальной частью профиля", "Чтобы не забыть пароль, запишите его на любом носителе информации, к которому не имеют доступ третьи лица");
            dataGridView1.Rows.Add("Если я хочу изменить лишь некоторые данные профиля (например, только пароль), то что надо указать в остальных полях, которые появляются после нажатия кнопки \"Изменить\"?", "Укажите текущие значения этих параметров. Если оставить их пустыми, произойдет ошибка", "-");
            dataGridView1.AutoResizeColumns();
            dataGridView1.CurrentCell = null;
        }
        private void Info_Load(object sender, EventArgs e)
        {
            column1.HeaderText = "Проблема (вопрос)";
            column1.Name = "Problem";
            column1.ReadOnly = true;
            column1.CellTemplate = new DataGridViewTextBoxCell();

            column2.HeaderText = "Причина проблемы (ответ на вопрос)";
            column2.Name = "Reason";
            column2.ReadOnly = true;
            column2.CellTemplate = new DataGridViewTextBoxCell();

            column3.HeaderText = "Возможные действия пользователя";
            column3.Name = "Answer";
            column3.ReadOnly = true;
            column3.CellTemplate = new DataGridViewTextBoxCell();

            dataGridView1.Columns.Add(column1);
            dataGridView1.Columns.Add(column2);
            dataGridView1.Columns.Add(column3);
            dataGridView1.AllowUserToAddRows = false;
        }
    }
}
