using EmployeeVerificationSystem;
using EmployeeVerificationSystem.Models;
using EmployeeVerificationSystem.Interface;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using AutoMapper;
using EmployeeVerificationSystemApi.Mappper;
using System.Reflection;
using Microsoft.Net.Http.Headers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Serilog;

var builder = WebApplication.CreateBuilder(args); // y

//apply the logger
var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);




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
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("V1", new OpenApiInfo { Title = "My Employee Verification System API", Version = "V1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Description = "Bearer Authentication with JWT Token",
        Type = SecuritySchemeType.Http
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Id = "Bearer",
                        Type = ReferenceType.SecurityScheme
                }
            },
            new List < string > ()
        }
    });
});

builder.Services.AddAuthentication(opt => {
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options => {
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        ValidAudience = builder.Configuration["JWT:ValidAudience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))
    };
  });
//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer
//    (options =>
//    {
//        options.TokenValidationParameters = new()
//        {
//            ValidateIssuer = true,
//            ValidateAudience = true,
//            ValidateLifetime = true,
//            ValidateIssuerSigningKey = true,
//            ValidIssuer = builder.Configuration["Jwt:Issuer"],
//            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
//        };
//    }
//);



// Configure automapper
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddControllersWithViews();

// Adding Interface
builder.Services.AddScoped<IEmployeeInfo, EmployeeInformation>();
builder.Services.AddScoped<IEmployeeLogin, EmployeeLogin>();
builder.Services.AddScoped<IEducational, Educational>();
builder.Services.AddScoped<IWorkExp, WokExperience>();
builder.Services.AddScoped<ICertification, CertificationBAL>();
builder.Services.AddScoped<IDocumentsUpload, DocumentsUpload>();

// Another way to Register dependencies
// builder.Services.AddTransient<IEmployeeInfo, EmployeeInformation>();
// builder.Services.AddSingleton<IEmployeeInfo, EmployeeInformation>();


var app = builder.Build(); // y

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/V1/swagger.json", "My Employee Verification System API V1");
    });
}

// Configure the HTTP request pipeline.

app.UseAuthentication();
app.UseAuthorization(); // y
app.MapControllers(); // y
app.UseCors(x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
app.Run(); // y
