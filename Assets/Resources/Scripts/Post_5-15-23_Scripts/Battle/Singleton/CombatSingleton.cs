using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatSingleton
{
    
    private static CombatSingleton instance;

    public List<Cube> Cubes = new List<Cube>();
    public GameObject CursorCube;
    public List<GameObject> Panels;
    public List<GameObject> Combatants;
    public GameObject FocusCharacter;
    public GameObject InfoCharacter;
    public bool isUiOn = false;

    public List<GameObject> SaveObjects;
    public List<GameObject> SaveTriggers;
    public List<GameObject> SaveMovers;
    public BattleSystem battleSystem;
    public ActionData actionData;

    public static CombatSingleton Instance 
    { 
        get 
        { 
            if(instance == null)
            {
                instance = new CombatSingleton();
            }
            return instance; 
        } 
    }
}
