using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoreButtonController : MonoBehaviour
{

    public GameObject CardItemPrefab;
    public GameObject CardEquipmentPrefab;
    public GameObject CardNamePrefab;
    public GameObject CardCharacterPrefab;

    private bool isBuy = false;

    // Start is called before the first frame update
    void Start()
    {
        ResetPanels();
    }

    public void ButtonBuy()
    {
        ResetPanels();
        isBuy = true;
        ActivatePanel(0);
    }

    public void ButtonSell()
    {
        ResetPanels();
        isBuy = false;
        ActivatePanel(0);
    }

    public void ButtonExit()
    {

    }

    public void ButtonEquipment()
    {
        ResetPanels();
        ActivatePanel(0);
        TypeClicked(false);
    }

    public void ButtonItems()
    {
        ResetPanels();
        ActivatePanel(0);
        TypeClicked(true);
    }


    private void TypeClicked(bool isItem)
    {
        if (isBuy)
        {
            if (isItem)
            {

                ActivatePanel(1);
                FillCards.ClearViewport(StoreSingleton.Instance.Content[0]);
                foreach(Item item in StoreSingleton.Instance.itemList)
                {
                    GameObject card = FillCards.ItemCard(CardItemPrefab, StoreSingleton.Instance.Content[0], item, true);
                    card.GetComponent<Button>().onClick.AddListener(() => SelectForPurchaces(item));
                }
            }
            else
            {
                ActivatePanel(1);
                FillCards.ClearViewport(StoreSingleton.Instance.Content[0]);
                foreach (Equipment equip in StoreSingleton.Instance.equipmentList)
                {
                    GameObject card = FillCards.EquipmentCard(CardEquipmentPrefab, StoreSingleton.Instance.Content[0], equip, true);
                    card.GetComponent<Button>().onClick.AddListener(() => SelectForPurchaces(equip));
                }
            }
        }
        else
        {
            ActivatePanel(2);
            FillCards.ClearViewport(StoreSingleton.Instance.Content[1]);

            GameObject cardInventory = FillCards.NameCardInventory(CardNamePrefab, StoreSingleton.Instance.Content[1]);
            cardInventory.GetComponent<Button>().onClick.AddListener(() => InventoryStorage(isItem));

            foreach (Character stats in StoreSingleton.Instance.stats)
            {
                GameObject card = FillCards.NameCard(CardNamePrefab, StoreSingleton.Instance.Content[1], stats);
                card.GetComponent<Button>().onClick.AddListener(() => characterInventory(stats, isItem));
            }
        }
    }

    /**************************************************************************************************
     * 
     * 
     *                      Purchase Code
     * 
     * 
     * ************************************************************************************************/

    private void SelectForPurchaces(Item item)
    {

        ActivatePanel(2);
        FillCards.ClearViewport(StoreSingleton.Instance.Content[1]);
        GameObject cardInventory = FillCards.NameCardInventory(CardNamePrefab, StoreSingleton.Instance.Content[1]);
        cardInventory.GetComponent<Button>().onClick.AddListener(() => SelectInventoryForPurchace(item));

        foreach (Character stats in StoreSingleton.Instance.stats)
        {
            GameObject card = FillCards.NameCard(CardNamePrefab, StoreSingleton.Instance.Content[1], stats);
            card.GetComponent<Button>().onClick.AddListener(() => SelectCharacterForPurchace(item, stats));
        }
    }
    private void SelectForPurchaces(Equipment equip)
    {

        ActivatePanel(2);
        FillCards.ClearViewport(StoreSingleton.Instance.Content[1]);
        GameObject cardInventory = FillCards.NameCardInventory(CardNamePrefab, StoreSingleton.Instance.Content[1]);
        cardInventory.GetComponent<Button>().onClick.AddListener(() => SelectInventoryForPurchace(equip));

        foreach (Character stats in StoreSingleton.Instance.stats)
        {
            GameObject card = FillCards.NameCard(CardNamePrefab, StoreSingleton.Instance.Content[1], stats);
            card.GetComponent<Button>().onClick.AddListener(() => SelectCharacterForPurchace(equip, stats));
        }
    }

    private void SelectCharacterForPurchace(Equipment equip, Character stats)
    {
        ActivatePanel(3);
        ActivatePanel(4);

        FillCards.ClearViewport(StoreSingleton.Instance.Content[2]);
        GameObject card = FillCards.CharacterCard(CardCharacterPrefab, StoreSingleton.Instance.Content[2], stats);


        StoreSingleton.Instance.Panels[4].transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text =
            "Give " + equip.equipmentName + " to " +  stats.characterName + "?";
        StoreSingleton.Instance.Panels[4].transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<Button>().onClick.RemoveAllListeners();
        StoreSingleton.Instance.Panels[4].transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<Button>().onClick.AddListener(
            delegate { PurchaseEquipment(stats, equip); });

    }
    private void SelectCharacterForPurchace(Item item, Character stats)
    {

        ActivatePanel(3);
        ActivatePanel(4);

        FillCards.ClearViewport(StoreSingleton.Instance.Content[2]);
        GameObject card = FillCards.CharacterCard(CardCharacterPrefab, StoreSingleton.Instance.Content[2], stats);


        StoreSingleton.Instance.Panels[4].transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text =
            "Give " + item.itemName + " to " +  stats.characterName + "?";
        StoreSingleton.Instance.Panels[4].transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<Button>().onClick.RemoveAllListeners();
        StoreSingleton.Instance.Panels[4].transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<Button>().onClick.AddListener(
            delegate { PurchaseItem(stats, item); });
    }
    private void SelectInventoryForPurchace(Equipment equip)
    {
        ActivatePanel(4);
        DeactivatePanel(3);

        StoreSingleton.Instance.Panels[4].transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text =
            "Send " + equip.equipmentName + " to inventory?";
        StoreSingleton.Instance.Panels[4].transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<Button>().onClick.RemoveAllListeners();
        StoreSingleton.Instance.Panels[4].transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<Button>().onClick.AddListener(
            delegate { PurchaseEquipment(equip); });
    }
    private void SelectInventoryForPurchace(Item item)
    {
        ActivatePanel(4);
        DeactivatePanel(3);

        StoreSingleton.Instance.Panels[4].transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text =
            "Send " + item.itemName + " to inventory?";
        StoreSingleton.Instance.Panels[4].transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<Button>().onClick.RemoveAllListeners();
        StoreSingleton.Instance.Panels[4].transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<Button>().onClick.AddListener(
            delegate { PurchaseItem(item); });
    }

    private void PurchaseItem(Character stats, Item item)
    {
        if (SaveData.Current.mainData.playerData.money >= item.buyPrice)
        {
            if (InventoryManipulator.AddItem(stats, item))
            {
                DeactivatePanel(4);
                FillCards.ClearViewport(StoreSingleton.Instance.Content[2]);
                GameObject card = FillCards.CharacterCard(CardCharacterPrefab, StoreSingleton.Instance.Content[2], stats);
                SaveData.Current.mainData.playerData.money -= item.buyPrice;
            }
            else
            {
                StoreSingleton.Instance.Panels[4].transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text =
            stats.characterName + " already has this item.";
            }
        }
        else
        {
            StoreSingleton.Instance.Panels[4].transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text =
            "Not Enough Gold";
        }
    }
    private void PurchaseItem(Item item)
    {
        DeactivatePanel(4);
        InventoryManipulator.AddItem(item);
        SaveData.Current.mainData.playerData.money -= item.buyPrice;
    }

    private void PurchaseEquipment(Character stats, Equipment equip)
    {
        if(SaveData.Current.mainData.playerData.money >= equip.buyPrice)
        {
            if(InventoryManipulator.AddEquipment(stats, equip))
            {
                DeactivatePanel(4);
                FillCards.ClearViewport(StoreSingleton.Instance.Content[2]);
                GameObject card = FillCards.CharacterCard(CardCharacterPrefab, StoreSingleton.Instance.Content[2], stats);
                SaveData.Current.mainData.playerData.money -= equip.buyPrice;
            }
            else
            {
                StoreSingleton.Instance.Panels[4].transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text =
            stats.characterName + " already has this item.";
            }
        }
        else
        {

            StoreSingleton.Instance.Panels[4].transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text =
        "Not Enough Gold";
        }
    }
    private void PurchaseEquipment(Equipment equip)
    {

        DeactivatePanel(4);

        InventoryManipulator.AddEquipment(equip);
        SaveData.Current.mainData.playerData.money -= equip.buyPrice;
    }


    /**************************************************************************************************
     * 
     * 
     *                      Sale Code
     * 
     * 
     * ************************************************************************************************/

    private void SelectForSale(Item item)
    {

        ActivatePanel(4);

        StoreSingleton.Instance.Panels[4].transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text =
            "Sell " + item.itemName + "?";
        StoreSingleton.Instance.Panels[4].transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<Button>().onClick.RemoveAllListeners();
        StoreSingleton.Instance.Panels[4].transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<Button>().onClick.AddListener(
            delegate { SellItem(item); });
    }
    private void SelectForSale(Equipment equip)
    {
        ActivatePanel(4);

        StoreSingleton.Instance.Panels[4].transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text =
            "Sell " + equip.equipmentName + "?";
        StoreSingleton.Instance.Panels[4].transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<Button>().onClick.RemoveAllListeners();
        StoreSingleton.Instance.Panels[4].transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<Button>().onClick.AddListener(
            delegate { SellEquipment(equip); });
    }

    private void SelectForSale(Character stats, Item item)
    {
        ActivatePanel(4);

        StoreSingleton.Instance.Panels[4].transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text =
            "Sell "+ item.itemName + "?";
        StoreSingleton.Instance.Panels[4].transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<Button>().onClick.RemoveAllListeners();
        StoreSingleton.Instance.Panels[4].transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<Button>().onClick.AddListener(
            delegate { SellItem(stats, item); });
    }
    private void SelectForSale(Character stats, Equipment equip)
    {
        ActivatePanel(4);

        StoreSingleton.Instance.Panels[4].transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text =
            "Sell "+ equip.equipmentName + "?";
        StoreSingleton.Instance.Panels[4].transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<Button>().onClick.RemoveAllListeners();
        StoreSingleton.Instance.Panels[4].transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<Button>().onClick.AddListener(
            delegate { SellEquipment(stats, equip); });
    }
    private void SellEquipment(Character stats, Equipment equip)
    {
        DeactivatePanel(4);
        InventoryManipulator.RemoveEquipment(stats, equip);
        SaveData.Current.mainData.playerData.money += equip.sellPrice;
        characterInventory(stats, false);
    }

    private void SellEquipment(Equipment equip)
    {
        DeactivatePanel(4);
        InventoryManipulator.RemoveEquipment(equip);
        SaveData.Current.mainData.playerData.money += equip.sellPrice;
        InventoryStorage(false);
    }
    private void SellItem(Character stats, Item item)
    {
        DeactivatePanel(4);
        InventoryManipulator.RemoveItem(stats, item);
        SaveData.Current.mainData.playerData.money += item.sellPrice;
        characterInventory(stats, true);
    }

    private void SellItem(Item item)
    {
        DeactivatePanel(4);
        InventoryManipulator.RemoveItem(item);
        SaveData.Current.mainData.playerData.money += item.sellPrice;
        InventoryStorage(true);
    }

    private void InventoryStorage(bool isItem)
    {
        ActivatePanel(1);
        FillCards.ClearViewport(StoreSingleton.Instance.Content[0]);
        if (isItem)
        {
            foreach (Item item in SaveData.Current.mainData.itemStorage)
            {
                GameObject card = FillCards.ItemCard(CardItemPrefab, StoreSingleton.Instance.Content[0], item, false);
                card.GetComponent<Button>().onClick.AddListener(() => SelectForSale(item));
            }
        }
        else
        {
            foreach (Equipment equip in SaveData.Current.mainData.equipmentStorage)
            {
                GameObject card = FillCards.EquipmentCard(CardEquipmentPrefab, StoreSingleton.Instance.Content[0], equip, false);
                card.GetComponent<Button>().onClick.AddListener(() => SelectForSale(equip));
                
            }
        }
    }

    private void characterInventory(Character stats, bool isItem)
    {
        ActivatePanel(1);
        List<int> items = CharacterInfoRetriever.GetListOfItems(stats);
        List<int> equip = CharacterInfoRetriever.GetListOfEquipment(stats);
        JsonRetriever jsonRetriever = new JsonRetriever();

        FillCards.ClearViewport(StoreSingleton.Instance.Content[0]);

        if (isItem)
        {
            foreach(int item in items)
            {
                if(item != -1)
                {
                    Item itemStats = jsonRetriever.Load1Item(item);
                    GameObject card = FillCards.ItemCard(CardItemPrefab, StoreSingleton.Instance.Content[0], itemStats, false);
                    card.GetComponent<Button>().onClick.AddListener(() => SelectForSale(stats, itemStats));
                }
            }
        }
        else
        {
            foreach (int item in equip)
            {
                if (item != -1)
                {
                    Equipment equipmentStats = jsonRetriever.Load1Equipment(item);
                    GameObject card = FillCards.EquipmentCard(CardEquipmentPrefab, StoreSingleton.Instance.Content[0], equipmentStats, false);
                    card.GetComponent<Button>().onClick.AddListener(() => SelectForSale(stats, equipmentStats));
                }
            }
        }
    }

    public void ButtonQuit()
    {
        ResetPanels();
    }



    /**************************************************************************************************
     * 
     * 
     *                      Misc Code
     * 
     * 
     * ************************************************************************************************/

    private void ActivatePanel(int panel)
    {
        StoreSingleton.Instance.Panels[panel].SetActive(true);
    }

    private void DeactivatePanel(int panel)
    {
        StoreSingleton.Instance.Panels[panel].SetActive(false);
    }

    private void ResetPanels()
    {
        foreach (GameObject item in StoreSingleton.Instance.Panels)
        {
            item.SetActive(false);
        }
    }
}
