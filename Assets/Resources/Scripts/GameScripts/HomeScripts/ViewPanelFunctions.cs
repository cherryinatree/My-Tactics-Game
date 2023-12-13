using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GamingTools;
using UnityEngine.Events;

public class ViewPanelFunctions 
{

    public GameObject focusOnCard;


    public void CardFocus(GameObject newFocus)
    {
        focusOnCard = newFocus;
    }

    public void PanelChange(GameObject Panel)
    {
        if (Panel.name == "HealingPanel")
        {
            AddImages(HomeSingleton.Instance.FocusOnCard.items, "Items/Healing",
                Panel.transform.Find("NameCard").transform.Find("IconsPanel").transform.Find("Panel1").transform.Find("Panel2").gameObject, true, "CharacterInventory");
            AddImages(HomeSingleton.Instance.save.game.items, "Items/Healing",
                Panel.transform.Find("IconsPanel").transform.Find("Panel1").transform.Find("Panel2").gameObject, false, "ActiveInventory");
            NameCards(Panel);

        }
        if (Panel.name == "BattlePanel")
        {
            AddImages(HomeSingleton.Instance.FocusOnCard.items, "Items/Battle",
                Panel.transform.Find("NameCard").transform.Find("IconsPanel").transform.Find("Panel1").transform.Find("Panel2").gameObject, true, "CharacterInventory");
            AddImages(HomeSingleton.Instance.save.game.items, "Items/Battle",
                Panel.transform.Find("IconsPanel").transform.Find("Panel1").transform.Find("Panel2").gameObject, false, "ActiveInventory");
            NameCards(Panel);
        }
        if (Panel.name == "SpecialPanel")
        {
            AddImages(HomeSingleton.Instance.FocusOnCard.items, "Items/Special",
                Panel.transform.Find("NameCard").transform.Find("IconsPanel").transform.Find("Panel1").transform.Find("Panel2").gameObject, true, "CharacterInventory");
            AddImages(HomeSingleton.Instance.save.game.items, "Items/Special",
                Panel.transform.Find("IconsPanel").transform.Find("Panel1").transform.Find("Panel2").gameObject, false, "ActiveInventory");
            NameCards(Panel);
        }
        if (Panel.name == "WeaponsPanel")
        {
            AddEquipmentImages(HomeSingleton.Instance.FocusOnCard.equipment, Panel.transform.Find("NameCard").transform.Find("MyEquipmentPanel").gameObject);
            AddImages(HomeSingleton.Instance.save.game.items, "Items/Weapons",
                Panel.transform.Find("IconsPanel").transform.Find("Panel1").transform.Find("Panel2").gameObject, false, "ActiveInventory");
            NameCards(Panel);
        }
        if (Panel.name == "ArmorPanel")
        {
            AddEquipmentImages(HomeSingleton.Instance.FocusOnCard.equipment, Panel.transform.Find("NameCard").transform.Find("MyEquipmentPanel").gameObject);
            AddImages(HomeSingleton.Instance.save.game.items, "Items/Armor",
                Panel.transform.Find("IconsPanel").transform.Find("Panel1").transform.Find("Panel2").gameObject, false, "ActiveInventory");
            NameCards(Panel);
        }
        if (Panel.name == "MiscPanel")
        {
            AddEquipmentImages(HomeSingleton.Instance.FocusOnCard.equipment, Panel.transform.Find("NameCard").transform.Find("MyEquipmentPanel").gameObject);
            AddImages(HomeSingleton.Instance.save.game.items, "Items/Misc",
                Panel.transform.Find("IconsPanel").transform.Find("Panel1").transform.Find("Panel2").gameObject, false, "ActiveInventory");
            NameCards(Panel);
        }
        if (Panel.name == "CharacterSwapPanel" || Panel.name == "SwapPanel")
        {
            if (Panel.name == "CharacterSwapPanel")
            {
                AddImages(HomeSingleton.Instance.save.game.equipedBuffs, "Items/BattleBuffs",
                    Panel.transform.Find("SwapPanel").transform.Find("IconsPanel1").transform.Find("Panel1").transform.Find("Panel2").gameObject, false, "BuffInventory");
                AddImages(HomeSingleton.Instance.save.game.items, "Items/BattleBuffs",
                    Panel.transform.Find("SwapPanel").transform.Find("IconsPanel").transform.Find("Panel1").transform.Find("Panel2").gameObject, false, "ActiveInventory");
            }
            else
            {
                AddImages(HomeSingleton.Instance.save.game.equipedBuffs, "Items/BattleBuffs",
                Panel.transform.Find("IconsPanel1").transform.Find("Panel1").transform.Find("Panel2").gameObject, false, "BuffInventory");
                AddImages(HomeSingleton.Instance.save.game.items, "Items/BattleBuffs",
                    Panel.transform.Find("IconsPanel").transform.Find("Panel1").transform.Find("Panel2").gameObject, false, "ActiveInventory");

            }
            HomeSingleton.Instance.controlPanel.CardListBattleRoster(GameObject.Find("BattleScroll").transform.Find("Viewport").gameObject);
            
        }

        if (Panel.name == "StatsPanel")
        {
            NameCards(Panel);
            AddStats();
        }
        if (Panel.name == "AbilitiesPanel")
        {
            NameCards(Panel);
        }
        if (Panel.name == "SpellsPanel")
        {
            NameCards(Panel);
        }
    }

