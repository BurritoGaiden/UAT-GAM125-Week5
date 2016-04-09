using L4.Unity.Common;
using L4.Unity.Common.Extensions;
using UnityEngine;
using UnityEngine.UI;

public class Player : BaseScript
{
    #region Dependencies
    [Header("HUD Elements")]
    [SerializeField]
    private Image _batteryImage;
    [SerializeField]
    private Image _objectiveImage;

    [Header("Audio")]
    [SerializeField]
    private AudioClip _objectivePickedUpSFX;
    [SerializeField]
    private AudioClip _keyPickedUpSFX;
    [SerializeField]
    private AudioClip _detectedAlertSFX;
    [SerializeField]
    private AudioClip _seenAlertSFX;
    [SerializeField]
    private AudioSource _sfxAudioSource;
    #endregion

    #region Properties
    [Header("Gameplay Logic")]
    [SerializeField]
    private bool _hasKey;
    public bool HasKey
    {
        get { return _hasKey; }
        private set { SetProperty("HasKey", ref _hasKey, value); }
    }

    [SerializeField]
    private bool _hasObjective;
    public bool HasObjective
    {
        get { return _hasObjective; }
        private set { SetProperty("HasObjective", ref _hasObjective, value); }
    }
    #endregion

    public void AddItem(ItemType item)
    {
        // play the proper SFX, assign the boolean, and display the proper HUD element for which type of item was added.
        if (item == ItemType.Key)
        {
            _sfxAudioSource.SwitchAndStartClip(_keyPickedUpSFX);
            HasKey = true;
            _batteryImage.gameObject.SetActiveState(true);
        }
        else
        {
            _sfxAudioSource.SwitchAndStartClip(_objectivePickedUpSFX, true);
            HasObjective = true;
            _objectiveImage.gameObject.SetActiveState(true);
        }
    }

    public void Detected()
    {
        _sfxAudioSource.SwitchAndStartClip(_detectedAlertSFX, false);
    }

    public void Seen()
    {
        // only play the clip and schedule an invoke if the current clip isn't already switched
        if (_sfxAudioSource.clip != _seenAlertSFX)
        {
            _sfxAudioSource.SwitchAndStartClip(_seenAlertSFX);
            Invoke("GoToCheckpoint", _seenAlertSFX.length);
        }
    }

    public void ConsumeKey()
    {
        // set the flag and hide the HUD element
        HasKey = false;
        _batteryImage.gameObject.SetActiveState(false);
    }

    protected override void CheckDependencies()
    {
        base.CheckDependencies();

        this.CheckIfDependencyIsNull(_batteryImage);
        this.CheckIfDependencyIsNull(_objectiveImage);
        this.CheckIfDependencyIsNull(_objectivePickedUpSFX);
        this.CheckIfDependencyIsNull(_keyPickedUpSFX);
        this.CheckIfDependencyIsNull(_sfxAudioSource);
        this.CheckIfDependencyIsNull(_detectedAlertSFX);
        this.CheckIfDependencyIsNull(_seenAlertSFX);
    }

    private void GoToCheckpoint()
    {
        GameManager.Instance.CurrentScene
            .As<MainLevel>()
            .LoadCheckpoint();
    }
}
