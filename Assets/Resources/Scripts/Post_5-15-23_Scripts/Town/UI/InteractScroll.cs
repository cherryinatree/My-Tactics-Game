using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InteractScroll : MonoBehaviour
{
    public RectTransform playerScrollView;
    public GameObject CardPrefab;

    public Character SelectedCharacter;
    private int characterListLength;

    public bool isStorage = false;

    public GUIListPopulator populator;
    List<Character> list;

    // Start is called before the first frame update
    void Start()
    {
        StartGUI();
    }

    private void OnEnable()
    {
        StartGUI();
    }

    private void StartGUI()
    {

        CardPrefab = Resources.Load<GameObject>("Prefabs/PrefabsUI/CharacterCard");

        if (isStorage)
        {
            list = SaveData.Current.mainData.charactersStorage;
        }
        else
        {
            list = SaveData.Current.mainData.characters;
        }

        if (list.Count > 0)
        {
            SelectedCharacter = list[0];
        }
        else
        {
            SelectedCharacter = null;
        }

        if (populator == null)
        {
            populator = new GUIListPopulator(playerScrollView, CardPrefab, SelectedCharacter);
        }

        characterListLength = list.Count;

        // Update the UI with the current item lists
        populator.NewCharacter(SelectedCharacter);
        populator.UpdateUI(list);
        populator.HighlightCard();
    }


    private void FixedUpdate()
    {
        if (characterListLength != list.Count)
        {
            populator.UpdateUI(list);
            characterListLength = list.Count;
        }
    }
}
