using L4.Unity.Common;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class MovementFreezer : MonoBehaviour
{
    private MainLevel _level;

    private void Start()
    {
        _level = GameManager.Instance.CurrentScene.As<MainLevel>();
    }

    private void Update()
    {
        if (_level.IsGameOver || _level.PlayerHasBeenSeen)
        {
            // destroy the component to lock player movement.
            // it is readded by the MainLevel logic later
            var obj = FindObjectOfType<RigidbodyFirstPersonController>();
            Destroy(obj);
        }
    }
}
