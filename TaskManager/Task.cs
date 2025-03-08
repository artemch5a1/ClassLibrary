using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager
{
    // Класс Task представляет собой модель для создания задач и работы с ними
    // Внешний фунционал:
    // Изменение задачи (ChangeTask) - метод изменяющий информацию о задаче на основе другого объекта класса
    // Принимает объект класса Task
    // Присвивает полям название, описание, приоретет, статус значение полей переданного объекта (не изменяет ссылку)
    // Выбрасывает исключение если переданный объект равен null
    // Возвращения информации в строковом формат. Берутся значение полей (задача, описание, статус, приоретет, команда, проект)
    // Далле возвращаются в определенном строковом формате
    // Внутренний функционал:
    // Установить команду (SetTeam): Вызывается методомами CreateTask и RemoveTask для добавление и снятие обратной связи с объектом класса Team
    // Установить проект (SetProject): Вызывается методом AddTask для установления обратной связи с объектом класса Project
    // Добавить исполнителя (AddPerf): Вызывается методом AddTask для установления обратной связи с объектом класса Member
    public class Task
    {
        private static int _nextId = 1; // приватный счетчик для уникального айди в рамках одного запуска программы
        public Task(string name, string description, Priority priority) // конструктор класса 
        {
            this.id = _nextId++; // новый айди
            this.name = name;
            this.description = description;
            this.priority = priority;
        }

        public int id { get; protected set; } // айди
        public string name { get; set; } // название задачи
        public string description { get; set; } // описание задачи
        public Status status { get; set; } = Status.New; // Статус задачи
        public Priority priority { get; set; } // Приоретет задачи
        public DateTime dateset { get; protected set; } = DateTime.Now; // дата создания
        public virtual Project project { get; protected set; } = null; // информация о проекте задачи
        public virtual Team team { get; protected set; } = null; // Команда которой назначена задача
        public virtual List<Member> performers { get; protected set; } = new List<Member>(); // Список исполнителей задачи

        internal void SetTeam(Team team) //Внутренний функционал
            { this.team = team; }
        internal void SetProject(Project project) //Внутренний функционал
        { this.project = project; }

        public void ChangeTask(Task task) // Изменение информации о задаче
        {
            if (task == null) // Исключение если переданный объект равен null
                throw new ArgumentNullException("Not content");
            else // Изменение полей текущего объекта на основе переданного объекта
            {
                this.name = task.name;
                this.description = task.description;
                this.priority = task.priority;
                this.status = task.status;
            }
        }
        public override string ToString() // переопределение метода ToString
        {
            string team = "нет"; // дефолт значение поля команда
            if(this.team != null) // если поле не пустое переопределяем строку на название команды
            {
                team = this.team.name; 
            }
            string project = "нет"; // дефолт значение поля проект
            if (this.project != null) // если поле не пустое переопределяем строку на название проекта
            {  project = this.project.name; }

            return $"Задача id = {this.id}: {this.name}\n" + //Возвращаем строку информации об текущем объекте в следующем формате
                $"Описание: {this.description}\n" +
                $"Статус: {this.status}\n" +
                $"Приоретет: {this.priority}\n" +
                $"Дата создания: {this.dateset}\n" +
                $"Название проекта: {project}\n" +
                $"Название команды: {team}";
        }
        internal void AddPerf(Member member) // Внутренний фунционал
        { this.performers.Add(member);}
        internal void ChangeDate(DateTime date) // Внутренний функционал для упрощения тестирования
        {
            this.dateset = date;
        }
    }
    public enum Status // значения статуса
    {
        New,
        InProgress,
        Completed,
        Cancelled 
    }
    public enum Priority // значения приоритета
    {
        Low,
        Medium,
        High
    }

    
}
