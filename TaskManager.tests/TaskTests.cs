using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

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
    }

}
