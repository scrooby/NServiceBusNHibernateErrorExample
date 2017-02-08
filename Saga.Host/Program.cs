using Topshelf;

namespace Saga.Host
{
    public class Program
    {
        public static void Main(string[] args)
        {
            HostFactory.Run(x =>
            {
                x.Service<IServiceHost>(s =>
                {
                    s.ConstructUsing(pc => new ServiceHost());
                    s.WhenStarted((pc, hostControl) => pc.Start(hostControl));
                    s.WhenStopped(pc => pc.Stop());
                });

                x.RunAsLocalSystem();

                x.SetDescription(HostConstants.ServiceName);
                x.SetDisplayName(HostConstants.ServiceName);
                x.SetServiceName(HostConstants.ServiceName);

                x.StartAutomaticallyDelayed();
            });
        }
    }
}