using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    /// <summary>
    /// Get all children of this GameObject that have a Waypoint component.
    /// </summary>
    public List<GameObject> Waypoints
    {
        get
        {
            return GetWaypoints();
        }
    }

    private List<GameObject> GetWaypoints()
    {
        List<GameObject> gameObjects = new List<GameObject>();
        Waypoint[] waypoints = GetComponentsInChildren<Waypoint>();

        foreach (Waypoint waypoint in waypoints)
        {
            gameObjects.Add(waypoint.gameObject);
        }

        return gameObjects;
    }
}