using System;
using NServiceBus;

namespace Saga.Host.SagaData
{
    public class TestSagaData : ContainSagaData
    {
        public virtual Guid CorrelationId { get; set; }
        public virtual string SagaState { get; set; }
        public virtual DateTime SagaStateUpdatedDate { get; set; }
    }
}