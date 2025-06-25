using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
// 关键：添加 Data 命名空间引用
using TestProject.Data;
// 新增：用于静态文件托管相关
using Microsoft.AspNetCore.StaticFiles;

var builder = WebApplication.CreateBuilder(args);

// 1. 注册数据库上下文（确保 AppDbContext 正确引用）
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 2. 注册控制器 + 配置 System.Text.Json（替代 Newtonsoft.Json）
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        options.JsonSerializerOptions.Converters.Add(new DateTimeConverter("yyyy-MM-dd HH:mm:ss"));
    });

// 3. 注册 Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "TestProject API",
        Version = "v1",
        Description = "基于 System.Text.Json 的 API 文档"
    });
});

// 4. 配置跨域
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// 新增：配置静态文件托管，指定前端文件所在目录（这里假设前端文件在 wwwroot 下）
builder.Services.Configure<StaticFileOptions>(options =>
{
    // 可根据实际情况调整，比如前端用了特定文件扩展名需要设置 mime 类型映射等
    var provider = new FileExtensionContentTypeProvider();
    // 如果有自定义的文件扩展名和 mime 类型对应，可在这里添加
    // provider.Mappings[".yourExt"] = "application/your-type";
    options.ContentTypeProvider = provider;
});

var app = builder.Build();

// 5. 开发环境启用 Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "TestProject API v1"));
}

// 新增：启用静态文件托管，这样才能访问到 wwwroot 里的前端页面
app.UseStaticFiles();

// 6. 基础中间件
app.UseHttpsRedirection();
app.UseCors();
app.UseAuthorization();

// 新增：路由重定向，访问根路径时跳转到前端 index.html 页面
app.MapGet("/", () => Results.Redirect("index.html"));

// 7. 映射控制器路由
app.MapControllers();

app.Run();

// 自定义日期转换器（注意：此处无多余逗号）
public class DateTimeConverter : JsonConverter<DateTime>
{
    private readonly string _format;

    public DateTimeConverter(string format)
    {
        _format = format;
    }

    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return DateTime.ParseExact(reader.GetString(), _format, null);
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString(_format));
    }
}
