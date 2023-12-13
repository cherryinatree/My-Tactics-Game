using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using GamingTools;
using Cinemachine;
using TMPro;
using UnityEngine.UI;

public class TownMaster : MonoBehaviour
{
    private BattleMaster battle;
    private int teams = 2;
    private SaveFile save;

    private HomeMain home;

    public GameObject testPanel;

    public Text gold;

    // Start is called before the first frame update
    void Awake()
    {
        LoadGame();
        UiSetUp();
        //SetUpSingleton();

        //Debug.Log(SaveData.Current.mainData.charactersStorage.Count);
        //Debug.Log(SaveData.Current.mainData.characters.Count);
    }



    private void LoadGame()
    {

        SaveManipulator.LoadSceneChange();
        //SaveManipulator.TestNewSaveAddAllCharacters();
    }

    private void SetUpSingleton()
    {
        HomeSingleton.Instance.save = save;
        HomeSingleton.Instance.controlPanel = new HomeControlPanel();
        HomeSingleton.Instance.homeMain = home;
        //HomeSingleton.Instance.FocusOnCard = SaveData.Current.mainData.charactersStorage[0];

        //HomeSingleton.Instance.controlPanel.CardListBattleRoster(testPanel);
    }

    private void UiSetUp()
    {
        MouseSpecialActions.setUp();
    }


    // Update is called once per frame
    void Update()
    {
    }


}
