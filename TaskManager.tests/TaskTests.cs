using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TaskManager.tests
{
    [TestClass]
    public class TaskTests
    {
        [TestMethod]
        public void ChangeStatus_NullTask_ThrowsArgumentNullExceptionNotContent()
        {
            //arrange
            Task task = null;
            Task task1 = new Task("s", "s", Priority.High);
            var expected = new ArgumentNullException("Not content");
            //act & assert
            var actual = Assert.ThrowsException<ArgumentNullException>(() => task1.ChangeTask(task));
            //act
            Assert.AreEqual(expected.Message, actual.Message);
        }
        [TestMethod]
        public void ChangeStatus_ValidTask_Task1ValuesEqualTask2EqualButTask1NotEqualTask2()
        {
            //arrange
            Task task1 = new Task("g", "g", Priority.Low);
            task1.status = Status.Cancelled;
            Task task2 = new Task("s", "s", Priority.High);
            bool expected = true;
            //act
            task1.ChangeTask(task2);
            bool actual = (task1.name == task2.name && task1.description == task2.description && task1.priority == task2.priority && task1.status == task2.status) && (!task1.Equals(task2));
            //assert
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void ToString_TaskWithNullProjAndNotNullTeam_CorrectString()
        {
            //arrange
            Task task1 = new Task("test", "test ToString", Priority.Low);
            Team team = new Team("testTeam");
            team.CreateTask(task1);
            string expected = $"Задача id = {task1.id}: test\n" +
                $"Описание: test ToString\n" +
                $"Статус: New\n" +
                $"Приоретет: Low\n" +
                $"Дата создания: {task1.dateset.ToString()}\n" +
                $"Название проекта: нет\n" +
                $"Название команды: testTeam";

            //act
            string actual = task1.ToString();

            //assert
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void ToString_TaskWithNullProjAndNullTeam_CorrectString()
        {
            //arrange
            Task task1 = new Task("test", "test ToString", Priority.Low);
            
            string expected = $"Задача id = {task1.id}: test\n" +
                $"Описание: test ToString\n" +
                $"Статус: New\n" +
                $"Приоретет: Low\n" +
                $"Дата создания: {task1.dateset.ToString()}\n" +
                $"Название проекта: нет\n" +
                $"Название команды: нет";

            //act
            string actual = task1.ToString();

            //assert
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void ToString_TaskWithNotNullProjAndNullTeam_CorrectString()
        {
            //arrange
            Task task1 = new Task("test", "test ToString", Priority.Low);
            Project project = new Project("test", "test p");
            task1.SetProject(project);
            string expected = $"Задача id = {task1.id}: test\n" +
                $"Описание: test ToString\n" +
                $"Статус: New\n" +
                $"Приоретет: Low\n" +
                $"Дата создания: {task1.dateset.ToString()}\n" +
                $"Название проекта: test\n" +
                $"Название команды: нет";

            //act
            string actual = task1.ToString();

            //assert
            Assert.AreEqual(expected, actual);
        }
         [TestMethod]
        public void ToString_TaskWithNotNullProjAndNotNullTeam_CorrectString()
        {
            //arrange
            Task task1 = new Task("test", "test ToString", Priority.Low);
            Project project = new Project("test", "test p");
            task1.SetProject(project);
            Team team = new Team("testTeam");
            team.CreateTask(task1);
            string expected = $"Задача id = {task1.id}: test\n" +
                $"Описание: test ToString\n" +
                $"Статус: New\n" +
                $"Приоретет: Low\n" +
                $"Дата создания: {task1.dateset.ToString()}\n" +
                $"Название проекта: test\n" +
                $"Название команды: testTeam";

            //act
            string actual = task1.ToString();

            //assert
            Assert.AreEqual(expected, actual);
        }
        
        
    }

    [TestClass]
    public class TeamTests
    {
        [TestMethod]
        public void CreateTask_NullTask_ThrowsArgumentNullException()
        {
            //arrange
            Task task = null;
            Team team = new Team("в");
            string expected = "Not content";

            //act & assert
            var actual = Assert.ThrowsException<ArgumentNullException>(() => team.CreateTask(task));

            //assert
            Assert.AreEqual(expected, actual.ParamName);
        }
        [TestMethod]
        public void CreateTask_BindedWithTeamTask_ThrowsArgumentException()
        {
            //arrange
            Task task = new Task("s", "s", Priority.Low);
            Team team = new Team("в");
            Team team1 = new Team("в");
            team1.CreateTask(task);
            string expected = "Task already binded to the team";

            //act & assert
            var actual = Assert.ThrowsException<ArgumentException>(() => team.CreateTask(task));

            //assert
            Assert.AreEqual(expected, actual.Message);
        }

        [TestMethod]
        public void CreateTask_ValidTask_AddTaskToTeamAndSetTeamInTask()
        {
            //arrange
            Task task = new Task("s", "s", Priority.Low);
            Team team = new Team("в");
            Task expected = task;

            //act
            team.CreateTask(task);
            Task actual = team.tasks[0];

            //assert
            Assert.AreEqual(expected, actual);
            Assert.AreEqual(task.team, team);
        }


        [TestMethod]
        public void RemoveTask_NullTask_ThrowsArgumentNullException()
        {
            //arrange
            Task task = null;
            Team team = new Team("в");
            string expected = "Not content";

            //act & assert
            var actual = Assert.ThrowsException<ArgumentNullException>(() => team.RemoveTask(task));

            //assert
            Assert.AreEqual(expected, actual.ParamName);
        }

        [TestMethod]
        public void RemoveTask_ValidTask_RemoveTaskAndSetNullTeamInTask()
        {
            //arrange
            Task task = new Task("s", "s", Priority.Low);
            Team team = new Team("в");
            Task expected = task;

            //act
            team.CreateTask(task);

            //assert
            Assert.IsTrue(team.tasks.Contains(task));
            Assert.AreEqual(task.team, team);

            //act
            team.RemoveTask(task);

            //assert
            Assert.IsFalse(team.tasks.Contains(task));
            Assert.AreEqual(task.team, null);
        }

        [TestMethod]
        public void RemoveTask_AnotherTeamTask_ThrowsArgumentException()
        {
            //arrange
            Task task = new Task("s", "s", Priority.Low);
            Team team = new Team("в");
            Team team1 = new Team("в");
            team1.CreateTask(task);
            var expected = new ArgumentException("Task already binded to the team");

      
            //act
            var actual = Assert.ThrowsException<ArgumentException>(() => team.RemoveTask(task));

            //assert
            Assert.AreEqual(expected.Message, actual.Message);
        }

        [TestMethod]
        public void RemoveTask_AbnormalTeamTask_ThrowsArgumentExceptionTaskNotFound()
        {
            //arrange
            Task task = new Task("s", "s", Priority.Low);
            Team team = new Team("в");
            task.SetTeam(team);
            var expected = new ArgumentException("Task not found");


            //act
            var actual = Assert.ThrowsException<ArgumentException>(() => team.RemoveTask(task));

            //assert
            Assert.AreEqual(expected.Message, actual.Message);
        }
        [TestMethod]
        public void GetTask_TaskId_RetunedTask()
        {
            //arrange
            Task task = new Task("test", "test", Priority.High);
            Team team = new Team("test");
            team.CreateTask(task);
            var expected = task;

            //act
            var actual = team.GetTask(task.id);

            //assert
            Assert.AreEqual(expected, actual);

        }
        [TestMethod]
        public void GetTask_IdNotBindedWithTeamTask_ThrowsArgumentNullException()
        {
            //arrange
            Task task = new Task("test", "test", Priority.High);
            Team team = new Team("test");
            var expected = new ArgumentNullException("No tasks with this ID were found");

            //act & assert
            var actual = Assert.ThrowsException<ArgumentNullException>(() => team.GetTask(task.id));

            //assert
            Assert.AreEqual(expected.Message, actual.Message);
        }
        [TestMethod]
        public void GetTaskToString_TaskId_returnedTaskToString()
        {
            //arrange
            Task task = new Task("test", "test", Priority.High);
            Team team = new Team("test");
            team.CreateTask(task);
            string expected = task.ToString();

            //act
            string actual = team.GetTaskToString(task.id);

            //assert
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void GetTaskToString_IdNotBindedWithTeamTask_returnedStringMessege()
        {
            //arrange
            Task task = new Task("test", "test", Priority.High);
            Team team = new Team("test");
            string expected = "у команды нет задачи с запрошенным id";
            

            //act
            string actual = team.GetTaskToString(task.id);

            //assert
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void GetAllTask_teamGetAllTask_returnedTeamTasks()
        {
            //arrange
            Task task = new Task("test", "test", Priority.High);
            Task task1 = new Task("test1", "test1", Priority.High);
            Team team = new Team("test");
            team.CreateTask(task1);
            team.CreateTask(task);
            var expected = team.tasks;


            //act
            var actual = team.GetAllTask();

            //assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void AddMember_validMember_MemberInMembersTeamAndMemberSetTeam()
        {
            //arrange
            Member member = new Member("test");
            Team team = new Team("testTeam");

            //act
            team.AddMember(member);

            //assert
            Assert.IsTrue(team.members.Contains(member));
            Assert.AreEqual(member.team, team);

        }
        [TestMethod]
        public void AddMember_NullMember_MemberInMembersTeamAndMemberSetTeam()
        {
            //arrange
            Member member = null;
            Team team = new Team("testTeam");
            var expected = new ArgumentNullException("member is null");
            //act & assert

            var actual = Assert.ThrowsException<ArgumentNullException>(() => team.AddMember(member));

            //assert
            Assert.AreEqual(expected.Message, actual.Message);

        }
        [TestMethod]
        public void AddMember_AnotherTeamMember_MemberInMembersTeamAndMemberSetTeam()
        {
            //arrange
            Member member = new Member("test");
            Team team1 = new Team("test2");
            team1.AddMember(member);
            Team team = new Team("testTeam");
            var expected = new ArgumentException("member has been already binded with team");
            //act & assert

            var actual = Assert.ThrowsException<ArgumentException>(() => team.AddMember(member));

            //assert
            Assert.AreEqual(expected.Message, actual.Message);

        }
        [TestMethod]
        public void AllTaskMember_ValidMember_returnedListTaskMemeber()
        {
            //arrange
            Team team = new Team("test");
            Task task = new Task("test", "test", Priority.High);
            team.CreateTask(task);
            Member member = new Member("test");
            team.AddMember(member);
            member.AddTask(task);

            //act
            var actual = team.AllTaskMember(member);

            //Arrange
            Assert.AreEqual(actual, member.tasks); 
        }
        [TestMethod]
        public void AllTaskMember_NullMember_returnedListTaskMemeber()
        {
            //arrange
            Team team = new Team("test");
            Task task = new Task("test", "test", Priority.High);
            team.CreateTask(task);
            Member member = null;
            var expected = new ArgumentNullException("Member is null");

            //act
            var actual = Assert.ThrowsException<ArgumentNullException>(() => team.AllTaskMember(member));

            //Arrange
            Assert.AreEqual(expected.Message, actual.Message);
        }
        [TestMethod]
        public void AllTaskMember_MemeberAnotherTeam_returnedListTaskMemeber()
        {
            //arrange
            Team team = new Team("test");
            Task task = new Task("test", "test", Priority.High);
            Team team1 = new Team("test");
            team1.CreateTask(task);
            Member member = new Member("test");
            team1.AddMember(member);
            member.AddTask(task);
            var expected = new ArgumentException("This member does not belong to this team");

            //act
            var actual = Assert.ThrowsException<ArgumentException>(() => team.AllTaskMember(member));

            //Arrange
            Assert.AreEqual(expected.Message, actual.Message);
        }
        [TestMethod]
        public void TasksByStatus_Status_returnedListStatus()
        {
            //arrange
            Task task1 = new Task("test", "test", Priority.Low);
            Task task2 = new Task("test", "test", Priority.Low);
            Task task3 = new Task("test", "test", Priority.Low);
            Task task4 = new Task("test", "test", Priority.Low);
            List<Task> tasksNew = new List<Task> { task1, task2, task3, task4};
            Task task5 = new Task("test", "test", Priority.Low);
            task5.status = Status.Completed;
            Team team = new Team("test");
            team.CreateTask(task1);
            team.CreateTask(task2);
            team.CreateTask(task3);
            team.CreateTask(task4);
            team.CreateTask(task5);
            Status status = Status.New;
            var expected = tasksNew;

            //act
            var actual = team.TasksByStatus(status);

            //Assert
            Assert.IsTrue(expected.SequenceEqual(actual));

        }

        [TestMethod]
        public void CountTasks_TeamAdd3Task_returned3()
        {
            //arrange
            Team team = new Team("test");
            Task task1 = new Task("test", "test", Priority.Low);
            Task task2 = new Task("test", "test", Priority.Low);
            Task task3 = new Task("test", "test", Priority.Low);
            team.CreateTask(task1);
            team.CreateTask(task2);
            team.CreateTask(task3);
            int expected = 3;

            //act
            int actual = team.CountTasks();

            //assert
            Assert.AreEqual(expected, actual);

        }

        [TestMethod]
        public void TasksByDate_ToDay_TaskAddToDay()
        {
            //arrange
            Team team = new Team("test");
            Task task1 = new Task("test", "test", Priority.Low);
            Task task3 = new Task("test", "test", Priority.Low);
            List<Task> tasks = new List<Task> { task1, task3};
            Task task2 = new Task("test", "test", Priority.Low);
            task2.ChangeDate(new DateTime(2025, 06, 03));
            var expected = tasks;
            team.CreateTask(task1);
            team.CreateTask(task2);
            team.CreateTask(task3);

            //act
            var actual = team.TasksByDate(DateTime.Today);

            //assert
            Assert.IsTrue(expected.SequenceEqual(actual));
        }

        [TestMethod]
        public void TaskByProject_ProjectTask1Task3_returnedListTask1Task3()
        {
            //arrange
            Team team1 = new Team("test");
            Task task1 = new Task("test", "test", Priority.Low);
            Task task2 = new Task("test", "test", Priority.Low);
            Task task3 = new Task("test", "test", Priority.Low);
            List<Task> expected = new List<Task> {task1, task3};
            team1.CreateTask(task1);
            team1.CreateTask(task2);
            team1.CreateTask(task3);
            Project project1 = new Project("test", "test");
            project1.AddTask(task1);
            project1.AddTask(task3);

            //act
            var actual = team1.TaskByProject(project1);

            //assert
            Assert.IsTrue(expected.SequenceEqual(actual));
        }

        [TestMethod]
        public void TaskByProject_ProjectNull_ThrowsArgumentNullException()
        {
            //arrange
            Team team1 = new Team("test");
            Project project1 = null;
            var expected = new ArgumentNullException("Project is null");
            
            //act
            var actual = Assert.ThrowsException<ArgumentNullException>(() => team1.TaskByProject(project1));

            //assert
            Assert.AreEqual(expected.Message, actual.Message);
        }
    }

    [TestClass]
    public class MemberTest
    {
        [TestMethod]
        public void AddTask_ValidTask_AddMemberInTaskAddTaskInMember()
        {
            //arrange
            Team team = new Team("test");
            Task task = new Task("test", "test", Priority.High);
            team.CreateTask(task);
            Member member = new Member("test");
            team.AddMember(member);

            //act
            member.AddTask(task);


            //assert
            Assert.AreEqual(team.GetTask(task.id).performers, task.performers);
            Assert.IsTrue(task.performers.Contains(member));
            Assert.IsTrue(member.tasks.Contains(task));
        }
        [TestMethod]
        public void AddTask_AnotherTeamTask_ThrowsArgumentException()
        {
            //arrange
            Team team = new Team("test");
            Task task = new Task("test", "test", Priority.High);
            team.CreateTask(task);
            Team team1 = new Team("test");
            Member member = new Member("test");
            team1.AddMember(member);
            var expected = new ArgumentException("This task does not belong to member's team");

            //act & assert
            var actual = Assert.ThrowsException<ArgumentException>(() => member.AddTask(task));


            //assert
            Assert.AreEqual(expected.Message, actual.Message);
        }
        [TestMethod]
        public void AddTask_NullTask_ThrowsArgumentNullException()
        {
            //arrange
            Team team = new Team("test");
            Task task = null;
            Member member = new Member("test");
            team.AddMember(member);
            var expected = new ArgumentNullException("task is null");
            //act & assert
            var actual = Assert.ThrowsException<ArgumentNullException>(() => member.AddTask(task));

            //assert
            Assert.AreEqual(expected.Message, actual.Message);
        }

        
    }

}
