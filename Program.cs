using Microsoft.AspNetCore.StaticFiles;
using Serilog;
using CityInfo.api.Services;
using CityInfo.api;
using CityInfo.api.DbContexts;
using Microsoft.EntityFrameworkCore;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.File("log/cityinfo.text", rollingInterval: RollingInterval.Day)
    .CreateLogger()
    ;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();

// Add services to the container.

builder.Services.AddControllers(
    options=> { 
        
        options.ReturnHttpNotAcceptable = false;
    
    } 
    )
    .AddNewtonsoftJson()
    .AddXmlDataContractSerializerFormatters();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<FileExtensionContentTypeProvider>();

#if DEBUG
builder.Services.AddTransient<IMailService, LocalMailService>();
#else 
builder.Services.AddTransient<IMailService, CloudMailService>(); 
#endif

builder.Services.AddSingleton<CitiesDataStore>();

builder.Services.AddDbContext<CityInfoContext>(DbContextOptions => DbContextOptions.UseSqlite(builder.Configuration["ConnectionStrings:CityInfoDBConnectionString"]));
builder.Services.AddScoped<ICityInfoReposotiory, CityInfoReposotory>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseRouting();
app.UseEndpoints(endpoints => {
    endpoints.MapControllers();
});

app.UseAuthorization();

app.MapControllers();


app.Run();
