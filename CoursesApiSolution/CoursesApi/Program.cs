using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using CoursesApi;
var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers(options =>
    options.Filters.Add<OperationCancelledExceptionFilterAttribute>()
    ).AddJsonOptions(options =>
    {

        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());

    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<CourseCatalog>();
// Adapters
builder.Services.AddDbContext<CoursesDataContext>(config =>
{
    config.UseSqlServer(builder.Configuration.GetConnectionString("courses-db"));
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
