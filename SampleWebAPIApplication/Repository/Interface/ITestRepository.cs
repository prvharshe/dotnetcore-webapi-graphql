using Microsoft.AspNetCore.Mvc;
using SampleWebAPIApplication.Models;
using SampleWebAPIApplication.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleWebAPIApplication.Repository.Interface
{
    public interface ITestRepository
    {
        Task<List<TodoItemEntity>> GetTodoItems();
        Task<TodoItemEntity> GetTodoItem(long id);
        Task<TodoItemEntity> PutTodoItem(TodoItemEntity todoItemEntity);
        Task<TodoItemEntity> PostTodoItem(TodoItemEntity todoItemEntity);
        //Task<ActionResult<TodoItem>> PostTodoItem(TodoItem todoItem);
        Task<TodoItemEntity> DeleteTodoItem(int id);
    }
}
