using Domain.Common.Records.Bases;

namespace Application.Common.Responses.Bases
{
    public record Response : Record
    {
        public bool IsSuccessful { get; }
        public string Message { get; }

        protected Response(bool isSuccessful, string message, int id) : base(id)
        {
            IsSuccessful = isSuccessful;
            Message = message;
        }
    }
}
