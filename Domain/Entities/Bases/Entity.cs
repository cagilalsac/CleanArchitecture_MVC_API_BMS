using Domain.Common.Records.Bases;

namespace Domain.Entities.Bases
{
    public abstract class Entity : IRecord
    {
        public int Id { get; set; }
    }
}
