using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InteractSwap : MonoBehaviour
{
    public RectTransform playerScrollView;
    public GameObject CardPrefab;

    public Character SelectedCharacter;
    private int characterListLength;


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(SaveData.Current.mainData.charactersStorage.Count);
        Debug.Log(SaveData.Current.mainData.characters.Count);


        CardPrefab = Resources.Load<GameObject>("Prefabs/PrefabsUI/CharacterCard");
        if (SaveData.Current.mainData.charactersStorage.Count > 0)
        {
            SelectedCharacter = SaveData.Current.mainData.charactersStorage[0];
        }
        else
        {
            SelectedCharacter = null;
        }
        characterListLength = SaveData.Current.mainData.charactersStorage.Count;
        UpdateUI();
        HighlightBattleCard();
    }
    private void OnEnable()
    {

        // Update the UI with the current item lists
        UpdateUI();
        HighlightBattleCard();
    }


    private void UpdateUI()
    {
        // Clear the previous contents of the list
        ClearViewport(playerScrollView.gameObject);

        // Add items to the player's inventory list
        foreach (Character stats in SaveData.Current.mainData.charactersStorage)
        {
            GameObject card = GameObject.Instantiate(CardPrefab);
            card.transform.SetParent(playerScrollView.transform, false);
            card.GetComponentInChildren<Button>().onClick.AddListener(delegate { SelectBattleCard(stats); });
            CardLoader(card, stats);
        }
    }

    private void SelectBattleCard(Character stats)
    {
        SelectedCharacter = stats;
        Debug.Log(stats.characterName);
        HighlightBattleCard();
    }

    public void HighlightBattleCard()
    {
        Transform scroll = playerScrollView;
        if (SelectedCharacter != null)
        {
            for (int i = 0; i < scroll.childCount; i++)
            {
                string text = scroll.GetChild(i).Find("CharacterID").GetComponent<TextMeshProUGUI>().text;

                if (text == SelectedCharacter.characterID)
                {
                    scroll.GetChild(i).GetComponent<Image>().color = new Color32(253, 244, 151, 255);
                }
                else
                {
                    scroll.GetChild(i).GetComponent<Image>().color = new Color32(151, 156, 253, 255);
                }
            }
        }
    }

    private void CardLoader(GameObject card, Character stats)
    {
        card.transform.Find("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>(stats.icon);
        card.transform.Find("Text").GetComponent<Text>().text = stats.characterName + "\n\n" + stats.characterClass + "\nLevel: " + stats.level;
        card.transform.Find("CharacterID").GetComponent<TextMeshProUGUI>().text = stats.characterID;
    }
    private void ClearViewport(GameObject Panel)
    {
        foreach (Transform child in Panel.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    private void FixedUpdate()
    {
        if(characterListLength != SaveData.Current.mainData.charactersStorage.Count)
        {
            UpdateUI();
            characterListLength = SaveData.Current.mainData.charactersStorage.Count;
        }
    }
}
