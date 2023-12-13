using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapButtons : MonoBehaviour
{

    public InteractScroll scroll;
    public InteractScroll swap;


    public void StoreCharacter()
    {
        if (scroll.populator.SelectedCharacter != null)
        {
            SaveData.Current.mainData.charactersStorage.Add(scroll.populator.SelectedCharacter);
            SaveData.Current.mainData.characters.Remove(scroll.populator.SelectedCharacter);
            SaveManipulator.AutoSave();
        }
    }

    public void UnstoreCharacter()
    {
        if(swap.populator.SelectedCharacter != null)
        {
            SaveData.Current.mainData.characters.Add(swap.populator.SelectedCharacter);
            SaveData.Current.mainData.charactersStorage.Remove(swap.populator.SelectedCharacter);
            SaveManipulator.AutoSave();
        }
    }
}
