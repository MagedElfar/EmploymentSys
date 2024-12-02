
namespace Core.Errors
{
    public class ApiErrorResponse
    {
        public ApiErrorResponse(int statuseCode, string? message = null)
        {
            StatuseCode = statuseCode;
            Message = message ?? GetDefulteMessage(StatuseCode);
        }

        public string Type => "Error";
        public int StatuseCode { get; set; }
        public string? Message { get; set; }

        private string? GetDefulteMessage(int statuseCode)
        {
            return StatuseCode switch
            {
                400 => "Bad Requset Error",
                401 => "Unauthorized",
                403 => "Forbidden",
                404 => "Not Found Error",
                500 => "Internal Server Error",
                _ => null
            };
        }
    }
}
