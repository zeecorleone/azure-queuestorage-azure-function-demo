using Azure.Storage.Queues;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace AzQueueStorage.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class QueueStorageController : ControllerBase
    {
        private readonly ILogger<QueueStorageController> _logger;
        private readonly QueueClient _queueClient;

        public QueueStorageController(ILogger<QueueStorageController> logger, QueueClient queueClient)
        {
            _logger = logger;
            _queueClient = queueClient;
        }

        [HttpPost]
        public async Task Post([FromBody] WeatherForecast weatherForecast)
        {

            var msg = JsonSerializer.Serialize(weatherForecast);

            //message will stay only for 10 seconds
            //await queueClient.SendMessageAsync(msg, null, TimeSpan.FromSeconds(10));

            //appears in queueu after 10 seconds and then stays for 20 seconds
            //await queueClient.SendMessageAsync(msg, TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(20));

            //stays forever (never expires)
            await _queueClient.SendMessageAsync(msg, null, TimeSpan.FromSeconds(-1));

            //await queueClient.SendMessageAsync(msg);
        }
    }
}
