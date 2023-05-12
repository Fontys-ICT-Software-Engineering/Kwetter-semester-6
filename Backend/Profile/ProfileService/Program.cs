using MassTransit;
using ProfileService.Models.RabbitMq;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using ProfileService.Services;
using Microsoft.EntityFrameworkCore;
using ProfileService.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<DataContext>(options =>
{

    if (builder.Environment.IsDevelopment())
    {
        options.UseMySql(builder.Configuration.GetConnectionString("MigrationConnection"), new MySqlServerVersion(new Version(5, 7, 31)));
    }
    else
    {
        options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"), new MySqlServerVersion(new Version(5, 7, 31)));
        //options.UseMySql(builder.Configuration.GetConnectionString("KubernetesConnection"), new MySqlServerVersion(new Version(5, 7, 31)));
    }
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IProfileService, ProfileServiceLayer>();

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
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<DataContext>();
    context.Database.Migrate();
}
// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI();

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
