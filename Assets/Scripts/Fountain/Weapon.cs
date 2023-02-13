using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : StructureParent
{
    [SerializeField] private int damageAmount;


    public override void UpdateStats(Collider[] hits)
    {
        for (int i = 0; i < hits.Length; i ++)
        {
            hits[i].gameObject.GetComponent<UnitStats>().ChangeDamage(damageAmount);
        }
    }
}
