using TestProject.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models; // 确保添加此命名空间

var builder = WebApplication.CreateBuilder(args);

// 1. 注册数据库上下文
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 2. 注册控制器 + 启用 Newtonsoft.Json（替换默认的 System.Text.Json）
builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

// 3. 注册 Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "TestProject API", Version = "v1" });
});

// 4. 注册跨域
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// 5. 配置开发环境中间件
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "TestProject API v1"));
}

// 6. 基础中间件
app.UseHttpsRedirection();
app.UseCors();
app.UseAuthorization();

// 7. 映射控制器路由
app.MapControllers();

app.Run();
