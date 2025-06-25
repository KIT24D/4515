using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TestProject.Data;

var builder = WebApplication.CreateBuilder(args);

// ע�����ݿ������ģ�����ʵ���������������� EF Core ���� ��
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// �ؼ����滻 AddControllers Ϊ AddControllersWithViews������Ŀ֧�� MVC ����������ͼ
builder.Services.AddControllersWithViews();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // ���þ�̬�ļ����� CSS��JS��ͼƬ�� ��

app.UseRouting();

app.UseAuthorization();

// �ؼ������� MVC ·�ɣ�����Ĭ����ҳ
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
