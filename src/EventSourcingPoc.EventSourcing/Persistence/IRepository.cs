using System;
using EventSourcingPoc.EventSourcing.Domain;

namespace EventSourcingPoc.EventSourcing.Persistence
{
    public interface IRepository
    {
        T GetById<T>(Guid id) where T : EventStream, new();

        void Save(params EventStream[] streamItems);
    }
}