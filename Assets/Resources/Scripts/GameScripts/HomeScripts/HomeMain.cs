using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.AI;

public class HomeMain
{
    CinemachineVirtualCamera cam;
    CinemachineTransposer Transposer;
    HomeUI Ui;
    Build build;
    public HomeMain(CinemachineVirtualCamera cam)
    {
        this.cam = cam;
        Transposer = cam.GetCinemachineComponent<CinemachineTransposer>();
        Transposer.m_FollowOffset = Constants.CameraConstants.HomeDefault;
        build = new Build();
        Ui = new HomeUI();
    }

    public void Controls()
    {
        CameraControls(cam);
        Ui.UiUpdate();
        build.BuildUpdate();
        UiControls();
        Test(); 
    }

    private void Test()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            HomeSingleton.Instance.FocusOnCard.statPoints += 3;
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            HomeSingleton.Instance.save.game.items.Add("ChestPlate");
            HomeSingleton.Instance.save.game.items.Add("Shuriken");
            HomeSingleton.Instance.save.game.items.Add("Muscle");
            HomeSingleton.Instance.save.game.items.Add("HealthPotion");
            HomeSingleton.Instance.save.game.items.Add("Amulet");
            HomeSingleton.Instance.save.game.items.Add("Teleport");
            HomeSingleton.Instance.save.game.items.Add("Sword");
        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            foreach(string a in HomeSingleton.Instance.save.game.items)
            {
                Debug.Log(a);
            }
        }
    }

    private void UiControls()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            EscapeKey();
        }
    }

    public void EscapeKey()
    {
        Ui.ClearUI();
    }

    private void CameraControls(CinemachineVirtualCamera cam)
    {

        Transposer.m_FollowOffset.y -= Input.mouseScrollDelta.y * 2;
        if (Transposer.m_FollowOffset.y > 25)
        {
            Transposer.m_FollowOffset.y = 25;
        }
        if (Transposer.m_FollowOffset.y < 3)
        {
            Transposer.m_FollowOffset.y = 3;
        }
    }
}
