using CloudTasker.Api.Models;
using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudTasker.Api.Data
{
    public class CosmosTaskRepository: ITaskRepository
    {
        private readonly Container _container;

        public CosmosTaskRepository(Container container)
        {
            _container = container;
        }

        public async Task<IEnumerable<TaskItem>> ListAsync(CancellationToken ct = default)
        {
            var query = _container.GetItemQueryIterator<TaskItem>("SELECT * FROM c");
            var results = new List<TaskItem>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync(ct);
                results.AddRange(response.ToList());
            }
            return results;
        }

        public async Task<TaskItem?> GetAsync(string id, CancellationToken ct = default)
        {
            try
            {
                var response = await _container.ReadItemAsync<TaskItem>(id, new PartitionKey(id), cancellationToken: ct);
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task<TaskItem> CreateAsync(TaskItem item, CancellationToken ct = default)
        {
            item.Id = Guid.NewGuid().ToString();
            var response = await _container.CreateItemAsync(item, new PartitionKey(item.Id), cancellationToken: ct);
            return response.Resource;
        }

        public async Task<bool> UpdateAsync(TaskItem item, CancellationToken ct = default)
        {
            try
            {
                await _container.UpsertItemAsync(item, new PartitionKey(item.Id), cancellationToken: ct);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(string id, CancellationToken ct = default)
        {
            try
            {
                await _container.DeleteItemAsync<TaskItem>(id, new PartitionKey(id), cancellationToken: ct);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}