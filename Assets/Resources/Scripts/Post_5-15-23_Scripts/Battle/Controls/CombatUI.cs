using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatUI
{


    public CombatUIenable enact;

    public CombatUI()
    {
        enact = new CombatUIenable();
    }

    public void KeyboardInput()
    {
        MenuNavigate();
        MenuSelect();
    }


    private void MenuNavigate()
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
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            enact.Key("esc");
        }
    }

    private void MenuSelect()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            enact.Key("space");
        }
    }

}
