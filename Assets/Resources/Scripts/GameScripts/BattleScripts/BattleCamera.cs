using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using GamingTools;

public static class BattleCamera
{

    private static int currentFocus = 0;
    private static int outOfMoves = 0;
    private static bool freeLook = false;
    private static bool targetGroup = false;

    private static bool delay = false;
    private static bool switchGreenLight = false;

    private static float timeRemaining = 3;
    private static float TurnTimeRemaining = 3;

    public static void CameraMovement()
    {

        PlayerInteractions();

    }

    private static void PlayerInteractions()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Clear();
            SwitchToFollow();
            //updateFreeLook(focusOnMe);
        }

        BattleSingleton.Instance.cam.m_Lens.FieldOfView -= Input.mouseScrollDelta.y * 2;
        if (BattleSingleton.Instance.cam.m_Lens.FieldOfView > 80)
        {
            BattleSingleton.Instance.cam.m_Lens.FieldOfView = 80;
        }
        if (BattleSingleton.Instance.cam.m_Lens.FieldOfView < 5)
        {
            BattleSingleton.Instance.cam.m_Lens.FieldOfView = 5;
        }

        if (Input.GetMouseButton(1))
        {
            SwitchToFreeLook();
            BattleSingleton.Instance.freeLookObject.transform.Rotate(0, Input.GetAxis("Mouse X") * 2, 0);
        }
        if (Input.GetKey(KeyCode.W))
        {
            SwitchToFreeLook();
            BattleSingleton.Instance.freeLookObject.transform.position += BattleSingleton.Instance.freeLookObject.transform.forward/4;
        }
        if (Input.GetKey(KeyCode.A))
        {
            SwitchToFreeLook();
            BattleSingleton.Instance.freeLookObject.transform.position -= BattleSingleton.Instance.freeLookObject.transform.right / 4;
        }
        if (Input.GetKey(KeyCode.D))
        {
            SwitchToFreeLook();
            BattleSingleton.Instance.freeLookObject.transform.position += BattleSingleton.Instance.freeLookObject.transform.right / 4;
        }
        if (Input.GetKey(KeyCode.S))
        {
            SwitchToFreeLook();
            BattleSingleton.Instance.freeLookObject.transform.position -= BattleSingleton.Instance.freeLookObject.transform.forward / 4;
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            Clear();
            TurnController.NextTurn();
            SwitchTurnDelay(3);
        }
        if(BattleSingleton.Instance.battleState == BattleMaster.BATTLESTATE.ATTACK || BattleSingleton.Instance.battleState == BattleMaster.BATTLESTATE.MAGIC)
        {
            UnityAddOn.RotateY(BattleSingleton.Instance.targetGroup.transform, 0.5f);
        }

        if (delay && !switchGreenLight)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
            }
            else
            {
                switchGreenLight = false;
                SwitchToFollow();
            }
        }
        if (switchGreenLight)
        {
            if (TurnTimeRemaining > 0)
            {
                Debug.Log(TurnTimeRemaining);
                Debug.Log(Time.deltaTime);
                UnityAddOn.RotateY(BattleSingleton.Instance.freeLookObject.transform, 0.5f);
                TurnTimeRemaining -= Time.deltaTime;
            }
            else
            {
                switchGreenLight = false;
                delay = false;
                var transposer = BattleSingleton.Instance.cam.GetCinemachineComponent<CinemachineTransposer>();
                transposer.m_FollowOffset = Constants.CameraConstants.Default;
                UserInterface.Banner("", false);
                SwitchToFollow();
            }
        }
    }

    public static void updateFreeLook()
    {
        BattleSingleton.Instance.freeLookObject.transform.position = BattleSingleton.Instance.focusOnMe.transform.position;
        BattleSingleton.Instance.freeLookObject.transform.rotation = BattleSingleton.Instance.focusOnMe.transform.rotation;
    }
    public static void SwitchToFreeLook()
    {
        if (!freeLook)
        {
            BattleSingleton.Instance.cam.Follow = BattleSingleton.Instance.freeLookObject.transform;
            BattleSingleton.Instance.cam.LookAt = BattleSingleton.Instance.freeLookObject.transform;
            freeLook = true;
        }
    }
    public static void SwitchToTargetGroup()
    {
            BattleSingleton.Instance.cam.Follow = BattleSingleton.Instance.targetGroup.transform;
            BattleSingleton.Instance.cam.LookAt = BattleSingleton.Instance.targetGroup.transform;
            BattleSingleton.Instance.targetGroup.GetComponent<CinemachineTargetGroup>().m_Targets[0].target = BattleSingleton.Instance.focusOnMe.transform;
            BattleSingleton.Instance.targetGroup.GetComponent<CinemachineTargetGroup>().m_Targets[1].target = Targeting.target.transform;
           
    }
    public static void SwitchToFollow()
    {
        Clear();
        if (outOfMoves < BattleSingleton.Instance.ourTurn.Count)
        {
            if (currentFocus >= BattleSingleton.Instance.ourTurn.Count - 1)
            {
                currentFocus = 0;
            }
            else
            {
                currentFocus++;
            }
            if (BattleSingleton.Instance.ourTurn[currentFocus].GetComponent<CharacterController>().myStats.actionsLeft > 0)
            {
                    BattleSingleton.Instance.focusOnMe = BattleSingleton.Instance.ourTurn[currentFocus];
                    BattleSingleton.Instance.cam.Follow = BattleSingleton.Instance.focusOnMe.transform;
                    BattleSingleton.Instance.cam.LookAt = BattleSingleton.Instance.focusOnMe.transform;
                    updateFreeLook();
                    freeLook = false;
                    targetGroup = false;
                    delay = false;
                    outOfMoves = 0;
            }
            else
            {
                outOfMoves++;
                SwitchToFollow();
            }
        }
        else
        {
            outOfMoves = 0;
            foreach (GameObject character in BattleSingleton.Instance.ourTurn)
            {
                character.GetComponent<CharacterController>().myStats.actionsLeft = character.GetComponent<CharacterController>().myStats.actions;
            }

            Clear();
            TurnController.NextTurn();

            SwitchTurnDelay(3);
        }
    }

    public static void SwitchDelay(float time)
    {
        timeRemaining = time;
        delay = true;
    }
    public static void SwitchTurnDelay(float time)
    {
        SwitchToFreeLook();
        var transposer = BattleSingleton.Instance.cam.GetCinemachineComponent<CinemachineTransposer>();
        transposer.m_FollowOffset = Constants.CameraConstants.TurnChange;
        TurnTimeRemaining = time;
        switchGreenLight = true;
    }

    private static void Clear()
    {
        UserInterface.TurnAllOff();
        BattleMaster.battleState = BattleMaster.BATTLESTATE.IDLE;
        BattleSingleton.Instance.aimCam.enabled = false;
        var transposer = BattleSingleton.Instance.cam.GetCinemachineComponent<CinemachineTransposer>();
        transposer.m_FollowOffset = Constants.CameraConstants.Default;
    }
}
