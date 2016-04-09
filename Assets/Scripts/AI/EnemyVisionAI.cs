using L4.Unity.Common;
using UnityEngine;

public class EnemyVisionAI : BaseScript
{
    [SerializeField]
    private float _maxVisionDistance = 100f;
    [SerializeField]
    private float _seenFieldOfView = 45.0f;
    [SerializeField]
    private float _warningFieldOfView = 60.0f;

    [SerializeField]
    private Light _spotlightVisual;

    protected override void Start()
    {
        base.Start();

        // increase the spotlight visual by 1.5, because of camera distortion
        // this gives a bit more of a reliable visual to players if they're "inside" or not.
        _spotlightVisual.spotAngle = _seenFieldOfView * 1.5f;
    }

    protected override void Update()
    {
        if (CanSeePlayer())
        {
            GameManager.Instance.CurrentScene.As<MainLevel>().PlayerSeen();
        }
        else if (CanDetectPlayer())
        {
            GameManager.Instance.CurrentScene.As<MainLevel>().PlayerDetected();
        }
    }

    protected override void CheckDependencies()
    {
        base.CheckDependencies();

        this.CheckIfDependencyIsNull(_spotlightVisual);
    }

    private bool CanSeePlayer()
    {
        return IsPlayerWithinView(_seenFieldOfView);
    }

    private bool CanDetectPlayer()
    {
        return IsPlayerWithinView(_warningFieldOfView);
    }

    private bool IsPlayerWithinView(float fieldOfView)
    {
        // following Lecture 5.2

        // get the target's position
        Vector3 targetPosition = GameManager.Instance.Player.transform.position;
        // get the agent's position in relation to the target
        Vector3 agentToTargetVector = targetPosition - transform.position;

        // find the angle between the agent's relative to target position, and the "forward" of the agent.
        float angleToTarget = Vector3.Angle(agentToTargetVector, transform.forward);

        if (angleToTarget <= fieldOfView)
        {
            // create a raycast
            Ray rayToTarget = new Ray();

            rayToTarget.origin = transform.position;
            rayToTarget.direction = agentToTargetVector;

            RaycastHit hitInfo;

            if (Physics.Raycast(rayToTarget, out hitInfo, _maxVisionDistance))
            {
                if (hitInfo.collider.tag == ProjectTags.PLAYER)
                {
                    return true;
                }
            }
        }

        return false;
    }
}
