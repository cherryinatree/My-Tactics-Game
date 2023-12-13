using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneChange 
{
   public static void NextScene(string sceneName, bool isBattle)
    {

        SaveData.Current.mainData.loadSceneData.currentSceneName = sceneName;
        SaveData.Current.mainData.loadSceneData.isBattle = isBattle;


        SaveManipulator.AutoSave();
        SaveManipulator.SaveSceneChange();
        SceneManager.LoadScene(sceneName);
    }
}
