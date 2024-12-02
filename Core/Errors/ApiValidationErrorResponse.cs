namespace Core.Errors
{
    public class ApiValidationErrorResponse : ApiErrorResponse
    {
        public ApiValidationErrorResponse() : base(400)
        {
        }

        public ApiValidationErrorResponse(IEnumerable<string> errors):base(400)
        {
            Errors = errors;
        }

        public IEnumerable<string> Errors { get; set; }
    }
}
