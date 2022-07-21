using CryptoAvenue.Application.Commands.CoinCommands;
using CryptoAvenue.Application.Commands.UserCommands;
using CryptoAvenue.Application.Commands.WalletCommands;
using CryptoAvenue.Dal;
using CryptoAvenue.Dal.Repositories;
using CryptoAvenue.Domain.IRepositories;
using CryptoAvenue.Services;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<CryptoAvenueDbContext>();

builder.Services.AddScoped<IScopedService, ScopedService>();
builder.Services.AddTransient<ITransientService, TransientService>();
builder.Services.AddSingleton<ISingletonService, SingletonService>();

builder.Services.AddScoped<ICoinRepository, CoinRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IWalletRepository, WalletRepository>();
builder.Services.AddScoped<ITradeOfferRepository, TradeOfferRepository>();

builder.Services.AddMediatR(typeof(Program));
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddMediatR(typeof(CreateUserCommand));
builder.Services.AddMediatR(typeof(CreateCoinCommand));
builder.Services.AddMediatR(typeof(CreateWalletCommand));

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
