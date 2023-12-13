using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatKeyboard : MonoBehaviour
{
    private CombatUI UI;
    private CombatControls RegularControls;

    // Start is called before the first frame update
    void Start()
    {
        UI = new CombatUI();
        RegularControls = new CombatControls();
    }

    // Update is called once per frame
    void Update()
    {
        DoesPlayerHaveControl();
    }


    private void DoesPlayerHaveControl()
    {
        if (CombatSingleton.Instance.battleSystem.CurrentTeam ==0)
        {
            IsTheUiOn();
            InfoCheckIfOccupied();
        }
    }


    private void IsTheUiOn()
    {
        if (CombatSingleton.Instance.isUiOn)
        {
            UI.KeyboardInput();
        }
        else
        {
            RegularControls.KeyboardInput();
        }
    }

    private void InfoCheckIfOccupied()
    {
        if (CombatSingleton.Instance.CursorCube.GetComponent<Cube>().MyType == GROUNDTYPE.Occupied)
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
