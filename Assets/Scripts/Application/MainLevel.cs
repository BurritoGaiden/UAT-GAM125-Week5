using L4.Unity.Common;
using L4.Unity.Common.Extensions;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public enum MissionStatus { None, Success, Failure }

public class MainLevel : SceneBase
{
    [SerializeField]
    private int _playerSeenCounter;
    [SerializeField]
    private int _maxTriesAllowed = 3;

    private Player _player;
    [SerializeField]
    private AudioClip _loseMusic;
    [SerializeField]
    private AudioClip _winMusic;
    [SerializeField]
    private AudioSource _musicAudioSource;

    #region Properties
    [SerializeField]
    private bool _playerHasBeenSeen;
    public bool PlayerHasBeenSeen { get { return _playerHasBeenSeen; } }

    [SerializeField]
    private bool _isGameOver;
    public bool IsGameOver
    {
        get { return _isGameOver; }
    }

    [SerializeField]
    private MissionStatus _missionStatus;
    public MissionStatus MissionStatus
    {
        get { return _missionStatus; }
        private set { SetProperty("MissionStatus", ref _missionStatus, value); }
    }

    [SerializeField]
    private Checkpoint _lastCheckpoint;
    public Checkpoint LastCheckpoint
    {
        get { return _lastCheckpoint; }
        private set { SetProperty("LastCheckpoint", ref _lastCheckpoint, value); }
    }
    #endregion

    public void SetCheckPoint(Checkpoint point)
    {
        LastCheckpoint = point;
    }

    public void PlayerSeen()
    {
        // if the player has hit the "max lives"
        if (_playerSeenCounter >= _maxTriesAllowed)
        {
            // only do the following if IsGameOver hasn't been set to true already
            if (!IsGameOver)
            {
                // set to true to avoid multiple executions
                _isGameOver = true;
                // assign mission status for HUD display logic
                MissionStatus = MissionStatus.Failure;
                _musicAudioSource.SwitchAndStartClip(_loseMusic);
                // delay execution until the audio has finished
                Invoke("GoToMainMenu", _loseMusic.length);
            }
        }
        else
        {
            // only do the following if the player hasn't been seen already
            if (!_playerHasBeenSeen)
            {
                // increase the counter
                _playerSeenCounter++;

                // set to true to avoid multiple executions
                _playerHasBeenSeen = true;
                _player.Seen();
            }
        }
    }

    public void PlayerDetected()
    {
        _player.Detected();
    }

    public void LoadCheckpoint()
    {
        // move the player to the checkpoint position
        _player.gameObject.transform.position = LastCheckpoint.Position;
        // set to false to allow the player to be seen again
        _playerHasBeenSeen = false;

        // there might be situations where the component wasn't removed, so only re-add it if it was removed.
        if (_player.gameObject.GetComponent<RigidbodyFirstPersonController>() == null)
        {
            _player.gameObject.AddComponent<RigidbodyFirstPersonController>();
        }
    }

    public void ObjectiveFinished()
    {
        if (!IsGameOver)
        {
            _isGameOver = true;
            MissionStatus = MissionStatus.Success;
            _musicAudioSource.SwitchAndStartClip(_winMusic);

            Invoke("GoToMainMenu", _winMusic.length);
        }
    }

    protected override void Start()
    {
        _player = GameManager.Instance.Player;
        base.Start();
    }

    protected override void CheckDependencies()
    {
        base.CheckDependencies();
        this.CheckIfDependencyIsNull(_loseMusic);
        this.CheckIfDependencyIsNull(_winMusic);
        this.CheckIfDependencyIsNull(_musicAudioSource);
    }

    private void GoToMainMenu()
    {
        GameManager.Instance.GoToMainMenu();
    }
}
