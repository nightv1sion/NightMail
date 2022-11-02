using backend;
using backend.Extensions;
using Microsoft.EntityFrameworkCore;
using Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.ConfigureDbContext(builder.Configuration);

builder.Services.ConfigureIdentity();

builder.Services.AddAutoMapper(options => { options.AddProfile<MappingProfile>(); });

builder.Services.ConfigureServiceManager();

builder.Services.ConfigureJwtAuthentication(builder.Configuration);

builder.Services.ConfigureRepositoryManager();

builder.Services.ConfigureCors();

var app = builder.Build();

app.UseCors("_policy");

app.ConfigureExceptionHandler();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}   

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

using(var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetService<RepositoryContext>();
    if(context.Database.GetPendingMigrations().Any())
    {
        context.Database.Migrate();    
    }
}

app.Run();


