//using GraphQL;
//using GraphQL.Types;
//using Microsoft.AspNetCore.Mvc;
//using SampleWebAPIApplication.Models;
//using SampleWebAPIApplication.Repository.Interface;
//using System;
//using System.Collections.Generic;
//using System.Threading.Tasks;

//namespace SampleWebAPIApplication.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class GraphQLController: ControllerBase
//    {
//        //private readonly ISchema _schema;
//        //private readonly IDocumentExecuter _executer;
//        private readonly ITestRepository _testRepository;

//        //public GraphQLController(ISchema schema,
//        //IDocumentExecuter executer)
//        //{
//        //    _schema = schema;
//        //    _executer = executer;
//        //}
//        public GraphQLController(ITestRepository testRepository)
//        {
//            _testRepository = testRepository;
//        }

//        [HttpPost]
//        public async Task<IActionResult> Post([FromBody] GraphQLQueryDTO query)
//        {
//            if (query == null)
//            {
//                throw new ArgumentNullException(nameof(query));
//            }

//            var values = query.Variables?.ToObject<Dictionary<string, object>>(); //ToInputs() accepts only object of type Dictionary<string,object>
//            var inputs = values?.ToInputs();
//            //var result = await _executer.ExecuteAsync(_ =>
//            //{
//            //    _.Schema = _schema;
//            //    _.Query = query.Query;
//            //    _.Inputs = inputs;
//            //});
//            var executionOptions = new ExecutionOptions
//            {
//                Schema = _schema,
//                Query = query.Query,
//                Inputs = inputs
//            };

//            var result = await _executer.ExecuteAsync(executionOptions);

//            if (result.Errors?.Count > 0)
//            {
//                return BadRequest();
//            }
//            return Ok(result.Data);
//        }

//        [HttpGet("GetTodoItems")]
//        public IActionResult GetTodoItems()
//        {
//            //Call repository function fetching data from database.
//            return Ok(_testRepository.GetTodoItems());
//        }

//    }
//}
