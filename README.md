Demonstrates Azure Queue Storage and Azure Function for understanding basic working of these two services. The solution contains two projects.

 ### AzQueueStorage: 

 - This is a `WebApi` project. The `post` method of `QueueStorage`
   controller can be used to enqueue message to the queue. The method shows different ways to enqueue. Inside
   `program.cs`, you can configure connection to your azure account. By
   default, it is using `Managed Identity`, but following inline
   comments, you can easily substitute your
   `<storage-acc-connection-string>` and `<queue-name>` and make start
   enqueueing messages.
 - This WebApi project has a `BackgroundJob` named `WeatherDataService`
   which is listening to the queue and dequeues any message as soon as
   it arrives in the queue. By default, it is disabled, because I had to
   add `Azure Function` to demonstrate `QueueTrigger`. You can enable it
   on line 12 and 13 of `Program.cs`.

### AzWeatherDataIngestor:
This is `Azure Function` app, that uses `QueueTrigger` to dequeue the messages from the queue. You can substitute your `<storage-acc-connection-string>` and `<queue-name>` and run it.
