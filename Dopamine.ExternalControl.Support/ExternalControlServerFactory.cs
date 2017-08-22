using System.IO.MemoryMappedFiles;
using System.Threading;
using Dopamine.ExternalControl.Support.Interface;

namespace Dopamine.ExternalControl.Support
{
    public static class ExternalControlServerFactory
    {
        public static ExternalControlClientCallback CreateExternalControlClientCallback()
        {
            return new ExternalControlClientCallback();
        }

        public static IExternalControlServer CreateExternalControlServer<T>(T callback) where T:IExternalControlServerCallback
        {
            return ExternalControlChannelFactory.CreateExternalControlServerFactory(callback).CreateChannel();
        }

        public static IFftDataServer CreateFftDataServer()
        {
            return ExternalControlChannelFactory.CreateFftDataServerFactory().CreateChannel();
        }

        public static void CreateFftDataMemoryMap(IFftDataServer fftDataServer, out MemoryMappedFile map, out MemoryMappedViewStream mapViewStream, out byte[] fftData)
        {
            var fftDataSize = fftDataServer.GetFftDataSize();
            map = MemoryMappedFile.OpenExisting("DopamineFftDataMemory", MemoryMappedFileRights.Read);
            mapViewStream = map.CreateViewStream(0, fftDataSize, MemoryMappedFileAccess.CopyOnWrite);
            fftData = new byte[fftDataSize];
        }
    }
}