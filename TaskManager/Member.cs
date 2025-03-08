using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager
{
    // Класс member предназначен для создания и работы с учатниками команды
    // Внешний функционал: Добавление задач (AddTask):
    // добваляет задачу участнику команды
    // Добавляет задаче исполнителя в лице учатника команды
    // Внутренний функционал: Усатновить команду (SetTeam): обратная связь с классом Team, вызывается методом Team.AddMember
    public class Member
    {
        private static int _nextId = 1; // приватный счетчик для уникального айди в рамках одного запуска программы
        public Member(string name) // конструктор класса
        {
            this.id = _nextId++; // новый айди
            this.name = name;
        }

        public int id { get; protected set; } // айди
        public string name { get; set; } // Название учатника
        public virtual Team team { get; protected set; } = null; // Команда учатника
        public List<Task> tasks { get; protected set; } = new List<Task>(); // Список задач учатника

        internal void SetTeam(Team team) // закрытый на уровне сборки метод для внутреннего использования
        { this.team = team; } 
        public void AddTask(Task task) // Добавление задач. Открытый метод
        {
            if (task == null) throw new ArgumentNullException("task is null"); // если передана пустая задача, выбрасывается исключение
            else if (task.team != this.team) throw new ArgumentException("This task does not belong to member's team"); // если задача не выполняется командой участника, выбрасывается исключение
            else
            {
                this.tasks.Add(task); // добавление задачи
                task.AddPerf(this); // установление обратной связи через внутренний метод класс Task
            }
        }

    }
}
