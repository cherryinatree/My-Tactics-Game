using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TestSave
{
   // List<Stats> _playerStats;
    Inventory _generalInventory;
    TownChanges _town;
    PlayerChoices _playerChoices;
    GameProgress _progress;
    KeyMap _allTheKeys;
    Settings _settings;

    public TestSave()
    {
        /*
        PlayerStats = new List<Stats>();
        Stats char1 = new Stats();
        Stats char2 = new Stats();
        Stats char3 = new Stats();
        PlayerStats.Add(char1);
        PlayerStats.Add(char2);
        PlayerStats.Add(char3);
       // GeneralInventory = new Inventory(this);
        Town = new TownChanges();
        PlayerChoices = new PlayerChoices();
        Progress = new GameProgress();
        TheKeyMap = new KeyMap();
        TheSettings = new Settings();*/


    }
    /*
        public List<Stats> PlayerStats
        {
            get { return _playerStats; }
            set { _playerStats = value; }
        }*/

    public Inventory GeneralInventory
    {
        get { return _generalInventory; }
        set { _generalInventory = value; }
    }

    public TownChanges Town
    {
        get { return _town; }
        set { _town = value; }
    }
    
    public PlayerChoices PlayerChoices
    {
        get { return _playerChoices; }
        set { _playerChoices = value; }
    }

    public GameProgress Progress
    {
        get { return _progress; }
        set { _progress = value; }
    }

    public KeyMap TheKeyMap
    {
        get { return _allTheKeys; }
        set { _allTheKeys = value; }
    }

    public Settings TheSettings
    {
        get { return _settings; }
        set { _settings = value; }
    }
}
