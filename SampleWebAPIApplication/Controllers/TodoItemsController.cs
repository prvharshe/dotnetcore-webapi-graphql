using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SampleWebAPIApplication.Models;
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
        /// 
        /// </summary>
        /// <returns></returns>
        // GET: api/TodoItems
        [HttpGet("GetTodoItems")]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoItems()
        {
            //Call repository function fetching data from database.
            var itemsList = await _testRepository.GetTodoItems();
            return itemsList;
        }

        // GET: api/TodoItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItem>> GetTodoItem(long id)
        {
            //var todoItem = await 

            var todoItem = await _testRepository.GetTodoItem(id);

            if(todoItem == null || todoItem.Value == null)
            {
                return NotFound();
            }

            return todoItem;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItem(long id, TodoItem todoItem)
        {
            if (id != todoItem.Id)
            {
                return BadRequest();
            }
            //await _testRepository.PutTodoItem(id, todoItem);

            try
            {
                await _testRepository.PutTodoItem(todoItem);//_context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/TodoItems
        [HttpPost]
        public async Task<ActionResult<TodoItem>> PostTodoItem(TodoItem todoItem)
        {
            await _testRepository.PostTodoItem(todoItem);
            return CreatedAtAction(nameof(GetTodoItem), new { id = todoItem.Id }, todoItem);
        }

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
