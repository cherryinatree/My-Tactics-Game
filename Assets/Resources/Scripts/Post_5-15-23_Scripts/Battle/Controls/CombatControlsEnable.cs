using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatControlsEnable
{

    private InteractMenus interactMenus;
    private Actions actions;
    private SelectKey selectKey;

    public CombatControlsEnable()
    {
        interactMenus = GameObject.Find("GameMaster").GetComponent<InteractMenus>();
        actions = GameObject.Find("ActionsMaster").GetComponent<Actions>();
        selectKey = new SelectKey();
    }

    public void Key(string key)
    {
        switch (key)
        {

            //*********************** Movement Buttons ************************

            case "w":
                Movement(Camera.main.transform.forward);
            break;

            case "s":
                Movement(-Camera.main.transform.forward);
            break;

            case "a":
                Movement(-Camera.main.transform.right);
            break;

            case "d":
                Movement(Camera.main.transform.right);
            break;
        
            //*********************** Exit and Select ************************
            case "esc":
                EscapeButton();
                break;

            case "space":

                if (CombatSingleton.Instance.battleSystem.isKeyboardControl)
                {
                    selectKey.SelectCube();
                }
                break;


            case "Tab":
                CycleThroughCharacters();
                break;


            //*********************** Test Buttons ************************
            case "testTurnChange":
                CombatSingleton.Instance.battleSystem.TurnChange();
                break;
        }
    }


    private void Movement(Vector3 direction)
    {

        if (CombatSingleton.Instance.battleSystem.State == BATTLESTATE.PLAYERTURN)
        {
            CubeManipulator.ChangeCursorCube(direction);
            MultiTargetDisplay();
        }
    }

    private void MultiTargetDisplay()
    {
        if (CombatSingleton.Instance.actionData.Preview == PREVIEWMODE.ActionMulti)
        {
            CubeManipulator.ResetAllCubes();
            if (Astar.WithAbilityRange(false))
            {
                CombatSingleton.Instance.actionData.actions.PreviewAbility(CombatSingleton.Instance.actionData.ChosenAbility.id);
            }
        }
    }

    private void EscapeButton()
    {
        if (CubeRetriever.AreCubesNotNormal())
        {
            CubeManipulator.ResetAllCubes();
            CombatSingleton.Instance.actionData.ResetActionData();
        }
        else
        {
            CombatPanelManipulator.ActivatePanel("MenuPanel");
            CombatSingleton.Instance.isUiOn = true;
        }
    }

    private void CycleThroughCharacters()
    {
        Debug.Log("Cycle");
        List<GameObject> CycleFocusGroup = new List<GameObject>();
        foreach(GameObject ally in CombatSingleton.Instance.Combatants)
        {
            if(ally.GetComponent<CombatCharacter>().team == 0 && ally.GetComponent<CombatCharacter>().myStats.actionsRemaining >0)
            {
                CycleFocusGroup.Add(ally);
            }
        }
        int focus = 0;
        for(int i = 0; i < CycleFocusGroup.Count; i++)
        {
            if(CycleFocusGroup[i] == CombatSingleton.Instance.FocusCharacter)
            {
                focus = i;
                break;
            }
        }
        if(focus == CycleFocusGroup.Count - 1)
        {
            focus=0;
        }
        else
        {
            focus++;
        }
        CombatSingleton.Instance.FocusCharacter = CycleFocusGroup[focus];

        CombatSingleton.Instance.CursorCube.GetComponent<Cube>().NoLongerCursor();
        CombatSingleton.Instance.FocusCharacter.GetComponent<CombatCharacter>().myCube.GetComponent<Cube>().BecomeCursor();
    }

}
