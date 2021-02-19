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

namespace SampleWebAPIApplication.Repository
{
    public class TestRepository : BaseAsyncRepository, ITestRepository
    {
        //private readonly TodoContext _context;
        private readonly IConfiguration _configuration;

        public TestRepository(IConfiguration configuration) : base(configuration)
        {
            //_context = context;
            _configuration = configuration;
        }

        public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoItems()
        {
            List<TodoItem> ItemsList = new List<TodoItem>();

            try
            {
                Log.Information("Inside GetTodoItems: ");
                using (DbConnection dbConnection = new SqlConnection(_configuration.GetSection("SQLServerDBInfo:ReaderConnectionString").Value))
                {
                    await dbConnection.OpenAsync();
                    Log.Information("Db connection open");
                    var result = await dbConnection.QueryAsync<TodoItem>(@"SELECT TOP (1000) [Id]
                                                                         ,[Description]
                                                                         ,[IsComplete]
                                                                        FROM [TodoDb].[dbo].[Items]");


                    if (result?.FirstOrDefault()?.Id != null)
                    {
                        ItemsList.Add(new TodoItem
                        {
                            Id = result.FirstOrDefault().Id,
                            Description = result?.FirstOrDefault()?.Description,
                            IsComplete = result.FirstOrDefault().IsComplete
                        });
                    }
                    else
                    {
                        //ItemsList = new List<TodoItem>();
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Information("Error in GetTranspotersByContainerNumber : " + ex.Message.ToString());
                var errorMessage = string.Format("Error encountered on server. Message:'{0}' when writing an object", ex.Message);
                //Log in database
            }
            return ItemsList?.ToList();
        }

        public async Task<int> DeleteTodoItem(int id)
        {
            int result = 0;
            if (!string.IsNullOrEmpty(id.ToString()))
            {
                using (DbConnection dbConnection = new SqlConnection(_configuration.GetSection("SQLServerDBInfo:ReaderConnectionString").Value))
                {
                    await dbConnection.OpenAsync();
                    result = await dbConnection.ExecuteAsync(@"DELETE FROM dbo.Items where Id = @Id",
                    new { Id = id });
                }
            }
            return result;
        }

        public async Task<ActionResult<TodoItem>> GetTodoItem(long id)
        {
            TodoItem ItemList = new TodoItem();

            try
            {
                Log.Information("GetTranspotersByContainerNumber Param : ");
                using (DbConnection dbConnection = new SqlConnection(_configuration.GetSection("SQLServerDBInfo:ReaderConnectionString").Value))
                {
                    await dbConnection.OpenAsync();
                    Log.Information("Db connection open");
                    var result = await dbConnection.QueryAsync<TodoItem>(@"SELECT TOP (1000) [Id]
                                                                         ,[Description]
                                                                         ,[IsComplete]
                                                                        FROM [TodoDb].[dbo].[Items] where Id =" + id);


                    if (result?.FirstOrDefault()?.Id != null)
                    {
                        ItemList = result.FirstOrDefault();
                    }
                    else
                    {
                        //
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Information("Error in GetTranspotersByContainerNumber : " + ex.Message.ToString());
                var errorMessage = string.Format("Error encountered on server. Message:'{0}' when writing an object", ex.Message);
                //Log in database
            }
            return ItemList;
        }

        public async Task<int> PostTodoItem(TodoItem todoItem)
        {
            int result = 0;

            Log.Information("GetTranspotersByContainerNumber Param : ");
            using (DbConnection dbConnection = new SqlConnection(_configuration.GetSection("SQLServerDBInfo:ReaderConnectionString").Value))
            {
                await dbConnection.OpenAsync();
                Log.Information("Db connection open");
                result = await dbConnection.ExecuteAsync(@"INSERT INTO dbo.Items(Id, Description, IsComplete) 
                                    VALUES(@Id,@Description,@IsComplete)", todoItem);
                //INSERT INTO dbo.Items(Id, Description, IsComplete) VALUES(1, 'Item 1', 0);
            }
            return result;
            //_context.TodoItems.Add(todoItem);
            //await _context.SaveChangesAsync();
            //return null;
        }

        public async Task<int> PutTodoItem(TodoItem todoItem)
        {
            using (DbConnection dbConnection = new SqlConnection(_configuration.GetSection("SQLServerDBInfo:ReaderConnectionString").Value))
            {
                int result = 0;
                await dbConnection.OpenAsync();

                var itemList = await dbConnection.QueryAsync<TodoItem>(@"SELECT Id,Description,IsComplete FROM dbo.Items WHERE
                                                                                    Id = @Id",
                                                                       new { Id = todoItem.Id });
                var selectedItem = itemList.FirstOrDefault();
                if (selectedItem != null)
                {
                    string updateQuery = string.Format("Update dbo.Items SET Description='{0}' , IsComplete = '{1}' WHERE Id = {2}", todoItem.Description, todoItem.IsComplete, todoItem.Id);
                    result = await dbConnection.ExecuteAsync(updateQuery);
                }
                return result;
            }
        }
            //_context.Entry(todoItem).State = EntityState.Modified;

            //var dbTodoItems = await _context.TodoItems.FindAsync(todoItem.Id);
            //if (todoItem == null)
            //{
            //    return null;
            //}

            //dbTodoItems.Description = todoItem.Description;
            //dbTodoItems.IsComplete = todoItem.IsComplete;

            ////try
            ////{
            //await _context.SaveChangesAsync();
            ////}

            ////catch (DbUpdateConcurrencyException) when (!TodoItemExists(todoItem.Id))
            ////{
            ////    return null;
            ////}

            //return null;
        //}

        //private bool TodoItemExists(long id)
        //{
        //    return _context.TodoItems.Any(e => e.Id == id);
        //}
    }
}
