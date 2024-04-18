using Application.Contexts.Bases;

namespace Application.Common.Handlers.Bases
{
    public abstract class HandlerBase : IDisposable
    {
        protected readonly IDb _db;

        protected HandlerBase(IDb db)
        {
            _db = db;
        }

        public void Dispose()
        {
            _db?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
