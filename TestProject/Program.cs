using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
// �ؼ������ Data �����ռ�����
using TestProject.Data;

var builder = WebApplication.CreateBuilder(args);

// 1. ע�����ݿ������ģ�ȷ�� AppDbContext ��ȷ���ã�
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 2. ע������� + ���� System.Text.Json����� Newtonsoft.Json��
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        options.JsonSerializerOptions.Converters.Add(new DateTimeConverter("yyyy-MM-dd HH:mm:ss"));
    });

// 3. ע�� Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "TestProject API",
        Version = "v1",
        Description = "���� System.Text.Json �� API �ĵ�"
    });
});

// 4. ���ÿ���
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

// 5. ������������ Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "TestProject API v1"));
}

// 6. �����м��
app.UseHttpsRedirection();
app.UseCors();
app.UseAuthorization();

// 7. ӳ�������·��
app.MapControllers();

app.Run();

// �Զ�������ת������ע�⣺�˴��޶��ය�ţ�
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
