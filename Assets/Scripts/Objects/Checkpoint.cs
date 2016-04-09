using L4.Unity.Common;
using UnityEngine;

public class Checkpoint : BaseScript
{
    // I planned to do more with this, but I didn't give myself enough time to flesh it out

    public Vector3 Position{ get { return gameObject.transform.position; } }

    private void OnTriggerEnter(Collider otherObj)
    {
        if (otherObj.tag == ProjectTags.PLAYER)
        {
            GameManager.Instance.CurrentScene
                .As<MainLevel>()
                .SetCheckPoint(this);
        }
    }
}
