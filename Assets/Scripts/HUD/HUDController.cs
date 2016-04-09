using L4.Unity.Common;
using L4.Unity.Common.Extensions;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : BaseScript
{
    private MainLevel _level;

    [SerializeField]
    private Text _gameOverText;
    [SerializeField]
    private Text _gameWinText;
    [SerializeField]
    private Text _seenText;

    protected override void Start()
    {
        base.Start();

        // store the local reference
        _level = GameManager.Instance.CurrentScene.As<MainLevel>();
    }

    protected override void Update()
    {
        base.Update();

        if (_level.IsGameOver)
        {
            // set the seen text visibility to false so there's no overlapping
            _seenText.gameObject.SetActiveState(false);

            // depending on the mission status, set the appropriate text component to active
            if (_level.MissionStatus == MissionStatus.Failure)
            {
                _gameOverText.gameObject.SetActiveState(true);
            }
            else if (_level.MissionStatus == MissionStatus.Success)
            {
                _gameWinText.gameObject.SetActiveState(true);
            }
        }
        else
        {
            // set the seen text active state to if the player has been seen
            _seenText.gameObject.SetActiveState(_level.PlayerHasBeenSeen);
        }
    }

    protected override void CheckDependencies()
    {
        base.CheckDependencies();

        this.CheckIfDependencyIsNull(_gameWinText);
        this.CheckIfDependencyIsNull(_gameOverText);
        this.CheckIfDependencyIsNull(_seenText);
    }
}
