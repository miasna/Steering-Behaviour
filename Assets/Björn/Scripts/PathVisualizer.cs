using UnityEngine;

public class PathVisualizer : MonoBehaviour
{
    [SerializeField] private Color color = Color.white;
    [SerializeField] private bool wireframeMode = false;
    [SerializeField] private float radius = 0.2f;
    [SerializeField] private bool alwaysVisualize = false;

    private void OnDrawGizmos()
    {
        if (!alwaysVisualize) 
        { 
            return; 
        }

        VisualizePath();
    }

    private void OnDrawGizmosSelected()
    {
        if (alwaysVisualize) 
        { 
            return; 
        }

        VisualizePath();
    }

    private void VisualizePath()
    {
        Gizmos.color = color;
        Transform[] transforms = GetComponentsInChildren<Transform>();

        foreach (Transform transform in transforms)
        {
            if (wireframeMode)
            {
                Gizmos.DrawWireSphere(transform.position, radius);
            }
            else
            {
                Gizmos.DrawSphere(transform.position, radius);
            }
        }
    }
}