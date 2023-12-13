using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ItemEffects
{
    public static void Heal(Item item, Character Target)
    {
        Target.currentHealth += item.effect0;
        if (Target.currentHealth > Target.maxHealth) Target.currentHealth = Target.maxHealth;
    }
}
