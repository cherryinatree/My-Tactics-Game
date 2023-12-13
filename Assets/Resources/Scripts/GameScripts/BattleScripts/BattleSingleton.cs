using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using GamingTools;

public class BattleSingleton
{
    private static BattleSingleton instance = null;

    public List<List<GameObject>> Combatants = new List<List<GameObject>>();
    public List<GameObject> ourTurn = new List<GameObject>();

    public GameObject focusOnMe;
    public GameObject freeLookObject;
    public GameObject targetGroup;

    public CinemachineVirtualCamera cam;
    public Cinemachine3rdPersonAim aimCam;

    public CinemachineTransposer transposer;

    public int currentTurn;
    public int NumberOfTeams;

    public SaveFile save;

    public List<GameObject> particles;

    public BattleMaster.BATTLESTATE battleState;

    private BattleSingleton()
    {
    }
    public void Clear()
    {
        Instance.Clear();
    }

    public static BattleSingleton Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new BattleSingleton();
            }
            return instance;
        }
    }
}
