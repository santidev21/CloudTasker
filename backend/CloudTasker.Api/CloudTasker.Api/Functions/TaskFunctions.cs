using CloudTasker.Api.Contracts;
using CloudTasker.Api.Data;
using CloudTasker.Api.Infra;
using CloudTasker.Api.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using System.Net;

namespace CloudTasker.Api.Functions
{
    public class TaskFunctions
    {
        private readonly ITaskRepository _repo;

        public TaskFunctions(ITaskRepository repo) => _repo = repo;

        [Function("GetTasks")]
        public async Task<HttpResponseData> GetTasks(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "tasks")] HttpRequestData req)
        {
            var items = (await _repo.ListAsync()).Select(x => x.ToResponse());
            return await req.JsonAsync(items);
        }

        [Function("GetTaskById")]
        public async Task<HttpResponseData> GetTaskById(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "tasks/{id}")] HttpRequestData req, string id)
        {
            var item = await _repo.GetAsync(id);
            if (item is null) return req.Empty(HttpStatusCode.NotFound);
            return await req.JsonAsync(item.ToResponse());
        }

        [Function("CreateTask")]
        public async Task<HttpResponseData> CreateTask(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "tasks")] HttpRequestData req)
        {
            var body = await req.ReadJsonAsync<TaskWriteRequest>();
            // Basic validation
            if (body is null || string.IsNullOrWhiteSpace(body.Title))
                return await req.JsonAsync(new { error = "Title is required." }, HttpStatusCode.BadRequest);

            var now = DateTimeOffset.UtcNow;
            var item = new TaskItem(
                Id: Guid.NewGuid().ToString("n"),
                Title: body.Title!.Trim(),
                Description: body.Description,
                DueDate: body.DueDate,
                IsDone: body.IsDone ?? false,
                CreatedAt: now,
                UpdatedAt: now);

            var created = await _repo.CreateAsync(item);
            return await req.JsonAsync(created.ToResponse(), HttpStatusCode.Created);
        }

        [Function("UpdateTask")]
        public async Task<HttpResponseData> UpdateTask(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "tasks/{id}")] HttpRequestData req, string id)
        {
            var existing = await _repo.GetAsync(id);
            if (existing is null) return req.Empty(HttpStatusCode.NotFound);

            var body = await req.ReadJsonAsync<TaskWriteRequest>();
            if (body is null || string.IsNullOrWhiteSpace(body.Title) || body.IsDone is null)
                return await req.JsonAsync(new { error = "Title and isDone are required." }, HttpStatusCode.BadRequest);

            var updated = existing with
            {
                Title = body.Title!.Trim(),
                Description = body.Description,
                DueDate = body.DueDate,
                IsDone = body.IsDone!.Value,
                UpdatedAt = DateTimeOffset.UtcNow
            };

            var ok = await _repo.UpdateAsync(updated);
            if (!ok) return req.Empty(HttpStatusCode.NotFound);
            return await req.JsonAsync(updated.ToResponse());
        }

        [Function("DeleteTask")]
        public async Task<HttpResponseData> DeleteTask(
            [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "tasks/{id}")] HttpRequestData req, string id)
        {
            var ok = await _repo.DeleteAsync(id);
            return ok ? req.Empty(HttpStatusCode.NoContent) : req.Empty(HttpStatusCode.NotFound);
        }
    }
}
