using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleTurnController
{
    

    public BattleTurnController()
    {
    }


    public void CheckIfTurnOver()
    {
        bool turnOver = true;
        if (CombatSingleton.Instance.battleSystem.CurrentTeam == 0)
        {
            foreach (GameObject character in CombatSingleton.Instance.Combatants)
            {
                if (character.GetComponent<CombatCharacter>().team == CombatSingleton.Instance.battleSystem.CurrentTeam)
                {
                    if (character.GetComponent<CombatCharacter>().myStats.actionsRemaining > 0)
                    {
                        turnOver = false;
                    }
                }
            }

            if (turnOver)
            {
                NextTurn();
            }
        }
    }

    private void NextTurn()
    {
        CombatSingleton.Instance.battleSystem.TurnChange();

    }

    
}
