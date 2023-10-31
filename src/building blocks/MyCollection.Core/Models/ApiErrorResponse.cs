namespace MyCollection.Core.Models
{
    public class ApiErrorResponse
    {
        private const string Message = "Messages";
        private readonly Dictionary<string, string[]> _response = new();
        private readonly List<string> Errors = new();
        public ApiErrorResponse()
        {

        }
        public ApiErrorResponse(List<string> errors)
        {
            Errors.AddRange(errors);
            _response.Add(Message, Errors.ToArray());
        }

        public Dictionary<string, string[]> Response { get => _response; }

        public bool HasErrors() => Errors.Count > 0;

        public void AddErrors()
        {
            _response.Add(Message, Errors.ToArray());
        }

        public void AddError(string error)
        {
            Errors.Add(error);
            _response.Add(Message, Errors.ToArray());
        }
    }
}
