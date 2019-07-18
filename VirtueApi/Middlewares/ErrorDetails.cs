using Newtonsoft.Json;

// FROM: https://code-maze.com/global-error-handling-aspnetcore/
namespace VirtueApi.Middlewares
{
    public class ErrorDetails
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}