using L4.Unity.Common.Audio;

public class AudioSourceExtender : AudioSourceExtenderBase
{
    protected override void Start()
    {
        base.Start();

        // set the initial values at start
        AudioSource.volume = GameManager.Instance.Settings.GetAudioLevelFromChannel(AudioChannel);
        // register for future changes
        GameManager.Instance.Settings.ChannelVolumeChanged += OnVolumeSourceChanged;
    }

    protected override void OnVolumeSourceChanged(object sender, ChannelVolumeChangedEventArgs e)
    {
        if (e.Channel == AudioChannel.Master ||
            e.Channel == AudioChannel)
        {
            AudioSource.volume = GameManager.Instance.Settings.GetAudioLevelFromChannel(AudioChannel);
        }
    }
}
