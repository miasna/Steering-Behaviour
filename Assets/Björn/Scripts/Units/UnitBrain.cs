using System.Collections.Generic;
using UnityEngine;
using GLU.SteeringBehaviours;

[RequireComponent(typeof(Steering), typeof(UnitStats))]
public abstract class UnitBrain : MonoBehaviour
{
    [Tooltip("Path containing a list of Waypoints.")]
    [SerializeField] protected Path path;

    [Tooltip("Transform of this units eyes.")]
    [SerializeField] protected Transform eyesTransform;
    
    protected Steering steering;                        // The Steering component used by this unit.
    protected UnitStats unitStats;                      // The UnitStats used by this unit.
    [SerializeField] protected UnitStates unitState;    // The UnitState this unit is in.

    protected List<IBehavior> travelBehavior;           // Behaviors used for travelling around the level.
    protected List<IBehavior> chaseBehavior;            // Behaviors used for chasing (hostile) units.
    protected List<IBehavior> attackBehavior;           // Behaviors used for attacking hostile units.
    protected List<IBehavior> fleeBehavior;             // Behaviors used for fleeing from (hostile) units.
    protected List<IBehavior> dieBehavior;              // Behaviors used for when this unit dies.

    protected GameObject target;                        // The target this unit interacts with.

    protected virtual void Awake()
    {
        steering = GetComponent<Steering>();
        unitStats = GetComponent<UnitStats>();
    }

    protected virtual void Start()
    {
        TransitionToTravelBehavior(path);
    }

    protected virtual void TransitionToTravelBehavior(Path path)
    {
        travelBehavior = new List<IBehavior>()
        {
            new AvoidObstacle(),
            new AvoidWall(),
            new Flock(gameObject),
            new FollowPath(path.Waypoints)
        };

        steering.SetBehaviors(travelBehavior);
        unitState = UnitStates.Travel;
    }

    protected virtual void TransitionToChaseBehavior(GameObject target)
    {
        chaseBehavior = new List<IBehavior>()
        {
            new AvoidObstacle(),
            new AvoidWall(),
            new Seek(target)
        };

        steering.SetBehaviors(chaseBehavior);
        unitState = UnitStates.Chase;
    }

    protected virtual void TransitionToAttackBehavior()
    {
        attackBehavior = new List<IBehavior>()
        {
            new Idle()
        };

        steering.SetBehaviors(attackBehavior);
        unitState = UnitStates.Attack;
    }

    protected virtual void TransitionToFleeBehavior(GameObject target)
    {
        fleeBehavior = new List<IBehavior>()
        {
            new AvoidObstacle(),
            new AvoidWall(),
            new Flee(target)
        };

        steering.SetBehaviors(fleeBehavior);
        unitState = UnitStates.Flee;
    }

    protected virtual void TransitionToDieBehavior()
    {
        dieBehavior = new List<IBehavior>()
        {
            new Idle()
        };

        steering.SetBehaviors(dieBehavior);
        unitState = UnitStates.Die;
    }

    protected GameObject[] GetGameObjectsInSight(string gameObjectTag)
    {
        Collider[] colliders = Physics.OverlapSphere(eyesTransform.position, unitStats.SightRange);
        List<GameObject> GameObjectsInSight = new List<GameObject>();

        if (colliders.Length == 0)
        {
            return null;
        }

        foreach (Collider collider in colliders)
        {
            if (!collider.CompareTag(gameObjectTag))
            {
                continue;
            }

            if (IsGameObjectInSight(collider.gameObject))
            {
                GameObjectsInSight.Add(collider.gameObject);
            }
        }

        return GameObjectsInSight.ToArray();
    }

    protected GameObject GetClosestGameObjectInSight(string gameObjectTag)
    {
        Collider[] colliders = Physics.OverlapSphere(eyesTransform.position, unitStats.SightRange);

        if (colliders.Length == 0)
        {
            return null;
        }

        float furthestDistanceToCollider = Mathf.Infinity;
        GameObject closestGameObjectInSight = null;

        foreach (Collider collider in colliders)
        {
            if (!collider.CompareTag(gameObjectTag))
            {
                continue;
            }

            if (IsGameObjectInSight(collider.gameObject))
            {
                float currentDistanceToCollider = Vector3.Distance(eyesTransform.position, collider.transform.position);

                if (currentDistanceToCollider < furthestDistanceToCollider)
                {
                    furthestDistanceToCollider = currentDistanceToCollider;
                    closestGameObjectInSight = collider.gameObject;
                }  
            }
        }

        return closestGameObjectInSight;
    }

    protected bool IsGameObjectInSight(GameObject gameObject)
    {
        Vector3 direction = (gameObject.transform.position - eyesTransform.position).normalized;
        Ray ray = new Ray(eyesTransform.position, direction);
        RaycastHit hit = new RaycastHit();

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.gameObject == gameObject)
            {
                return true;
            }
        }

        return false;
    }
}