using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SaveManipulator
{
    public static List<Character> CharacterList;
    public static List<Item> ItemList;
    public static List<Equipment> EquipmentList;

    public static void GetLists()
    {
        JsonRetriever jsonRetriever = new JsonRetriever();
        CharacterList = jsonRetriever.LoadAllCharacters(RetrieverConstants.JsonCharacter);
        ItemList = jsonRetriever.LoadAllItems();
        EquipmentList = jsonRetriever.LoadAllEquipment();

    }

    public static void LoadSceneChange()
    {
        //GetLists();
        SaveData.Current.mainData = (MainSaveFile)SerializationManager.Load(Application.persistentDataPath + "/saves/SceneChange.save");
        //SaveData.Current.mainData.characters = CharacterList;
    }
    public static void SaveSceneChange()
    {
        SerializationManager.Save("SceneChange", SaveData.Current.mainData);
    }

    public static void Save(string saveName, MainSaveFile save)
    {
        SerializationManager.Save(saveName, save);
    }

    public static void NewSave()
    {
        MainSaveFile mainSaveFile = new MainSaveFile();
        mainSaveFile.itemInventory = new List<Item>();
        mainSaveFile.itemStorage = new List<Item>();
        mainSaveFile.equipmentStorage = new List<Equipment>();
        mainSaveFile.characters = new List<Character>();
        mainSaveFile.charactersStorage = new List<Character>();
        mainSaveFile.playerData = new PlayerData();
        mainSaveFile.playerData.Scene = 1;
        SaveData.Current.mainData = mainSaveFile;
        SerializationManager.Save("AutoSave", SaveData.Current);
    }

    public static void AutoSave()
    {
        SerializationManager.Save("AutoSave", SaveData.Current);
    }

    public static string[] GetListOfSaveNames()
    {
        return SerializationManager.LoadAllSaveNames();
    }

    public static void UpdateBattleSave()
    {
        SaveData.Current.mainData.loadSceneData.isBattle = true;
        SaveData.Current.mainData.loadSceneData.isLoad = true;
        SaveData.Current.mainData.loadSceneData.enemyList = new List<Character>();
        SaveData.Current.mainData.loadSceneData.playerList = new List<Character>();
        SaveData.Current.mainData.loadSceneData.currentSceneName = SceneManager.GetActiveScene().name;

        if (CombatSingleton.Instance.SaveObjects != null)
        {
            foreach (GameObject gObject in CombatSingleton.Instance.SaveObjects)
            {
                if (gObject != null)
                    gObject.GetComponent<MyDataUploader>().UploadChangesToSave();
            }
        }
        if (CombatSingleton.Instance.SaveTriggers != null)
        {
            foreach (GameObject gObject in CombatSingleton.Instance.SaveTriggers)
            {
                if (gObject != null)
                    gObject.GetComponent<CubeTrigger>().UploadChangesToSave();
            }
        }
        if (CombatSingleton.Instance.SaveMovers != null)
        {
            foreach (GameObject gObject in CombatSingleton.Instance.SaveMovers)
            {
                if (gObject != null)
                    gObject.GetComponent<TriggerMove>().UploadChangesToSave();
            }
        }

        foreach (GameObject character in CombatSingleton.Instance.Combatants)
        {
            if(character.GetComponent<CombatCharacter>().team != 0)
            {
                character.GetComponent<CombatCharacter>().myStats.myCubeName = character.GetComponent<CombatCharacter>().myCube.name;
                character.GetComponent<CombatCharacter>().myStats.characterName = character.name;
                SaveData.Current.mainData.loadSceneData.enemyList.Add(character.GetComponent<CombatCharacter>().myStats);
            }
            else
            {
                character.GetComponent<CombatCharacter>().myStats.myCubeName = character.GetComponent<CombatCharacter>().myCube.name;
                SaveData.Current.mainData.loadSceneData.playerList.Add(character.GetComponent<CombatCharacter>().myStats);
            }
        }
    }

    public static void UnloadLoadSceneData()
    {

        SaveData.Current.mainData.loadSceneData.isBattle = false;
        SaveData.Current.mainData.loadSceneData.isLoad = false;
        SaveData.Current.mainData.loadSceneData.enemyList = new List<Character>();
        SaveData.Current.mainData.loadSceneData.playerList = new List<Character>();
        SaveData.Current.mainData.loadSceneData.boardChanges.changes.Clear();
        SaveData.Current.mainData.loadSceneData.currentSceneName = SceneManager.GetActiveScene().name;
    }



    public static void TestNewSaveAddAllCharacters()
    {
        GetLists();
        MainSaveFile mainSaveFile = new MainSaveFile();
        mainSaveFile.itemInventory = new List<Item>();
        mainSaveFile.itemStorage = ItemList;
        mainSaveFile.characters = CharacterList;
        mainSaveFile.equipmentStorage = EquipmentList;
        mainSaveFile.charactersStorage = new List<Character>();
        mainSaveFile.playerData = new PlayerData();
        mainSaveFile.playerData.Scene = 1;
        mainSaveFile.loadSceneData = new LoadSceneData();
        SaveData.Current.mainData = mainSaveFile;
        SerializationManager.Save("AutoSave", SaveData.Current);
    }


    public static void ItemTransfer(int id, int howmany, bool toStorage)
    {
        if (toStorage)
        {

        }
        else
        {

        }
    }
    public static void CharacterTransfer(string inventoryId, bool toStorage)
    {
        if (toStorage)
        {
            
        }
        else
        {

        }
    }

    public static void AddCharacter(int id)
    {
        Character character = new Character();
        for (int i = 0; i < CharacterList.Count; i++)
        {
            if (CharacterList[i].id == id)
            {
                character.id = CharacterList[i].id;

                character.characterName = CharacterList[i].characterName;
                character.scriptName = CharacterList[i].scriptName;
                character.scriptPosition = CharacterList[i].scriptPosition;
                character.level = CharacterList[i].level;
                character.maxHealth = CharacterList[i].maxHealth;
                character.attack = CharacterList[i].attack;
                character.defense = CharacterList[i].defense;
                character.speed = CharacterList[i].speed;
            }
        }

        SaveData.Current.mainData.charactersStorage.Add(character);
    }
    public static void AddItem(int id)
    {

    }

    public static void DeleteCharacter()
    {

    }
    public static void DeleteItem()
    {

    }

    public static void ModifyCharacter()
    {

    }

    public static Character GetCharacter()
    {
        return null;
    }
    public static Item GetItem()
    {
        return null;
    }
}
