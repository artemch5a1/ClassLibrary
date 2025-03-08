using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.tests
{
    [TestClass]
    public class TeamTests
    {
        // Тестирование метода CreateTask
        [TestClass]
        public class CreateTaskTests
        {
            // Пробуем передать пустой объект task в метод
            // Ожидание: исключение ArgumentNullException
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

            // Пробуем передать в метод объект task, который уже связан с другим объектом Team
            // Ожидание: исключение ArgumentException
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

            // Передаем правильный task
            // Ожидание: объект team содержит переданную до этого в метод task
            // поле объекта task равно текущей команде
            [TestMethod]
            public void CreateTask_ValidTask_AddTaskToTeamAndSetTeamInTask()
            {
                //arrange
                Task task = new Task("s", "s", Priority.Low);
                Team team = new Team("в");
                var expected = task;

                //act
                team.CreateTask(task);
                var actual = team.tasks;

                //assert
                Assert.IsTrue(actual.Contains(expected));
                Assert.AreEqual(task.team, team);
            }
        }

        // Тестирование метода RemoveTask
        [TestClass]
        public class RemoveTaskTests
        {
            // Пробуем передать пустой объект task в метод
            // Ожидание: исключение ArgumentNullException
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

            // Передаем правильный task
            // Перед выполнением проверяем что team содержит task
            // У объекта task в поле team текущая команда
            // После выполнения проверям, что team больше не содержит task
            // У объекта task в поле team теперь пустая ссылка
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


            // Пробуем передать в метод объект task, который связан с другим объектом Team или не связан
            // Ожидание: исключение ArgumentException
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

            // Передаем аномальный объект task с установленной текущей team в соотвествующем поле, но не имеющим обратной связи
            // Ожидание: исключение ArgumentException
            // Данный сценарий возможен только внутри сборки
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


        // Тестирование метода GetTask
        [TestClass]
        public class GetTaskTests
        {
            // Проверка получения задачи по ID
            // Ожидание: возвращается задача с указанным ID
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

            // Проверка получения задачи по ID, если задача не связана с командой
            // Ожидание: выбрасывается исключение ArgumentNullException
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
        }

        // Тестирование метода GetTaskToString
        [TestClass]
        public class GetTaskToStringTests
        {
            // Проверка получения строкового представления задачи по ID
            // Ожидание: возвращается строковое представление задачи
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

            // Проверка получения строкового представления задачи по ID, если задача не связана с командой
            // Ожидание: возвращается сообщение об отсутствии задачи
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
        }

        // Тестирование метода GetAllTask
        [TestClass]
        public class GetAllTaskTests
        {
            // Проверка получения всех задач команды
            // Ожидание: возвращается список всех задач команды
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
        }

        // Тестирование метода AddMember
        [TestClass]
        public class AddMemberTests
        {
            // Проверка добавления участника в команду
            // Ожидание: участник добавлен в список участников команды, и его поле team установлено на текущую команду
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

            // Проверка добавления null-участника в команду
            // Ожидание: выбрасывается исключение ArgumentNullException
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

            // Проверка добавления участника, уже связанного с другой командой
            // Ожидание: выбрасывается исключение ArgumentException
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

        // Тестирование метода AllTaskMember
        [TestClass]
        public class AllTaskMemberTests
        {
            // Проверка получения всех задач участника
            // Ожидание: возвращается список задач участника
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

            // Проверка получения задач для null-участника
            // Ожидание: выбрасывается исключение ArgumentNullException
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

            // Проверка получения задач для участника, не принадлежащего текущей команде
            // Ожидание: выбрасывается исключение ArgumentException
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
        }

        // Тестирование метода TasksByStatus
        [TestClass]
        public class TasksByStatusTests
        {
            // Проверка фильтрации задач по статусу
            // Ожидание: возвращается список задач с указанным статусом
            [TestMethod]
            public void TasksByStatus_Status_returnedListStatus()
            {
                //arrange
                Task task1 = new Task("test", "test", Priority.Low);
                Task task2 = new Task("test", "test", Priority.Low);
                Task task3 = new Task("test", "test", Priority.Low);
                Task task4 = new Task("test", "test", Priority.Low);
                List<Task> tasksNew = new List<Task> { task1, task2, task3, task4 };
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
        }

        // Тестирование метода CountTasks
        [TestClass]
        public class CountTasksTests
        {
            // Проверка подсчета количества задач в команде
            // Ожидание: возвращается количество задач, равное 3
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
        }

        // Тестирование метода TasksByDate
        [TestClass]
        public class TasksByDateTests
        {
            // Проверка фильтрации задач по дате
            // Ожидание: возвращается список задач, созданных сегодня
            [TestMethod]
            public void TasksByDate_ToDay_TaskAddToDay()
            {
                //arrange
                Team team = new Team("test");
                Task task1 = new Task("test", "test", Priority.Low);
                Task task3 = new Task("test", "test", Priority.Low);
                List<Task> tasks = new List<Task> { task1, task3 };
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
        }

        // Тестирование метода TaskByProject
        [TestClass]
        public class TaskByProjectTests
        {
            // Проверка фильтрации задач по проекту
            // Ожидание: возвращается список задач, связанных с указанным проектом
            [TestMethod]
            public void TaskByProject_ProjectTask1Task3_returnedListTask1Task3()
            {
                //arrange
                Team team1 = new Team("test");
                Task task1 = new Task("test", "test", Priority.Low);
                Task task2 = new Task("test", "test", Priority.Low);
                Task task3 = new Task("test", "test", Priority.Low);
                List<Task> expected = new List<Task> { task1, task3 };
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

            // Проверка фильтрации задач по null-проекту
            // Ожидание: выбрасывается исключение ArgumentNullException
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

    }
}
