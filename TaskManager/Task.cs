using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager
{
    public class Team
    {
        private static int _nextId = 1;
        public Team(string name)
        {
            this.id = _nextId++;
            this.name = name;
        }

        public int id { get; protected set; }
        public string name { get; set; }
        public virtual List<Member> members { get; protected set; } = new List<Member>();
        internal virtual List<Task> tasks { get; set; } = new List<Task>();

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
        public Task GetTask(int id)
        {
            Task task = this.tasks.FirstOrDefault(t => t.id == id);
            if(task == null)
                throw new ArgumentNullException("No tasks with this ID were found");
            else 
                return task;
        }
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

        public List<Task> GetAllTask()
        {
            return this.tasks;
        }
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

        public List<Task> TasksByStatus(Status status)
        {

            return this.tasks.Where(x => x.status == status).ToList();

        }
        public int CountTasks()
        {
            return this.tasks.Count;
        }
        public List<Task> TasksByDate(DateTime date)
        {
            return this.tasks.Where(x => x.dateset.Date.Equals(date.Date)).ToList();   
        }
        public List<Task> TaskByProject(Project project)
        {
            if (project == null) throw new ArgumentNullException("Project is null");
            else
            {
                return this.tasks.Where(x => x.project == project).ToList();
            }
        }
    }
    public class Project
    {
        private static int _nextId = 1;
        public Project(string name, string description)
        {
            this.name = name;
            this.description = description;
            this.id = _nextId++;
        }

        public int id { get; protected set; }
        public string name { get; set; }
        public string description { get; set; }
        public virtual List<Task> tasks { get; protected set; } = new List<Task>();

        public void AddTask(Task task)
        { 
            this.tasks.Add(task);
            task.SetProject(this);
        }

    }
    public class Member
    {
        private static int _nextId = 1;
        public Member(string name)
        {
            this.id = _nextId++;
            this.name = name;
        }

        public int id { get; protected set; }
        public string name { get; set; }
        public virtual Team team { get; protected set; } = null;
        public List<Task> tasks { get; protected set; } = new List<Task>();

        public void SetTeam(Team team)
        { this.team = team; }
        public void AddTask(Task task)
        {
            if (task == null) throw new ArgumentNullException("task is null");
            else if (task.team != this.team) throw new ArgumentException("This task does not belong to member's team");
            else
            {
                this.tasks.Add(task);
                task.AddPerf(this);
            }
        }
        
    }
    public class Task
    {
        private static int _nextId = 1;
        public Task(string name, string description, Priority priority)
        {
            this.id = _nextId++;
            this.name = name;
            this.description = description;
            this.priority = priority;
        }

        public int id { get; protected set; }
        public string name { get; set; }
        public string description { get; set; }
        public Status status { get; set; } = Status.New;
        public Priority priority { get; set; }
        public DateTime dateset { get; protected set; } = DateTime.Now;
        public virtual Project project { get; protected set; } = null;
        public virtual Team team { get; protected set; } = null;
        public virtual List<Member> performers { get; protected set; } = new List<Member>();

        internal void SetTeam(Team team)
            { this.team = team; }
        internal void SetProject(Project project)
            { this.project = project; }

        public void ChangeTask(Task task)
        {
            if (task == null)
                throw new ArgumentNullException("Not content");
            else
            {
                this.name = task.name;
                this.description = task.description;
                this.priority = task.priority;
                this.status = task.status;
            }
        }
        public override string ToString()
        {
            string team = "нет";
            if(this.team != null)
            {
                team = this.team.name;
            }
            string project = "нет";
            if(this.project != null)
            {  project = this.project.name; }

            return $"Задача id = {this.id}: {this.name}\n" +
                $"Описание: {this.description}\n" +
                $"Статус: {this.status}\n" +
                $"Приоретет: {this.priority}\n" +
                $"Дата создания: {this.dateset}\n" +
                $"Название проекта: {project}\n" +
                $"Название команды: {team}";
        }
        internal void AddPerf(Member member)
        { this.performers.Add(member);}
        internal void ChangeDate(DateTime date)
        {
            this.dateset = date;
        }
    }
    public enum Status
    {
        New,
        InProgress,
        Completed,
        Cancelled 
    }
    public enum Priority
    {
        Low,
        Medium,
        High
    }

    
}
