using Microsoft.EntityFrameworkCore;
using TeachersApi.AppDbContext;
using TeachersApi.ContractInterface;
using TeachersApi.MappingProfiles;
using TeachersApi.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<TeacherDbcontext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("TeacherConnectionstring")));

builder.Services.AddScoped<ITeacherRepository, TeacherRepository>();
builder.Services.AddAutoMapper(typeof(MappingProfiles));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();