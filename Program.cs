using Microsoft.EntityFrameworkCore;
using WukongDemo.Data;
using WukongDemo.inAppMessage.Service;
using WukongDemo.joinRequest.Service;
using WukongDemo.project.Service;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(connectionString));

builder.Services.AddScoped<InAppMessageService>();
builder.Services.AddScoped<JoinRequestService>();
builder.Services.AddScoped<ProjectService>();
builder.Services.AddScoped<ProjectMemberService>();

builder.Services.AddControllers();


var app = builder.Build();

app.UseRouting();
app.MapControllers();

app.Run();
