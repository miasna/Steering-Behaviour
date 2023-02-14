using UnityEngine;
using GLU.SteeringBehaviours;

public class AggressiveUnit : UnitBrain
{
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
    }

    private void Update()
    {
        if (GetClosestGameObjectInSight(Extension.HostileUnitTag) != null)
        {
            target = GetClosestGameObjectInSight(Extension.HostileUnitTag);
        }

        switch (unitState)
        {
            case UnitStates.Travel:
                if (target != null)
                    TransitionToChaseBehavior(target);           
                break;

            case UnitStates.Chase:
                if (Vector3.Distance(transform.position, target.transform.position) <= steering.m_settings.m_arriveDistance)
                    TransitionToAttackBehavior();
                break;

            case UnitStates.Attack:
                if (Vector3.Distance(transform.position, target.transform.position) > steering.m_settings.m_arriveDistance)
                    TransitionToChaseBehavior(target);
                break;

            case UnitStates.Die:
                
                break;
        }
    }
}