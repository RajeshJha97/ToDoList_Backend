using System.Net;

namespace ToDoList.Models
{
    public class APIResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public bool isSuccess { get; set; } = false;
        public string Message { get; set; }
        public object Result { get; set; }
        public object Error { get; set; }
    }
}
