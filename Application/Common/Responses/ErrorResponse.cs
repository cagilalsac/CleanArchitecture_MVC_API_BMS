using Application.Common.Responses.Bases;

namespace Application.Common.Responses
{
    public record ErrorResponse : Response
    {
        public ErrorResponse(string message) : base(false, message, default)
        {
        }

        public ErrorResponse() : base(false, string.Empty, default)
        {
        }
    }
}