    private void NameCards(GameObject Panel)
    {
        Panel.transform.Find("NameCard").transform.Find("NameText").GetComponent<Text>().text = HomeSingleton.Instance.FocusOnCard.name;
        if (Panel.transform.Find("NameCard").transform.Find("CLText"))
        {
            Panel.transform.Find("NameCard").transform.Find("CLText").GetComponent<Text>().text = "Class: " +
                HomeSingleton.Instance.FocusOnCard.CharacterClass + " | Level: " + HomeSingleton.Instance.FocusOnCard.level;
        }
        if (Panel.transform.Find("NameCard").transform.Find("StatsText"))
        {
            Panel.transform.Find("NameCard").transform.Find("StatsText").GetComponent<Text>().text = "Stat Points: " +
                HomeSingleton.Instance.FocusOnCard.statPoints;
        }
        if (Panel.transform.Find("NameCard").transform.Find("AbilitiesText"))
        {
            Panel.transform.Find("NameCard").transform.Find("AbilitiesText").GetComponent<Text>().text = "Ability Points: " +
                HomeSingleton.Instance.FocusOnCard.AbilityPoints;
        }
        if (Panel.transform.Find("NameCard").transform.Find("SpellsText"))
        {
            Panel.transform.Find("NameCard").transform.Find("SpellsText").GetComponent<Text>().text = "Spell Points: " +
                HomeSingleton.Instance.FocusOnCard.SpellPoints;
        }
    } 

    private void AddImages(List<string> imageNames, string location, GameObject iconsPanel, bool isSearchAll, string tag)
    {
        int place = 0;
        Transform parentPanel = iconsPanel.transform;

        List<string> relevantImages = new List<string>();

        if (!isSearchAll)
        {
            foreach (string a in imageNames)
            {
                if (ResourseLoader.GetSprite("Images/" + location + "/" + a))
                {
                    relevantImages.Add(a);
                }
            }
        }

        int stringCount = 0;
        if (isSearchAll)
        {
            stringCount = imageNames.Count;
        }
        else
        {
            stringCount = relevantImages.Count;
        }

        for (int i = 0; i < parentPanel.childCount; i++)
        {
            Transform item1 = parentPanel.GetChild(place);
            Transform item2 = parentPanel.GetChild(i);

            if (i < stringCount)
            {
                if (ResourseLoader.SearchForSprite(imageNames[i]) != null)
                {
                    if (!isSearchAll)
                    {
                        if (ResourseLoader.GetSprite("Images/" + location + "/" + relevantImages[i]) != null)
                        {
                            place++;
                            item2.transform.Find("Panel").transform.Find("Panel").transform.Find("Panel").GetComponent<Image>().sprite = ResourseLoader.GetSprite("Images/Actions/Default");
                            item1.transform.Find("Panel").transform.Find("Panel").transform.Find("Panel").GetComponent<Image>().sprite = ResourseLoader.GetSprite("Images/" + location + "/" + relevantImages[i]);
                            item1.transform.Find("Panel").transform.Find("Panel").transform.Find("Panel").tag = "ItemImage";
                            item2.transform.Find("Panel").transform.Find("Panel").tag = tag;
                        }
                        else
                        {
                            item2.transform.Find("Panel").transform.Find("Panel").transform.Find("Panel").GetComponent<Image>().sprite = ResourseLoader.GetSprite("Images/Actions/Default");
                            item2.transform.Find("Panel").transform.Find("Panel").transform.Find("Panel").tag = tag;
                            item2.transform.Find("Panel").transform.Find("Panel").tag = tag;
                        }
                    }
                    else
                    {
                        place++;
                        item2.transform.Find("Panel").transform.Find("Panel").transform.Find("Panel").GetComponent<Image>().sprite = ResourseLoader.GetSprite("Images/Actions/Default");
                        item1.transform.Find("Panel").transform.Find("Panel").transform.Find("Panel").GetComponent<Image>().sprite = ResourseLoader.SearchForSprite(imageNames[i]);
                        item1.transform.Find("Panel").transform.Find("Panel").transform.Find("Panel").tag = "ItemImage";
                        item2.transform.Find("Panel").transform.Find("Panel").tag = tag;
                    }
                }
                else
                {
                    item2.transform.Find("Panel").transform.Find("Panel").transform.Find("Panel").GetComponent<Image>().sprite = ResourseLoader.GetSprite("Images/Actions/Default");
                    item2.transform.Find("Panel").transform.Find("Panel").transform.Find("Panel").tag = tag;
                    item2.transform.Find("Panel").transform.Find("Panel").tag = tag;
                }
            }
            else
            {
                item2.transform.Find("Panel").transform.Find("Panel").transform.Find("Panel").GetComponent<Image>().sprite = ResourseLoader.GetSprite("Images/Actions/Default");
                item2.transform.Find("Panel").transform.Find("Panel").transform.Find("Panel").tag = tag;
                item2.transform.Find("Panel").transform.Find("Panel").tag = tag;
            }

        }


    }


