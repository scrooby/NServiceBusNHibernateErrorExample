using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Threading;
using Dapper;
using NServiceBus;
using NUnit.Framework;
using Ploeh.AutoFixture;
using Saga.Host.Commands;
using Saga.Host.Events;
using Saga.Host.SagaData;
using TechTalk.SpecFlow;

namespace Saga.Host.FeatureTests.Steps
{
    [Binding]
    public sealed class TestSagaSteps
    {
        private readonly Fixture fixture;

        private Guid correlationId;

        public TestSagaSteps()
        {
            fixture = new Fixture();
        }

        [Given(@"I have sent a start test saga command")]
        public void GivenIHaveSentAStartTestSagaCommand()
        {
            correlationId = fixture.Create<Guid>();

            var message = fixture.Build<StartTestSagaCommand>()
                .With(x => x.CorrelationId, correlationId)
                .Create();

            var endpointConfiguration = new EndpointConfiguration("Test.Host");

            endpointConfiguration.UseSerialization<JsonSerializer>();
            endpointConfiguration.UsePersistence<NHibernatePersistence>();
            endpointConfiguration.EnableInstallers();

            var endpointInstance = Endpoint.Start(endpointConfiguration).ConfigureAwait(false).GetAwaiter().GetResult();

            endpointInstance.Send(message).GetAwaiter().GetResult();

            endpointInstance.Stop().ConfigureAwait(false).GetAwaiter().GetResult();
        }

        [Given(@"I have an existing test saga")]
        public void GivenIHaveAnExistingTestSaga()
        {
            correlationId = fixture.Create<Guid>();

            var sagaData = fixture.Build<TestSagaData>()
                .With(x => x.CorrelationId, correlationId)
                .With(x => x.SagaState, "Started")
                .Create();

            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Default"].ConnectionString))
            {
                var sql = "insert into dbo.TestSagaData (Id, Originator, OriginalMessageId, CorrelationId, SagaState, SagaStateUpdatedDate) " +
                          $"values ('{sagaData.Id}', '{sagaData.Originator}', '{sagaData.OriginalMessageId}', '{sagaData.CorrelationId}', '{sagaData.SagaState}', GETUTCDATE())";

                connection.Execute(sql);
            }            
        }

        [When(@"I have sent a long running task failure event")]
        public void WhenIHaveSentALongRunningTaskFailureEvent()
        {
            var message = fixture.Build<LongRunningTaskFailedEvent>()
                .With(x => x.CorrelationId, correlationId)
                .Create();

            var endpointConfiguration = new EndpointConfiguration("Test.Host");

            endpointConfiguration.UseSerialization<JsonSerializer>();
            endpointConfiguration.UsePersistence<NHibernatePersistence>();
            endpointConfiguration.EnableInstallers();

            var endpointInstance = Endpoint.Start(endpointConfiguration).ConfigureAwait(false).GetAwaiter().GetResult();

            endpointInstance.Publish(message).GetAwaiter().GetResult();

            endpointInstance.Stop().ConfigureAwait(false).GetAwaiter().GetResult();
        }

        [Then(@"the test saga data state should be LongRunningTaskFailed")]
        public void ThenTheTestSagaDataStateShouldBeLongRunningTaskFailed()       
        {
            Thread.Sleep(5000);

            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Default"].ConnectionString))
            {
                var data = connection.QueryFirstOrDefault<TestSagaData>($"select * from dbo.TestSagaData where CorrelationId = '{correlationId}'");

                Assert.IsNotNull(data);
                Assert.AreEqual("LongRunningTaskFailed", data.SagaState);
            }
        }
    }
}