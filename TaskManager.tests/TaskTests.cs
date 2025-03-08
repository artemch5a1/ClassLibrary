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
