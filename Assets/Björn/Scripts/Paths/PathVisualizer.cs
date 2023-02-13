using UnityEngine;

public class PathVisualizer : MonoBehaviour
{
    [Tooltip("The color of the Gizmos.")]
    [SerializeField] private Color color = Color.white;

    [Tooltip("The radius of the Gizmos.")]
    [SerializeField] private float radius = 0.2f;

    [Tooltip("Should the first and last Gizmos be connected?")]
    [SerializeField] private bool loop = false;

    [Tooltip("Should the Gizmos always be visualized?")]
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

        for (int i = 0; i < transforms.Length; i++)
        {
            Gizmos.DrawSphere(transforms[i].position, radius);

            if (i < transforms.Length - 1)
            {
                Gizmos.DrawLine(transforms[i].position, transforms[i + 1].position);
            }

            if (loop)
            {
                if (i == transforms.Length - 1)
                {
                    Gizmos.DrawLine(transforms[i].position, transforms[0].position);
                }
            }
        }
    }
}