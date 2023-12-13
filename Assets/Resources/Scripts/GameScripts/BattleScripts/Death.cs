using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Death
{
    
    public static void RemoveGameObject(GameObject character)
    {
        int x = 0;
        int actualX = 0;
        foreach (List<GameObject> items in BattleSingleton.Instance.Combatants)
        {
            foreach (var item in items)
            {
                if (item == character)
                {
                    actualX = x;
                }
            }
            x++;
        }
        BattleSingleton.Instance.Combatants[actualX].Remove(character);
        RemainingTeams();
    }

    private static void RemainingTeams()
    {
        List<string> tags = new List<string>();
        foreach (List<GameObject> items in BattleSingleton.Instance.Combatants)
        {
            foreach (GameObject item in items)
            {
                bool alreadyAdded = false;
                foreach  (string tag in tags)
                {
                    if(tag== item.tag)
                    {
                        alreadyAdded = true;
                    }
                }
                if (alreadyAdded == false)
                {
                    tags.Add(item.tag);
                }
            }
        }
        BattleSingleton.Instance.NumberOfTeams = tags.Count;
        Debug.Log(tags.Count);
    }
}
