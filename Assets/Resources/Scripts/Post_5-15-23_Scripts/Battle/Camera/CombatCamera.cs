using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using GamingTools;

public class CombatCamera : MonoBehaviour
{
    public CinemachineVirtualCamera cam;
    //public Cinemachine3rdPersonAim aimCam;

    private float currentRoation = 0;
    public float rotateAmount = 90; 
    public Transform CursorPosition;

    private CinemachineTransposer transposer;
    private Vector3 origionalTransposerBody;

    public float turnChangeRotateSpeed = 40;

    public CinemachineTargetGroup targetGroup;


    private void Start()
    {
        transposer = cam.GetCinemachineComponent<CinemachineTransposer>();
        origionalTransposerBody = transposer.m_FollowOffset;
    }

    // Update is called once per frame
    void Update()
    {
        isTurnChange();
    }


    private void isTurnChange()
    {
        if(CombatSingleton.Instance.battleSystem.State == BATTLESTATE.TURNCHANGE)
        {
            TurnChangeRotate();
        }
        else if(CombatSingleton.Instance.battleSystem.State == BATTLESTATE.ACTION)
        {
            ActionCamera();
        }
        else
        {
            CurrentRotation();
        }
    }

    private void ActionCamera()
    {

        if (transposer.m_FollowOffset != Constants.CameraConstants.Duel)
        {
            transposer.m_FollowOffset = Constants.CameraConstants.Duel;
        }

        cam.Follow = targetGroup.Transform;
        cam.LookAt = targetGroup.Transform;
    }

    private void TurnChangeRotate()
    {

        if (transposer.m_FollowOffset != Constants.CameraConstants.TurnChange)
        {
            transposer.m_FollowOffset = Constants.CameraConstants.TurnChange;
        }
        UnityAddOn.RotateY(CursorPosition, turnChangeRotateSpeed);
    }

    private void CurrentRotation()
    {
        if(transposer.m_FollowOffset != origionalTransposerBody)
        {
            LookAtCursor();
            transposer.m_FollowOffset = origionalTransposerBody;
        }

        if (CursorPosition.position != CombatSingleton.Instance.CursorCube.transform.position)
        {
            LookAtCursor();
        }
        if (CombatSingleton.Instance.battleSystem.isKeyboardControl)
        {
            RotateControls();
        }
    }

    private void RotateControls()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            currentRoation += rotateAmount;
            if (currentRoation > 360)
            {
                currentRoation -= 360;
            }
            LookAtCursor();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            currentRoation -= rotateAmount;
            if (currentRoation < 0)
            {
                currentRoation += 360;
            }
            LookAtCursor();
        }
    }

    private void LookAtCursor()
    {
        CursorPosition.position = CombatSingleton.Instance.CursorCube.transform.position;
        CursorPosition.rotation = CombatSingleton.Instance.CursorCube.transform.rotation;
        CursorPosition.Rotate(new Vector3(0f, currentRoation, 0f));
        cam.Follow = CursorPosition;
        cam.LookAt = CursorPosition;
    }
}
