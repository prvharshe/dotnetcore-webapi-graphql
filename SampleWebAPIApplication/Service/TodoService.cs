using Microsoft.AspNetCore.Mvc;
using SampleWebAPIApplication.Models;
using SampleWebAPIApplication.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleWebAPIApplication.Service
{
    public class TodoService
    {
        private readonly TestRepository _todoRepository;

        public TodoService(TestRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }

        //public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoItems()
        //{
        //    return await _todoRepository.GetTodoItems();
        //}

        public async Task<int> DeleteTodoItems(int id)
        {
            return await _todoRepository.DeleteTodoItem(id);
        }

        public async Task<ActionResult<TodoItem>> GetTodoItem(long id)
        {
            return await _todoRepository.GetTodoItem(id);
        }

        public async Task<int> PostTodoItem(TodoItem todoItem)
        {
            return await _todoRepository.PostTodoItem(todoItem);
        }

        public async Task<int> PutTodoItem(TodoItem todoItem)
        {
            return await _todoRepository.PutTodoItem(todoItem);
        }
    }
}
