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
        public virtual List<Task> tasks { get; protected set; } = new List<Task>();

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
        public virtual Team team { get; protected set; }
        public List<Task> tasks { get; protected set; } = new List<Task>();
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
        public virtual Project project { get; protected set; }
        public virtual Team team { get; protected set; }
        public virtual List<Member> performers { get; protected set; } = new List<Member>();

        internal void SetTeam(Team team)
            { this.team = team; }

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
