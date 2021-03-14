using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SampleWebAPIApplication.Models;
using SampleWebAPIApplication.Models.Context;
using SampleWebAPIApplication.Repository.Interface;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using SampleWebAPIApplication.Models.Entity;

namespace SampleWebAPIApplication.Repository
{
    public class TestRepository : ITestRepository//BaseAsyncRepository, ITestRepository
    {
        //private readonly TodoContext _context;
        private readonly IConfiguration _configuration;
        private readonly TodoContext _sampleAppDbContext;

        public TestRepository(TodoContext sampleAppDbContext)
        {
            _sampleAppDbContext = sampleAppDbContext;
        }

        public async Task<List<TodoItemEntity>> GetTodoItems()
        {
            List<TodoItemEntity> ItemsList = new List<TodoItemEntity>();
            try
            {
                Log.Information("Inside GetTodoItems: ");
                return _sampleAppDbContext.Items.ToList();
            }
            catch (Exception ex)
            {
                Log.Information("Error in GetTodoItems : " + ex.Message.ToString());
                var errorMessage = string.Format("Error encountered on server. Message:'{0}' when writing an object", ex.Message);
                //Log in database
            }
            return ItemsList?.ToList();
        }

        public async Task<TodoItemEntity> DeleteTodoItem(int id)
        {
            Log.Information("Inside DeleteTodoItem: "+ id);
            var items = _sampleAppDbContext.Items
                    .Where(e => e.Id == id)
                    .FirstOrDefault();

            TodoItemEntity item = new TodoItemEntity();

            if (items != null)
            {
                item = _sampleAppDbContext.Items.Where(x => x.Id == id).First();
                _sampleAppDbContext.Items.Remove(item);
                await _sampleAppDbContext.SaveChangesAsync();
                return item;
            }
            return item;
        }

        public async Task<TodoItemEntity> GetTodoItem(long id)
        {
            TodoItemEntity ItemList = new TodoItemEntity();

            try
            {
                Log.Information("Inside GetTodoItem Param : " + id);
                var items = _sampleAppDbContext.Items
                    .Where(e => e.Id == id)
                    .FirstOrDefault();

                if (items != null)
                    return items;

                return null;
            }
            catch (Exception ex)
            {
                Log.Information("Error in GetTranspotersByContainerNumber : " + ex.Message.ToString());
                var errorMessage = string.Format("Error encountered on server. Message:'{0}' when writing an object", ex.Message);
                //Log in database
            }
            return ItemList;
        }

        public async Task<TodoItemEntity> PostTodoItem(TodoItemEntity todoItemEntity)
        {
            Log.Information("Inside PostTodoItem:");
            await _sampleAppDbContext.Items.AddAsync(todoItemEntity);
            await _sampleAppDbContext.SaveChangesAsync();
            return todoItemEntity;
        }

        public async Task<TodoItemEntity> PutTodoItem(TodoItemEntity todoItemEntity)
        {
            Log.Information("Inside PutTodoItem:");
            var items = _sampleAppDbContext.Items
                    .Where(e => e.Id == todoItemEntity.Id)
                    .FirstOrDefault();

            if (items != null)
            {
                items.Description = todoItemEntity.Description;
                items.IsComplete = todoItemEntity.IsComplete;
                await _sampleAppDbContext.SaveChangesAsync();
                return todoItemEntity;
            }

            return todoItemEntity;
        }
    }
}
