using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RewardsCalculator
{
    public static int AttackXP(GameObject Target)
    {
        Character targetStats = Target.GetComponent<CombatCharacter>().myStats;
        int xp = targetStats.level * 20;
        CombatSingleton.Instance.actionData.OriginCharacter.GetComponent<CombatCharacter>().myStats.xp += xp;
        return xp;
    }
    public static int FriendlyAbilityXP(GameObject Target)
    {
        Character targetStats = Target.GetComponent<CombatCharacter>().myStats;
        int xp = targetStats.level * 40;
        CombatSingleton.Instance.actionData.OriginCharacter.GetComponent<CombatCharacter>().myStats.xp += xp;
        return xp;
    }
    public static int SlayXP(GameObject Target)
    {
        Character targetStats = Target.GetComponent<CombatCharacter>().myStats;
        int xp = targetStats.level * 100;
        CombatSingleton.Instance.actionData.OriginCharacter.GetComponent<CombatCharacter>().myStats.xp += xp;
        return xp;
    }
    public static int ItemXP(GameObject Target)
    {
        Character targetStats = Target.GetComponent<CombatCharacter>().myStats;
        int xp = targetStats.level * 10;
        CombatSingleton.Instance.actionData.OriginCharacter.GetComponent<CombatCharacter>().myStats.xp += xp;
        return xp;
    }

    public static int CaptureXP(GameObject Target)
    {

        Character targetStats = Target.GetComponent<CombatCharacter>().myStats;
        int xp = targetStats.level * 50;
        CombatSingleton.Instance.actionData.OriginCharacter.GetComponent<CombatCharacter>().myStats.xp += xp;

        return xp;  
    }

    public static bool CheckIfCharacterLeveledUp(Character character)
    {
        int XpRequiredToLevel = (character.level * character.level * 100);

        if (character.xp >= XpRequiredToLevel)
        {
            int health = 1;
            int mana = 1;
            int attack = 1;
            int defense = 1;
            int intelligence = 1;

            if (character.maxHealth * 1.2f >= 2)
            {
                health = (int)(character.maxHealth * 1.2f);
            }
            if (character.maxHealth * 1.2f >= 2)
            {
                mana = (int)(character.maxMana * 1.2f);
            }
            if (character.maxHealth * 1.2f >= 2)
            {
                attack = (int)(character.attack * 1.2f);
            }
            if (character.maxHealth * 1.2f >= 2)
            {
                defense = (int)(character.defense * 1.2f);
            }
            if (character.maxHealth * 1.2f >= 2)
            {
                intelligence = (int)(character.intelligence * 1.2f);
            }
            
            character.xp -= XpRequiredToLevel;

            character.level += 1;
            character.maxHealth = health;
            character.maxMana = mana;
            character.attack = attack;
            character.defense = defense;
            character.intelligence = intelligence;

            return true;
        }

        return false;
    }
}
