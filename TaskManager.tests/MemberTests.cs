using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.tests
{
    
    [TestClass]
    public class MemberTests
    {
        // Тестирование метода AddTask
        [TestClass]
        public class AddTaskTests
        {
            // Проверка добавления задачи участнику
            // Ожидание: задача добавляется в список задач участника, а участник добавляется в список исполнителей задачи.
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
                Assert.AreEqual(team.GetTask(task.id).performers, task.performers); // Проверка, что участник добавлен в задачу
                Assert.IsTrue(task.performers.Contains(member)); // Проверка, что участник есть в списке исполнителей задачи
                Assert.IsTrue(member.tasks.Contains(task)); // Проверка, что задача есть в списке задач участника
            }

            // Проверка добавления задачи, которая принадлежит другой команде
            // Ожидание: выбрасывается исключение ArgumentException с сообщением "This task does not belong to member's team".
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
                Assert.AreEqual(expected.Message, actual.Message); // Проверка, что сообщение исключения соответствует ожидаемому
            }

            // Проверка добавления null-задачи
            // Ожидание: выбрасывается исключение ArgumentNullException с сообщением "task is null".
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
                Assert.AreEqual(expected.Message, actual.Message); // Проверка, что сообщение исключения соответствует ожидаемому
            }
        }
    }
}
