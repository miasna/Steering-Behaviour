using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructureParent : MonoBehaviour
{
    [SerializeField] private float m_radius;
    [SerializeField] private LayerMask m_unitMask;
    [SerializeField] private float updateTime;

    private void Start()
    {
        StartCoroutine(Check());
    }

    private IEnumerator Check()
    {
        while (true){
            Collider[] hits = Physics.OverlapSphere(transform.position, m_radius, m_unitMask);

            if (hits.Length > 0)
            {
                UpdateStats(hits);
            }

            yield return new WaitForSeconds(updateTime);
        }
    }

    public virtual void UpdateStats(Collider[] hits)
    {
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, m_radius);
    }
}
