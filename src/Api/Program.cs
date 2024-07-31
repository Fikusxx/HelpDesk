using UI.BackgroundServices;
using UsersAPI.Middlewares;
using Infrastructure;
using Application;
using Api;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddEventStoreOptions()
	.AddMessageBrokerOptions();
builder.Services.RegisterApplicationServices();
builder.Services.RegisterInfrastructureServices();

builder.Services.AddSingleton(TimeProvider.System);

builder.Services.AddHostedService<DomainEventsPublisherService>();

var app = builder.Build();

app.UseDeveloperExceptionPage();

app.UseCors(builder =>
		 builder.AllowAnyOrigin()
				.AllowAnyMethod()
				.AllowAnyHeader());

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<ExceptionMiddleware>();

app.MapControllers();

app.Run();
