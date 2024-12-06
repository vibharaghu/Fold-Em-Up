using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public string unitName;
    public int unitLevel;
    public int damage;
    public int maxHP;
    public int currentHP;
    public int shields;
    public bool shielded;

    public bool TakeDamage(int dmg)
    {
        if (!shielded)
            currentHP -= dmg;
        else
            shielded = false;

        if (currentHP <= 0)
            return true;
        else
            return false;
    }

    public void UseShield()
    {
        if (shields >= 1)
        {
            shields--;
            shielded = true;
        }
    }
}
