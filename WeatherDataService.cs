using Azure.Storage.Queues;
using System.Text.Json;

namespace AzQueueStorage
{
    public class WeatherDataService : BackgroundService
    {
        private readonly ILogger<WeatherDataService> _logger;
        private readonly QueueClient _queueClient;

        public WeatherDataService(ILogger<WeatherDataService> logger, QueueClient queueClient)
        {
            _logger = logger;
            _queueClient = queueClient;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
           
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Starting read...");
                var queueMessage = await _queueClient.ReceiveMessageAsync();
                if(queueMessage is not null && queueMessage.Value is not null)
                {
                    var weatherData = JsonSerializer.Deserialize<WeatherForecast>(queueMessage.Value.MessageText);
                    _logger.LogInformation($"Read Message: {weatherData.Summary}");
                    await _queueClient.DeleteMessageAsync(queueMessage.Value.MessageId, queueMessage.Value.PopReceipt);
                }

                await Task.Delay(TimeSpan.FromSeconds(10));
            }
        }
    }
}
