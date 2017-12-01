using EventSourcingPoc.Data;
using EventSourcingPoc.EventProcessing;
using EventSourcingPoc.Messages;

namespace EventSourcingPoc.Application
{
    public class Bootstrapper
    {
        public static PretendApplication Bootstrap()
        {
            var eventBus = new EventBus();
            var store = new InMemoryEventStore(eventBus);
            var handlerFactory = new CommandHandlerFactory(store);
            var dispatcher = new CommandDispatcher(handlerFactory);

            var readModelStore = new InMemoryReadModelStore();
            var eventHandlerFactory = new EventHandlerFactory(store, dispatcher, readModelStore);
            var eventDispatcher = new EventDispatcher(eventHandlerFactory);
            var eventProcessor = new EventProcessor(eventBus, eventDispatcher);

            return new PretendApplication(readModelStore, dispatcher);
        }

        public class PretendApplication
        {
            private readonly CommandDispatcher _dispatcher;

            public PretendApplication(InMemoryReadModelStore readModelStore, CommandDispatcher dispatcher)
            {
                ReadModelStore = readModelStore;
                _dispatcher = dispatcher;
            }

            public InMemoryReadModelStore ReadModelStore { get; }

            public void Send<TCommand>(TCommand cmd) where TCommand : ICommand
            {
                _dispatcher.Send(cmd);
            }
        }
    }
}
