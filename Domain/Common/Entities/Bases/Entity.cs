using Domain.Common.Records.Bases;

namespace Domain.Common.Entities.Bases
{
    public abstract class Entity : IRecord
    {
        public int Id { get; set; }
    }
}
