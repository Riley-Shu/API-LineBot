using Sample07_Service.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//= = = = = 註冊服務:ServiceURL = = = = = 
ServiceURL serviceURL = new ServiceURL();
ConfigurationManager manager = builder.Configuration;
IConfigurationSection section = manager.GetSection("Services");
section.Bind(serviceURL);
builder.Services.AddSingleton(serviceURL);
//= = = = = = = = = = = = = = = = = = = =

//= = = = = 註冊服務: AddHttpClient = = = = = 
builder.Services.AddHttpClient();
//= = = = = = = = = = = = = = = = = = = =



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


app.Run();