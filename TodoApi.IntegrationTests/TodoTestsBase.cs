using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Json;
using TodoApi.Models;

namespace TodoApi.IntegrationTests
{
    public abstract class TodoTestsBase
    {
        protected async Task<int> PostTodoItem(HttpClient client)
        {
            var todoItem = ValidTodoItem();

            var response = await client.PostAsJsonAsync("/api/todoitems", todoItem);
            var result = await response.Content.ReadFromJsonAsync<TodoItemDTO>();

            return result.Id!;
        }

        private static TodoItemDTO ValidTodoItem() => new TodoItemDTO()
        {
            Id = 0,
            Name = "Test",
            IsComplete = true
        };

        protected async Task<TodoItemDTO> GetTodoItem(HttpClient client, int id)
        {
            var response = await client.GetAsync($"/api/todoitems/{id}");
            return await response.Content.ReadFromJsonAsync<TodoItemDTO>()!;
        }
    }

    internal class WebApplicationFactoryWithInMemoryDb : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.Remove(services.First(s => s.ServiceType == typeof(TodoContext)));
                services.AddDbContext<TodoContext>(options => options.UseInMemoryDatabase("InMemoryDbForTesting"));
            });

            base.ConfigureWebHost(builder);
        }
    }
}
