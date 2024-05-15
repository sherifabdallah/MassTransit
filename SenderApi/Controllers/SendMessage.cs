using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Contracts;
using System.Data.SQLite;
using SenderApi.Data;
// using SenderApi.Services;


namespace SenderApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SendMessageController : ControllerBase
    {
        readonly IBus _bus;
        readonly IRequestClient<RequestMessage> _requestClient;
        private readonly MessageDbContext _dbContext;



        public SendMessageController(IBus bus, IRequestClient<RequestMessage> requestClient, MessageDbContext dbContext)
        {
            _bus = bus;
            _requestClient = requestClient;
            _dbContext = dbContext;
        }

        [HttpPost]
        public async Task<IActionResult> Post(string msg)
        {
            // var sendEndpoint = await _bus.GetSendEndpoint(new Uri("queue:test_queue"));

            // await sendEndpoint.Send(new Message { Value = $"Msg is: {msg}" });

            // return Ok("Message Sent");
            var request = new RequestMessage { Value = msg };
            var response = await _requestClient.GetResponse<ResponseMessage>(request);

            

 
            // Add the Request and Response Messages to db here
            // Add the request and response messages to the database
            var requestResponse = new RequestResponseMessage
            {
                RequestMessage = msg,
                ResponseMessage = response.Message.Value
            };
            _dbContext.RequestResponseMessages.Add(requestResponse);
            await _dbContext.SaveChangesAsync();
            

            return Ok(response.Message.Value);

        }
    }
}
