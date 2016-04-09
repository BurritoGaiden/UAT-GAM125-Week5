using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    public void OnPlayButtonClicked()
    {
        // go to the first level
        GameManager.Instance.GoToMainLevel();
    }

    public void OnQuitButtonClicked()
    {
        // quit the game
        Application.Quit();
    }
}
