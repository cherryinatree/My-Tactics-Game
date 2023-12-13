using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public static class FillCards
{

    public static GameObject FillAttackDefendCard_NoHealthBar_FillInText(GameObject prefab, GameObject parent, GameObject character, string text)
    {
        GameObject content = prefab.transform.GetChild(0).GetChild(0).gameObject;
        Character stats = character.GetComponent<CombatCharacter>().myStats;
        Slider slider = content.transform.Find("Slider").GetComponent<Slider>();
        prefab.transform.parent = parent.transform;


        content.transform.Find("Name").GetComponent<Text>().text = stats.characterName;
        content.transform.Find("Level").GetComponent<Text>().text = text;
        content.transform.Find("Icon").GetComponent<Image>().sprite = Resources.Load<Sprite>(stats.icon);
        GameObject.Destroy(slider.gameObject);
        return content;
    }

    public static GameObject FillAttackDefendCard_Hit(GameObject prefab, GameObject parent, GameObject character)
    {
        GameObject content = prefab.transform.GetChild(0).GetChild(0).gameObject;
        Character stats = character.GetComponent<CombatCharacter>().myStats;
        Slider slider = content.transform.Find("Slider").GetComponent<Slider>();
        prefab.transform.parent = parent.transform;


        content.transform.Find("Name").GetComponent<Text>().text = stats.characterName;
        if(stats.currentHealth <= 0)
        {

            content.transform.Find("Level").GetComponent<Text>().text = "Defeated!!!";
        }
        else
        {
            content.transform.Find("Level").GetComponent<Text>().text = "Hit!!!";
        }
        content.transform.Find("Icon").GetComponent<Image>().sprite = Resources.Load<Sprite>(stats.icon);
        slider.maxValue = stats.maxHealth;
        slider.minValue = 0;
        slider.value = stats.currentHealth;
        return content;
    }

    public static GameObject FillAttackDefendCard(GameObject prefab, GameObject parent, GameObject character)
    {
        GameObject content = prefab.transform.GetChild(0).GetChild(0).gameObject;
        Character stats = character.GetComponent<CombatCharacter>().myStats;
        Slider slider = content.transform.Find("Slider").GetComponent<Slider>();
        prefab.transform.parent = parent.transform;


        content.transform.Find("Name").GetComponent<Text>().text = stats.characterName;
        content.transform.Find("Level").GetComponent<Text>().text = "Level: " + stats.level.ToString();
        content.transform.Find("Icon").GetComponent<Image>().sprite = Resources.Load<Sprite>(stats.icon);
        slider.maxValue = stats.maxHealth;
        slider.minValue = 0;
        slider.value = stats.currentHealth;
        return content;
    }

    public static GameObject CombatActionCard(GameObject prefab, GameObject parent, string[] cardInfo)
    {

        GameObject card = GameObject.Instantiate(prefab);
        card.transform.SetParent(parent.GetComponent<RectTransform>().transform, false);
        card.transform.Find("Icon").GetComponent<Image>().sprite = Resources.Load<Sprite>(cardInfo[0]);
        card.transform.Find("Name").GetComponent<TextMeshProUGUI>().text = cardInfo[1];

        if (cardInfo[2] != "0")
        {
            card.transform.Find("MPCost").GetComponent<TextMeshProUGUI>().text = "MP\n" + cardInfo[2];
        }
        else
        {
            card.transform.Find("MPCost").GetComponent<TextMeshProUGUI>().text = "";
        }

        card.transform.Find("ID").GetComponent<TextMeshProUGUI>().text = cardInfo[3];
        card.transform.Find("ActionType").GetComponent<TextMeshProUGUI>().text = cardInfo[4];

        return card;
    }

    public static GameObject NameCard(GameObject prefab, GameObject parent, Character stats)
    {

        GameObject card = GameObject.Instantiate(prefab);
        card.transform.SetParent(parent.GetComponent<RectTransform>().transform, false);
        card.transform.Find("TextName").GetComponent<TextMeshProUGUI>().text = stats.characterName;
        card.transform.Find("CharacterID").GetComponent<TextMeshProUGUI>().text = stats.characterID;

        return card;
    }
    public static GameObject NameCardInventory(GameObject prefab, GameObject parent)
    {

        GameObject card = GameObject.Instantiate(prefab);
        card.transform.SetParent(parent.GetComponent<RectTransform>().transform, false);
        card.transform.Find("TextName").GetComponent<TextMeshProUGUI>().text = "Inventory";
        card.transform.Find("CharacterID").GetComponent<TextMeshProUGUI>().text = "Inventory";

        return card;
    }

    public static GameObject AbilityCard(GameObject prefab, GameObject parent, Abilities ability, bool isOwned)
    {

        GameObject card = GameObject.Instantiate(prefab);
        card.transform.SetParent(parent.GetComponent<RectTransform>().transform, false);
        card.transform.Find("ImageIcon").GetComponent<Image>().sprite = Resources.Load<Sprite>(ability.icon);
        card.transform.Find("TextName").GetComponent<TextMeshProUGUI>().text = ability.abilityName;
        card.transform.Find("TextLevel").GetComponent<TextMeshProUGUI>().text = "Lvl: " +ability.requiredLevel.ToString();
        if (isOwned)
        {
            card.transform.Find("TextCost").GetComponent<TextMeshProUGUI>().text = "Owned";
        }
        else
        {
            card.transform.Find("TextCost").GetComponent<TextMeshProUGUI>().text = ability.buyPrice.ToString();
        }
        card.transform.Find("TextID").GetComponent<TextMeshProUGUI>().text = ability.id.ToString();

        return card;
    }

    public static GameObject ItemCard(GameObject prefab, GameObject parent, Item item, bool isBuy)
    {
        GameObject card = GameObject.Instantiate(prefab);
        card.transform.SetParent(parent.GetComponent<RectTransform>().transform, false);

        card.transform.Find("ImageItem").GetComponent<Image>().sprite = Resources.Load<Sprite>(item.icon);
        card.transform.Find("TextName").GetComponent<TextMeshProUGUI>().text = item.itemName;
        if (isBuy)
        {
            card.transform.Find("TextPrice").GetComponent<TextMeshProUGUI>().text = item.buyPrice.ToString();
        }
        else
        {
            card.transform.Find("TextPrice").GetComponent<TextMeshProUGUI>().text = item.sellPrice.ToString();
        }
        card.transform.Find("ItemID").GetComponent<TextMeshProUGUI>().text = item.id.ToString();

        return card;
    }


    public static GameObject EquipmentCard(GameObject prefab, GameObject parent, Equipment equipment, bool isBuy)
    {
        GameObject card = GameObject.Instantiate(prefab);
        card.transform.SetParent(parent.GetComponent<RectTransform>().transform, false);

        card.transform.Find("ImageItem").GetComponent<Image>().sprite = Resources.Load<Sprite>(equipment.icon);
        card.transform.Find("TextName").GetComponent<TextMeshProUGUI>().text = equipment.equipmentName;
        if (isBuy)
        {
            card.transform.Find("TextPrice").GetComponent<TextMeshProUGUI>().text = equipment.buyPrice.ToString();
        }
        else
        {
            card.transform.Find("TextPrice").GetComponent<TextMeshProUGUI>().text = equipment.sellPrice.ToString();
        }
        card.transform.Find("EquipmentID").GetComponent<TextMeshProUGUI>().text = equipment.id.ToString();

        return card;
    }


    public static GameObject CharacterCard(GameObject prefab, GameObject parent, Character stats)
    {
        GameObject card = GameObject.Instantiate(prefab);
        card.transform.SetParent(parent.GetComponent<RectTransform>().transform, false);

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

        return card;
    }

    public static void ClearViewport(GameObject Panel)
    {
        foreach (Transform child in Panel.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
}
