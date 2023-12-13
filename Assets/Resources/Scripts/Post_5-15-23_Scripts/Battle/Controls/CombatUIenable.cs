using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatUIenable
{


    public InteractMenus interactMenus;
    public Actions actions;

    public CombatUIenable()
    {
        interactMenus = GameObject.Find("GameMaster").GetComponent<InteractMenus>();
        actions = GameObject.Find("ActionsMaster").GetComponent<Actions>();
    }

    public void Key(string key)
    {
        if(CombatSingleton.Instance.actionData.Preview == PREVIEWMODE.Facing)
        {
            Facing(key);
        }
        else
        {
            NavigateMenu(key);
        }

    }

    private void Facing(string key)
    {
        FacingArrows.FacingDirection W = FaceWhichWay(key);

        Debug.Log(W.ToString());

        switch (key)
        {
            case "w":
                CombatSingleton.Instance.FocusCharacter.GetComponentInChildren<FacingArrows>().SetDirectionArrow(W);
                break;
            case "s":
                CombatSingleton.Instance.FocusCharacter.GetComponentInChildren<FacingArrows>().SetDirectionArrow(W);
                break;
            case "a":
                CombatSingleton.Instance.FocusCharacter.GetComponentInChildren<FacingArrows>().SetDirectionArrow(W);
                break;
            case "d":
                CombatSingleton.Instance.FocusCharacter.GetComponentInChildren<FacingArrows>().SetDirectionArrow(W);
                break;

            case "esc":
                interactMenus.ResetPanels();

                CombatSingleton.Instance.FocusCharacter.GetComponentInChildren<FacingArrows>().ArrowsActive();
                CombatSingleton.Instance.isUiOn = false;
                CombatSingleton.Instance.actionData.ResetActionData();
                break;

            case "space":
                CombatSingleton.Instance.FocusCharacter.GetComponentInChildren<FacingArrows>().ArrowsActive();
                CombatSingleton.Instance.isUiOn = false;
                CombatSingleton.Instance.actionData.ResetActionData();
                break;
        }
    }


    private FacingArrows.FacingDirection FaceWhichWay(string key)
    {
        float facing = Front();

        FacingArrows.FacingDirection Forward;
        FacingArrows.FacingDirection Left;
        FacingArrows.FacingDirection Right;
        FacingArrows.FacingDirection Down;

        if(facing == 0)
        {
            Forward = FacingArrows.FacingDirection.Up;
            Left = FacingArrows.FacingDirection.Left;
            Right = FacingArrows.FacingDirection.Right;
            Down = FacingArrows.FacingDirection.Down;
        }
        else if (facing == 90)
        {
            Forward = FacingArrows.FacingDirection.Right;
            Left = FacingArrows.FacingDirection.Up;
            Right = FacingArrows.FacingDirection.Down;
            Down = FacingArrows.FacingDirection.Left;
        }
        else if (facing == 180)
        {
            Forward = FacingArrows.FacingDirection.Down;
            Left = FacingArrows.FacingDirection.Right;
            Right = FacingArrows.FacingDirection.Left;
            Down = FacingArrows.FacingDirection.Up;
        }
        else
        {
            Forward = FacingArrows.FacingDirection.Left;
            Left = FacingArrows.FacingDirection.Down;
            Right = FacingArrows.FacingDirection.Up;
            Down = FacingArrows.FacingDirection.Right;
        }


        if (key == "w")
        {
            return Forward;
        }
        if (key == "s")
        {
            return Down;
        }
        if (key == "a")
        {
            return Left;
        }
        if (key == "d")
        {
            return Right;
        }


        return FacingArrows.FacingDirection.Up;
    }

    private float Front()
    {
        float facing = Camera.main.transform.eulerAngles.y;

        if (facing <= 45 && facing >= -45)
        {
            facing = 0;
        }
        else if (facing <= 135 && facing >= 45)
        {
            facing = 90;
        }
        else if (facing <= 225 && facing >= 135)
        {
            facing = 180;
        }
        else if (facing <= 315 && facing >= 225)
        {
            facing = 270;
        }
        else if (facing <= 405 && facing >= 315)
        {
            facing = 0;
        }
        else if (facing >= -135 && facing <= -45)
        {
            facing = 270;
        }
        else if (facing >= -225 && facing <= -135)
        {
            facing = 180;
        }
        else if (facing >= -315 && facing <= -225)
        {
            facing = 90;
        }
        else if (facing >= -405 && facing <= -315)
        {
            facing = 0;
        }
        return facing;
    }

    private void NavigateMenu(string key)
    {

        switch (key)
        {
            case "w":
                interactMenus.Previous();
                break;

            case "s":
                interactMenus.Next();
                break;

            case "esc":
                interactMenus.ResetPanels();
                CombatSingleton.Instance.isUiOn = false;
                break;

            case "space":
                interactMenus.Select();
                break;
        }
    }
}
