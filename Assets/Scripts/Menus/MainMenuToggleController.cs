using L4.Unity.Common;
using UnityEngine;

public class MainMenuToggleController : BaseScript
{
    [SerializeField]
    private Canvas _mainMenuCanvas;
    [SerializeField]
    private Canvas _settingsMenuCanvas;

    /// <summary>
    /// Hides the main menu canvas and shows the settings canvas.
    /// </summary>
    public void OnSettingsButtonClicked()
    {
        // main buttons hidden, settings UI elements visible
        _mainMenuCanvas.gameObject.SetActive(false);
        _settingsMenuCanvas.gameObject.SetActive(true);
    }

    /// <summary>
    /// Hides the settings canvas and shows the main menu canvas.
    /// </summary>
    public void OnBackButtonClicked()
    {
        // settings UI elements hidden, main buttons visible
        _settingsMenuCanvas.gameObject.SetActive(false);
        _mainMenuCanvas.gameObject.SetActive(true);
    }

    protected override void Start()
    {
        // reset the cursor mode on level load
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    protected override void CheckDependencies()
    {
        this.CheckIfDependencyIsNull(_mainMenuCanvas);
        this.CheckIfDependencyIsNull(_settingsMenuCanvas);
    }
}
