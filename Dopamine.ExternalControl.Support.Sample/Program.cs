using System;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Threading;
using Dopamine.ExternalControl.Support.Interface;

namespace Dopamine.ExternalControl.Support.Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            var callback = ExternalControlServerFactory.CreateExternalControlClientCallback();
            callback.PlaybackPaused += () => Console.WriteLine("Event PlaybackPaused raised");
            callback.PlaybackResumed += () => Console.WriteLine("Event PlaybackResumed raised");

            try
            {
                var controlServer = ExternalControlServerFactory.CreateExternalControlServer(callback);
                controlServer.RegisterClient();
                controlServer.PlayNext();
                Console.WriteLine(controlServer.GetCurrenTrack().TrackTitle);
            }
            catch (Exception ex)
            {
                ShowException(ex);

                Environment.Exit(0);
            }

            IFftDataServer fftDataServer = null;
            try
            {
                fftDataServer = ExternalControlServerFactory.CreateFftDataServer();
            }
            catch (Exception ex)
            {
                ShowException(ex);

                Environment.Exit(0);
            }

            ExternalControlServerFactory.CreateFftDataMemoryMap(fftDataServer, out MemoryMappedFile map, out MemoryMappedViewStream mapViewStream, out byte[] fftData);
            var fftFloat = new float[fftData.Length / sizeof(float)];
            var reader = new BinaryReader(mapViewStream);
            try
            {
                fftDataServer.GetFftData();
                mapViewStream.Seek(0, SeekOrigin.Begin);
                fftData = reader.ReadBytes(fftData.Length);
                Buffer.BlockCopy(fftData, 0, fftFloat, 0, fftData.Length);
                foreach (var fft in fftFloat)
                {
                    Console.WriteLine(fft);
                }
                //Console.ReadLine();
            }
            catch (Exception ex)
            {
                ShowException(ex);
            }
            finally
            {
                reader.Dispose();
                mapViewStream.Dispose();
                map?.Dispose();
            }
        }

        static void ShowException(Exception ex)
        {
            Console.WriteLine(ex.Message);
            Console.WriteLine(ex.StackTrace);
        }
    }
}
