using Domain.Entities.Bases;

namespace Application.Common.Responses.Bases
{
    public abstract record Response : IRecord
    {
        public int Id { get; set; }
        public bool IsSuccessful { get; }
        public string Message { get; }

        protected Response(bool isSuccessful, string message, int id)
        {
            IsSuccessful = isSuccessful;
            Message = message;
            Id = id;
        }
    }
}
