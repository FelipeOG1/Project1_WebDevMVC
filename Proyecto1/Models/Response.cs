namespace Proyecto1.Models
{
    public class Response<T>
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public T? Content { get; set; } 

        public static Response<T> CreateResponse(bool status, string message, T? content)
        {
            return new Response<T>
            {
                Status = status,
                Message = message,
                Content = content
            };
        }
    }

}
