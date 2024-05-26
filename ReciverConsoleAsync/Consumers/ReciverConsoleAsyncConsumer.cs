using Internal;
using System;
namespace ReciverConsoleAsync.Consumers
{
    using System.Threading.Tasks;
    using MassTransit;
    using Microsoft.Extensions.Logging;
    using Contracts;
    using System;

    public class ReciverConsoleAsyncConsumer :
        IConsumer<RequestMessage>
    {
        readonly ILogger<ReciverConsoleAsyncConsumer> _logger;

        public ReciverConsoleAsyncConsumer(ILogger<ReciverConsoleAsyncConsumer> logger)
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<RequestMessage> context)
        {
            // Log the received message
            _logger.LogInformation("Received Text: {Text}", context.Message.Value);

            // Prompt the user for a response
            //_logger.LogInformation("Please Enter a response for this message: ");
            DateTime responseMsg = DateTime.Now;

            // Create the response message
            var response = new ResponseMessage
            {
                Value = $"Processed: {context.Message.Value} & This Message From Console: {responseMsg}"
            };

            // Send the response message asynchronously
            await context.RespondAsync(response);

            // Optional: you can remove this commented-out line since it's redundant
            // return Task.CompletedTask;

            // _logger.LogInformation($"Response message sent successfully: {context.Message.Value}");
        }

    }
}