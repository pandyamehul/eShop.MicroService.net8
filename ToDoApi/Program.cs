using Microsoft.EntityFrameworkCore;
using ToDoApi;

var builder = WebApplication.CreateBuilder(args);

//Add dependency Injection
builder.Services.AddDbContext<TodoDb>(opt => opt.UseInMemoryDatabase("TodoList"));

var app = builder.Build();

// Configure pipeline - use methoild

//GET /todoitems
app.MapGet("/todoitems", async(TodoDb todoDb) =>
        await todoDb.TodoItems.ToListAsync());

//GET /todoitems/{id}
app.MapGet("/todoitems/{id}", async (int id, TodoDb todoDb) => 
        await todoDb.TodoItems.FindAsync(id));

//POST /todoitems
app.MapPost("/todoitems", async(TodoItem todoItem, TodoDb todoDb) =>
{
    todoDb.TodoItems.Add(todoItem);
    await todoDb.SaveChangesAsync();
    return Results.Created($"/todoitems/{todoItem.Id}", todoItem);
});

//PUT /todoitems
app.MapPut("/todoitems/{id}", async (int id, TodoItem todoitem, TodoDb todoDb) =>
{
    var todo = await todoDb.TodoItems.FindAsync(id);
    if(todo == null) return Results.NotFound();
    todo.Name = todoitem.Name;
    todo.IsCompleted = todoitem.IsCompleted;
    await todoDb.SaveChangesAsync();
    return Results.NoContent();
});

//DELETE /todoitems
app.MapDelete("/todoitems/{id}", async (int id, TodoDb todoDb) =>
{
    if(await todoDb.TodoItems.FindAsync(id) is TodoItem todoItem)
    {
        todoDb.TodoItems.Remove(todoItem);
        await todoDb.SaveChangesAsync();
        return Results.NoContent();
    }
    return Results.NotFound();
});

app.Run();
