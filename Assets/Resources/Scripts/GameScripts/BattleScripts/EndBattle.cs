using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GamingTools;

public static class EndBattle
{
    private static int Gold;
    private static int Materials;
    private static string text;
    

    public static void EndTheBattle()
    {
        Rewards();
        YourOptions();
        VictoryBanner();
    }

    private static void VictoryBanner()
    {
        if (BattleSingleton.Instance.ourTurn[0].tag == "PlayerCharacter")
        {
            UserInterface.Banner("Victory", true);
        }
        else
        {
            UserInterface.Banner("Defeat", true);
        }
    }

    private static void YourOptions()
    {
        if (BattleSingleton.Instance.ourTurn[0].tag == "PlayerCharacter")
        {
            MakeVictoryText();
            Transform panel = UserInterface.optionPanel.transform.Find("Panel").transform.Find("Panel");
            panel.Find("OptionButton1").transform.Find("Panel").transform.Find("Panel").transform.Find("Button").GetComponent<Button>().onClick.AddListener(VictoryOption1);
            panel.Find("OptionButton1").transform.Find("Panel").transform.Find("Panel").transform.Find("Button").transform.Find("Text").GetComponent<Text>().text = 1.ToString();
            panel.Find("OptionButton2").transform.Find("Panel").transform.Find("Panel").transform.Find("Button").GetComponent<Button>().onClick.AddListener(VictoryOption2);
            panel.Find("OptionButton2").transform.Find("Panel").transform.Find("Panel").transform.Find("Button").transform.Find("Text").GetComponent<Text>().text = 2.ToString();
            panel.Find("Text").GetComponent<Text>().text = text;
            UserInterface.TurnOffOrOn("OptionPanel");
        }
        else
        {
            SaveSystem.SaveGame(SaveSystem._lastLoadedFile, BattleSingleton.Instance.save);
            MakeDefeatText();
            Transform panel = UserInterface.optionPanel.transform.Find("Panel").transform.Find("Panel");
            panel.Find("OptionButton1").transform.Find("Panel").transform.Find("Panel").transform.Find("Button").GetComponent<Button>().onClick.AddListener(DefeatOption1);
            panel.Find("OptionButton1").transform.Find("Panel").transform.Find("Panel").transform.Find("Button").transform.Find("Text").GetComponent<Text>().text = "Yes";
            panel.Find("OptionButton2").transform.Find("Panel").transform.Find("Panel").transform.Find("Button").GetComponent<Button>().onClick.AddListener(DefeatOption2);
            panel.Find("OptionButton2").transform.Find("Panel").transform.Find("Panel").transform.Find("Button").transform.Find("Text").GetComponent<Text>().text = "No";
            panel.Find("Text").GetComponent<Text>().text = text;
            UserInterface.TurnOffOrOn("OptionPanel");
        }
    }
    private static void Rewards()
    {
        Gold = Random.Range(25, 50) * BattleSingleton.Instance.save.numberOfEnemies.Count * BattleSingleton.Instance.save.enemyChallengeRating;
        Materials = Random.Range(10, 20) * BattleSingleton.Instance.save.numberOfEnemies.Count * BattleSingleton.Instance.save.enemyChallengeRating;
    }

    private static void VictoryOption1()
    {
        BattleSingleton.Instance.save.game.gold += Gold;
        BattleSingleton.Instance.save.game.questsCompleted += 1;
        BattleSingleton.Instance.save.game.days += 1;
        BattleSingleton.Instance.save.game.sceneName = "HomeScene";
        SaveSystem.SaveGame(SaveSystem._lastLoadedFile, BattleSingleton.Instance.save);
        SceneController.ChangeScene("HomeScene");
    }
    private static void VictoryOption2()
    {
        BattleSingleton.Instance.save.game.buildingMaterials += Materials;
        BattleSingleton.Instance.save.game.questsCompleted += 1;
        BattleSingleton.Instance.save.game.days += 1;
        BattleSingleton.Instance.save.game.sceneName = "HomeScene";
        SaveSystem.SaveGame(SaveSystem._lastLoadedFile, BattleSingleton.Instance.save);
        SceneController.ChangeScene("HomeScene");
    }
    private static void DefeatOption1()
    {
        SceneController.ChangeScene("HomeScene");
    }
    private static void DefeatOption2()
    {
        SceneController.ChangeScene("MainMenu");
    }
    private static void MakeVictoryText()
    {
        text = "Rewards:\nYou have enough room to take either\nOption 1: " + Gold.ToString() +
            " Gold\nOr Option 2: " + Materials.ToString() + " Materials";
    }
    private static void MakeDefeatText()
    {
        text = "You lost the fight, would you like to continue?";
    }

}
