using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AnimationControllerScript
{
    
    public static void TriggerAnimation(GameObject character, string trigger, bool isActive)
    {
        character.GetComponent<Animator>().SetBool(trigger, isActive); 
    }
}
