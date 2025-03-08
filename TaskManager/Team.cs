using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager
{
    //Класс Team представляет собой модель команды, которая может управлять задачами и участниками.
    //Он содержит методы для создания, удаления, поиска и фильтрации задач, добавления участников в команду.
    //Весь функционал внешние
    
    public class Team
    {
        private static int _nextId = 1; // // приватный счетчик для уникального айди в рамках одного запуска программы
        public Team(string name)
        {
            this.id = _nextId++; // новый айди
            this.name = name;
        }

        public int id { get; protected set; } // айди
        public string name { get; set; } // название
        public virtual List<Member> members { get; protected set; } = new List<Member>(); // учатники
        internal virtual List<Task> tasks { get; set; } = new List<Task>(); // задачи


        //Создать задачу (CreateTask)
        //Создает (добавляет) новую задачу и связывает ее с командой.
        //Если задача уже связана с другой командой, выбрасывается исключение ArgumentException
        //Если задача равна null, выбрасывается исключение ArgumentNullException.
        public void CreateTask(Task task)
        {
            if (task == null)
                throw new ArgumentNullException("Not content");
            else if (task.team != null)
                throw new ArgumentException("Task already binded to the team");
            else
            {
                this.tasks.Add(task);
                task.SetTeam(this);
            }
        }

        //Удалить задачу (RemoveTask)
        //Удаляет задачу из списка задач команды и отвязывает ее от команды.
        //Если задача не принадлежит этой команде или не найдена в списке, выбрасываются соответствующие исключения.
        public void RemoveTask(Task task)
        {
            if (task == null)
                throw new ArgumentNullException("Not content");
            else if (task.team != this)
                throw new ArgumentException("Task already binded to the team");
            else if (!this.tasks.Contains(task))
                throw new ArgumentException("Task not found");
            else
            {
                this.tasks.Remove(task);
                task.SetTeam(null);
            }
        }
        //Получить задачу (GetTask)
        //Возвращает задачу по ее идентификатору.
        //Если задача с таким идентификатором не найдена, выбрасывается исключение ArgumentNullException.
        public Task GetTask(int id)
        {
            Task task = this.tasks.FirstOrDefault(t => t.id == id);
            if (task == null)
                throw new ArgumentNullException("No tasks with this ID were found");
            else
                return task;
        }

        //Получить задачу в строковом формате (GetTaskToString)
        //Возвращает строковое представление задачи по ее идентификатору.
        //Если задача не найдена, возвращается сообщение об ошибке.
        public string GetTaskToString(int id)
        {
            string result = null;
            Task task;
            try
            {
                task = this.GetTask(id);
                result = task.ToString();
            }
            catch (ArgumentNullException)
            {
                result = "у команды нет задачи с запрошенным id";
            }
            catch
            {
                throw new Exception("неизвествная ошибка");
            }

            return result;
        }

        // Получение всех задач команды (GetAllTask) - метод возвращает список всех задач, связанных с командой.
        // Возвращает список объектов класса Task.
        public List<Task> GetAllTask()
        {
            return this.tasks;
        }

        // Добавление участника в команду (AddMember) - метод добавляет участника в команду.
        // Принимает объект класса Member.
        // Если переданный объект равен null, выбрасывает исключение ArgumentNullException.
        // Если участник уже связан с другой командой, выбрасывает исключение ArgumentException.
        // Добавляет участника в список участников команды и устанавливает обратную связь с командой через метод SetTeam.
        public void AddMember(Member member)
        {
            if (member == null) throw new ArgumentNullException("member is null");
            else if (member.team != null) throw new ArgumentException("member has been already binded with team");
            else
            {
                this.members.Add(member);
                member.SetTeam(this);
            }
        }

        // Получение всех задач участника (AllTaskMember) - метод возвращает список всех задач, связанных с конкретным участником команды.
        // Принимает объект класса Member.
        // Если участник не принадлежит текущей команде или равен null, выбрасывает исключение ArgumentException или ArgumentNullException.
        // Возвращает список задач, связанных с участником.
        public List<Task> AllTaskMember(Member member)
        {
            List<Task> result = new List<Task>();

            if (member == null) throw new ArgumentNullException("Member is null");
            else if (member.team != this) throw new ArgumentException("This member does not belong to this team");
            else
            {
                result = member.tasks;
            }
            return result;
        }

        // Фильтрация задач по статусу (TasksByStatus) - метод возвращает список задач, отфильтрованных по статусу.
        // Принимает значение перечисления Status.
        // Возвращает список задач, у которых статус совпадает с переданным значением.
        public List<Task> TasksByStatus(Status status)
        {

            return this.tasks.Where(x => x.status == status).ToList();

        }

        // Подсчет количества задач (CountTasks) - метод возвращает количество задач, связанных с командой.
        // Возвращает целое число, представляющее количество задач.
        public int CountTasks()
        {
            return this.tasks.Count;
        }

        // Фильтрация задач по дате (TasksByDate) - метод возвращает список задач, созданных в определенную дату.
        // Принимает объект DateTime.
        // Возвращает список задач, у которых дата создания совпадает с переданной датой.
        public List<Task> TasksByDate(DateTime date)
        {
            return this.tasks.Where(x => x.dateset.Date.Equals(date.Date)).ToList();
        }

        // Фильтрация задач по проекту (TaskByProject) - метод возвращает список задач, связанных с определенным проектом.
        // Принимает объект класса Project.
        // Если переданный объект равен null, выбрасывает исключение ArgumentNullException.
        // Возвращает список задач, связанных с проектом.
        public List<Task> TaskByProject(Project project)
        {
            if (project == null) throw new ArgumentNullException("Project is null");
            else
            {
                return this.tasks.Where(x => x.project == project).ToList();
            }
        }
    }
}
