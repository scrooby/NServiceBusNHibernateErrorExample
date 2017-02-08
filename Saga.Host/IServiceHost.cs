using Topshelf;

namespace Saga.Host
{
    public interface IServiceHost
    {
        bool Start(HostControl hostControl);
        bool Stop();
    }
}