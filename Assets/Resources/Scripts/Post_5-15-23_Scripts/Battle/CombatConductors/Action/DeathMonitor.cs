using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DeathMonitor
{
    public static bool DeathCheck(GameObject target)
    {
        if(target.GetComponent<CombatCharacter>().myStats.currentHealth <= 0)
        {
            target.GetComponent<CharacterDeath>().Death(3);
            return true;
        }
        return false;
    }
}
