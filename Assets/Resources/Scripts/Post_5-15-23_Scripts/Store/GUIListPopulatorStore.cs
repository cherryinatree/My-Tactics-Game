using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GUIListPopulatorStore : MonoBehaviour
{

    private RectTransform View;
    private GameObject CardPrefab;
    private GameObject CardEquipment;
    private GameObject CardNamePrefab;
    private GameObject CardInfoPrefab;
    public Character SelectedCharacter;
    public Item SelectedItem;
    public Equipment SelectedEquipment;

    private string inventoryToSendTo;
    public GameObject[] Content;

    private StoreOwner storeOwner;

    public GameObject[] Panels;

    private string populatedType;

    private bool isBuy = true;

    public GUIListPopulatorStore(GameObject[] content, GameObject cardPrefab, GameObject cardEquipment, StoreOwner storeOwn, GameObject[] panels, GameObject cardNamePrefab,
        GameObject cardInfo, bool isItem)
    {
        Content = content;
        CardPrefab = cardPrefab;
        CardEquipment = cardEquipment;
        storeOwner = storeOwn;
        Panels = panels;
        CardNamePrefab = cardNamePrefab;
        CardInfoPrefab = cardInfo;
        if (isItem)
        {
            populatedType = "ItemID";
        }
        else
        {
            populatedType = "EquipmentID";
        }
    }

    public void ItemOr(bool isItem)
    {

        if (isItem)
        {
            populatedType = "ItemID";
        }
        else
        {
            populatedType = "EquipmentID";
        }
    }
    public void BuyOr(bool isBuy)
    {

        if (isBuy)
        {
            populatedType = "ItemID";
        }
        else
        {
            populatedType = "EquipmentID";
        }
    }

    public void NewCharacter(Character selectedCharacter)
    {
        SelectedCharacter = selectedCharacter;
    }

    /****************************************************************************************************************
     * 
     * 
     * 
     *                              Sell Code
     * 
     * 
     * 
     * **************************************************************************************************************/


    public void SellEqupment()
    {
        // Clear the previous contents of the list
        ClearViewport(Content[1].GetComponent<RectTransform>().gameObject);

        GameObject card = GameObject.Instantiate(CardNamePrefab, Content[1].GetComponent<RectTransform>());

        List<Equipment> equip = SaveData.Current.mainData.equipmentStorage;
        card.GetComponent<Button>().onClick.AddListener(() => SelectSellNameCard(equip));
        CardNameLoader(card, "Inventory", "-1");


        // Add items to the player's inventory list
        foreach (Character stats in SaveData.Current.mainData.characters)
        {
            GameObject characterCard = GameObject.Instantiate(CardNamePrefab);
            characterCard.transform.SetParent(Content[1].GetComponent<RectTransform>(), false);
            characterCard.GetComponent<Button>().onClick.AddListener(delegate { SelectSellNameCard(stats, false); });
            CardNameLoader(characterCard, stats.characterName, stats.id.ToString());
        }

        foreach (Character stats in SaveData.Current.mainData.charactersStorage)
        {
            GameObject characterCard = GameObject.Instantiate(CardNamePrefab);
            characterCard.transform.SetParent(Content[1].GetComponent<RectTransform>(), false);
            characterCard.GetComponent<Button>().onClick.AddListener(delegate { SelectSellNameCard(stats, false); });
            CardNameLoader(characterCard, stats.characterName, stats.id.ToString());
        }
    }

    public void SellItems()
    {
        // Clear the previous contents of the list
        ClearViewport(Content[1].GetComponent<RectTransform>().gameObject);

        GameObject card = GameObject.Instantiate(CardNamePrefab, Content[1].GetComponent<RectTransform>());

        List<Item> item_list = SaveData.Current.mainData.itemStorage;
        card.GetComponent<Button>().onClick.AddListener(() => SelectSellNameCard(item_list));
        CardNameLoader(card, "Inventory", "-1");


        // Add items to the player's inventory list
        foreach (Character stats in SaveData.Current.mainData.characters)
        {
            GameObject characterCard = GameObject.Instantiate(CardNamePrefab);
            characterCard.transform.SetParent(Content[1].GetComponent<RectTransform>(), false);
            characterCard.GetComponent<Button>().onClick.AddListener(delegate { SelectSellNameCard(stats, true); });
            CardNameLoader(characterCard, stats.characterName, stats.id.ToString());
        }

        foreach (Character stats in SaveData.Current.mainData.charactersStorage)
        {
            GameObject characterCard = GameObject.Instantiate(CardNamePrefab);
            characterCard.transform.SetParent(Content[1].GetComponent<RectTransform>(), false);
            characterCard.GetComponent<Button>().onClick.AddListener(delegate { SelectSellNameCard(stats, true); });
            CardNameLoader(characterCard, stats.characterName, stats.id.ToString());
        }
    }


    private void SelectSellNameCard(Character stats, bool isItem)
    {
        SelectedCharacter = stats;
        ActivatePanel("StoreProductPanel");
        // Clear the previous contents of the list
        ClearViewport(Content[0].GetComponent<RectTransform>().gameObject);

        if (isItem)
        {
            List<Item> items = new List<Item>();
            JsonRetriever jsonRetriever = new JsonRetriever();
            if (stats.item0 != -1)
            {
                items.Add(jsonRetriever.Load1Item(stats.item0));
            }
            if (stats.item1 != -1)
            {
                items.Add(jsonRetriever.Load1Item(stats.item1));
            }
            if (stats.item2 != -1)
            {
                items.Add(jsonRetriever.Load1Item(stats.item2));
            }

            // Add items to the player's inventory list
            foreach (Item stat in items)
            {
                GameObject card = GameObject.Instantiate(CardPrefab);
                card.transform.SetParent(Content[0].GetComponent<RectTransform>().transform, false);
                if (populatedType == "ItemID")
                {
                    card.GetComponent<Button>().onClick.AddListener(delegate { SelectSellCard(stat, stats.characterID); });
                    CardLoader(card, stat);
                }
            }
        }
        else
        {
            List<Equipment> equipment = new List<Equipment>();
            JsonRetriever jsonRetriever = new JsonRetriever();
            if (stats.weapon != -1)
            {
                equipment.Add(jsonRetriever.Load1Equipment(stats.weapon));
            }
            if (stats.offHand != -1)
            {
                equipment.Add(jsonRetriever.Load1Equipment(stats.offHand));
            }
            if (stats.helm != -1)
            {
                equipment.Add(jsonRetriever.Load1Equipment(stats.helm));
            }
            if (stats.armor != -1)
            {
                equipment.Add(jsonRetriever.Load1Equipment(stats.armor));
            }
            if (stats.boots != -1)
            {
                equipment.Add(jsonRetriever.Load1Equipment(stats.boots));
            }
            if (stats.amulet != -1)
            {
                equipment.Add(jsonRetriever.Load1Equipment(stats.amulet));
            }

            // Add items to the player's inventory list
            foreach (Equipment stat in equipment)
            {
                GameObject card = GameObject.Instantiate(CardPrefab);
                card.transform.SetParent(Content[0].GetComponent<RectTransform>().transform, false);
                if (populatedType == "ItemID")
                {
                    card.GetComponent<Button>().onClick.AddListener(delegate { SelectSellCard(stat, stats.characterID); });
                    CardLoader(card, stat);
                }
            }
        }
    }
    private void SelectSellNameCard(List<Item> sell_items)
    {

        ActivatePanel("StoreProductPanel");
        // Clear the previous contents of the list
        ClearViewport(Content[0].GetComponent<RectTransform>().gameObject);


        // Add items to the player's inventory list
        foreach (Item stats in sell_items)
        {
            GameObject card = GameObject.Instantiate(CardPrefab);
            card.transform.SetParent(Content[0].GetComponent<RectTransform>().transform, false);
            if (populatedType == "ItemID")
            {
                card.GetComponent<Button>().onClick.AddListener(delegate { SelectSellCard(stats, "Inventory"); });
                CardLoader(card, stats);
            }
        }
    }
    private void SelectSellNameCard(List<Equipment> sell_equip)
    {

    }


    private void SelectSellCard(Item stats, string id)
    {
        populatedType = "ItemID";
        if (id == "Inventory")
        {
            inventoryToSendTo = "Inventory";
            SelectedItem = stats; 
            ActivatePanel("StoreConfirmPanel");
            UpdateSellConfirmString("Store");
        }
        else
        {
            inventoryToSendTo = id;
            SelectedItem = stats;
            ActivatePanel("StoreConfirmPanel");
            UpdateSellConfirmString("Store");

        }

    }

    private void SelectSellCard(Equipment stats, string id)
    {
        populatedType = "EquipmentID";
        if (id == "Inventory")
        {
            inventoryToSendTo = "Inventory";
            SelectedEquipment = stats;
            ActivatePanel("StoreConfirmPanel");
            UpdateSellConfirmString("Store");
        }
        else
        {
            inventoryToSendTo = id;
            SelectedEquipment = stats;
            ActivatePanel("StoreConfirmPanel");
            UpdateSellConfirmString("Store");

        }

    }

    private void UpdateSellConfirmString(string recipient)
    {

        for (int i = 0; i < Panels.Length; i++)
        {
            if (Panels[i].name == "StoreConfirmPanel")
            {

                Panels[i].transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Sell to" +
                    "\n" + recipient;
                Character stats1 = SaveData.Current.mainData.characters[0];
                Panels[i].transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<Button>().onClick.RemoveAllListeners();
                Panels[i].transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<Button>().onClick.AddListener(delegate { ConfirmSell(stats1); });
            }
        }
    }
    private void ConfirmSell(Character stats1)
    {
        if (populatedType == "ItemID")
        {
            if (inventoryToSendTo == "Inventory")
            {
                if (SaveData.Current.mainData.itemStorage.Contains(SelectedItem))
                {
                    SaveData.Current.mainData.itemStorage.Remove(SelectedItem);
                    SaveData.Current.mainData.playerData.money += SelectedItem.sellPrice; 
                }
            }
            else
            {
                foreach (Character stats in SaveData.Current.mainData.characters)
                {
                    if (stats.characterID == inventoryToSendTo)
                    {
                        RemoveItem(stats);
                    }
                }

                foreach (Character stats in SaveData.Current.mainData.charactersStorage)
                {
                    if (stats.characterID == inventoryToSendTo)
                    {
                        RemoveItem(stats);
                    }
                }
            }
        }
        else if (populatedType == "EquipmentID")
        {
            if (inventoryToSendTo == "Inventory")
            {
                if (SaveData.Current.mainData.equipmentStorage.Contains(SelectedEquipment))
                {
                    SaveData.Current.mainData.equipmentStorage.Remove(SelectedEquipment);
                    SaveData.Current.mainData.playerData.money += SelectedEquipment.sellPrice;
                }
            }
            else
            {
                foreach (Character stats in SaveData.Current.mainData.characters)
                {
                    if (stats.characterID == inventoryToSendTo)
                    {
                        RemoveEquipment(stats);
                    }
                }

                foreach (Character stats in SaveData.Current.mainData.charactersStorage)
                {
                    if (stats.characterID == inventoryToSendTo)
                    {
                        RemoveEquipment(stats);
                    }
                }
            }
        }
        PostPurchaseUpdate(SelectedCharacter);
    }

    private void RemoveItem(Character stats)
    {
        if (stats.item0 == SelectedItem.id)
        {
            stats.item0 = -1;
            SaveData.Current.mainData.playerData.money -= SelectedItem.buyPrice;
        }
        else if (stats.item1 == SelectedItem.id)
        {
            stats.item1 = -1;
            SaveData.Current.mainData.playerData.money -= SelectedItem.buyPrice;
        }
        else if (stats.item2 == SelectedItem.id)
        {
            stats.item2 = -1;
            SaveData.Current.mainData.playerData.money -= SelectedItem.buyPrice;
        }
    }

    private void RemoveEquipment(Character stats)
    {
        switch (SelectedEquipment.type)
        {
            case "Weapon":
                if (stats.weapon == SelectedEquipment.id)
                {
                    stats.weapon = -1;
                    SaveData.Current.mainData.playerData.money += SelectedEquipment.sellPrice;
                }
                break;
            case "OffHand":
                if (stats.offHand == SelectedEquipment.id)
                {
                    stats.offHand = -1;
                    SaveData.Current.mainData.playerData.money += SelectedEquipment.sellPrice;
                }
                break;
            case "Helm":
                if (stats.helm == SelectedEquipment.id)
                {
                    stats.helm = -1;
                    SaveData.Current.mainData.playerData.money += SelectedEquipment.sellPrice;
                }
                break;
            case "Armor":
                if (stats.armor == SelectedEquipment.id)
                {
                    stats.armor = -1;
                    SaveData.Current.mainData.playerData.money += SelectedEquipment.sellPrice;
                }
                break;
            case "Boots":
                if (stats.boots == SelectedEquipment.id)
                {
                    stats.boots = -1;
                    SaveData.Current.mainData.playerData.money += SelectedEquipment.sellPrice;
                }
                break;
            case "Amulet":
                if (stats.amulet == SelectedEquipment.id)
                {
                    stats.amulet = -1;
                    SaveData.Current.mainData.playerData.money += SelectedEquipment.sellPrice;
                }
                break;
        }
    }

    /****************************************************************************************************************
     * 
     * 
     * 
     *                              Buy Code
     * 
     * 
     * 
     * **************************************************************************************************************/

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
        ClearViewport(Content[0].GetComponent<RectTransform>().gameObject);


        // Add items to the player's inventory list
        foreach (Item stats in items)
        {
            GameObject card = GameObject.Instantiate(CardPrefab);
            card.transform.SetParent(Content[0].GetComponent<RectTransform>().transform, false);
            if (populatedType == "ItemID")
            {
                card.GetComponent<Button>().onClick.AddListener(delegate { SelectCard(stats); });
                CardLoader(card, stats);
            }
        }
    }


    public void UpdateUI(List<Equipment> equipment)
    {
        // Clear the previous contents of the list
        ClearViewport(Content[0].GetComponent<RectTransform>().gameObject);


        // Add items to the player's inventory list
        foreach (Equipment stats in equipment)
        {
            GameObject card = GameObject.Instantiate(CardEquipment);
            card.transform.SetParent(Content[0].GetComponent<RectTransform>().transform, false);
            if (populatedType == "EquipmentID")
            {
                card.GetComponent<Button>().onClick.AddListener(delegate { SelectCard(stats); });
                CardLoader(card, stats);
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
        ActivatePanel("StoreCharacterNamePanel");

        PopulateNamePanel();
        HighlightCard();
    }

    private void SelectCard(Equipment stats)
    {
        SelectedEquipment = stats;
        ActivatePanel("StoreCharacterNamePanel");

        PopulateNamePanel();
        HighlightCard();
    }

    private void SelectInventoryCard2(Character stats)
    {
        inventoryToSendTo = "Inventory";
        ActivatePanel("StoreConfirmPanel");
        UpdateConfirmString("Inventory");
        DeactivatePanel("StoreCharacterInfoPanel");
        HighlightCard();
    }
    private void SelectCharacterCard(Character stats)
    {
        inventoryToSendTo = stats.characterID;
        SelectedCharacter = stats;
        ActivatePanel("StoreConfirmPanel");
        UpdateConfirmString(stats.characterName);
        ActivatePanel("StoreCharacterInfoPanel");
        CardLoader(CardInfoPrefab, stats);
        HighlightCard();
    }


    private void PostPurchaseUpdate(Character stats)
    {
        if(SelectedCharacter != null)
        {

            CardLoader(CardInfoPrefab, stats);
        }
    }

    private void UpdateConfirmString(string recipient)
    {

        for (int i = 0; i < Panels.Length; i++)
        {
            if (Panels[i].name == "StoreConfirmPanel")
            {

                Panels[i].transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Give to:" +
                    "\n" + recipient;
                Character stats1 = SaveData.Current.mainData.characters[0];
                Panels[i].transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<Button>().onClick.RemoveAllListeners();
                Panels[i].transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<Button>().onClick.AddListener(delegate { ConfirmSale(stats1); });
            }
        }
    }

    private void ConfirmSale(Character stats1)
    {
        if (populatedType == "ItemID")
        {
            if (SelectedItem.buyPrice <= SaveData.Current.mainData.playerData.money)
            {
                MakeSale();
            }
        }
        else if (populatedType == "EquipmentID")
        {

            if (SelectedEquipment.buyPrice <= SaveData.Current.mainData.playerData.money)
            {
                MakeSale();
            }
        }
        PostPurchaseUpdate(SelectedCharacter);
    }

    private void MakeSale()
    {
        if (inventoryToSendTo == "Inventory")
        {
            if (populatedType == "ItemID")
            {
                SaveData.Current.mainData.itemStorage.Add(SelectedItem);
                SaveData.Current.mainData.playerData.money -= SelectedItem.buyPrice;
            }
            else if (populatedType == "EquipmentID")
            {

                SaveData.Current.mainData.equipmentStorage.Add(SelectedEquipment);
                SaveData.Current.mainData.playerData.money -= SelectedEquipment.buyPrice;
            }
        }
        else
        {

            if (populatedType == "ItemID")
            {
                foreach (Character stats in SaveData.Current.mainData.characters)
                {
                    if (stats.characterID == inventoryToSendTo)
                    {

                        AddItem(stats);
                    }
                }

                foreach (Character stats in SaveData.Current.mainData.charactersStorage)
                {
                    if (stats.characterID == inventoryToSendTo)
                    {

                        AddItem(stats);
                    }
                }
            }
            else if (populatedType == "EquipmentID")
            {
                foreach (Character stats in SaveData.Current.mainData.characters)
                {
                    if (stats.characterID == inventoryToSendTo)
                    {
                        AddEquipment(stats);
                    }
                }

                foreach (Character stats in SaveData.Current.mainData.charactersStorage)
                {
                    if (stats.characterID == inventoryToSendTo)
                    {
                        AddEquipment(stats);
                    }
                }
            }
        }
    }

    private void AddItem(Character stats)
    {
        if (stats.item0 == -1)
        {
            stats.item0 = SelectedItem.id;
            SaveData.Current.mainData.playerData.money -= SelectedItem.buyPrice;
        }
        else if (stats.item1 == -1)
        {
            stats.item1 = SelectedItem.id;
            SaveData.Current.mainData.playerData.money -= SelectedItem.buyPrice;
        }
        else if (stats.item2 == -1)
        {
            stats.item2 = SelectedItem.id;
            SaveData.Current.mainData.playerData.money -= SelectedItem.buyPrice;
        }
    }

    private void AddEquipment(Character stats)
    {

        switch (SelectedEquipment.type)
        {
            case "Weapon":
                if (stats.weapon == -1)
                {
                    stats.weapon = SelectedEquipment.id; 
                    SaveData.Current.mainData.playerData.money -= SelectedEquipment.buyPrice;
                }
                break;
            case "OffHand":
                if (stats.offHand == -1)
                {
                    stats.offHand = SelectedEquipment.id;
                    SaveData.Current.mainData.playerData.money -= SelectedEquipment.buyPrice;
                }
                break;
            case "Helm":
                if (stats.helm == -1)
                {
                    stats.helm = SelectedEquipment.id;
                    SaveData.Current.mainData.playerData.money -= SelectedEquipment.buyPrice;
                }
                break;
            case "Armor":
                if (stats.armor == -1)
                {
                    stats.armor = SelectedEquipment.id;
                    SaveData.Current.mainData.playerData.money -= SelectedEquipment.buyPrice;
                }
                break;
            case "Boots":
                if (stats.boots == -1)
                {
                    stats.boots = SelectedEquipment.id;
                    SaveData.Current.mainData.playerData.money -= SelectedEquipment.buyPrice;
                }
                break;
            case "Amulet":
                if (stats.amulet == -1)
                {
                    stats.amulet = SelectedEquipment.id;
                    SaveData.Current.mainData.playerData.money -= SelectedEquipment.buyPrice;
                }
                break;
        }
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



    private void PopulateNamePanel()
    {
        // Clear the previous contents of the list
        ClearViewport(Content[1].GetComponent<RectTransform>().gameObject);

        GameObject card = GameObject.Instantiate(CardNamePrefab, Content[1].GetComponent<RectTransform>());

        Character stats1 = SaveData.Current.mainData.characters[0];
        card.GetComponent<Button>().onClick.AddListener(() => SelectInventoryCard2(stats1));
        CardNameLoader(card, "Inventory", "-1");


        // Add items to the player's inventory list
        foreach (Character stats in SaveData.Current.mainData.characters)
        {
            GameObject characterCard = GameObject.Instantiate(CardNamePrefab);
            characterCard.transform.SetParent(Content[1].GetComponent<RectTransform>(), false);
            characterCard.GetComponent<Button>().onClick.AddListener(delegate { SelectCharacterCard(stats); });
            CardNameLoader(characterCard, stats.characterName, stats.id.ToString());
        }

        foreach (Character stats in SaveData.Current.mainData.charactersStorage)
        {
            GameObject characterCard = GameObject.Instantiate(CardNamePrefab);
            characterCard.transform.SetParent(Content[1].GetComponent<RectTransform>(), false);
            characterCard.GetComponent<Button>().onClick.AddListener(delegate { SelectCharacterCard(stats); });
            CardNameLoader(characterCard, stats.characterName, stats.id.ToString());
        }
    }

    public void HighlightCard()
    {
        Transform scroll = View;
        if (SelectedCharacter != null)
        {

            string id = "";
            if (populatedType == "CharacterID")
            {
                id = SelectedCharacter.characterID;
            }
            else if (populatedType == "ItemID")
            {
                scroll = Content[0].GetComponent<RectTransform>();
                id = SelectedItem.id.ToString();
            }
            else if (populatedType == "EquipmentID")
            {
                scroll = Content[0].GetComponent<RectTransform>();
                id = SelectedEquipment.id.ToString();
            }

            for (int i = 0; i < scroll.childCount; i++)
            {
                string text = scroll.GetChild(i).Find(populatedType).GetComponent<TextMeshProUGUI>().text;


                if (text == id)
                {
                    //scroll.GetComponent<Image>().color = new Color32(253, 244, 151, 255);
                }
                else
                {
                    //scroll.GetComponent<Image>().color = new Color32(151, 156, 253, 255);
                }
            }
        }
    }

    private void CardLoader(GameObject card, Character stats)
    {
        JsonRetriever jsonRetriever = new JsonRetriever();

        string weapon = "Empty";
        string offHand = "Empty";
        string helm = "Empty";
        string armor = "Empty";
        string boots = "Empty";
        string amulet = "Empty";
        string item0 = "Empty";
        string item1 = "Empty";
        string item2 = "Empty";

        if (stats.weapon != -1) weapon = jsonRetriever.Load1Equipment(stats.weapon).equipmentName;
        if (stats.offHand != -1) offHand = jsonRetriever.Load1Equipment(stats.offHand).equipmentName;
        if (stats.helm != -1) helm = jsonRetriever.Load1Equipment(stats.helm).equipmentName;
        if (stats.armor != -1) armor = jsonRetriever.Load1Equipment(stats.armor).equipmentName;
        if (stats.boots != -1) boots = jsonRetriever.Load1Equipment(stats.boots).equipmentName;
        if (stats.amulet != -1) amulet = jsonRetriever.Load1Equipment(stats.amulet).equipmentName;
        if (stats.item0 != -1) item0 = jsonRetriever.Load1Item(stats.item0).itemName;
        if (stats.item1 != -1) item1 = jsonRetriever.Load1Item(stats.item1).itemName;
        if (stats.item2 != -1) item2 = jsonRetriever.Load1Item(stats.item2).itemName;



        card.transform.Find("ImageIcon").GetComponent<Image>().sprite = Resources.Load<Sprite>(stats.icon);
        card.transform.Find("TextEquipment").GetComponent<TextMeshProUGUI>().text = "Weapon: " + weapon + "\nOffHand: " + offHand + 
            "\nHelm: " + helm + "\nArmor: " + armor + "\nBoots: " + boots + "\nAmulet: " + amulet;
        card.transform.Find("TextItems").GetComponent<TextMeshProUGUI>().text = "Items:\n " + item0 + "\t" + item1 + "\t" + item2;
        card.transform.GetChild(0).Find("TextStats").GetComponent<TextMeshProUGUI>().text = "\n HP: " + stats.maxHealth + " \tMP: " + stats.maxMana +
            "\tAtk: " + stats.attack + "\n\n" + " Def: " + stats.defense + " \tSpd: " + stats.speed + " \tInt: " + stats.intelligence;
        card.transform.Find("CharacterID").GetComponent<TextMeshProUGUI>().text = stats.characterID;
    }
    private void CardLoader(GameObject card, Item stats)
    {
        card.transform.Find("ImageItem").GetComponent<Image>().sprite = Resources.Load<Sprite>(stats.icon);
        card.transform.Find("TextName").GetComponent<TextMeshProUGUI>().text = stats.itemName;
        card.transform.Find("TextPrice").GetComponent<TextMeshProUGUI>().text = stats.buyPrice.ToString();
        card.transform.Find("ItemID").GetComponent<TextMeshProUGUI>().text = stats.id.ToString();
    }
    private void CardLoader(GameObject card, Equipment stats)
    {
        card.transform.Find("ImageItem").GetComponent<Image>().sprite = Resources.Load<Sprite>(stats.icon);
        card.transform.Find("TextName").GetComponent<TextMeshProUGUI>().text = stats.equipmentName;
        card.transform.Find("TextPrice").GetComponent<TextMeshProUGUI>().text = stats.buyPrice.ToString();
        card.transform.Find("EquipmentID").GetComponent<TextMeshProUGUI>().text = stats.id.ToString();
    }
    private void CardNameLoader(GameObject card, string name, string id)
    {
        card.transform.Find("TextName").GetComponent<TextMeshProUGUI>().text = name;
        card.transform.Find("CharacterID").GetComponent<TextMeshProUGUI>().text = id;
    }
    private void ClearViewport(GameObject Panel)
    {
        foreach (Transform child in Panel.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
}
