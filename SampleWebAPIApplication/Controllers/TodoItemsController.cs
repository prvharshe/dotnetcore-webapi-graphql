using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SampleWebAPIApplication.Models;
using SampleWebAPIApplication.Models.Entity;
using SampleWebAPIApplication.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SampleWebAPIApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly ITestRepository _testRepository;


        public TodoItemsController(ITestRepository testRepository)
        {
            _testRepository = testRepository;
        }

        /// <summary>
        ///Get all todo items 
        /// </summary>
        /// <returns></returns>
        // GET: api/TodoItems
        [HttpGet("GetTodoItems")]
        public async Task<ActionResult<List<TodoItemEntity>>> GetTodoItems()
        {
            //Call repository function fetching data from database.
            var itemsList = await _testRepository.GetTodoItems();
            return itemsList;
        }

        /// <summary>
        ///Get todo item by id
        /// </summary>
        // GET: api/TodoItem/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItemEntity>> GetTodoItem(long id)
        {
            //var todoItem = await 

            var todoItem = await _testRepository.GetTodoItem(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            return todoItem;
        }

        /// <summary>
        ///Edit todo item
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItem(long id, TodoItemEntity todoItemEntity)
        {
            if (id != todoItemEntity.Id)
            {
                return BadRequest();
            }
            //await _testRepository.PutTodoItem(id, todoItem);

            try
            {
                await _testRepository.PutTodoItem(todoItemEntity);//_context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }

            return NoContent();
        }

        /// <summary>
        ///Add new todo item
        /// </summary>
        // POST: api/TodoItems
        [HttpPost]
        public async Task<ActionResult<TodoItemEntity>> PostTodoItem(TodoItemEntity todoItemEntity)
        {
            await _testRepository.PostTodoItem(todoItemEntity);
            return CreatedAtAction(nameof(GetTodoItem), new { id = todoItemEntity.Id }, todoItemEntity);
        }

        /// <summary>
        ///Delete todo item
        /// </summary>
        //DELETE: api/TodoItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(int id)
        {
            try
            {
                await _testRepository.DeleteTodoItem(id);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
