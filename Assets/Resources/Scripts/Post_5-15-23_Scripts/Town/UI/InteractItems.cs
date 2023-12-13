using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InteractItems : MonoBehaviour
{
    public InteractScroll scroll;

    public Image[] itemDisplay;

    public Image characterItem0;
    public Image characterItem1;
    public Image characterItem2;

    private Character character;

    public GameObject IconPanel;
    private int page;
    private string itemType;

    private GameObject iconPrefab;
    private Sprite defaultIcon;

    public Text pageText;


    private void OnEnable()
    {
        itemType = "all";
        defaultIcon = Resources.Load<Sprite>("Images/Actions/Default");

        if (scroll.populator != null)
        {
            character = scroll.populator.SelectedCharacter;
        }
        page = 0;
        UpdatePageText();
        UpdateUI();
        UpdatePlayerItems();
    }


    public void ItemTransferUpdateUItoPlayer(Item icon)
    {
       if(character != null)
        {
            if (character.item0 == -1)
            {
                character.item0 = icon.id;
                SaveData.Current.mainData.itemStorage.Remove(icon);
            }
            else if(character.item1 == -1)
            {
                character.item1 = icon.id;
                SaveData.Current.mainData.itemStorage.Remove(icon);

            }
            else if(character.item2 == -1)
            {

                character.item2 = icon.id;
                SaveData.Current.mainData.itemStorage.Remove(icon);
            }
            
        }

        UpdatePageText();
        UpdateUI();
        UpdatePlayerItems();
    }

    public void ItemTransferUpdateUItoStorage(Item icon, int iconId)
    {
       
        if (character != null)
        {
            if (iconId == 0)
            {
                character.item0 = -1;
                SaveData.Current.mainData.itemStorage.Add(icon);
            }
            else if(iconId == 1)
            {
                character.item1 = -1;
                SaveData.Current.mainData.itemStorage.Add(icon);

            }
            else if(iconId == 2)
            {
                character.item2 = -1;
                SaveData.Current.mainData.itemStorage.Add(icon);
            }
        }

        UpdatePageText();
        UpdateUI();
        UpdatePlayerItems();
    }

    private void UpdatePlayerItems()
    {
        if (character != null)
        {
            if (character.item0 != -1)
            {
                ButtonUpdater(characterItem0, character.item0, 0);
            }
            else
            {
                characterItem0.GetComponent<Button>().onClick.RemoveAllListeners();
                characterItem0.sprite = defaultIcon;
            }
            if (character.item1 != -1)
            {
                ButtonUpdater(characterItem1, character.item1, 1);
            }
            else
            {
                characterItem1.GetComponent<Button>().onClick.RemoveAllListeners();
                characterItem1.sprite = defaultIcon;
            }
            if (character.item2 != -1)
            {
                ButtonUpdater(characterItem2, character.item2, 2);
            }
            else
            {
                characterItem2.GetComponent<Button>().onClick.RemoveAllListeners();
                characterItem2.sprite = defaultIcon;
            }
            
        }
    }

    private void ButtonUpdater(Image image, int itemRetrieve, int position)
    {

        JsonRetriever jsonRetriever = new JsonRetriever();
        Item item = jsonRetriever.Load1Item(itemRetrieve);
        image.sprite = Resources.Load<Sprite>(item.icon);
        image.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = item.id.ToString();
        image.GetComponent<Button>().onClick.RemoveAllListeners();
        image.GetComponent<Button>().onClick.AddListener(delegate
        { ItemTransferUpdateUItoStorage(item, position); });
    }

    private void UpdatePageText()
    {
        int maxPage = getMaxPage();
        pageText.text = (page + 1).ToString() + "/" + maxPage.ToString();

    }

    private int getMaxPage()
    {
        int maxPage = 0;
        int total = 0;
        if (itemType == "all")
        {
            maxPage = (int)Mathf.Floor(SaveData.Current.mainData.itemStorage.Count / 10) + 1;
        }
        else
        {
            for (int i = 0; i < SaveData.Current.mainData.itemStorage.Count; i++)
            {
                if (SaveData.Current.mainData.itemStorage[i].type == itemType)
                {
                    total++;
                }
            }
            maxPage = (int)Mathf.Floor(total / 10) + 1;
        }

        return maxPage;
    }

    private void UpdateUI()
    {
        iconPrefab = Resources.Load<GameObject>("Prefabs/PrefabsUI/ImageButton");
        ClearPanel();

        for (int i = 0; i < 10; i++)
        {
            GameObject icon = GameObject.Instantiate(iconPrefab);
            icon.transform.SetParent(IconPanel.transform, false);
            IconLoader(icon.transform.GetChild(0).transform.GetChild(0).gameObject, i);
        }
    }

    private void IconLoader(GameObject icon, int position)
    {
        int truePosition = position + (10 * page);
        int step = 0;

        if (SaveData.Current.mainData.itemStorage.Count > truePosition)
        {

            if (itemType == "all")
            {
                icon.transform.Find("itemImage").GetComponent<Image>().sprite = Resources.Load<Sprite>(
                    SaveData.Current.mainData.itemStorage[truePosition].icon);
                icon.transform.Find("id").GetComponent<TextMeshProUGUI>().text = SaveData.Current.mainData.itemStorage[truePosition].id.ToString();

                icon.transform.Find("itemImage").GetComponent<Button>().onClick.RemoveAllListeners();
                icon.transform.Find("itemImage").GetComponent<Button>().onClick.AddListener(delegate
                { ItemTransferUpdateUItoPlayer(SaveData.Current.mainData.itemStorage[truePosition]); });
                return;
            }
            else
            {
                for (int i = 0; i < SaveData.Current.mainData.itemStorage.Count; i++)
                {
                    if (SaveData.Current.mainData.itemStorage[i].type == itemType)
                    {
                        if (step == truePosition)
                        {

                            icon.transform.Find("itemImage").GetComponent<Image>().sprite = Resources.Load<Sprite>(
                                SaveData.Current.mainData.itemStorage[i].icon);
                            icon.transform.Find("id").GetComponent<TextMeshProUGUI>().text = SaveData.Current.mainData.itemStorage[i].id.ToString();


                            icon.transform.Find("itemImage").GetComponent<Button>().onClick.RemoveAllListeners();
                            icon.transform.Find("itemImage").GetComponent<Button>().onClick.AddListener(delegate
                            { ItemTransferUpdateUItoPlayer(SaveData.Current.mainData.itemStorage[i]); });
                            return;
                        }
                        else
                        {
                            step++;
                        }
                    }
                }

                icon.transform.Find("itemImage").GetComponent<Image>().sprite = defaultIcon; 
                icon.transform.Find("itemImage").GetComponent<Button>().onClick.RemoveAllListeners();
                icon.transform.Find("id").GetComponent<TextMeshProUGUI>().text = "null";
            }
        }
        else
        {
            icon.transform.Find("itemImage").GetComponent<Image>().sprite = defaultIcon;
            icon.transform.Find("itemImage").GetComponent<Button>().onClick.RemoveAllListeners();
            icon.transform.Find("id").GetComponent<TextMeshProUGUI>().text = "null";
        }
    }

    private void ClearPanel()
    {
        foreach (Transform child in IconPanel.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    public void ButtonPageUp()
    {
        if (page + 1 < getMaxPage())
        {
            page++;
        }
        UpdateUI();
        UpdatePageText();
    }

    public void ButtonPageDown()
    {
        if (page > 0)
        {
            page--;
        }
        UpdateUI();
        UpdatePageText();
    }


    public void ButtonAll()
    {
        ItemTypeChange("all");
    }

    public void ButtonHealing()
    {
        ItemTypeChange("Healing");
    }

    public void ButtonBattle()
    {
        ItemTypeChange("Battle");
    }

    public void ButtonSpecial() 
    { 
        ItemTypeChange("Special");
    }

    private void ItemTypeChange(string theType)
    {
        itemType = theType;
        page = 0;
        UpdateUI();
        UpdatePageText();
    }

    private void Update()
    {
        if (character != scroll.populator.SelectedCharacter)
        {
            character = scroll.populator.SelectedCharacter;
            UpdatePlayerItems();
        }
    }
}
