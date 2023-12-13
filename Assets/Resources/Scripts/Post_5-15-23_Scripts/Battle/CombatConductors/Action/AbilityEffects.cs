using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AbilityEffects 
{
    
    public static void Heal(Abilities ability, Character Target, Character Origin)
    {
        Target.currentHealth += Random.Range(ability.maxEffect, ability.maxEffect) + (Origin.intelligence/2);
        if (Target.currentHealth > Target.maxHealth) Target.currentHealth = Target.maxHealth;
    }
}
