using UnityEngine;

public class UnitStats : MonoBehaviour
{
    public int MoveSpeed;
    public int AttackSpeed;
    public int AttackDamage;
    public int SightRange;
    public int Defense;
    public UnitBehavior Behavior;

    [SerializeField] private int maxDefense;

    bool changedAttack;
    


    public void ChangeHealth(int amount)
    {
        Defense += amount;
        if(Defense > maxDefense)
        {
            Defense = maxDefense;
        }
    }

    public void ChangeDamage(int amount)
    {
        if (changedAttack == false)
        {
            AttackDamage += amount;
            AttackSpeed += amount;

            changedAttack = true;
        }
    }
}
