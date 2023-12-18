using Microsoft.AspNetCore.StaticFiles;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(
    options=> { 
        
        options.ReturnHttpNotAcceptable = true;
    
    }
    ).AddXmlDataContractSerializerFormatters();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<FileExtensionContentTypeProvider>();

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
