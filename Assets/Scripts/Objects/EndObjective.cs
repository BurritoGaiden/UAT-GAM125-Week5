using L4.Unity.Common;
using UnityEngine;

public class EndObjective : MonoBehaviour
{
    private void OnTriggerEnter(Collider otherObj)
    {
        if (otherObj.tag == ProjectTags.PLAYER)
        {
            var player = GameManager.Instance.Player;

            if (player.HasObjective)
            {
                GameManager.Instance.CurrentScene
                    .As<MainLevel>()
                    .ObjectiveFinished();
            }
        }
    }
}
