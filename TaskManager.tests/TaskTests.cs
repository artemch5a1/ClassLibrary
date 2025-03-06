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

            // act & assert
            var actual = Assert.ThrowsException<ArgumentNullException>(() => team.CreateTask(task));

            //assert
            Assert.AreEqual(expected, actual.ParamName);
        }
        
    }
}
