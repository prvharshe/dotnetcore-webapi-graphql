using GraphQL.Types;
using HotChocolate;
using Microsoft.AspNetCore.Mvc;
//using SampleWebAPIApplication.Models.Types;
using SampleWebAPIApplication.Repository;
using SampleWebAPIApplication.Repository.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleWebAPIApplication.Models.Queries
{
    public class TodoItemsQuery //: ObjectGraphType
    {
        //private readonly ITestRepository _testRepository;

        //public async Task<ActionResult<IEnumerable<TodoItem>>> GetBooks(ITestRepository testRepository) => await testRepository.GetTodoItems();

        //public TodoItemsQuery(ITestRepository testRepository)
        //{
        //    _testRepository = testRepository;

        //    //Field<ListGraphType<TodoItemsType>>(
        //    //name: "items", resolve: context =>
        //    //{
        //    //    return _testRepository.GetTodoItems();
        //    //});

        //}
        //Hardcoded
        public TodoItem GetTodo() =>
        new TodoItem
        {
            Id = 1,
            Description = "Test desc",
            IsComplete = true
        };
        //return _testRepository.

        //public IEnumerable<TodoItem> GetTodo([Service] TestRepository testRepository) =>
        //    testRepository.GetTodoItems();

    }
}
