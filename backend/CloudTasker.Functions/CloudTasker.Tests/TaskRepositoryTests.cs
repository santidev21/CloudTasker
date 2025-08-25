using CloudTasker.Api.Data;
using CloudTasker.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudTasker.Tests
{
    public class TaskRepositoryTests
    {
        [Fact]
        public async Task Create_And_Get_Task_Should_Work()
        {
            // Arrange
            var repo = new InMemoryTaskRepository();
            var task = new TaskItem
            {
                Title = "Test Task",
                Description = "Unit test create",
                DueDate = DateTimeOffset.UtcNow.AddDays(1),
                IsDone = false
            };

            // Act
            var created = await repo.CreateAsync(task);
            var result = await repo.GetAsync(task.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(created.Id, result!.Id);
            Assert.Equal("Test Task", result.Title);
        }

        [Fact]
        public async Task Update_Task_Should_Work()
        {
            // Arrange
            var repo = new InMemoryTaskRepository();
            var task = new TaskItem
            {
                Title = "To Update",
                Description = "Before update"
            };
            await repo.CreateAsync(task);

            // Act
            task.Title = "Updated Title";
            var updated = await repo.UpdateAsync(task);
            var result = await repo.GetAsync(task.Id);

            // Assert
            Assert.True(updated);
            Assert.NotNull(result);
            Assert.Equal("Updated Title", result!.Title);
        }

        [Fact]
        public async Task Delete_Task_Should_Work()
        {
            // Arrange
            var repo = new InMemoryTaskRepository();
            var task = new TaskItem
            {
                Title = "To Delete",
                Description = "This will be deleted"
            };
            await repo.CreateAsync(task);

            // Act
            var deleted = await repo.DeleteAsync(task.Id);
            var result = await repo.GetAsync(task.Id);

            // Assert
            Assert.True(deleted);
            Assert.Null(result);
        }

        [Fact]
        public async Task List_Should_Return_Tasks()
        {
            // Arrange
            var repo = new InMemoryTaskRepository();

            // Act
            var tasks = await repo.ListAsync();

            // Assert
            Assert.NotEmpty(tasks);
        }
    }
}
