using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectKey
{


    private InteractMenus interactMenus;
    private Actions actions;

    public SelectKey()
    {
        interactMenus = GameObject.Find("GameMaster").GetComponent<InteractMenus>();
        actions = GameObject.Find("ActionsMaster").GetComponent<Actions>();
    }

    public void SelectCube()
    {

        CUBEPHASE phase = CombatSingleton.Instance.CursorCube.GetComponent<Cube>().PreviousPhase;

        switch (CombatSingleton.Instance.actionData.Preview)
        {
            case (PREVIEWMODE.Off):
                NormalPhase();
                break;

            case (PREVIEWMODE.Move):
                PhaseActionSelector(phase);
                break;

            case (PREVIEWMODE.Action):
                PhaseActionSelector(phase);
                break;

            case (PREVIEWMODE.ActionMulti):
                SelectMulit();
                break;

            case (PREVIEWMODE.Item):
                PhaseActionSelector(phase);
                break;

            case (PREVIEWMODE.Capture):
                PhaseActionSelector(phase);
                break;
        }

        
    }

    private void SelectMulit()
    {
        if (Astar.WithAbilityRange(false))
        {
            CombatSingleton.Instance.FocusCharacter.GetComponent<CombatCharacter>().myStats.currentMana -=
                CombatSingleton.Instance.actionData.ChosenAbility.MPCost;

            CharacterManipulator.RemoveActionPoints(CombatSingleton.Instance.actionData.OriginCharacter, 1);
            GetTargetsInRange();
            CombatSingleton.Instance.actionData.actions.Abiliy();
        }
    }

    private void GetTargetsInRange()
    {
        CombatSingleton.Instance.actionData.TargetCharacters = new List<GameObject>();

        foreach (GameObject character in CombatSingleton.Instance.Combatants)
        {
            if(character.GetComponent<CombatCharacter>().myCube.GetComponent<CubePhase>().myPhase != CUBEPHASE.NORMAL)
            {
                CombatSingleton.Instance.actionData.TargetCharacters.Add(character);
            }
        }
    }

    private void PhaseActionSelector(CUBEPHASE phase)
    {

        switch (phase)
        {

            case CUBEPHASE.NORMAL:
                NormalPhase();
                break;

            case CUBEPHASE.MOVE:
                CharacterManipulator.RemoveActionPoints(CombatSingleton.Instance.FocusCharacter, 1);
                actions.Movement();
                break;

            case CUBEPHASE.DOUBLEMOVE:
                CharacterManipulator.RemoveActionPoints(CombatSingleton.Instance.FocusCharacter, 2);
                actions.Movement();
                break;

            case CUBEPHASE.ATTACK:
                if (CheckCubeForTarget())
                {
                    CombatSingleton.Instance.FocusCharacter.GetComponent<CombatCharacter>().myStats.currentMana -=
                        CombatSingleton.Instance.actionData.ChosenAbility.MPCost;

                    CharacterManipulator.RemoveAllActionPoints(CombatSingleton.Instance.FocusCharacter);
                    actions.Abiliy();
                }
                break;
            case CUBEPHASE.MAGIC:
                if (CheckCubeForTarget())
                {
                    CombatSingleton.Instance.FocusCharacter.GetComponent<CombatCharacter>().myStats.currentMana -=
                        CombatSingleton.Instance.actionData.ChosenAbility.MPCost;

                    CharacterManipulator.RemoveAllActionPoints(CombatSingleton.Instance.FocusCharacter);
                    actions.Abiliy();
                }
                break;
            case CUBEPHASE.ITEM:
                if (CheckCubeForTarget())
                {
                    CharacterManipulator.RemoveAllActionPoints(CombatSingleton.Instance.FocusCharacter);
                    actions.Item();
                }
                break;
            case CUBEPHASE.CAPTURE:
                if (CheckCubeForTarget())
                {
                    CharacterManipulator.RemoveAllActionPoints(CombatSingleton.Instance.FocusCharacter);
                    actions.Capture();
                }
                break;
            case CUBEPHASE.SPECIAL:
                if (CheckCubeForTarget())
                {
                    CharacterManipulator.RemoveActionPoints(CombatSingleton.Instance.FocusCharacter, 1);
                    actions.Special();
                }
                break;
        }
    }
    private void NormalPhase()
    {
        if (CombatSingleton.Instance.CursorCube.GetComponent<Cube>().MyType == GROUNDTYPE.Occupied)
        {
            if (CheckCubeForCharacter())
            {
                CubeManipulator.ResetAllCubes();
                ActivateMenus();
            }
        }
        else
        {
            CubeManipulator.ResetAllCubes();
        }
    }
    private void ActivateMenus()
    {
        if (CombatSingleton.Instance.FocusCharacter.GetComponent<CombatCharacter>().myStats.actionsRemaining > 0)
        {
            interactMenus.OpenMenu("ActionPanel");
            CombatSingleton.Instance.isUiOn = true;
        }
    }

    private bool CheckCubeForCharacter()
    {

        if (CubeRetriever.CheckCubeForCharacter())
        {
            if(CubeRetriever.GetCharacterOnCursor().GetComponent<CombatCharacter>().team ==
            CombatSingleton.Instance.battleSystem.CurrentTeam)
            {
                CombatSingleton.Instance.FocusCharacter = CubeRetriever.GetCharacterOnCursor();
                return true;
            }
        }
        return false;
    }


    private bool CheckCubeForTarget()
    {

        if (CubeRetriever.CheckCubeForCharacter())
        {
            CombatSingleton.Instance.actionData.TargetCharacters = new List<GameObject>();
            CombatSingleton.Instance.actionData.TargetCharacters.Add(CubeRetriever.GetCharacterOnCursor());
            return true;
                
            
        }
        return false;
    }
}
