using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GamingTools;
using Cinemachine;

public class HomeSingleton
{
    private static HomeSingleton instance = null;

    public SaveFile save;
    public HomeControlPanel controlPanel;
    public HomeMain homeMain;
    public CinemachineVirtualCamera cam;
    public Stats FocusOnCard;
    public Stats BattleCard;

    private HomeSingleton()
    {
    }

    public static HomeSingleton Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new HomeSingleton();
            }
            return instance;
        }
    }
}
