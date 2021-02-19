using Microsoft.AspNetCore.Mvc;
using SampleWebAPIApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleWebAPIApplication.Repository.Interface
{
    public interface ITestRepository
    {
        Task<ActionResult<IEnumerable<TodoItem>>> GetTodoItems();
        Task<ActionResult<TodoItem>> GetTodoItem(long id);
        Task<int> PutTodoItem(TodoItem todoItem);
        Task<int> PostTodoItem(TodoItem todoItem);
        //Task<ActionResult<TodoItem>> PostTodoItem(TodoItem todoItem);
        Task<int> DeleteTodoItem(int id);
    }
}
