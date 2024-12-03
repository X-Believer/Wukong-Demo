// Program.cs
using Microsoft.EntityFrameworkCore;
using WukongDemo.Data;
using WukongDemo.inAppMessage.Services;

var builder = WebApplication.CreateBuilder(args);

// 获取数据库连接字符串
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// 注册数据库上下文，使用 SQLite
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(connectionString));

// 注册其他服务
builder.Services.AddScoped<InAppMessageService>();


builder.Services.AddControllers();

var app = builder.Build();

app.UseRouting();
app.MapControllers();

app.Run();
