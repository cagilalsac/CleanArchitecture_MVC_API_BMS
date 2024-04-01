using Application.Contexts.Bases;

namespace Application.Common.Handlers.Bases
{
    public abstract class HandlerBase
    {
        protected readonly IDb _db;

        protected HandlerBase(IDb db)
        {
            _db = db;
        }
    }
}
