using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

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
    }
}
