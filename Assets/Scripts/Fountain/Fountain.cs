using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fountain : StructureParent
{
    [SerializeField] private int healAmount;


    public override void UpdateStats(Collider[] hits)
    {
        for (int i = 0; i < hits.Length; i ++)
        {
            hits[i].gameObject.GetComponent<UnitStats>().ChangeHealth(healAmount);
        }
    }
}