    private void AddEquipmentImages(List<string> imageNames, GameObject Panel)
    {
        string weapon = null;
        string offHand = null;
        string helm = null;
        string armor = null;
        string boots = null;
        string amulet = null;

        foreach (string item in imageNames)
        {
            if (ResourseLoader.GetSprite("Images/Items/Weapons/" + item)) weapon = item;
            if (ResourseLoader.GetSprite("Images/Items/Armor/" + item))
            {
                for (int i = 0; i < Constants.EquipmentList.Armor.GetLength(0); i++)
                {
                    if (Constants.EquipmentList.Armor[i, 0] == item)
                    {
                        if (Constants.EquipmentList.Armor[i, 1] == "OffHand")
                        {
                            offHand = item;
                        }
                        else if (Constants.EquipmentList.Armor[i, 1] == "Helm")
                        {
                            helm = item;
                        }
                        else if (Constants.EquipmentList.Armor[i, 1] == "Armor")
                        {
                            armor = item;
                        }
                        else if (Constants.EquipmentList.Armor[i, 1] == "Boots")
                        {
                            boots = item;
                        }
                    }
                }
            }
            if (ResourseLoader.GetSprite("Images/Items/Misc/" + item))
            {
                for (int i = 0; i < Constants.EquipmentList.Misc.GetLength(0); i++)
                {
                    if (Constants.EquipmentList.Misc[i, 0] == item)
                    {
                        if (Constants.EquipmentList.Misc[i, 1] == "OffHand")
                        {
                            offHand = item;
                        }
                        else if (Constants.EquipmentList.Misc[i, 1] == "Amulet")
                        {
                            amulet = item;
                        }
                    }
                }
            }
        }
        if (weapon != null)
        {
            Panel.transform.Find("EquipWeaponPanel").transform.Find("Image").GetComponent<Image>().sprite = ResourseLoader.SearchForSprite(weapon);
            Panel.transform.Find("EquipWeaponPanel").transform.Find("Image").tag = "ItemImage";
            Panel.transform.Find("EquipWeaponPanel").tag = "CharacterEquipment";
        }
        else
        {
            Panel.transform.Find("EquipWeaponPanel").transform.Find("Image").GetComponent<Image>().sprite =
                ResourseLoader.GetSprite("Images/Icons/64 flat icons/png/128px/" + Constants.ImageList.equipment[0]);
            Panel.transform.Find("EquipWeaponPanel").transform.Find("Image").tag = "CharacterEquipment";
            Panel.transform.Find("EquipWeaponPanel").tag = "CharacterEquipment";
        }
        if (offHand != null)
        {
            Panel.transform.Find("EquipOffHandPanel").transform.Find("Image").GetComponent<Image>().sprite = ResourseLoader.SearchForSprite(offHand);
            Panel.transform.Find("EquipOffHandPanel").transform.Find("Image").tag = "ItemImage";
            Panel.transform.Find("EquipOffHandPanel").tag = "CharacterEquipment";
        }
        else
        {
            Panel.transform.Find("EquipOffHandPanel").transform.Find("Image").GetComponent<Image>().sprite = 
                ResourseLoader.GetSprite("Images/Icons/64 flat icons/png/128px/" + Constants.ImageList.equipment[1]);
            Panel.transform.Find("EquipOffHandPanel").transform.Find("Image").tag = "CharacterEquipment";
            Panel.transform.Find("EquipOffHandPanel").tag = "CharacterEquipment";
        }
        if (helm != null)
        {
            Panel.transform.Find("EquipHelmPanel").transform.Find("Image").GetComponent<Image>().sprite = ResourseLoader.SearchForSprite(helm);
            Panel.transform.Find("EquipHelmPanel").transform.Find("Image").tag = "ItemImage";
            Panel.transform.Find("EquipHelmPanel").tag = "CharacterEquipment";
        }
        else
        {
            Panel.transform.Find("EquipHelmPanel").transform.Find("Image").GetComponent<Image>().sprite =
                ResourseLoader.GetSprite("Images/Icons/64 flat icons/png/128px/" + Constants.ImageList.equipment[3]);
            Panel.transform.Find("EquipHelmPanel").transform.Find("Image").tag = "CharacterEquipment";
            Panel.transform.Find("EquipHelmPanel").tag = "CharacterEquipment";
        }
        if (armor != null)
        {
            Panel.transform.Find("EquipArmorPanel").transform.Find("Image").GetComponent<Image>().sprite = ResourseLoader.SearchForSprite(armor);
            Panel.transform.Find("EquipArmorPanel").transform.Find("Image").tag = "ItemImage";
            Panel.transform.Find("EquipArmorPanel").tag = "CharacterEquipment";
        }
        else
        {
            Panel.transform.Find("EquipArmorPanel").transform.Find("Image").GetComponent<Image>().sprite =
                ResourseLoader.GetSprite("Images/Icons/64 flat icons/png/128px/" + Constants.ImageList.equipment[2]);
            Panel.transform.Find("EquipArmorPanel").transform.Find("Image").tag = "CharacterEquipment";
            Panel.transform.Find("EquipArmorPanel").tag = "CharacterEquipment";
        }
        if (boots != null)
        {
            Panel.transform.Find("EquipBootsPanel").transform.Find("Image").GetComponent<Image>().sprite = ResourseLoader.SearchForSprite(boots);
            Panel.transform.Find("EquipBootsPanel").transform.Find("Image").tag = "ItemImage";
            Panel.transform.Find("EquipBootsPanel").tag = "CharacterEquipment";
        }
        else
        {
            Panel.transform.Find("EquipBootsPanel").transform.Find("Image").GetComponent<Image>().sprite =
                ResourseLoader.GetSprite("Images/Icons/64 flat icons/png/128px/" + Constants.ImageList.equipment[4]);
            Panel.transform.Find("EquipBootsPanel").transform.Find("Image").tag = "CharacterEquipment";
            Panel.transform.Find("EquipBootsPanel").tag = "CharacterEquipment";
        }
        if (amulet != null)
        {
            Panel.transform.Find("EquipAmuletPanel").transform.Find("Image").GetComponent<Image>().sprite = ResourseLoader.SearchForSprite(amulet);
            Panel.transform.Find("EquipAmuletPanel").transform.Find("Image").tag = "ItemImage";
            Panel.transform.Find("EquipAmuletPanel").tag = "CharacterEquipment";
        }
        else
        {
            Panel.transform.Find("EquipAmuletPanel").transform.Find("Image").GetComponent<Image>().sprite =
                ResourseLoader.GetSprite("Images/Icons/64 flat icons/png/128px/" + Constants.ImageList.equipment[5]);
            Panel.transform.Find("EquipAmuletPanel").transform.Find("Image").tag = "CharacterEquipment";
            Panel.transform.Find("EquipAmuletPanel").tag = "CharacterEquipment";
        }

    }


