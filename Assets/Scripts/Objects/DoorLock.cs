using L4.Unity.Common;
using UnityEngine;

public class DoorLock : BaseScript
{
    [SerializeField]
    private DoorObject _doorObject;

    protected override void CheckDependencies()
    {
        base.CheckDependencies();

        this.CheckIfDependencyIsNull(_doorObject);
    }

    private void OnTriggerEnter(Collider otherObj)
    {
        if (otherObj.tag == ProjectTags.PLAYER)
        {
            if (GameManager.Instance.Player.HasKey)
            {
                _doorObject.UnlockDoor();
                GameManager.Instance.Player.ConsumeKey();
            }
        }
    }
}
