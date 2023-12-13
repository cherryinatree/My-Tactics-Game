using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PREVIEWMODE { Off, Action, ActionMulti, Move, Item, Capture, Facing}
public class ActionData : MonoBehaviour 
{

    public GameObject OriginCharacter;
    public List<GameObject> TargetCharacters;
    public Abilities ChosenAbility;
    public Item ChosenItem;

    public PREVIEWMODE Preview = PREVIEWMODE.Off;

    public bool AiAction;
    //public bool previewActionOn = false;
    //public bool previewMoveOn = false;

    public Actions actions;

    private void Awake()
    {
        CombatSingleton.Instance.actionData = this;
        ResetActionData();
    }

    public void ResetActionData()
    {
        TargetCharacters = new List<GameObject>();
        OriginCharacter = null;
        ChosenAbility = null;
        ChosenItem = null;
        AiAction = false;
        //previewActionOn = false;
        //previewMoveOn = false;
        Preview = PREVIEWMODE.Off;
    }
}
