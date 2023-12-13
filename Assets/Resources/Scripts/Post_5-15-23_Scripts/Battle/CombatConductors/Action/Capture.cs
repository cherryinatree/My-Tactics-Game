using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Capture
{
   
    public static void CaptureEnemy(GameObject target)
    {

            
        SaveData.Current.mainData.charactersStorage.Add(target.GetComponent<CombatCharacter>().myStats);
        target.GetComponent<CharacterDeath>().Death(3);
        SaveManipulator.AutoSave();
        Debug.Log(SaveData.Current.mainData.charactersStorage.Count);

    }
}
