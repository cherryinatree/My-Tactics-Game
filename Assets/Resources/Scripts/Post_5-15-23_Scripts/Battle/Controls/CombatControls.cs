using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatControls
{

    public CombatControlsEnable enact;

    public CombatControls()
    {
        enact = new CombatControlsEnable();
    }
    public void KeyboardInput()
    {
        EscapeButton();
        MoveCursor();
        Select();
        CycleThroughCharacters();
    }

    private void MoveCursor()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            enact.Key("w");
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            enact.Key("s");
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            enact.Key("a");
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            enact.Key("d");
        }
    }
    private void EscapeButton()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            enact.Key("esc");
        }
    }
    private void Select()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            enact.Key("space");
        }
    }

    private void CycleThroughCharacters()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            enact.Key("Tab");
        }
    }

}
