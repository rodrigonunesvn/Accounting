using Accounting.Application.Services;
using Accounting.Application.Validation;
using Accounting.Core.Services;
using Accounting.Infrastructure;
using Accounting.Infrastructure.Cache;
using Accounting.Infrastructure.DatabaseContext;
using Microsoft.Azure.ServiceBus;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AccountingDbContext>(db => db.UseSqlServer(builder.Configuration.GetConnectionString("SQLServerConnectionString")), ServiceLifetime.Singleton);
builder.Services.AddDistributedRedisCache(options =>
{
	options.Configuration = builder.Configuration.GetConnectionString("AccountingRedisConnectionString");
	options.InstanceName = "AccountingCache";
});

builder.Services.AddSingleton<IMongoDbContext>(sp =>
{
    var connectionString = builder.Configuration.GetConnectionString("MongoDBConnectionString");
    var databaseName = builder.Configuration.GetConnectionString("MongoDBDatabaseName");

	return new MongoDbContext(connectionString, databaseName);
});

var serviceBusConnectionString = builder.Configuration.GetConnectionString("ServiceBusConnectionString");
var queueName = builder.Configuration["ServiceBusQueueName"];

builder.Services.AddSingleton<IQueueClient>(x => new QueueClient(serviceBusConnectionString, queueName));

builder.Services.AddScoped<ITransactionCacheManager, TransactionCacheManager>();
builder.Services.AddScoped<IReportsRepository, ReportsRepository>();
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
builder.Services.AddScoped<ITransactionService, TransactionService>();
builder.Services.AddScoped<ITransactionValidationService, TransactionValidationService>();
builder.Services.AddScoped<IReportsService, ReportsService>();
builder.Services.AddScoped<IReportsRepository, ReportsRepository>();

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
