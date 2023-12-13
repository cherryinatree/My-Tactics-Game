using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GUIListPopulator
{

    private RectTransform View;
    private GameObject CardPrefab;
    public Character SelectedCharacter;
    public Item SelectedItem;
    public Equipment SelectedEquipment;

    private string populatedType;

    public GUIListPopulator(RectTransform view, GameObject cardPrefab, Character selectedCharacter)
    {
        View = view;
        CardPrefab = cardPrefab;
        SelectedCharacter = selectedCharacter;
        populatedType = "CharacterID";
    }

    public GUIListPopulator(RectTransform view, GameObject cardPrefab, Item selectedItem)
    {
        View = view;
        CardPrefab = cardPrefab;
        SelectedItem = selectedItem;
        populatedType = "ItemID";
    }
    public GUIListPopulator(RectTransform view, GameObject cardPrefab, Equipment selectedEquipment)
    {
        View = view;
        CardPrefab = cardPrefab;
        SelectedEquipment = selectedEquipment;
        populatedType = "EquipmentID";
    }

    public void NewCharacter(Character selectedCharacter)
    {
        SelectedCharacter = selectedCharacter;
    }

    public void UpdateUI(List<Character> characters)
    {
        // Clear the previous contents of the list
        ClearViewport(View.gameObject);


        // Add items to the player's inventory list
        foreach (Character stats in characters)
        {
            GameObject card = GameObject.Instantiate(CardPrefab);
            card.transform.SetParent(View.transform, false);
            if (populatedType == "CharacterID")
            {
                card.GetComponentInChildren<Button>().onClick.AddListener(delegate { SelectCard(stats); });
                CardLoader(card, stats);
            }
        }
    }
    public void UpdateUI(List<Item> items)
    {
        // Clear the previous contents of the list
        ClearViewport(View.gameObject);


        // Add items to the player's inventory list
        foreach (Item stats in items)
        {
            GameObject card = GameObject.Instantiate(CardPrefab);
            card.transform.SetParent(View.transform, false);
            if (populatedType == "ItemID")
            {
                card.GetComponentInChildren<Button>().onClick.AddListener(delegate { SelectCard(stats); });
                //CardLoader(card, stats);
            }
        }
    }


    public void UpdateUI(List<Equipment> equipment)
    {
        // Clear the previous contents of the list
        ClearViewport(View.gameObject);


        // Add items to the player's inventory list
        foreach (Equipment stats in equipment)
        {
            GameObject card = GameObject.Instantiate(CardPrefab);
            card.transform.SetParent(View.transform, false);
            if (populatedType == "EquipmentID")
            {
                card.GetComponentInChildren<Button>().onClick.AddListener(delegate { SelectCard(stats); });
                //CardLoader(card, stats);
            }
        }
    }


    private void SelectCard(Character stats)
    {
        SelectedCharacter = stats;
        HighlightCard();
    }

    private void SelectCard(Item stats)
    {
        SelectedItem = stats;
        HighlightCard();
    }

    private void SelectCard(Equipment stats)
    {
        SelectedEquipment = stats;
        HighlightCard();
    }


    public void HighlightCard()
    {
        Transform scroll = View;
        if (SelectedCharacter != null)
        {

            string id = "";
            if(populatedType == "CharacterID")
            {
                id = SelectedCharacter.characterID;
            }
            else if (populatedType == "ItemID")
            {

                id = SelectedItem.id.ToString();
            }
            else if (populatedType == "EquipmentID")
            {

                id = SelectedEquipment.id.ToString();
            }

            for (int i = 0; i < scroll.childCount; i++)
            {
                string text = scroll.GetChild(i).Find(populatedType).GetComponent<TextMeshProUGUI>().text;


                if (text == id)
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
    }/*
    private void CardLoader(GameObject card, Item stats)
    {
        card.transform.Find("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>(stats.icon);
        card.transform.Find("Text").GetComponent<Text>().text = stats.characterName + "\n\n" + stats.characterClass + "\nLevel: " + stats.level;
        card.transform.Find("CharacterID").GetComponent<TextMeshProUGUI>().text = stats.characterID;
    }
    private void CardLoader(GameObject card, Equipment stats)
    {
        card.transform.Find("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>(stats.icon);
        card.transform.Find("Text").GetComponent<Text>().text = stats.characterName + "\n\n" + stats.characterClass + "\nLevel: " + stats.level;
        card.transform.Find("CharacterID").GetComponent<TextMeshProUGUI>().text = stats.characterID;
    }*/
    private void ClearViewport(GameObject Panel)
    {
        foreach (Transform child in Panel.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
}
