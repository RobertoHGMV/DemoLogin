namespace DemoLogin.Api.Fire.ViewModels
{
    public class ResultViewModel
    {
        public ResultViewModel() { }

        public ResultViewModel(bool success, string message, object docs)
        {
            Success = success;
            Message = message;
            Docs = docs;
        }

        public bool Success { get; set; }
        public string Message { get; set; }
        public object Docs { get; set; }
    }
}
