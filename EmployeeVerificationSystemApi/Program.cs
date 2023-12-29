using EmployeeVerificationSystem;
using EmployeeVerificationSystem.Models;
using EmployeeVerificationSystem.Interface;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args); // y

// Add services to the container.

builder.Services.AddControllers(); // y

// Add services to the container.
//Add Db context
builder.Services.AddDbContext<EmployeeContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("LMSCONN")));

// Enable CORS
builder.Services.AddCors(x => x.AddPolicy("EmployeeSystem", x => x.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin()));

//swagger Documentation
//Configuring Swagger OPENAPI settings
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("V1", new OpenApiInfo { Title = "My Employee Verification System API", Version = "V1" });
});

// Adding Interface
builder.Services.AddScoped<IEmployeeInfo, EmployeeInformation>();

// Another way to Register dependencies
// builder.Services.AddTransient<IEmployeeInfo, EmployeeInformation>();
// builder.Services.AddSingleton<IEmployeeInfo, EmployeeInformation>();


var app = builder.Build(); // y

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => {
        c.SwaggerEndpoint("/swagger/V1/swagger.json", "My Employee Verification System API V1");
    });
}

// Configure the HTTP request pipeline.
app.UseCors(x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
app.UseAuthorization(); // y

app.MapControllers(); // y

app.Run(); // y