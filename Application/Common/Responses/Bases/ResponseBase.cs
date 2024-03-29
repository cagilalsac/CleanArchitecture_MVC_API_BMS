namespace Application.Common.Responses.Bases
{
    public abstract record ResponseBase
    {
        public int Id { get; set; }

        protected ResponseBase(int id)
        {
            Id = id;
        }

        protected ResponseBase()
        {
        }
    }
}
