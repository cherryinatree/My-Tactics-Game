using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CharacterManipulator
{
    public static BattleTurnController turnController;

    private static void isTurnControllerNull()
    {
        if(turnController == null)
        {
            turnController = new BattleTurnController();
        }
    }

    public static void RemoveActionPoints(GameObject character, int howMany)
    {
        isTurnControllerNull();
        character.GetComponent<CombatCharacter>().myStats.actionsRemaining -= howMany;
        turnController.CheckIfTurnOver();
    }
    public static void RemoveActionPoints(CombatCharacter character, int howMany)
    {
        isTurnControllerNull();
        character.myStats.actionsRemaining -= howMany;
        turnController.CheckIfTurnOver();
    }
    public static void RemoveActionPoints(Character character, int howMany)
    {
        isTurnControllerNull();
        character.actionsRemaining -= howMany;
        turnController.CheckIfTurnOver();
    }

    public static void RemoveAllActionPoints(GameObject character)
    {
        isTurnControllerNull();
        character.GetComponent<CombatCharacter>().myStats.actionsRemaining = 0;
        turnController.CheckIfTurnOver();
    }
    public static void RemoveAllActionPoints(CombatCharacter character)
    {
        isTurnControllerNull();
        character.myStats.actionsRemaining = 0;
        turnController.CheckIfTurnOver();
    }
    public static void RemoveAllActionPoints(Character character)
    {
        isTurnControllerNull();
        character.actionsRemaining = 0;
        turnController.CheckIfTurnOver();
    }

    public static void LevelUp(Character character)
    {

    }

    public static void ResetMoves()
    {
        foreach (GameObject character in CombatSingleton.Instance.Combatants)
        {
            character.GetComponent<CombatCharacter>().ResetActions();
        }
    }
}
