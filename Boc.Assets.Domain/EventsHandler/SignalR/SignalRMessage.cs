using System;

namespace Boc.Assets.Domain.EventsHandler.SignalR
{
    public class SignalRMessage
    {
        public SignalRMessage(string principal, string target, string message)
        {
            Principal = principal;
            Target = target;
            Message = message;
            TimeStamp = GetCurrentTimeStamp();
        }
        public string Principal { get; set; }
        public string Target { get; set; }
        public string Message { get; set; }
        public long TimeStamp { get; set; }

        private long GetCurrentTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalMilliseconds);
        }
    }
}