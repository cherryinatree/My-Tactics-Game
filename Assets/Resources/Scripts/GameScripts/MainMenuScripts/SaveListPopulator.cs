using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SaveListPopulator
{
    private RectTransform View;
    private GameObject CardPrefab;
    public string Selected;


    public SaveListPopulator(RectTransform view, GameObject cardPrefab, string selected)
    {
        View = view;
        CardPrefab = cardPrefab;
        Selected = selected;
    }


    public void NewCharacter(string selected)
    {
        Selected = selected;
    }

    public void UpdateUI(string[] list)
    {
        // Clear the previous contents of the list
        ClearViewport(View.gameObject);

        int step = 0;
        // Add items to the player's inventory list
        foreach (string item in list)
        {
            GameObject card = GameObject.Instantiate(CardPrefab);
            CardLoader(card, item, step);
            card.transform.SetParent(View.transform, false);

            card.GetComponentInChildren<Button>().onClick.AddListener(delegate { SelectCard(item); });
            step++;
        }
    }


    private void SelectCard(string save)
    {
        Selected = save;
        HighlightCard();
    }
    public void HighlightCard()
    {
        Transform scroll = View;
        if (Selected != null)
        {

            string id = Selected;

            for (int i = 0; i < scroll.childCount; i++)
            {
                string text = scroll.GetChild(i).Find("NameText").GetComponent<TextMeshProUGUI>().text;


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

    private void CardLoader(GameObject card, string name, int saveNumber)
    {
        card.transform.Find("NameText").GetComponent<TextMeshProUGUI>().text = name;
        card.transform.Find("NumberText").GetComponent<TextMeshProUGUI>().text = (saveNumber+1).ToString();
    }
    private void ClearViewport(GameObject Panel)
    {
        foreach (Transform child in Panel.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
}
