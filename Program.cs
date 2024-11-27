using Microsoft.EntityFrameworkCore;
using WukongDemo.Data;
using WukongDemo.Services;
using WukongDemo.Repositories;

var builder = WebApplication.CreateBuilder(args);

// 添加数据库上下文
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))); // 连接字符串

// 添加其他服务
builder.Services.AddScoped<IRecruitmentService, RecruitmentService>();
builder.Services.AddScoped<IRecruitmentRepository, RecruitmentRepository>();

builder.Services.AddControllers();

var app = builder.Build();

// 配置请求管道
app.UseRouting();
app.MapControllers();

app.Run();
