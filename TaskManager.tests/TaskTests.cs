using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TaskManager.tests
{
    [TestClass]
    public class TaskTests
    {
        [TestMethod]
        public void TestMethod1()
        {

        }
    }

    [TestClass]
    public class TeamTests
    {
        [TestMethod]
        public void CreateCreateTask_NullTask_ThrowsArgumentNullException()
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
        public void CreateCreateTask_BindedWithTeamTask_ThrowsArgumentException()
        {
            //arrange
            Task task = new Task("s", "s", Priority.Low, new Project("s", "s"));
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
        public void CreateCreateTask_ValidTask_AddTaskToTeamAndSetTeamInTask()
        {
            //arrange
            Task task = new Task("s", "s", Priority.Low, new Project("s", "s"));
            Team team = new Team("в");
            Task expected = task;

            //act
            team.CreateTask(task);
            Task actual = team.tasks[0];

            //assert
            Assert.AreEqual(expected, actual);
            Assert.AreEqual(task.team, team);
        }
    }
}
