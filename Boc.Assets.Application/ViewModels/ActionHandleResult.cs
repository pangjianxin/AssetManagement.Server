namespace Boc.Assets.Application.ViewModels
{
    public class ActionHandleResult
    {
        public bool Success { get; set; }
        public object Data { get; set; }
        public string Message { get; set; }

        public ActionHandleResult(bool success, string message, object data = null)
        {
            Success = success;
            Message = message;
            Data = data;
        }
    }
}