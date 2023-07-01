using AzQueueStorage;
using Azure.Identity;
using Azure.Storage.Queues;
using Microsoft.Extensions.Azure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//commenting because: we'll now use Azure Function to Dequeue the message
//builder.Services.AddHostedService<WeatherDataService>();



builder.Services.AddAzureClients(builder =>
{
    builder.AddClient<QueueClient, QueueClientOptions>((options, _, _) =>
    {
        options.MessageEncoding = QueueMessageEncoding.Base64;

        var credentials = new DefaultAzureCredential();
        var queueUri = new Uri("https://<storage-acc-name>.queue.core.windows.net/<queue-name>");
        var queueClient = new QueueClient(queueUri, credentials, options);

        //Without MangedIdentity
        //var connectionString = "<storage-acc-connection-string>";
        //var queueName = "<queue-name>";
        //var queueClient = new QueueClient(connectionString, queueName, options);
        
        
        return queueClient;
    });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
