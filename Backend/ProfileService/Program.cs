using MassTransit;
using ProfileService.Models.RabbitMq;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var rabbitMqSettings = builder.Configuration.GetSection(nameof(RabbitMqSettings)).Get<RabbitMqSettings>();
builder.Services.AddMassTransit(mt => mt.AddMassTransit(x => {
    mt.AddConsumer<ProfileConsumer>();
    x.UsingRabbitMq((ctx, cfg) => {
        cfg.Host(rabbitMqSettings.Uri, c => {
            c.Username(rabbitMqSettings.UserName);
            c.Password(rabbitMqSettings.Password);
        });
        cfg.ReceiveEndpoint("profile", c =>
        {
            c.ConfigureConsumer<ProfileConsumer>(ctx);

        });
    });
}));

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}












//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
