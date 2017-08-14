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


        private const int FftDataLength = 256 * sizeof(float);
        public static void CreateFftDataMemoryMap(out MemoryMappedFile map, out Mutex mapStreamMutex, out MemoryMappedViewStream mapViewStream, out byte[] fftData)
        {
            map = MemoryMappedFile.OpenExisting("DopamineFftDataMemory", MemoryMappedFileRights.Read);
            mapStreamMutex = Mutex.OpenExisting("DopamineFftDataMemoryMutex");
            mapViewStream = map.CreateViewStream(0, FftDataLength, MemoryMappedFileAccess.Read);
            fftData = new byte[FftDataLength];
        }
    }
}