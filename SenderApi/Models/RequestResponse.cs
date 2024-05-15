namespace Contracts
{
    public class RequestMessage
    {
        public string Value { get; set; }
    }

    public class ResponseMessage
    {
        public string Value { get; set; }
    }
    public class RequestResponseMessage {
        public int Id { get; set; }
        public string? RequestMessage { get; set; }
        public string? ResponseMessage { get; set; }
    }
}
