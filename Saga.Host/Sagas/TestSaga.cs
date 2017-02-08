using System;
using System.Threading.Tasks;
using NServiceBus;
using Saga.Host.Commands;
using Saga.Host.Events;
using Saga.Host.SagaData;

namespace Saga.Host.Sagas
{
    public class TestSaga : Saga<TestSagaData>,
        IAmStartedByMessages<StartTestSagaCommand>,
        IHandleMessages<LongRunningTaskFailedEvent>
    {
        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<TestSagaData> mapper)
        {
            mapper.ConfigureMapping<StartTestSagaCommand>(m => m.CorrelationId).ToSaga(s => s.CorrelationId);
            mapper.ConfigureMapping<LongRunningTaskFailedEvent>(m => m.CorrelationId).ToSaga(s => s.CorrelationId);
        }

        public async Task Handle(StartTestSagaCommand message, IMessageHandlerContext context)
        {
            Data.CorrelationId = message.CorrelationId;
            Data.SagaState = "Started";
            Data.SagaStateUpdatedDate = DateTime.UtcNow;

            await context.Publish(new LongRunningTaskFailedEvent
            {
                CorrelationId = message.CorrelationId
            });
        }

        public Task Handle(LongRunningTaskFailedEvent message, IMessageHandlerContext context)
        {
            Data.SagaState = "LongRunningTaskFailed";
            Data.SagaStateUpdatedDate = DateTime.UtcNow;

            return Task.CompletedTask;
        }
    }
}