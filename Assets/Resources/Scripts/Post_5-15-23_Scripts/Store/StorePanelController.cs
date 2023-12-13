using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorePanelController : MonoBehaviour
{
    public GameObject[] Panels;
    public GameObject[] Content;
    public int storeID = 0;

    public GameObject CardItemPrefab;
    public GameObject CardEquipmentPrefab;
    public GameObject CardNamePrefab;
    public GameObject CardCharacterPrefab;

    public List<StoreOwner> storeOwnerList;
    private StoreOwner storeOwner;

    private JsonRetriever jsonRetriever;
    private GUIListPopulatorStore populatorItem;
    private GUIListPopulatorStore populatorEquipment;

    private List<Item> itemList;
    private List<Equipment> equipmentList;

    private bool isBuy = false;

    private void Start()
    {
        SaveManipulator.TestNewSaveAddAllCharacters();
        ResetPanels();
        LoadJSON();
        ChooseStoreOwner();
        CreatePopulators();
        CreateLists();
    }

    private void CreatePopulators()
    {
        populatorItem = new GUIListPopulatorStore(Content, CardItemPrefab, CardEquipmentPrefab, storeOwner, Panels, 
            CardNamePrefab, CardCharacterPrefab, true);
    }

    private void CreateLists()
    {
        itemList = new List<Item>();
        equipmentList = new List<Equipment>();

        for (int i = 0; i < storeOwner.items.Length; i++)
        {
            itemList.Add(jsonRetriever.Load1Item(storeOwner.items[i]));
        }
        for (int i = 0; i < storeOwner.equipment.Length; i++)
        {
            equipmentList.Add(jsonRetriever.Load1Equipment(storeOwner.equipment[i]));
        }
    }

    private void LoadJSON()
    {
        jsonRetriever = new JsonRetriever();
        storeOwnerList = jsonRetriever.LoadAllStoreOwners();
    }

    private void ChooseStoreOwner()
    {
        for (int i = 0; i < storeOwnerList.Count; i++)
        {
            if(storeOwnerList[i].id == storeID)
            {
                storeOwner = storeOwnerList[i];
            }
        }
    }

    public void ButtonBuy()
    {
        ResetPanels();
        isBuy = true;
        populatorItem.BuyOr(true);
        ActivatePanel("StoreBuySellPanel");
    }

    public void ButtonSell()
    {

        ResetPanels();
        isBuy = false;
        populatorItem.BuyOr(false);
        ActivatePanel("StoreBuySellPanel");
    }

    public void ButtonExit()
    {

    }

    public void ButtonEquipment()
    {
        TypeClicked(false);

    }

    public void ButtonItems()
    {
        TypeClicked(true);
    }


    private void TypeClicked(bool isItem)
    {
        if (isBuy)
        {
            if (isItem)
            {

                ActivatePanel("StoreProductPanel");
                populatorItem.ItemOr(isItem);
                populatorItem.UpdateUI(itemList);
            }
            else
            {
                ActivatePanel("StoreProductPanel");
                populatorItem.ItemOr(isItem);
                populatorItem.UpdateUI(equipmentList);
            }
        }
        else
        {

            if (isItem)
            {
                ActivatePanel("StoreCharacterNamePanel");
                populatorItem.ItemOr(isItem);
                populatorItem.SellItems();
            }
            else
            {
                ActivatePanel("StoreCharacterNamePanel");
                populatorItem.ItemOr(isItem);
                populatorItem.SellEqupment();
            }
        }
    }

    public void ButtonQuit()
    {
        ResetPanels();
    }

    private void ActivatePanel(string activateMe)
    {

        for (int i = 0; i < Panels.Length; i++)
        {
            if (Panels[i].name == activateMe)
            {
                Panels[i].SetActive(true);
            }
        }
    }


    private void DeactivatePanel(string activateMe)
    {

        for (int i = 0; i < Panels.Length; i++)
        {
            if (Panels[i].name == activateMe)
            {
                Panels[i].SetActive(false);
            }
        }
    }

    private void ResetPanels()
    {
        for (int i = 0; i < Panels.Length; i++)
        {
            if(Panels[i].name != "StoreMainPanel")
            {
                Panels[i].SetActive(false);
            }
        }
    }

}
