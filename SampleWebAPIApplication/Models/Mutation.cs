using HotChocolate;
using HotChocolate.Subscriptions;
using SampleWebAPIApplication.Models.Entity;
using SampleWebAPIApplication.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleWebAPIApplication.Models
{
    public class Mutation
    {
        public async Task<TodoItemEntity> CreateTodoItem([Service] TestRepository testRepository,
            [Service] ITopicEventSender eventSender, string description, bool isComplete)
        {
            var newTodoItem = new TodoItemEntity
            {
                Description = description,
                IsComplete = isComplete
            };
            var createdTodoItem = await testRepository.PostTodoItem(newTodoItem);

            await eventSender.SendAsync("TodoItemCreated", createdTodoItem);

            return createdTodoItem;
        }

        public async Task<TodoItemEntity> EditTodoItem([Service] TestRepository testRepository,
            [Service] ITopicEventSender eventSender, int id, string description,bool isComplete)
        {
            var newTodoItem = new TodoItemEntity
            {
                Description = description,
                Id = id,
                IsComplete = isComplete
            };
            var createdTodoItem = await testRepository.PutTodoItem(newTodoItem);

            await eventSender.SendAsync("TodoItemCreated", createdTodoItem);

            return createdTodoItem;
        }

        public async Task<TodoItemEntity> DeleteTodoItem([Service] TestRepository testRepository,
            [Service] ITopicEventSender eventSender, int id)
        {
            var newTodoItem = new TodoItemEntity
            {
                Id = id,
            };
            var deletedTodoItem = await testRepository.DeleteTodoItem(newTodoItem.Id);

            await eventSender.SendAsync("TodoItemDeleted", deletedTodoItem);

            return deletedTodoItem;
        }
    }
}
