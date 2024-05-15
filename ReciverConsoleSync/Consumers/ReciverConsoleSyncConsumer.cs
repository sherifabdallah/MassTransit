namespace ReciverConsoleSync.Consumers
{
    using MassTransit;
    using Microsoft.Extensions.Logging;
    using Contracts;
    using System;
    using System.Threading.Tasks;

    public class ReciverConsoleSyncConsumer : IConsumer<RequestMessage>
    {
        readonly ILogger<ReciverConsoleSyncConsumer> _logger;

        public ReciverConsoleSyncConsumer(ILogger<ReciverConsoleSyncConsumer> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<RequestMessage> context)
        {
            // Log the received message
            _logger.LogInformation("Received Text: {Text}", context.Message.Value);

            // Prompt the user for a response
            _logger.LogInformation("Please Enter a response for this message: ");
            string responseMsg = Console.ReadLine();

            // Create the response message
            var response = new ResponseMessage
            {
                Value = $"Processed: {context.Message.Value} & This Message From Console: {responseMsg}"
            };

            // Send the response message
            context.Respond(response);

            _logger.LogInformation("Response message sent successfully");

            // Return a completed Task since the method signature requires it
            return Task.CompletedTask;
        }
    }
}
