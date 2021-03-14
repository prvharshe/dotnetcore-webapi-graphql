using AWS.Logger.SeriLog;
using GraphQL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SampleWebAPIApplication.Middlewares;
using SampleWebAPIApplication.Middlewares.Builder;
using SampleWebAPIApplication.Models;
using SampleWebAPIApplication.Models.Context;
using SampleWebAPIApplication.Models.Queries;
using SampleWebAPIApplication.Repository;
using SampleWebAPIApplication.Repository.Interface;
using Serilog;
using System;
using System.IO;

namespace SampleWebAPIApplication
{
    public class Startup
    {
        private readonly string AllowedOrigin = "allowedOrigin";
        public Startup(IWebHostEnvironment env)
        {

            //string basePath = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            //Configuration = configuration;
            var builder = new ConfigurationBuilder()
                //.SetBasePath(basePath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            if (env.IsDevelopment())
            {
                builder.AddUserSecrets<Startup>(optional: true);
            }
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<TodoContext>(options =>
             options.UseSqlServer(Configuration.GetConnectionString("AppDbContext")));
            services.AddInMemorySubscriptions();

            //GraphQL services
            services
            .AddRouting()
            .AddGraphQLServer()
            .AddQueryType<TodoItemsQuery>()
            .AddMutationType<Mutation>()
            .AddSubscriptionType<Subscription>();

            services.AddScoped<TestRepository, TestRepository>();
            services.AddScoped<ITestRepository, TestRepository>();
            services.AddSingleton<IServiceProvider>(_ => new
            FuncServiceProvider(_.GetRequiredService));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddLogging(loggingBuilder =>
                loggingBuilder.AddSerilog(dispose: true));

            services.AddControllers();
            //services.AddDbContext<TodoContext>(opt => opt.UseInMemoryDatabase("TodoList")); //IN CASE WE NEED TO USE IN MEMORY DATABASE

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "TodoList API", Version = "v1" });
            });

            services.AddCors(option =>
            {
                option.AddPolicy("allowedOrigin",
                    builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()
                    );
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory, IHttpContextAccessor httpContextAccessor)
        {
            Log.Logger = new LoggerConfiguration()
                  .WriteTo.AWSSeriLog(Configuration)
                 .Enrich.With(new CorrelationLogEventEnricher(httpContextAccessor, Configuration["Logging:CorrelationHeaderKey"]))
                  .CreateLogger();

            // Important: it has to be first: enable global logger
            app.UseGlobalLoggerHandler();

            // Important: it has to be second: Enable global exception, error handling
            app.UseGlobalExceptionHandler();
            //app.UseExceptionHandler("/Error");

            //GraphQL 

            app.UseSwagger(c =>
            {
                c.RouteTemplate = "TodoItems/swagger/{documentname}/swagger.json";
            });

            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = "TodoItems/swagger";
                c.SwaggerEndpoint("/TodoItems/swagger/v1/swagger.json", "Weather Forecast API V1");
                c.DisplayRequestDuration();
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();


            app.UseAuthorization();

            app.UseCors(AllowedOrigin);
            app.UseWebSockets();

            app
                .UseRouting()
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                    endpoints.MapGraphQL();
                });
        }
    }
}
