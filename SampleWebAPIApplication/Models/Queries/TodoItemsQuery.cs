using GraphQL.Types;
using HotChocolate;
using HotChocolate.Subscriptions;
using Microsoft.AspNetCore.Mvc;
using SampleWebAPIApplication.Models.Entity;
//using SampleWebAPIApplication.Models.Types;
using SampleWebAPIApplication.Repository;
using SampleWebAPIApplication.Repository.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleWebAPIApplication.Models.Queries
{
    public class TodoItemsQuery
    {
        public Task<List<TodoItemEntity>> AllTodoItemsOnly([Service] TestRepository testRepository) =>
            testRepository.GetTodoItems();

        public async Task<TodoItemEntity> GetTodoItemsById([Service] TestRepository testRepository,
            [Service]ITopicEventSender eventSender, int id)
        {
            TodoItemEntity gotItemById = await testRepository.GetTodoItem(id);
            await eventSender.SendAsync("ReturnedItems", gotItemById);
            return gotItemById;
        }
    }
}
