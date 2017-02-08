using System;
using NServiceBus;

namespace Saga.Host.Events
{
    public class LongRunningTaskFailedEvent : IEvent
    {
        public Guid MessageId { get; set; }
        public Guid CorrelationId { get; set; }
    }
}