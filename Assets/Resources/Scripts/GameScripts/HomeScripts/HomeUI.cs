using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeUI 
{

    private GameObject[] UiElements;
    private GameObject infoPanel;
    private GameObject iconsPanel;
    private GameObject chancePanel;
    public GameObject optionPanel;
    private GameObject bannerPanel;
    private GameObject menuPanel;

    public HomeUI()
    {
        UiElements = GameObject.FindGameObjectsWithTag("UI");
        foreach (GameObject item in UiElements)
        {

            if (item.name != "TabPanel")
            {
                if (item.name == "OneInfoPanel")
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
                if (item.name == "MenuPanel")
                {
                    menuPanel = item;
                }
                item.SetActive(false); 
                if (item.name == "OneInfoPanel")
                {
                    item.SetActive(true);
                }
            }
        }
    }

    public void UiUpdate()
    {
        ChangeText(infoPanel.transform.GetChild(0).transform, "Gold:\n" + HomeSingleton.Instance.save.game.gold.ToString("n0"));
        ChangeText(infoPanel.transform.GetChild(1).transform, "Materials:\n" + HomeSingleton.Instance.save.game.buildingMaterials.ToString("n0"));
        ChangeText(infoPanel.transform.GetChild(2).transform, "Tax Income:\n" + HomeSingleton.Instance.save.game.TaxIncome.ToString("n0"));
        ChangeText(infoPanel.transform.GetChild(3).transform, "Day:\n" + DayConverter(HomeSingleton.Instance.save.game.days));
      
    }

    public void ClearUI()
    {
            bool wereAnyActive = false;
            foreach (GameObject item in UiElements)
            {

                if (item.name != "TabPanel" && item.name != "OneInfoPanel")
                {
                    if(item.activeSelf == true)
                    {
                        wereAnyActive = true;
                    }

                    item.SetActive(false);
                }
            }

            if(wereAnyActive == false)
            {
                menuPanel.SetActive(true);
            }
        
    }

    private void ChangeText(Transform t, string text)
    {
        t.Find("Panel").transform.Find("Panel").transform.Find("Text").GetComponent<Text>().text = text;
    }

    private string DayConverter(int days)
    {


        var totalYears = Math.Truncate((decimal)days / 365);
        var totalMonths = Math.Truncate(((decimal)days % 365) / 30);
        var remainingDays = Math.Truncate(((decimal)days % 365) % 30);

        string date = remainingDays.ToString("F0") + "/" + totalMonths.ToString("F0") + "/" + totalYears.ToString("F0");

        return date;
    }
}
