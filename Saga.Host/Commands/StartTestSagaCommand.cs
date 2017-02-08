using System;
using NServiceBus;

namespace Saga.Host.Commands
{
    public class StartTestSagaCommand : ICommand
    {
        public Guid MessageId { get; set; }
        public Guid CorrelationId { get; set; }
    }
}