        public void HighlightCard()
    {
        Transform scroll = GameObject.Find("Scroll").transform;
       
        for (int i = 0; i < scroll.GetChild(0).childCount; i++)
        {
            string text = scroll.GetChild(0).GetChild(i).Find("Text").GetComponent<Text>().text.Substring(0, HomeSingleton.Instance.FocusOnCard.name.Length);
           
            if (text == HomeSingleton.Instance.FocusOnCard.name)
            {
                scroll.GetChild(0).GetChild(i).GetComponent<Image>().color = new Color32(253, 244, 151, 255);
            }
            else
            {
                scroll.GetChild(0).GetChild(i).GetComponent<Image>().color = new Color32(151, 156, 253, 255);
            }
        }
    }
    public void HighlightBattleCard()
    {
        Transform scroll = GameObject.Find("BattleScroll").transform;
        if (HomeSingleton.Instance.BattleCard != null)
        {
            for (int i = 0; i < scroll.GetChild(0).childCount; i++)
            {
                string text = scroll.GetChild(0).GetChild(i).Find("Text").GetComponent<Text>().text.Substring(0, HomeSingleton.Instance.BattleCard.name.Length);

                if (text == HomeSingleton.Instance.BattleCard.name)
                {
                    scroll.GetChild(0).GetChild(i).GetComponent<Image>().color = new Color32(253, 244, 151, 255);
                }
                else
                {
                    scroll.GetChild(0).GetChild(i).GetComponent<Image>().color = new Color32(151, 156, 253, 255);
                }
            }
        }
    }
    private void AddStats()
    {
        GameObject statHolder = GameObject.Find("StatHolder");
        int i = 0;

        statHolder.transform.GetChild(i).Find("Image").Find("Text").GetComponent<Text>().text = 
            " Vitality: " + HomeSingleton.Instance.FocusOnCard.maxHealth;
        StatButton(statHolder.transform.GetChild(i), "Health", 2);

        i++;
        statHolder.transform.GetChild(i).Find("Image").Find("Text").GetComponent<Text>().text =
            " Strength: " + HomeSingleton.Instance.FocusOnCard.attackPower;
        StatButton(statHolder.transform.GetChild(i), "attackPower", 1);
        i++;
        statHolder.transform.GetChild(i).Find("Image").Find("Text").GetComponent<Text>().text =
            " Dexterity: " + HomeSingleton.Instance.FocusOnCard.dexterity;
        StatButton(statHolder.transform.GetChild(i), "dex", 1);
        i++;
        statHolder.transform.GetChild(i).Find("Image").Find("Text").GetComponent<Text>().text =
            " Intellect: " + HomeSingleton.Instance.FocusOnCard.magicPower;
        StatButton(statHolder.transform.GetChild(i), "magic", 1);
        i++;
        statHolder.transform.GetChild(i).Find("Image").Find("Text").GetComponent<Text>().text =
            " Agility: " + HomeSingleton.Instance.FocusOnCard.speed;
        StatButton(statHolder.transform.GetChild(i), "speed", 1);
        i++;
        statHolder.transform.GetChild(i).Find("Image").Find("Text").GetComponent<Text>().text =
            " Dodge: " + HomeSingleton.Instance.FocusOnCard.dodge;
        StatButton(statHolder.transform.GetChild(i), "dodge", 1);
        i++;
    }

