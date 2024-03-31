﻿namespace Application.Common.Responses.Bases
{
    public record Response 
    {
        public bool IsSuccessful { get; }
        public string Message { get; }
        public int Id { get; set; }

        protected Response(bool isSuccessful, string message, int id) 
        {
            IsSuccessful = isSuccessful;
            Message = message;
            Id = id;
        }
    }
}
