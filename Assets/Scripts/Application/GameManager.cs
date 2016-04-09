using L4.Unity.Common;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : AppManagerBase<GameManager>
{
    [SerializeField]
    private Player _player;
    public Player Player
    {
        get
        {
            if (_player == null)
            {
                _player = SingletonHelper.FindSingleInstance<Player>();
            }
            return _player;
        }
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(BuildIndexes.MAIN_MENU);
    }

    public void GoToMainLevel()
    {
        SceneManager.LoadScene(BuildIndexes.MAIN_LEVEL);
    }
}
