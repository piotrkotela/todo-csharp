using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using todo_csharp.Controllers;
using todo_csharp.Data;
using todo_csharp.Models;

public class TodoItemsControllerTests
{
    private readonly DbContextOptions<ApplicationDbContext> _dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
        .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
        .Options;

    private async Task<ApplicationDbContext> GetDatabaseContextWithSeededData()
    {
        var context = new ApplicationDbContext(_dbContextOptions);
        await context.TodoItems.AddAsync(new TodoItem { Name = "Test Item", IsComplete = false });
        await context.SaveChangesAsync();
        return context;
    }

    [Fact]
    public async Task PostTodoItem_ShouldAddItem()
    {
        // Arrange
        await using var context = new ApplicationDbContext(_dbContextOptions);
        var controller = new TodoItemsController(context);
        var newItem = new TodoItem { Name = "New Test Item", IsComplete = false };

        // Act
        var result = await controller.PostTodoItem(newItem);

        // Assert
        var actionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        var returnValue = Assert.IsType<TodoItem>(actionResult.Value);
        Assert.Equal(newItem.Name, returnValue.Name);
    }

    [Fact]
    public async Task PutTodoItem_ShouldUpdateItem()
    {
        await using var context = await GetDatabaseContextWithSeededData();
        var controller = new TodoItemsController(context);
        var existingItem = await context.TodoItems.FirstAsync();
        existingItem.Name = "Updated Item";

        var result = await controller.PutTodoItem(existingItem.Id, existingItem);

        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task DeleteTodoItem_ShouldRemoveItem()
    {
        await using var context = await GetDatabaseContextWithSeededData();
        var controller = new TodoItemsController(context);
        var existingItem = await context.TodoItems.FirstAsync();

        var result = await controller.DeleteTodoItem(existingItem.Id);

        Assert.IsType<NoContentResult>(result);
        Assert.False(context.TodoItems.Any(item => item.Id == existingItem.Id));
    }
}
