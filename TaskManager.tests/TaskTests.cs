using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TaskManager.tests
{
    // Тестирование функционала класса Task, связанного с изменением статуса и строковым представлением задачи.
    [TestClass]
    public class TaskTests
    {
        // Тестирование метода ChangeTask
        [TestClass]
        public class ChangeStatusTests
        {
            // Проверка изменения задачи с передачей null в качестве аргумента
            // Ожидание: выбрасывается исключение ArgumentNullException с сообщением "Not content".
            [TestMethod]
            public void ChangeStatus_NullTask_ThrowsArgumentNullExceptionNotContent()
            {
                //arrange
                Task task = null;
                Task task1 = new Task("s", "s", Priority.High);
                var expected = new ArgumentNullException("Not content");

                //act & assert
                var actual = Assert.ThrowsException<ArgumentNullException>(() => task1.ChangeTask(task));

                //assert
                Assert.AreEqual(expected.Message, actual.Message); // Проверка, что сообщение исключения соответствует ожидаемому
            }

            // Проверка изменения задачи с передачей валидного объекта Task
            // Ожидание: значения полей task1 изменяются на значения полей task2, но объекты остаются разными.
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
                Assert.AreEqual(expected, actual); // Проверка, что значения полей равны, но объекты разные
            }
        }

        // Тестирование метода ToString
        [TestClass]
        public class ToStringTests
        {
            // Проверка строкового представления задачи с null-проектом и не-null командой
            // Ожидание: возвращается строка с корректным представлением задачи, включая название команды.
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
                Assert.AreEqual(expected, actual); // Проверка, что строковое представление задачи корректно
            }

            // Проверка строкового представления задачи с null-проектом и null-командой
            // Ожидание: возвращается строка с корректным представлением задачи, где проект и команда указаны как "нет".
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
                Assert.AreEqual(expected, actual); // Проверка, что строковое представление задачи корректно
            }

            // Проверка строкового представления задачи с не-null проектом и null-командой
            // Ожидание: возвращается строка с корректным представлением задачи, включая название проекта.
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
                Assert.AreEqual(expected, actual); // Проверка, что строковое представление задачи корректно
            }

            // Проверка строкового представления задачи с не-null проектом и не-null командой
            // Ожидание: возвращается строка с корректным представлением задачи, включая название проекта и команды.
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
                Assert.AreEqual(expected, actual); // Проверка, что строковое представление задачи корректно
            }
        }
    }
}
    
