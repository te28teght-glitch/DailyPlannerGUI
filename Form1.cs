using System;
using System.Drawing;
using System.Windows.Forms;

namespace DailyPlannerGUI
{
    public partial class Form1 : Form
    {
        private ListBox tasksListBox;
        private TextBox titleTextBox;
        private DateTimePicker startTimePicker;
        private DateTimePicker endTimePicker;
        private Button addButton;
        private Button removeButton;
        private TaskManager taskManager;

        // Свойство для доступа к taskManager из Program.cs
        public TaskManager TaskManager => taskManager;

        public Form1()
        {
            taskManager = new TaskManager();
            InitializeCustomComponents();
        }

        private void InitializeCustomComponents()
        {
            // Список задач
            tasksListBox = new ListBox();
            tasksListBox.Location = new Point(20, 20);
            tasksListBox.Size = new Size(300, 200);
            tasksListBox.Font = new Font("Consolas", 10);
            tasksListBox.MouseDoubleClick += TasksListBox_MouseDoubleClick;

            // Поле для ввода названия
            titleTextBox = new TextBox();
            titleTextBox.Location = new Point(20, 240);
            titleTextBox.Size = new Size(300, 20);
            titleTextBox.PlaceholderText = "Название задачи";

            // Время начала
            startTimePicker = new DateTimePicker();
            startTimePicker.Location = new Point(20, 270);
            startTimePicker.Size = new Size(140, 20);
            startTimePicker.Format = DateTimePickerFormat.Time;
            startTimePicker.ShowUpDown = true;

            // Время окончания
            endTimePicker = new DateTimePicker();
            endTimePicker.Location = new Point(180, 270);
            endTimePicker.Size = new Size(140, 20);
            endTimePicker.Format = DateTimePickerFormat.Time;
            endTimePicker.ShowUpDown = true;

            // Кнопка "Добавить"
            addButton = new Button();
            addButton.Text = "Добавить задачу";
            addButton.Location = new Point(20, 300);
            addButton.Size = new Size(140, 30);
            addButton.Click += AddButton_Click;

            // Кнопка "Удалить"
            removeButton = new Button();
            removeButton.Text = "Удалить выбранную";
            removeButton.Location = new Point(180, 300);
            removeButton.Size = new Size(140, 30);
            removeButton.Click += RemoveButton_Click;

            // Добавляем элементы на форму
            Controls.Add(tasksListBox);
            Controls.Add(titleTextBox);
            Controls.Add(startTimePicker);
            Controls.Add(endTimePicker);
            Controls.Add(addButton);
            Controls.Add(removeButton);

            // Обновляем список задач
            RefreshTasksList();
        }

        private void RefreshTasksList()
        {
            tasksListBox.Items.Clear();
            var tasks = taskManager.GetAllTasks();

            for (int i = 0; i < tasks.Count; i++)
            {
                string status = tasks[i].IsCompleted ? "✓" : "✗";
                tasksListBox.Items.Add($"{i + 1}. {tasks[i].GetDescription()} [{status}]");
            }
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(titleTextBox.Text))
            {
                MessageBox.Show("Введите название задачи!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Task newTask = new Task();
            newTask.Title = titleTextBox.Text;
            newTask.StartTime = startTimePicker.Value;
            newTask.EndTime = endTimePicker.Value;
            newTask.IsCompleted = false;

            taskManager.AddTask(newTask);
            titleTextBox.Text = "";
            RefreshTasksList();

            MessageBox.Show("Задача добавлена!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void RemoveButton_Click(object sender, EventArgs e)
        {
            if (tasksListBox.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите задачу для удаления!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int index = tasksListBox.SelectedIndex;
            bool result = taskManager.RemoveTask(index);

            if (result)
            {
                RefreshTasksList();
                MessageBox.Show("Задача удалена!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Не удалось удалить задачу!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void RefreshTasks()
        {
            RefreshTasksList();
        }

        private void TasksListBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (tasksListBox.SelectedIndex != -1)
            {
                int index = tasksListBox.SelectedIndex;
                var tasks = taskManager.GetAllTasks();
                tasks[index].IsCompleted = !tasks[index].IsCompleted;
                RefreshTasksList();
            }
        }
    }
}