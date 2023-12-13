using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatKeyMouse : MonoBehaviour
{
    public InteractMenus interactMenus;
    public Actions actions;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        controls(); 
        InfoCheckIfOccupied();
    }

    private void controls()
    {
        if (CombatSingleton.Instance.isUiOn)
        {
            UiActivated();
        }
        else
        {
            UiDeactivated();
        }
    }

    private void UiActivated()
    {
        MenuNavigate();
        MenuSelect();
    }

    private void UiDeactivated()
    {
        EscapeButton();
        MoveCursor();
        Select();
    }

    private void MenuNavigate()
    {

        if (Input.GetKeyDown(KeyCode.W))
        {
            interactMenus.Previous();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            interactMenus.Next();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            interactMenus.ResetPanels();
            CombatSingleton.Instance.isUiOn = false;
        }
    }

    private void MenuSelect()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            interactMenus.Select();
        }
    }


    private void Select()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpaceButton();
        }
    }

    private void SpaceButton()
    {
        if(CombatSingleton.Instance.CursorCube.GetComponent<Cube>().PreviousPhase == CUBEPHASE.MOVE)
        {
            CombatSingleton.Instance.FocusCharacter.GetComponent<CombatCharacter>().myStats.actionsRemaining--;
            actions.Movement();
        }
        else if (CombatSingleton.Instance.CursorCube.GetComponent<Cube>().PreviousPhase == CUBEPHASE.DOUBLEMOVE)
        {

            CombatSingleton.Instance.FocusCharacter.GetComponent<CombatCharacter>().myStats.actionsRemaining -=2;
            actions.Movement();
        }
        else if (CombatSingleton.Instance.CursorCube.GetComponent<Cube>().MyType == GROUNDTYPE.Occupied)
        {
            if (CheckCubeForCharacter())
            {
                CubeManipulator.ResetAllCubes();
                ActivateMenus();
            }
        }
    }

    private void ActivateMenus()
    {
        if(CombatSingleton.Instance.FocusCharacter.GetComponent<CombatCharacter>().myStats.actionsRemaining > 0)
        {

            interactMenus.OpenMenu("ActionPanel");
            CombatSingleton.Instance.isUiOn = true;
        }
    }

    private bool CheckCubeForCharacter()
    {

        CombatSingleton.Instance.FocusCharacter = CubeRetriever.GetCharacterOnCursor();
        if(CombatSingleton.Instance.FocusCharacter != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void EscapeButton()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (CubeRetriever.AreCubesNotNormal())
            {
                CubeManipulator.ResetAllCubes();
            }
            else
            {
                CombatPanelManipulator.ActivatePanel("MenuPanel");
                CombatSingleton.Instance.isUiOn = true;
            }
        }
    }

    private void MoveCursor()
    {

        if (Input.GetKeyDown(KeyCode.W))
        {
            CubeManipulator.ChangeCursorCube(Camera.main.transform.forward);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            CubeManipulator.ChangeCursorCube(-Camera.main.transform.forward);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            CubeManipulator.ChangeCursorCube(-Camera.main.transform.right);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            CubeManipulator.ChangeCursorCube(Camera.main.transform.right);
        }
    }

    private void InfoCheckIfOccupied()
    {
        if(CombatSingleton.Instance.CursorCube.GetComponent<Cube>().MyType == GROUNDTYPE.Occupied)
        {
            CombatSingleton.Instance.InfoCharacter = CubeRetriever.GetCharacterOnCursor();
            CombatPanelManipulator.ActivatePanel("CharacterInfoPanel");
        }
        else
        {
            CombatPanelManipulator.DeactivatePanel("CharacterInfoPanel");
            CombatSingleton.Instance.InfoCharacter = null;
        }
    }
}
