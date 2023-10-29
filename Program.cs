using esuperfund.Data;
using esuperfund.Provider;
using esuperfund.Service;
using Microsoft.EntityFrameworkCore;
using Hangfire;
using Hangfire.Storage.SQLite;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDBContext>(options =>
               options.UseMySQL(
                   builder.Configuration.GetConnectionString("DefaultConnection")));

//registering the services
builder.Services.AddScoped<IBankTransactionService, BankTransactionProvider>();

builder.Services.AddScoped<IRawBankTransactionService, RawBankTransactionProvider>();

builder.Services.AddTransient<IBalanceCalculatorService, BalanceCalculatorProvider>();

//congifuring for the schedulers 
builder.Services.AddHangfire(configuration => configuration
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UseSQLiteStorage(builder.Configuration.GetConnectionString("HangfireConnection")));


builder.Services.AddHangfireServer();

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

app.UseHangfireDashboard();

app.MapHangfireDashboard();

//Job set to run daily to update BankTransaction table 
RecurringJob.AddOrUpdate<IBalanceCalculatorService>(x => x.CalculateClosingBalances(), Cron.Daily);

app.Run();

