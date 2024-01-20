using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject NewSaveField;

    public void Resume()
    {
        gameObject.SetActive(false);
    }
    public void Save()
    {
        NewSaveField.SetActive(!NewSaveField.activeSelf);
    }

    public void EndTurn()
    {
        CombatControlsEnable enact = new CombatControlsEnable();
        
        enact.Key("testTurnChange");
        GameObject.Find("GameMaster").GetComponent<InteractMenus>().ResetPanels();
    }
    public void Options()
    {

    }
    public void EscapeBattle()
    {

        CombatSingleton.Instance.isUiOn = false;
        SaveManipulator.UnloadLoadSceneData();
        SceneManager.LoadScene("SceneMap");
    }
    public void MainMenu()
    {
        CombatSingleton.Instance.isUiOn = false;
        SaveManipulator.UnloadLoadSceneData();
        SceneManager.LoadScene("MainMenu");
    }
}
