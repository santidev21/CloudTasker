using CloudTasker.Api.Data;
using CloudTasker.Api.Functions;
using CloudTasker.Api.Models;
using Microsoft.Azure.Functions.Worker.Http;
using Moq;
using System.Net;

public class TaskFunctionsTests
{
    [Fact]
    public async Task GetTasks_ReturnsOk()
    {
        // Arrange
        var repoMock = new Mock<ITaskRepository>();
        repoMock.Setup(r => r.GetAllAsync())
            .ReturnsAsync(new[] { new TaskItem { Id = "1", Name = "Test Task" } });

        var functions = new TaskFunctions(repoMock.Object);

        var requestMock = new Mock<HttpRequestData>(null!);
        var responseMock = new Mock<HttpResponseData>(requestMock.Object);

        responseMock.Setup(r => r.StatusCode).Returns(HttpStatusCode.OK);
        responseMock.Setup(r => r.WriteStringAsync(It.IsAny<string>()))
                    .Returns(Task.CompletedTask);

        requestMock.Setup(r => r.CreateResponse())
                   .Returns(responseMock.Object);

        // Act
        var response = await functions.GetTasks(requestMock.Object);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        repoMock.Verify(r => r.GetAllAsync(), Times.Once);
    }
}