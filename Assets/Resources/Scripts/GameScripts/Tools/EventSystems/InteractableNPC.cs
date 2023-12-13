using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GamingTools;

public class InteractableNPC : MonoBehaviour
{

    public int id;
    // Start is called before the first frame update
    void Start()
    {

        GameEvents.current.onTalkToNPC += OnTalkToNPC;
    }

    public void OnTalkToNPC(int id)
    {
        HomeSingleton.Instance.save.enemies.Clear();
        HomeSingleton.Instance.save.numberOfEnemies.Clear();

        HomeSingleton.Instance.save.enemies.Add("Golem");
        HomeSingleton.Instance.save.numberOfEnemies.Add(4);

        SaveSystem.SaveGame(SaveSystem._lastLoadedFile, HomeSingleton.Instance.save);

        SceneController.ChangeScene("BattleScene");
    }
}
