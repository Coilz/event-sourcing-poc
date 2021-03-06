﻿using System;
using System.Threading.Tasks;
using EventSourcingPoc.EventSourcing.Handlers;
using EventSourcingPoc.Messages;

namespace EventSourcingPoc.EventSourcing
{
    public class EventProcessor : IEventObserver
    {
        private readonly IEventDispatcher _dispatcher;
        private readonly Action _unsubscribeAction;

        public EventProcessor(IEventBus eventBus, IEventDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
            _unsubscribeAction = eventBus.Subscribe(this);
        }

        public async Task NotifyAsync<TEvent>(TEvent evt)
            where TEvent : Event
        {
            await _dispatcher.SendAsync(evt);
        }
    }
}
