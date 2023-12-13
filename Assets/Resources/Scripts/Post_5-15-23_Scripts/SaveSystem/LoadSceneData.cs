using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LoadSceneData
{
    public string currentSceneName;

    public bool isBattle;
    public bool isLoad;

    public List<Character> enemyList;
    public List<Character> playerList;
}