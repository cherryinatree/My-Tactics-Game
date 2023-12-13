using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameProgress
{
    public int gold;
    public int buildingMaterials;
    public int TaxIncome;
    public int days;
    public string sceneName;
    public int questsCompleted;
    public int mainStoryMilesonesCompleted;
    public List<string> items;
    public List<string> equipedBuffs;
    public int leadership;

    public GameProgress()
    {
        gold = 0;
        buildingMaterials = 0;
        TaxIncome = 0;
        days = 1;
        sceneName = "HomeScene";
        questsCompleted = 0;
        mainStoryMilesonesCompleted = 0;
        items = new List<string>();
        equipedBuffs = new List<string>();
        leadership = 3;
    }
}
