using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinChecker : MonoBehaviour
{
    public int currentLevel = 1;

    public GameObject Banner;
    public GameObject WinPanel;

    public Text BannerText;
    public Text WinPanelText;
    public Text GoldText;

    public int goldReward;
    public int goldPenalty;

    private void Start()
    {
        /*
        goldReward = 0;

        foreach(var character in CombatSingleton.Instance.Combatants)
        {
            if(CombatSingleton.Instance.Combatants[0].GetComponent<CombatCharacter>().team == 0)
            {
                goldReward++;
            }
        }
        goldReward *= 100;*/
    }

    public void Win()
    {
        Activate();
        BannerText.text = "Victory";
        WinPanelText.text = "Victory";
        CalculateGoldGain();
    }
    public void Loss()
    {

        Activate();
        BannerText.text = "Defeat";
        WinPanelText.text = "Defeat";
        CalculateGoldLoss();
    }

    private void Activate()
    {
        Banner.SetActive(true);
        WinPanel.SetActive(true);
    }

    private void CalculateGoldGain()
    {
        GoldText.text = "+" + goldReward.ToString();

        UpdateSave(goldReward);
    }

    private void CalculateGoldLoss()
    {

        if(goldPenalty < SaveData.Current.mainData.playerData.money)
        {
            goldPenalty = SaveData.Current.mainData.playerData.money;
        }
        GoldText.text = "-" + goldPenalty.ToString();
        UpdateSave(-goldPenalty);
    }

    private void UpdateSave(int gold)
    {
        SaveData.Current.mainData.playerData.money += gold;
        if(gold < 0)
        {
            gold = 0;
        }

        if (SaveData.Current.mainData.playerData.level < currentLevel)
        {
            SaveData.Current.mainData.playerData.level = currentLevel;
        }
        SaveManipulator.AutoSave();
    }

    public void ContinueButton()
    {
        //SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());

        SaveManipulator.UnloadLoadSceneData();
        SceneChange.NextScene("SceneMap",false);
    }
}
