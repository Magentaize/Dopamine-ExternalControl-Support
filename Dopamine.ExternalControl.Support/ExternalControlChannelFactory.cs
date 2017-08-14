using System.ServiceModel;
using Dopamine.ExternalControl.Support.Interface;

namespace Dopamine.ExternalControl.Support
{
    public class ExternalControlChannelFactory
    {
        public static ChannelFactory<IExternalControlServer> CreateExternalControlServerFactory<T>(T callback) where T:IExternalControlServerCallback
        {
            return new DuplexChannelFactory<IExternalControlServer>(new InstanceContext(callback), new NetNamedPipeBinding(),
                new EndpointAddress("net.pipe://localhost/Dopamine/ExternalControlService"));
        }

        public static ChannelFactory<IFftDataServer> CreateFftDataServerFactory()
        {
            return new ChannelFactory<IFftDataServer>(new NetNamedPipeBinding(),
                new EndpointAddress("net.pipe://localhost/Dopamine/ExternalControlService/FftDataServer"));
        }
    }
}