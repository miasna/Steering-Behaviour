using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GLU.SteeringBehaviours;

public class WandererBrain : UnitBrain
{
    [SerializeField] private GameObject go;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void InitializeTravelBehavior()
    {
        travelBehavior = new List<IBehavior>();
        travelBehavior.Add(new Pursue(go));
    }
}
