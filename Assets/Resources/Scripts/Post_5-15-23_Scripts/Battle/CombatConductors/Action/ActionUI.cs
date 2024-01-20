using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionUI 
{

    GameObject UI;
    GameObject targetUI;

    public void TweenUIBattleStart(GameObject CombatPrefab, Transform UiOriginParent, Transform UiTargetParent)
    {
        UI = GameObject.Instantiate(CombatPrefab);
        FillCards.FillAttackDefendCard(UI, UiOriginParent.gameObject, CombatSingleton.Instance.actionData.OriginCharacter);
        UI.transform.position = UiOriginParent.position;
        
        float MoveDown = 150;
        foreach (GameObject target in CombatSingleton.Instance.actionData.TargetCharacters)
        {

            GameObject targetUI = GameObject.Instantiate(CombatPrefab);
            FillCards.FillAttackDefendCard(targetUI, UiTargetParent.gameObject, target);
            targetUI.transform.position = new Vector3(UiTargetParent.position.x, UiTargetParent.position.y - MoveDown, UiTargetParent.position.z);
            MoveDown += 150;
        }
    }


    public bool isTweenStartCompleted()
    {
        if (UI == null)
        {
            return true;
        }
        return false;
    }


    public void TweenUIBattleEndInitiated(List<bool> isHit, GameObject CombatPrefab, 
        Transform UiTargetParent, Transform UiOriginParent, string originCardText)
    {

        UI = GameObject.Instantiate(CombatPrefab);
        FillCards.FillAttackDefendCard_NoHealthBar_FillInText(UI, UiOriginParent.gameObject, CombatSingleton.Instance.actionData.OriginCharacter, originCardText);
        UI.transform.position = UiOriginParent.position;

        float MoveDown = 150;
        for (int i = 0; i < CombatSingleton.Instance.actionData.TargetCharacters.Count; i++)
        {
            if (isHit[i] == true)
            {


                targetUI = GameObject.Instantiate(CombatPrefab);
                if (CombatSingleton.Instance.actionData.ChosenAbility.isFriendly)
                {

                    FillCards.FillAttackDefendCard(targetUI, UiTargetParent.gameObject, CombatSingleton.Instance.actionData.TargetCharacters[i]);
                }
                else
                {

                    FillCards.FillAttackDefendCard_Hit(targetUI, UiTargetParent.gameObject, CombatSingleton.Instance.actionData.TargetCharacters[i]);
                }
                targetUI.transform.position = new Vector3(UiTargetParent.position.x, UiTargetParent.position.y - MoveDown, UiTargetParent.position.z);
                targetUI.GetComponent<SelfDestruct>().NewTime(3);
                MoveDown += 150;
            }
            else
            {
                targetUI = GameObject.Instantiate(CombatPrefab);
                FillCards.FillAttackDefendCard_NoHealthBar_FillInText(targetUI, UiTargetParent.gameObject, 
                    CombatSingleton.Instance.actionData.TargetCharacters[i], "Miss");
                targetUI.transform.position = new Vector3(UiTargetParent.position.x, UiTargetParent.position.y - MoveDown, UiTargetParent.position.z);
                targetUI.GetComponent<SelfDestruct>().NewTime(3);
                MoveDown += 150;
            }
        }
    }


    public void TweenUIBattleEndInitiated_Item(GameObject CombatPrefab, Transform UiTargetParent, Transform UiOriginParent, string originCardText)
    {

        UI = GameObject.Instantiate(CombatPrefab);
        FillCards.FillAttackDefendCard_NoHealthBar_FillInText(UI, UiOriginParent.gameObject, CombatSingleton.Instance.actionData.OriginCharacter, originCardText);
        UI.transform.position = UiOriginParent.position;

        float MoveDown = 150;
        for (int i = 0; i < CombatSingleton.Instance.actionData.TargetCharacters.Count; i++)
        {
            
                targetUI = GameObject.Instantiate(CombatPrefab);
                FillCards.FillAttackDefendCard_Hit(targetUI, UiTargetParent.gameObject, CombatSingleton.Instance.actionData.TargetCharacters[i]);
                targetUI.transform.position = new Vector3(UiTargetParent.position.x, UiTargetParent.position.y - MoveDown, UiTargetParent.position.z);
                targetUI.GetComponent<SelfDestruct>().NewTime(3);
                MoveDown += 150;
            
        }
    }

    public void TweenUIBattleEndInitiated_Capture(List<bool> isHit, GameObject CombatPrefab, Transform UiTargetParent, 
        Transform UiOriginParent, string originCardText)
    {
        UI = GameObject.Instantiate(CombatPrefab);
        FillCards.FillAttackDefendCard_NoHealthBar_FillInText(UI, UiOriginParent.gameObject, CombatSingleton.Instance.actionData.OriginCharacter, originCardText);
        UI.transform.position = UiOriginParent.position;

        float MoveDown = 150;
        for (int i = 0; i < CombatSingleton.Instance.actionData.TargetCharacters.Count; i++)
        {
            if (isHit[i] == true)
            {
                targetUI = GameObject.Instantiate(CombatPrefab);
                FillCards.FillAttackDefendCard_NoHealthBar_FillInText(targetUI, UiTargetParent.gameObject,
                    CombatSingleton.Instance.actionData.TargetCharacters[i], "Captured!!!!");
                targetUI.transform.position = new Vector3(UiTargetParent.position.x, UiTargetParent.position.y - MoveDown, UiTargetParent.position.z);
                targetUI.GetComponent<SelfDestruct>().NewTime(3);
                MoveDown += 150;
            }
            else
            {
                targetUI = GameObject.Instantiate(CombatPrefab);
                FillCards.FillAttackDefendCard_NoHealthBar_FillInText(targetUI, UiTargetParent.gameObject, 
                    CombatSingleton.Instance.actionData.TargetCharacters[i], "Miss");
                targetUI.transform.position = new Vector3(UiTargetParent.position.x, UiTargetParent.position.y - MoveDown, UiTargetParent.position.z);
                targetUI.GetComponent<SelfDestruct>().NewTime(3);
                MoveDown += 150;
            }
        }
    }

    public bool IsTweenUIBattleEndCompleted()
    {
        if (targetUI == null)
        {
            return true;
        }
        return false;
    }
}
