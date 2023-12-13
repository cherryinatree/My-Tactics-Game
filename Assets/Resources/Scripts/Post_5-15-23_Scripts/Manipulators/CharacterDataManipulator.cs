using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CharacterDataManipulator
{
    
    public static void CharacterAbilityPurchace(Character stats, Abilities ability)
    {
        for (int i = 0; i < stats.Abilities.Length; i++)
        {
            if (stats.Abilities[i] == ability.id)
            {
                stats.AquiredAbilites[i] = true;
            }
        }
    }
}
