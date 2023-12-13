using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GamingTools;
using System;

public static class UserInterface
{
    private static GameObject[] UiElements;
    private static GameObject currentFocus;
    private static GameObject infoPanel;
    private static GameObject iconsPanel;
    private static GameObject chancePanel;
    public static GameObject optionPanel;
    private static GameObject bannerPanel;


    public static void CollectUI()
    {
        UiElements = GameObject.FindGameObjectsWithTag("UI");
        foreach (GameObject item in UiElements)
        {
            if (item.name != "ActionPanel")
            {
                if(item.name == "CharacterInfoPanel")
                {
                    infoPanel = item;
                }
                if (item.name == "IconsPanel")
                {
                    iconsPanel = item;
                }
                if (item.name == "ChancePanel")
                {
                    chancePanel = item;
                }
                if (item.name == "OptionPanel")
                {
                    optionPanel = item;
                }
                if (item.name == "BannerPanel")
                {
                    bannerPanel = item;
                }
                item.SetActive(false);
            }
        }
    }

    public static void ShowCharacterInfo(Stats stats)
    {
        infoPanel.SetActive(true);
        Transform parentPanel = infoPanel.transform.Find("Panel1").transform.Find("Panel2").transform;
        parentPanel.Find("Health").GetComponent<Text>().text = "HP: " + stats.health.ToString();
        parentPanel.Find("Name").GetComponent<Text>().text = stats.name;
        parentPanel.Find("Class").GetComponent<Text>().text = stats.CharacterClass;
        parentPanel.Find("Magic").GetComponent<Text>().text = "MP: " + stats.magicPower;
        parentPanel.Find("Level").GetComponent<Text>().text = "Level: " + stats.level;
        parentPanel.Find("Status").GetComponent<Text>().text = stats.statusEffect;
        string actions = "[] ";
        for (int i = 1; i < stats.actionsLeft; i++)
        {
            actions += actions;
        }
        if (stats.actionsLeft <= 0)
        {
            actions = "";
        }
        parentPanel.Find("Actions").GetComponent<Text>().text = actions;

        parentPanel.Find("Picture").GetComponent<Image>().sprite = ResourseLoader.GetSprite("Images/CharacterPics/" + stats.picture);
    }

    public static void AttackPanel(Stats stats)
    {
        AddImages(stats.attacks, "Actions");
    }
    public static void MagicPanel(Stats stats)
    {
        AddImages(stats.spells, "Actions");
    }
    public static void ItemsPanel(Stats stats)
    {
        AddImages(stats.items, "Items");
    }

    private static void AddImages(List<string> imageNames, string location)
    {
        if (iconsPanel.activeSelf == false)
        {
            iconsPanel.SetActive(true);
            Transform parentPanel = iconsPanel.transform.Find("Panel1").transform.Find("Panel2").transform;

            for (int i = 0; i < 16; i++)
            {
                Transform item1 = parentPanel.Find("ImageButton" + (i + 1).ToString()).transform;
                if (i < imageNames.Count)
                {
                    item1.transform.Find("Panel").transform.Find("Panel").transform.Find("Panel").GetComponent<Image>().sprite = ResourseLoader.GetSprite("Images/"+location+"/" + imageNames[i]);
                    
                }
                else
                {
                    item1.transform.Find("Panel").transform.Find("Panel").transform.Find("Panel").GetComponent<Image>().sprite = ResourseLoader.GetSprite("Images/Actions/Default");
                }
            }
        }
    }

    public static void ChanceToHitPanel(float chances)
    {
        chancePanel.transform.Find("Panel1").transform.Find("Panel2").transform.Find("Text").GetComponent<Text>().text = 
            "Chance:\n" + chances.ToString("F0")+"%";
    }

    public static void TurnOffOrOn(string whichOne)
    {
        foreach (GameObject item in UiElements)
        {
            if (item.name == whichOne)
            {
                if (item.activeSelf == true)
                {
                    item.SetActive(false);
                }
                else
                {
                    item.SetActive(true);
                }
            }
        }
    }
    public static void TurnOffOrOn(string whichOne, bool onOrOff)
    {
        foreach (GameObject item in UiElements)
        {
            if (item.name == whichOne)
            {
                item.SetActive(onOrOff);
            }
        }
    }

    public static void TurnAllOff()
    {
        foreach (GameObject item in UiElements)
        {
            if (item.name != "ActionPanel")
            {
                item.SetActive(false);
            }
        }
    }

    public static void InfoChance()
    {
        ChanceToHitPanel(BattleMechanics.HitChance(BattleSingleton.Instance.focusOnMe, Targeting.target, BattleMechanics.AttackType.Attack));
        ShowCharacterInfo(Targeting.target.GetComponent<CharacterController>().myStats);
    }

    public static void Banner(string text, bool onOrOff)
    {
        bannerPanel.SetActive(onOrOff);
        bannerPanel.transform.Find("Panel").transform.Find("Panel").transform.Find("Text").GetComponent<Text>().text = text;
    }
}
