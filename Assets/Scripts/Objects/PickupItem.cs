using UnityEngine;

public enum ItemType { Key, Objective }

public class PickupItem : MonoBehaviour
{
    [SerializeField]
    private ItemType _itemType;

    private void OnTriggerEnter(Collider otherObj)
    {
        if (otherObj.tag == ProjectTags.PLAYER)
        {
            otherObj.GetComponentInChildren<Player>().AddItem(_itemType);

            Destroy(gameObject);
        }
    }
}
