namespace EventSourcingPoc.Domain.Core.Commands
{
    public class CommandResponse
    {
        public static CommandResponse Ok => new CommandResponse(true);
        public static CommandResponse Fail => new CommandResponse();

        public CommandResponse(bool success = false)
        {
            Success = success;
        }

        public bool Success { get; }
    }
}
