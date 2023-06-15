using KweetReadService.Consumers.GDPR;
using KweetReadService.Consumers.Kweet;
using KweetReadService.Consumers.Like;
using KweetReadService.Consumers.Reaction;
using KweetReadService.Data.Kweet;
using KweetReadService.Data.Likes;
using KweetReadService.Data.MongoDB;
using KweetReadService.Data.Reaction;
using KweetReadService.DTOs.SharedClasses;
using KweetReadService.Models;
using KweetReadService.Services.Kweet;
using KweetReadService.Services.Like;
using KweetReadService.Services.Reaction;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MongoDB.Bson;
using Swashbuckle.AspNetCore.Filters;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

// Add services to the container.

builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDbSettings"));

builder.Services.AddSingleton<IMongoDbSettings>(ServiceProvider =>
ServiceProvider.GetRequiredService<IOptions<MongoDbSettings>>().Value);

// Add services to the container.



builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
                      });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//builder.Services.AddScoped<Ilikeservice, LikeService>();
builder.Services.AddScoped(typeof(IKweetMongoRepository<>), typeof(KweetRepository<>));
builder.Services.AddScoped(typeof(ILikeMongoRepository<>), typeof(LikeRepository<>));
builder.Services.AddScoped(typeof(IReactionMongoRepository<>), typeof(ReactionRepository<>));
builder.Services.AddScoped<IKweetReadService, KweetReadService.Services.Kweet.KweetReadService>();
builder.Services.AddScoped<Ilikeservice, LikeService>();
builder.Services.AddScoped<IReactionService, ReactionService>();

builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standard Authorization header using the Bearer scheme (\"bearer {token}\")",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme,
        options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value!)),
                ValidateIssuer = false,
                ValidateAudience = false
            };
            options.MapInboundClaims = false;
        });

var rabbitMqSettings = builder.Configuration.GetSection(nameof(RabbitMqSettings)).Get<RabbitMqSettings>();
builder.Services.AddMassTransit(mt => mt.AddMassTransit(x =>
{
    mt.AddConsumer<CreateKweetConsumer>();
    mt.AddRequestClient<CreateKweetConsumer>();

    mt.AddConsumer<DeleteKweetConsumer>();
    mt.AddRequestClient<DeleteKweetConsumer>();

    mt.AddConsumer<UpdateKweetConsumer>();
    mt.AddRequestClient<UpdateKweetConsumer>();

    mt.AddConsumer<LikeConsumer>();
    mt.AddRequestClient<LikeConsumer>();

    mt.AddConsumer<CreateReactionKweetConsumer>();
    mt.AddRequestClient<CreateReactionKweetConsumer>();

    mt.AddConsumer<DeleteReactionKweetConsumer>();
    mt.AddRequestClient<DeleteReactionKweetConsumer>();

    mt.AddConsumer<GDPRDeleteConsumer>();
    mt.AddRequestClient<GDPRDeleteConsumer>();

    x.UsingRabbitMq((ctx, cfg) =>
    {
        //cfg.Host(rabbitMqSettings.Uri, c =>
        //{
        //    c.Username(rabbitMqSettings.UserName);
        //    c.Password(rabbitMqSettings.Password);
        //});
        cfg.Host(rabbitMqSettings.Uri, "/", c =>
        {
            c.Username(rabbitMqSettings.UserName);
            c.Password(rabbitMqSettings.Password);
        });
        cfg.ReceiveEndpoint("CreateKweet", c =>
        {
            c.ConfigureConsumer<CreateKweetConsumer>(ctx);

        });
        cfg.ReceiveEndpoint("UpdateKweet", c =>
        {
            c.ConfigureConsumer<UpdateKweetConsumer>(ctx);

        });
        cfg.ReceiveEndpoint("DeleteKweet", c =>
        {
            c.ConfigureConsumer<DeleteKweetConsumer>(ctx);

        });
        cfg.ReceiveEndpoint("LikeKweet", c =>
        {
            c.ConfigureConsumer<LikeConsumer>(ctx);

        });
        cfg.ReceiveEndpoint("CreateReactionKweet", c =>
        {
            c.ConfigureConsumer<CreateReactionKweetConsumer>(ctx);

        });
        cfg.ReceiveEndpoint("DeleteReactionKweet", c =>
        {
            c.ConfigureConsumer<DeleteReactionKweetConsumer>(ctx);

        });
        cfg.ReceiveEndpoint("GDPR", c =>
        {
            c.ConfigureConsumer<GDPRDeleteConsumer>(ctx);
        });
    });



}));

var app = builder.Build();

// Configure the HTTP request pipeline.
    app.UseSwagger();
    app.UseSwaggerUI();

app.UseCors(MyAllowSpecificOrigins);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
