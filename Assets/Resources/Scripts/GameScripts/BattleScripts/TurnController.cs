using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TurnController 
{
    public static void NextTurn()
    {
        if (BattleSingleton.Instance.currentTurn >= BattleSingleton.Instance.Combatants.Count - 1)
        {
            BattleSingleton.Instance.currentTurn = 0;
        }
        else
        {
            BattleSingleton.Instance.currentTurn++;
        }
        foreach (GameObject item in BattleSingleton.Instance.ourTurn)
        {
            item.GetComponent<CharacterController>().myStats.actionsLeft = item.GetComponent<CharacterController>().myStats.actions;
        }
        BattleSingleton.Instance.ourTurn = BattleSingleton.Instance.Combatants[BattleSingleton.Instance.currentTurn];
        
        DisplayBanner();
    }

    private static void DisplayBanner()
    {
        if (BattleSingleton.Instance.Combatants.Count == 2)
        {
            if (BattleSingleton.Instance.currentTurn == 0)
            {
                UserInterface.Banner("Player's Turn", true);
            }
            else
            {
                UserInterface.Banner("Enemy's Turn", true);
            }
        }else if (BattleSingleton.Instance.Combatants.Count > 2)
        {

            UserInterface.Banner("Team " + (BattleSingleton.Instance.currentTurn+1).ToString() + "'s Turn", true);
        }
    }
}
