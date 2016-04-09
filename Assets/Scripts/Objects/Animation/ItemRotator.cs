using L4.Unity.Common;
using UnityEngine;

public class ItemRotator : BaseScript
{
    [SerializeField]
    private bool _shouldRotate = true;

    [SerializeField]
    [Tooltip("The angle (multiplied by Time.deltaTime) for each axis that the object should rotate every Update.")]
    private Vector3 _rotationAngles;

    protected override void Update()
    {
        base.Update();

        if (_shouldRotate)
        {
            transform.Rotate(_rotationAngles * Time.deltaTime);
        }
    }
}
