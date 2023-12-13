using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeUIMono : MonoBehaviour
{

    public GameObject ControlPanel;


    private void Update()
    {
    }

    public void CharacterPanel()
    {
        BannerText("Stats");
        HomeSingleton.Instance.controlPanel.CardListFullRoster(ControlPanel.transform.Find("Scroll").transform.Find("Viewport").gameObject);
    }
    public void CharacterSwapPanel()
    {
        BannerText("Battle Roster");
        HomeSingleton.Instance.controlPanel.CardListFullRoster(ControlPanel.transform.Find("Scroll").transform.Find("Viewport").gameObject);
    }
    public void EquipmentPanel()
    {
        BannerText("Equipment");
        HomeSingleton.Instance.controlPanel.CardListFullRoster(ControlPanel.transform.Find("Scroll").transform.Find("Viewport").gameObject);
    }
    public void ItemsPanel()
    {
        BannerText("Items");
        HomeSingleton.Instance.controlPanel.CardListFullRoster(ControlPanel.transform.Find("Scroll").transform.Find("Viewport").gameObject);
    }
    public void AdventurePanel()
    {
        BannerText("Adventure");
    }

    public void Build()
    {

    }

    private void BannerText(string s)
    {
        ControlPanel.transform.Find("Panel").transform.Find("Text").GetComponent<Text>().text = s;
    }

    public void AddToBattleRoster()
    {
        if (HomeSingleton.Instance.save.characterBattleTeam.Count < HomeSingleton.Instance.save.game.leadership)
        {
            bool isInRoster = false;
            Transform scroll = GameObject.Find("BattleScroll").transform;

            for (int i = 0; i < scroll.GetChild(0).childCount; i++)
            {
                string text = scroll.GetChild(0).GetChild(i).Find("Text").GetComponent<Text>().text.Substring(0, HomeSingleton.Instance.FocusOnCard.name.Length);

                if (text == HomeSingleton.Instance.FocusOnCard.name)
                {
                    isInRoster = true;
                }
            }
            if (!isInRoster)
            {
                HomeSingleton.Instance.save.characterBattleTeam.Add(HomeSingleton.Instance.FocusOnCard);
            }
            HomeSingleton.Instance.controlPanel.ReloadPanel();
        }
    }
    public void RemoveFromBattleRoster()
    {
        if (HomeSingleton.Instance.BattleCard != null)
        {
            HomeSingleton.Instance.save.characterBattleTeam.Remove(HomeSingleton.Instance.BattleCard);
        }
        HomeSingleton.Instance.controlPanel.ReloadPanel();
    }
}
