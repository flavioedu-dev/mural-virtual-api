using MuralVirtual.API.Extensions.IoC;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDI(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.AddMiddlewares();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
