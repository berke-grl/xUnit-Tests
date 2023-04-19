using ServiceContracts;
using Services;
using xUnitHomework;

var builder = WebApplication.CreateBuilder(args);
//Services
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();
builder.Services.Configure<TradingOptions>(builder.Configuration.GetSection("TradingOptions"));
builder.Services.AddScoped<IFinnhubService, FinnhubService>();
var app = builder.Build();

app.MapControllers();
app.UseStaticFiles();

app.Run();
