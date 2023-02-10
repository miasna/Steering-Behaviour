using System.Collections.Generic;
using UnityEngine;
using GLU.SteeringBehaviours;

[RequireComponent(typeof(Steering))]
public class UnitBrain : MonoBehaviour
{
    [SerializeField] private Path path;

    private Steering steering;
    private List<IBehavior> travelBehavior;

    private void Awake()
    {
        steering = GetComponent<Steering>();
        InitializeTravelBehavior();
    }

    private void Start()
    {
        steering.SetBehaviors(travelBehavior);
    }

    private void InitializeTravelBehavior()
    {
        travelBehavior = new List<IBehavior>();
        travelBehavior.Add(new AvoidObstacle());
        travelBehavior.Add(new AvoidWall());
        travelBehavior.Add(new Flock(gameObject));
        travelBehavior.Add(new FollowPath(path.Waypoints));
    }
}