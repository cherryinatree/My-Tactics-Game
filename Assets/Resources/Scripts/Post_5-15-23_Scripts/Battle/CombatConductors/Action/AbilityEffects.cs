using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AbilityEffects 
{
    
    public static void Heal(Abilities ability, Character Target, Character Origin)
    {
        Debug.Log("target name: " + Target.characterName);
        Debug.Log("Origin name: " + Origin.characterName);

        int amount = Random.Range(ability.maxEffect, ability.maxEffect) + (Origin.intelligence / 2);
        Debug.Log("Heal amount: " +amount);

        Target.currentHealth += amount;
        if (Target.currentHealth > Target.maxHealth) Target.currentHealth = Target.maxHealth;
    }
}
