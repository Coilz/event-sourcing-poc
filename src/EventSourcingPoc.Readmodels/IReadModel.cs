using System;

namespace EventSourcingPoc.Readmodels
{
    public interface IReadModel
    {
        Guid Id { get; }
    }
}
