using AutoMapper;
using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Configuration;
using WebApplication1.Data;
using WebApplication1.Infrustructure;
using WebApplication1.Models;
using WebApplication1.Services;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllersWithViews();




builder.Services.AddScoped<IDatabase,Database>();


builder.Services.AddSwaggerGen();


builder.Services.AddScoped(typeof(IFreindsRepository), typeof(FreindsRepository));


var config = new MapperConfiguration(cfg =>
{
    cfg.AddProfile<AutoMapperProfile>();
});
var mapper = config.CreateMapper();


builder.Services.AddEntityFrameworkSqlServer();


builder.Services.AddEntityFrameworkSqlServer().AddDbContext<MyContaxt>(config =>
{
    config.UseSqlServer("Data Source=DESKTOP-4RKBK3P;Initial Catalog=Maktabkhooneh_Exercise;Integrated Security=True");
});


builder.Services.AddHangfire(configuration => configuration
    .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
    .UseSimpleAssemblyNameTypeSerializer()
    .UseRecommendedSerializerSettings()
    .UseSqlServerStorage("Data Source=DESKTOP-4RKBK3P;Initial Catalog=Maktabkhooneh_Exercise;Integrated Security=True"));



builder.Services.AddSingleton(mapper);


builder.Services.AddHangfireServer();

var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.Map("/total-friends", (context) =>
{
    context.Run(async (httpcontext) => { await httpcontext.Response.WriteAsync("Total Number Of Friends Is :"+ WebApplication1.Controllers.FriendsController.GetTotalFriendsAsync()); });
});




app.Map("/friends-json", (cc) =>
{
    List<Friend> _friend = new List<Friend>();
    for (int i = 0; i < 10; i++)
    {
        _friend.Add(new Friend { Name = "Friend:" + i, Phone = i, Profile = "/images/Friend" + i + ".jfif" });
    }
    var json = JsonConvert.SerializeObject(_friend);
    return cc.Response.WriteAsync(json);
});




app.UseSwagger();
app.UseSwaggerUI(
    c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Adolf API");
    }
    );

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseHangfireDashboard();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