    private void StatButton(Transform statHolder, string stat, int change)
    {

        if (HomeSingleton.Instance.FocusOnCard.statPoints > 0)
        {
            int x = HomeSingleton.Instance.FocusOnCard.maxHealth;

            statHolder.Find("Image").Find("Image").gameObject.SetActive(true);
            statHolder.Find("Image").Find("Image").Find("Button").GetComponent<Button>().onClick.RemoveAllListeners();
            statHolder.Find("Image").Find("Image").Find("Button").GetComponent<Button>().onClick.AddListener(
                delegate { AddStat(stat, change); });
        }
        else
        {

            statHolder.Find("Image").Find("Image").gameObject.SetActive(false);
        }
    }

    private void AddStat(string stat, int howMuch)
    {
        if (stat == "Health") HomeSingleton.Instance.FocusOnCard.maxHealth += howMuch;
        if (stat == "attackPower") HomeSingleton.Instance.FocusOnCard.attackPower += howMuch;
        if (stat == "dex") HomeSingleton.Instance.FocusOnCard.dexterity += howMuch;
        if (stat == "magic") HomeSingleton.Instance.FocusOnCard.magicPower += howMuch;
        if (stat == "speed") HomeSingleton.Instance.FocusOnCard.speed += howMuch;
        if (stat == "dodge") HomeSingleton.Instance.FocusOnCard.dodge += howMuch;


        HomeSingleton.Instance.FocusOnCard.statPoints -= 1;
        NameCards(GameObject.Find("StatsPanel"));
        AddStats();
    }

}
