using L4.Unity.Common;
using UnityEngine;
using UnityEngine.UI;

public class AudioButtonChanger : BaseScript
{
    [SerializeField]
    [Range(0, 1)]
    private float _incrementBy;
    [SerializeField]
    private Slider _sliderToChange;

    public void IncreaseAudioSetting()
    {
        float val = (_sliderToChange.value + _incrementBy);

        // clamp the value to the maximum - 1
        if (val >= 1)
        {
            val = 1;
        }
        _sliderToChange.value = val;
    }

    public void DecreaseAudioSetting()
    {
        float val = (_sliderToChange.value - _incrementBy);

        // clamp the value to the minimum - 0
        if (val <= 0)
        {
            val = 0;
        }
        _sliderToChange.value = val;
    }

    protected override void CheckDependencies()
    {
        this.CheckIfDependencyIsNull(_sliderToChange);
    }
}
