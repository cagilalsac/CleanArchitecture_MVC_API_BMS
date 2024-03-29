namespace Application.Common.Responses.Bases
{
    public abstract record Response : ResponseBase
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
