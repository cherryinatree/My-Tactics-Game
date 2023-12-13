using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateInformation 
{
    CombatCharacter character;

    public StateInformation()
    {
        InformationLoad();
    }

    public void InformationLoad()
    {
        character = CombatSingleton.Instance.FocusCharacter.GetComponent<CombatCharacter>();
    }

    public void InformationUnload()
    {
        character = null;
    }
}
