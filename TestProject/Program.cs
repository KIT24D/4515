using TestProject.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models; // ȷ����Ӵ������ռ�

var builder = WebApplication.CreateBuilder(args);

// 1. ע�����ݿ�������
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 2. ע������� + ���� Newtonsoft.Json���滻Ĭ�ϵ� System.Text.Json��
builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

// 3. ע�� Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "TestProject API", Version = "v1" });
});

// 4. ע�����
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

// 5. ���ÿ��������м��
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
