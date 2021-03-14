using Moq;
using NUnit.Framework;
using SampleWebAPIApplication.Controllers;
using SampleWebAPIApplication.Models;
using SampleWebAPIApplication.Models.Entity;
using SampleWebAPIApplication.Repository.Interface;
using System.Threading.Tasks;

namespace TestSampleWebAPIApplication
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }

        [Test]
        public async Task TestGetTodoItems()
        {
            //Arrange
            Mock<ITestRepository> postRepositoryMock = new Mock<ITestRepository>();
            postRepositoryMock.Setup(it => it.GetTodoItem(It.IsAny<long>())).ReturnsAsync(GetInputData());
            TodoItemsController controller = new TodoItemsController(postRepositoryMock.Object);

            //Act
            var result = await controller.GetTodoItem(1);

            //Assert
            Assert.AreEqual("item1", result.Value.Description);
        }

        [Test]
        public async Task TestPostTodoItems()
        {
            //Arrange
            var testInput = GetInputData();
            Mock<ITestRepository> postRepositoryMock = new Mock<ITestRepository>();
            postRepositoryMock.Setup(it => it.PostTodoItem(It.IsAny<TodoItemEntity>())).ReturnsAsync(GetInputData()).Verifiable(); //new TodoItem { Id = 1, Name = "item1", IsComplete = true });
            TodoItemsController controller = new TodoItemsController(postRepositoryMock.Object);

            //Act
            var result = await controller.PostTodoItem(GetInputData());

            //Assert
            var postId = (TodoItemEntity)((Microsoft.AspNetCore.Mvc.ObjectResult)result.Result).Value;
            Assert.AreEqual(postId.Id, 1);
            postRepositoryMock.Verify();
        }

        private TodoItemEntity GetInputData()
        {
            TodoItemEntity sampleItem = new TodoItemEntity();
            sampleItem.Id = 1;
            sampleItem.Description = "item1";
            sampleItem.IsComplete = true;

            return sampleItem;
        }
    }
}