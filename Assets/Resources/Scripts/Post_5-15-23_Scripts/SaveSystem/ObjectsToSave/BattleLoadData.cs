using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BattleLoadData
{
    public int minLevel;
    public int maxLevel;

    public int enemyPerGroup;
    public int enemyGroupCount;

    public int monsterPerGroup;
    public int monsterGroupCount;

    public bool monster = false;
    public bool enemy = false;
    public bool villian = false;

    public int[] whichVillian;
}
