using System;
using L4.Unity.Common;
using UnityEngine;

public class OnOffToggleTimer : BaseScript
{
    [SerializeField]
    private float _secondsBetweenToggles;

    private DateTime _lastToggleTime;

    [SerializeField]
    private Light _lightComponent;
    [SerializeField]
    private EnemyVisionAI _visionAI;

    protected override void Update()
    {
        bool shouldToggle = (DateTime.Now - _lastToggleTime) >= TimeSpan.FromSeconds(_secondsBetweenToggles);

        if (shouldToggle)
        {
            // toggle the enabled states by inverting their current state
            _lightComponent.enabled = !_lightComponent.enabled;
            _visionAI.enabled = !_visionAI.enabled;
            _lastToggleTime = DateTime.Now;
        }
    }

    protected override void CheckDependencies()
    {
        base.CheckDependencies();

        this.CheckIfDependencyIsNull(_lightComponent);
        this.CheckIfDependencyIsNull(_visionAI);
    }
}
