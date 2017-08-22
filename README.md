# Dopamine-ExternalControl-Support
A plug-in to add Dopamine external control support

## How to use it
Add reference `Dopamine.ExternalControl.Support` in your project, and use in-built factory method to create Dopamine plug-in client. There's a simple sample in the other folder. For more APIs, please look at here: [IExternalControlServer](https://github.com/Magentaize/Dopamine/blob/master/Dopamine.Common/Services/ExternalControl/IExternalControlServer.cs), [IFftDataServer](https://github.com/Magentaize/Dopamine/blob/master/Dopamine.Common/Services/ExternalControl/IFftDataServer.cs)

### How to control behaviors of Dopamine
After create a IExternalControlServer, you can simply use `IExternalControlServer.PlayNext()` to force Dopamine to play next track and so on.

### How to get events raised by Dopamine
There are many events in `ExternalControlClientCallback`, you can subscribe them and then call  `IExternalControlServer.RegisterClient()` to register your client in Dopamine, once Dopamine raises some event such as `PlaybackPaused`, the corresponding method will be called.

### How to get FFT data of Dopamine
Dopamine uses shared memory to provide FFT data. Before reading it, you must call `IFftDataServer.GetFftData()` to notice Dopamine someone want to read FFT data, then you can read it.

### How to make sure Dopamine has been opened (external control is enabled)
1. If the client is opened after Dopamine and external control isn't enabled, WCF will throw a exception when create channel.
2. If the  client is opened before Dopamine, you can call `IExternalControlServer.SendHeartbeat()` to detect whether Dopamine is online or not, if external control isn't enabled, WCF will throw a exception.

## How to start using this api from scratch
1. Right click on your project, choose Add->Service Reference.
2. Open Dopamine and enable its external control service in Settings->Playback->External Control.
3. Go back to Visual Studio, enter "net.pipe://localhost/Dopamine/ExternalControlService/mex" in the textbox below "Address".
4. Edit the namespace of this service at the bottom of this window and then click OK.