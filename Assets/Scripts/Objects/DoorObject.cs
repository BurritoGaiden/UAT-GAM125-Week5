using L4.Unity.Common;
using UnityEngine;

public class DoorObject : BaseScript
{
    [SerializeField]
    private bool _isUnlocked;
    
    public void UnlockDoor()
    {
        _isUnlocked = true;
    }

    protected override void Update()
    {
        base.Update();

        if (_isUnlocked)
        {
            Destroy(gameObject);
        }
    }
}
