using System.Collections.Generic;
using UnityEngine;
using GLU.SteeringBehaviours;

[RequireComponent(typeof(Steering), typeof(UnitStats))]
public abstract class UnitBrain : MonoBehaviour
{
    [Tooltip("The path for the unit to follow.")]
    [SerializeField] protected Path path;

    protected Steering steering;
    protected List<IBehavior> travelBehavior;
    protected UnitStats unitStats;

    protected UnitStates unitStates;

    protected virtual void Awake()
    {
        steering = GetComponent<Steering>();
        InitializeTravelBehavior();
        unitStats = GetComponent<UnitStats>();
    }

    protected virtual void Start()
    {
        steering.SetBehaviors(travelBehavior);
    }

    protected void Update()
    {

    }

    protected virtual void InitializeTravelBehavior()
    {
        travelBehavior = new List<IBehavior>();
        travelBehavior.Add(new AvoidObstacle());
        travelBehavior.Add(new AvoidWall());
        travelBehavior.Add(new Flock(gameObject));
        travelBehavior.Add(new FollowPath(path.Waypoints));
    }
}