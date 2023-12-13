using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadMenu : MonoBehaviour
{

    public SaveMenu SaveMenu;
    public void LoadGame()
    {
        SaveData.Current.mainData = (MainSaveFile)SerializationManager.Load(Application.persistentDataPath + "/saves/" + SaveMenu.populator.Selected);
        SaveManipulator.SaveSceneChange();
        SceneChange.NextScene(SaveData.Current.mainData.loadSceneData.currentSceneName, SaveData.Current.mainData.loadSceneData.isBattle);
    }
    public void DeleteGame()
    {
        SerializationManager.Delete(SaveMenu.populator.Selected);
        SaveMenu.StartGUI();
    }
}
