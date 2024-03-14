using Domain.Entities.Bases;

namespace Application.Common.Responses.Bases
{
    public record Response : IRecord
    {
        public int Id { get; set; }
        public bool IsSuccessful { get; }
        public string Message { get; }

        public Response(bool isSuccessful, string message, int id)
        {
            IsSuccessful = isSuccessful;
            Message = message;
            Id = id;
        }
    }
}
