using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager
{
    //Класс Project предназначен для хранения информации о проекте задачи (класс Task)
    //Внешний функционал: Добавление задачи (AddTask):
    // Передается объект класса Task
    // Добавляет задачу в проект
    // Устанавливает обратную связь: добавляет информацию о проекте в объект класса Task
    public class Project
    {
        private static int _nextId = 1; // приватный счетчик для уникального айди в рамках одного запуска программы
        public Project(string name, string description) // конструктор класса
        {
            this.name = name;
            this.description = description;
            this.id = _nextId++; // новый айди
        }

        public int id { get; protected set; } // айди
        public string name { get; set; } // название проекта
        public string description { get; set; } // описание
        public virtual List<Task> tasks { get; protected set; } = new List<Task>(); // список задач проекта

        public void AddTask(Task task) //Внешний функционал: Добавление задачи (AddTask):
        {
            this.tasks.Add(task); // Добавляет задачу в проект
            task.SetProject(this);  // Устанавливает обратную связь: добавляет информацию о проекте в объект класса Task
        }

    }
}
