using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PullOtherCharactersInfo 
{

    public static List<GameObject> GetAllEnemies(CombatCharacter character)
    {
        List<GameObject> enemies = new List<GameObject>();
        for (int i = 0; i < CombatSingleton.Instance.Combatants.Count; i++)
        {
            if (CombatSingleton.Instance.Combatants[i].GetComponent<CombatCharacter>().team != character.team)
            {
                enemies.Add(CombatSingleton.Instance.Combatants[i]);
            }
        }
        return enemies;
    }
    public static List<GameObject> GetAllAllies(CombatCharacter character)
    {
        List<GameObject> allies = new List<GameObject>();
        for (int i = 0; i < CombatSingleton.Instance.Combatants.Count; i++)
        {
            if (CombatSingleton.Instance.Combatants[i].GetComponent<CombatCharacter>().team == character.team &&
                CombatSingleton.Instance.Combatants[i] != character.gameObject)
            {
                allies.Add(CombatSingleton.Instance.Combatants[i]);
            }
        }
        return allies;
    }
}
