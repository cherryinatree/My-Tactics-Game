using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ActionCamera
{

    public void ChangeCameraAngle(CinemachineTargetGroup TargetGroup)
    {
        if (TargetGroup.m_Targets.Length <= 0)
        {
            TargetGroup.AddMember(CombatSingleton.Instance.actionData.OriginCharacter.transform, 1f, 1f);

            foreach (GameObject target in CombatSingleton.Instance.actionData.TargetCharacters)
            {
                TargetGroup.AddMember(target.transform, 1, 1);
            }
        }
    }
}