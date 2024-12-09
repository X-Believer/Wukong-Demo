using Microsoft.EntityFrameworkCore;
using WukongDemo.Data;
using WukongDemo.inAppMessage.Service;
using WukongDemo.joinRequest.Service;
using WukongDemo.project.Service;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(connectionString));

// Register Services
builder.Services.AddScoped<InAppMessageService>();
builder.Services.AddScoped<JoinRequestService>();
builder.Services.AddScoped<ProjectService>();
builder.Services.AddScoped<ProjectMemberService>();

// Add Controllers
builder.Services.AddControllers();

// Add Authentication (JWT Bearer)
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"]))
        };
    });

// Add Authorization
builder.Services.AddAuthorization();

// Add Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Wukong Demo API",
        Version = "v1",
        Description = "A list of all APIs available in Wukong Demo."
    });
    // 配置 Swagger 以显示 JWT 认证的请求头
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token as 'Bearer <your_token>'",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

var app = builder.Build();

// Enable Authentication and Authorization
app.UseAuthentication();
app.UseAuthorization();

app.UseRouting();
app.MapControllers();

// Enable Swagger
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Wukong Demo API v1");
    options.RoutePrefix = "api-docs";
});

// Enable Static Files
app.UseStaticFiles();
app.MapGet("/home", () => Results.Redirect("/home.html"));
app.MapGet("/test", () => Results.Redirect("/testApi.html"));

app.Run();
