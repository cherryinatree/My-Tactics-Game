using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsTown : MonoBehaviour
{
    public void Store()
    {
        SceneChange.NextScene("SceneStore", false);
    }
    public void Train()
    {
        SceneChange.NextScene("SceneTrain", false);

    }
    public void Tavern()
    {
        SceneChange.NextScene("SceneTavern", false);

    }
    public void LeaveTown()
    {
        SceneChange.NextScene("SceneMap",false);
    }
    public void SaveAndExit()
    {
        SaveManipulator.AutoSave();
        SceneManager.LoadScene("MainMenu");
    }

    public void GoToTown()
    {
        SceneChange.NextScene("SceneTown1", false);

    }
}
