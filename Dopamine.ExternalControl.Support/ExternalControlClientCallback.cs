using System;
using Dopamine.ExternalControl.Support.Interface;

namespace Dopamine.ExternalControl.Support
{
    public class ExternalControlClientCallback : IExternalControlServerCallback
    {
        public event Action HeartBeat = delegate { };
        public event Action PlaybackSuccess = delegate { };
        public event Action PlaybackStopped = delegate { };
        public event Action PlaybackPaused = delegate { };
        public event Action PlaybackResumed = delegate { };
        public event Action PlaybackProgressChanged = delegate { };
        public event Action PlaybackVolumeChanged = delegate { };
        public event Action PlaybackMuteChanged = delegate { };
        public event Action PlayingTrackPlaybackInfoChanged = delegate { };
        public event Action PlayingTrackArtworkChanged = delegate { };

        public void SendHeartBeat() => HeartBeat();

        public void RaiseEventPlaybackSuccess() => PlaybackSuccess();

        public void RaiseEventPlaybackStopped() => PlaybackStopped();

        public void RaiseEventPlaybackPaused() => PlaybackPaused();

        public void RaiseEventPlaybackResumed() => PlaybackResumed();

        public void RaiseEventPlaybackProgressChanged() => PlaybackProgressChanged();

        public void RaiseEventPlaybackVolumeChanged() => PlaybackVolumeChanged();

        public void RaiseEventPlaybackMuteChanged() => PlaybackMuteChanged();

        public void RaiseEventPlayingTrackPlaybackInfoChanged() => PlayingTrackPlaybackInfoChanged();

        public void RaiseEventPlayingTrackArtworkChanged() => PlayingTrackArtworkChanged();
    }
}
