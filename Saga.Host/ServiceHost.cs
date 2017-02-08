using NServiceBus;
using Topshelf;

namespace Saga.Host
{
    public class ServiceHost : IServiceHost
    {
        private IEndpointInstance endpointInstance;

        public bool Start(HostControl hostControl)
        {
            var endpointConfiguration = new EndpointConfiguration(HostConstants.ServiceName);

            endpointConfiguration.UseSerialization<JsonSerializer>();
            endpointConfiguration.UsePersistence<NHibernatePersistence>();
            endpointConfiguration.EnableInstallers();

            var recoverability = endpointConfiguration.Recoverability();

            recoverability.Immediate(immediate =>
            {
                immediate.NumberOfRetries(0);
            });

            recoverability.Delayed(delayed =>
            {
                delayed.NumberOfRetries(0);
            });

            endpointInstance = Endpoint.Start(endpointConfiguration).GetAwaiter().GetResult();
            return true;
        }

        public bool Stop()
        {
            endpointInstance.Stop().GetAwaiter().GetResult();
            return true;
        }
    }
}