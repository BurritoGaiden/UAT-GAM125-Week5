using System;
using L4.Unity.Common;
using L4.Unity.Common.Editor;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenuController : BaseScript
{
    [Serializable]
    public class SettingsComponents : InspectorContainer<SettingsComponents>
    {
        #region Audio
        [Serializable]
        public class SliderComponents : InspectorContainer<SliderComponents>
        {
            public Slider MasterVolumeSlider;
            public Slider SFXVolumeSlider;
            public Slider MusicVolumeSlider;

            public override void CheckDependencies()
            {
                this.CheckIfDependencyIsNull(MasterVolumeSlider);
                this.CheckIfDependencyIsNull(SFXVolumeSlider);
                this.CheckIfDependencyIsNull(MusicVolumeSlider);
            }
        }
        [Serializable]
        public class InputFieldComponents : InspectorContainer<InputFieldComponents>
        {
            public InputField MasterVolumeInput;
            public InputField SFXVolumeInput;
            public InputField MusicVolumeInput;

            public override void CheckDependencies()
            {
                this.CheckIfDependencyIsNull(MasterVolumeInput);
                this.CheckIfDependencyIsNull(SFXVolumeInput);
                this.CheckIfDependencyIsNull(MusicVolumeInput);
            }
        }
        #endregion
        
        public SliderComponents Sliders;
        public InputFieldComponents InputFields;

        public override void CheckDependencies()
        {
            // inner InspectorContainers should have their CheckDependencies called 
            Sliders.CheckDependencies();
            InputFields.CheckDependencies();
        }
    }

    [SerializeField]
    private Text TabHeaderText;
    [SerializeField]
    private SettingsComponents SettingsEditingComponents;
    
    public void OnApplySettingsButtonClicked()
    {
        // commit all values to the GameManger
        // this probably could be a lot more selective to only commit changed values
        GameManager.Instance.Settings.MasterVolume = SettingsEditingComponents.Sliders.MasterVolumeSlider.value;
        GameManager.Instance.Settings.SFXVolume = SettingsEditingComponents.Sliders.SFXVolumeSlider.value;
        GameManager.Instance.Settings.MusicVolume = SettingsEditingComponents.Sliders.MusicVolumeSlider.value;
    }

    public void OnRevertChangesButtonClicked()
    {
        // resets to the last "apply" state.
        InitDefaults();
    }

    protected override void Start()
    {
        base.Start();
        // pulls initialized values from the GameManager.Settings
        InitDefaults();
        // adds listeners to the settings UI objects
        RegisterUIEvents();
    }

    protected override void CheckDependencies()
    {
        this.CheckIfDependencyIsNull(TabHeaderText);
        SettingsEditingComponents.CheckDependencies();
    }

    private void InitDefaults()
    {
        SettingsEditingComponents.Sliders.MasterVolumeSlider.value = GameManager.Instance.Settings.MasterVolume;
        SettingsEditingComponents.Sliders.SFXVolumeSlider.value = GameManager.Instance.Settings.SFXVolume;
        SettingsEditingComponents.Sliders.MusicVolumeSlider.value = GameManager.Instance.Settings.MusicVolume;
        // turns the float value between 0 and 1 from the sliders into a "readable" format of 0-100 for percentages
        SettingsEditingComponents.InputFields.MasterVolumeInput.text = (GameManager.Instance.Settings.MasterVolume * 100).ToString();
        SettingsEditingComponents.InputFields.SFXVolumeInput.text = (GameManager.Instance.Settings.SFXVolume * 100).ToString();
        SettingsEditingComponents.InputFields.MusicVolumeInput.text = (GameManager.Instance.Settings.MusicVolume * 100).ToString();
    }

    private void RegisterUIEvents()
    {
        // takes the new string, divides it by 100 (as values are displayed in text as percentages)
        // and converts to a double before casting to a float to proprogate the value to the slider's value
        #region InputField.onEndEdit handlers
        SettingsEditingComponents.InputFields.MasterVolumeInput.onEndEdit.AddListener((newString) =>
        {
            // if the new string will be empty, revert changes
            if (newString == string.Empty)
            {
                SettingsEditingComponents.InputFields.MasterVolumeInput.text = (SettingsEditingComponents.Sliders.MasterVolumeSlider.value * 100).ToString();
            }
            else
            {
                // convert the new string into a number
                var newValue = (float) Convert.ToDouble(newString);

                // if the number is greater than 100 (the max)
                if (newValue > 100)
                {
                    // set the newValue to 100
                    newValue = 100;
                    // set the text property to the new value (to update the text input)
                    SettingsEditingComponents.InputFields.MasterVolumeInput.text = newValue.ToString();
                    // return, as that change will cause this function to be re-called and we don't want to set the slider
                    // twice.
                    return;
                }

                // set the slider (whose values are betweeen 0-1) to the value divided by 100 (79 will be .79).
                SettingsEditingComponents.Sliders.MasterVolumeSlider.value = newValue / 100f;
            }
        });
        SettingsEditingComponents.InputFields.MusicVolumeInput.onEndEdit.AddListener((newString) =>
        {
            // if the new string will be empty, revert changes
            if (newString == string.Empty)
            {
                SettingsEditingComponents.InputFields.MusicVolumeInput.text = (SettingsEditingComponents.Sliders.MusicVolumeSlider.value * 100).ToString();
            }
            else
            {
                // convert the new string into a number
                var newValue = (float) Convert.ToDouble(newString);

                // if the number is greater than 100 (the max)
                if (newValue > 100)
                {
                    // set the newValue to 100
                    newValue = 100;
                    // set the text property to the new value (to update the text input)
                    SettingsEditingComponents.InputFields.MusicVolumeInput.text = newValue.ToString();
                    // return, as that change will cause this function to be re-called and we don't want to set the slider
                    // twice.
                    return;
                }

                // set the slider (whose values are betweeen 0-1) to the value divided by 100 (79 will be .79).
                SettingsEditingComponents.Sliders.MusicVolumeSlider.value = (float) Convert.ToDouble(newString) / 100f;
            }
        });
        SettingsEditingComponents.InputFields.SFXVolumeInput.onEndEdit.AddListener((newString) =>
        {
            // if the new string will be empty, revert changes
            if (newString == string.Empty)
            {
                SettingsEditingComponents.InputFields.SFXVolumeInput.text = (SettingsEditingComponents.Sliders.SFXVolumeSlider.value * 100).ToString();
            }
            else
            {
                // convert the new string into a number
                var newValue = (float) Convert.ToDouble(newString);

                // if the number is greater than 100 (the max)
                if (newValue > 100)
                {
                    // set the newValue to 100
                    newValue = 100;
                    // set the text property to the new value (to update the text input)
                    SettingsEditingComponents.InputFields.SFXVolumeInput.text = newValue.ToString();
                    // return, as that change will cause this function to be re-called and we don't want to set the slider
                    // twice.
                    return;
                }

                // set the slider (whose values are betweeen 0-1) to the value divided by 100 (79 will be .79).
                SettingsEditingComponents.Sliders.SFXVolumeSlider.value = (float) Convert.ToDouble(newString) / 100f;
            }
        });
        #endregion

        // takes the new value, multiplies it by 100 (as values are displayed in text as percentages)
        // and takes the string to assign to the inputfield's text variable.
        #region Slider.onValueChanged handlers
        SettingsEditingComponents.Sliders.MasterVolumeSlider.onValueChanged.AddListener((newValue) =>
        {
            SettingsEditingComponents.InputFields.MasterVolumeInput.text = (newValue * 100).ToString();
        });
        SettingsEditingComponents.Sliders.MusicVolumeSlider.onValueChanged.AddListener((newValue) =>
        {
            SettingsEditingComponents.InputFields.MusicVolumeInput.text = (newValue * 100).ToString();
        });
        SettingsEditingComponents.Sliders.SFXVolumeSlider.onValueChanged.AddListener((newValue) =>
        {
            SettingsEditingComponents.InputFields.SFXVolumeInput.text = (newValue * 100).ToString();
        });
        #endregion
    }
}
