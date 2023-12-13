using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GamingTools;
using System;

public class MainMenuUI : MonoBehaviour
{
    public static string CurrentFile;

    public GameObject NewGameScreen;
    public GameObject LoadGameScreen;
    private NewSaveUI newSaveUi;

    public void Start()
    {
        ResetScreens();
        newSaveUi = NewGameScreen.GetComponent<NewSaveUI>();
    }

    public void ResetScreens()
    {
        NewGameScreen.SetActive(false);
        LoadGameScreen.SetActive(false);
    }

    public void Test()
    {

        Debug.Log("trigger1");
    }

    public void NewGame()
    {
        NewGameScreen.SetActive(!NewGameScreen.activeSelf);
    }
    public void LoadGame()
    {
        LoadGameScreen.SetActive(!LoadGameScreen.activeSelf);
    }

    public void NewGameAccept()
    {

        MainSaveFile save = NewSave();

        string thisSaveName = "New Save";
        if (newSaveUi.enteredText.text != null && newSaveUi.enteredText.text != "")
        {
            string savename = DateTime.Now.ToString();
            savename = savename.Replace("/", "-");
            savename = savename.Replace("\\", "-");
            savename = savename.Replace(":", "-");
            thisSaveName = savename;
        }
        else
        {

            thisSaveName = newSaveUi.enteredText.text;
        }

        Debug.Log(save.loadSceneData.currentSceneName);

        save.saveName = thisSaveName;
        SaveData.Current.mainData = save;
        SaveManipulator.Save(thisSaveName, save);
        SaveManipulator.SaveSceneChange();

        SceneController.ChangeScene("SceneMap");

    }
    private MainSaveFile NewSave()
    {

        MainSaveFile mainSaveFile = new MainSaveFile();
        mainSaveFile.itemInventory = new List<Item>();
        mainSaveFile.itemStorage = new List<Item>();
        mainSaveFile.equipmentStorage = new List<Equipment>();
        mainSaveFile.characters = new List<Character>();
        mainSaveFile.charactersStorage = new List<Character>();

        JsonRetriever jsonRetriever = new JsonRetriever();
        List<Character> charList = jsonRetriever.LoadAllCharacters(RetrieverConstants.JsonCharacter);
        mainSaveFile.characters.Add(charList[0]);
        mainSaveFile.characters.Add(charList[1]);
        mainSaveFile.characters.Add(charList[2]);


        mainSaveFile.playerData = new PlayerData();
        mainSaveFile.playerData.Scene = 2;
        mainSaveFile.playerData.level = 2;

        mainSaveFile.loadSceneData = new LoadSceneData();
        mainSaveFile.loadSceneData.currentSceneName = "SceneMap";
        mainSaveFile.loadSceneData.isBattle = false;

        return mainSaveFile;
    }
}
