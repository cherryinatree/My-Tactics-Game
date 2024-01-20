using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ActionConductor : MonoBehaviour
{
    public CinemachineTargetGroup TargetGroup;
    public GameObject CombatPrefab;
    public Transform UiOriginParent;
    public Transform UiTargetParent;

    private GameObject AbilityPrefab;
    public GameObject CapturePrefab;


    private ActionUI actionUI;
    private ActionAnimation actionAnimation;
    private ActionCamera actionCamera;

    private bool MathCompleted = false;
    private bool UItweenStartInitiated = false;
    private bool UItweenStartCompleted = false;
    private bool AnimateActionInitiated = false;
    private bool AnimateActionCompleted = false;

    private bool FinalMathCompleted = false;
    private bool UItweenEndInitiated = false;
    private bool UItweenEndCompleted = false;
    private bool ActionCompleted = false;

    private List<bool> isHit;

    private float XPgained = 0;
    private bool isLeveled = false;


    // Start is called before the first frame update
    void Start()
    {
        actionUI = new ActionUI();
        actionAnimation = new ActionAnimation();
        actionCamera = new ActionCamera();
    }

    // Update is called once per frame
    void Update()
    {
        if (CombatSingleton.Instance.battleSystem.State == BATTLESTATE.ACTION)
        {
            ConductActionSteps();
        }
    }

    private void ConductActionSteps()
    {
        if (!MathCompleted)
        {
            ChangeCameraAngle();
            RunTheNumbers();
        }
        else if (!UItweenStartInitiated)
        {
            FaceTarget();
            TweenUIBattleStart();

        }else if (!UItweenStartCompleted)
        {
            isTweenStartCompleted();
        }else if (!AnimateActionInitiated)
        {
            AnimateActionInitated();
        }
        else if (!AnimateActionCompleted)
        {
            isAnimateActionCompleted();
        }
        else if (!FinalMathCompleted)
        {
            FinalMath();
        }
        else if (!UItweenEndInitiated)
        {
            TweenUIBattleEndInitiated();
        }
        else if (!UItweenEndCompleted)
        {
            IsTweenUIBattleEndCompleted();
        }
        else if (!ActionCompleted)
        {
            RestActionCheckPoints();
            RestBattleState();
        }
    }




    /**************************************************************
     * 
     *          The Steps of An Action
     * 
     * *************************************************************/


    private void ChangeCameraAngle()
    {
        GameObject.Find("PlayerCamera").GetComponent<CombatCamera>().ResetCamera();
        TargetGroup.m_Targets = new CinemachineTargetGroup.Target[0];
        actionCamera.ChangeCameraAngle(TargetGroup);
    }

    private void RunTheNumbers()
    {
        if(CombatSingleton.Instance.actionData.Preview == PREVIEWMODE.Action || CombatSingleton.Instance.actionData.Preview == PREVIEWMODE.ActionMulti)
        {
            if (!CombatSingleton.Instance.actionData.ChosenAbility.isFriendly)
            {
                isHit = ActionMath.CheckIfHits();
            }
            else
            {
                isHit = ActionMath.AlwaysHits();
            }
        }
        if (CombatSingleton.Instance.actionData.Preview == PREVIEWMODE.Item)
        {
            isHit = ActionMath.AlwaysHits();
        }
        if (CombatSingleton.Instance.actionData.Preview == PREVIEWMODE.Capture)
        {
            isHit = ActionMath.CheckIfCaptures();
        }
        MathCompleted = true;
    }


    private void FaceTarget()
    {
        //Debug.Log("target" + CombatSingleton.Instance.actionData.TargetCharacters[0]);
        //Debug.Log("target" + CombatSingleton.Instance.actionData.OriginCharacter);

        FaceTarget faceTarget = new();
        faceTarget.FaceDirection(CombatSingleton.Instance.actionData.OriginCharacter,
            CombatSingleton.Instance.actionData.TargetCharacters[0]);
    }

    private void TweenUIBattleStart()
    {
        actionUI.TweenUIBattleStart(CombatPrefab, UiOriginParent, UiTargetParent);
        UItweenStartInitiated = true;
    }

    private void isTweenStartCompleted()
    {
        UItweenStartCompleted = actionUI.isTweenStartCompleted();
    }

    private void AnimateActionInitated()
    {


        if (CombatSingleton.Instance.actionData.Preview == PREVIEWMODE.Item)
        {
            actionAnimation.AnimateActionInitated_Item(CombatSingleton.Instance.actionData.ChosenItem);
            CombatSingleton.Instance.actionData.OriginCharacter.gameObject.GetComponent<Animator>().SetTrigger("Attack2");

        }
        else if (CombatSingleton.Instance.actionData.Preview == PREVIEWMODE.Capture)
        {

            actionAnimation.AnimateActionInitated_Capture(isHit, CapturePrefab);
            CombatSingleton.Instance.actionData.OriginCharacter.gameObject.GetComponent<Animator>().SetTrigger("Attack2");
        }
        else
        {
            AbilityPrefab = GamingTools.ResourseLoader.GetGameObject(CombatSingleton.Instance.actionData.ChosenAbility.animation);

            actionAnimation.AnimateActionInitated_Range(isHit, AbilityPrefab, CombatSingleton.Instance.actionData.ChosenAbility);

            if (CombatSingleton.Instance.actionData.ChosenAbility.maxDistance <= 1)
            {
                CombatSingleton.Instance.actionData.OriginCharacter.gameObject.GetComponent<Animator>().SetTrigger("Attack1");
            }
            else
            {
                CombatSingleton.Instance.actionData.OriginCharacter.gameObject.GetComponent<Animator>().SetTrigger("Attack2");
            }
        }

        AnimateActionInitiated = true;
    }

    private void isAnimateActionCompleted()
    {
        AnimateActionCompleted = actionAnimation.isAnimateActionCompleted_Range();
    }

    private void FinalMath()
    {

        Character originStats = CombatSingleton.Instance.actionData.OriginCharacter.GetComponent<CombatCharacter>().myStats;

        for (int i = 0; i < CombatSingleton.Instance.actionData.TargetCharacters.Count; i++)
        {

            if (CombatSingleton.Instance.actionData.Preview == PREVIEWMODE.Item)
            {
                Debug.Log(CombatSingleton.Instance.actionData.ChosenItem.itemName);
                ActionMath.CalulateBenift(CombatSingleton.Instance.actionData.ChosenItem, CombatSingleton.Instance.actionData.TargetCharacters[i]);
                XPgained += RewardsCalculator.ItemXP(CombatSingleton.Instance.actionData.TargetCharacters[i]);
                isLeveled = RewardsCalculator.CheckIfCharacterLeveledUp(originStats);

            }
            else if (CombatSingleton.Instance.actionData.Preview == PREVIEWMODE.Capture)
            {
                if (isHit[i])
                {
                    Capture.CaptureEnemy(CombatSingleton.Instance.actionData.TargetCharacters[i]);
                    XPgained += RewardsCalculator.CaptureXP(CombatSingleton.Instance.actionData.TargetCharacters[i]);
                    isLeveled = RewardsCalculator.CheckIfCharacterLeveledUp(originStats);
                }
            }
            else
            {
                if (CombatSingleton.Instance.actionData.ChosenAbility.isFriendly)
                {
                    Debug.Log(CombatSingleton.Instance.actionData.ChosenAbility.abilityName);
                    ActionMath.CalulateBenift(CombatSingleton.Instance.actionData.ChosenAbility, CombatSingleton.Instance.actionData.TargetCharacters[i]);
                    XPgained += RewardsCalculator.FriendlyAbilityXP(CombatSingleton.Instance.actionData.TargetCharacters[i]);
                    isLeveled = RewardsCalculator.CheckIfCharacterLeveledUp(originStats);
                }
                else
                {
                    if (isHit[i])
                    {
                        ActionMath.CalulateDamage(CombatSingleton.Instance.actionData.ChosenAbility, CombatSingleton.Instance.actionData.TargetCharacters[i]);

                        CombatSingleton.Instance.actionData.TargetCharacters[i].gameObject.GetComponent<Animator>().SetTrigger("GetHit");
                        if (DeathMonitor.DeathCheck(CombatSingleton.Instance.actionData.TargetCharacters[i]))
                        {
                            CombatSingleton.Instance.actionData.TargetCharacters[i].gameObject.GetComponent<Animator>().SetTrigger("Dead");
                            XPgained += RewardsCalculator.SlayXP(CombatSingleton.Instance.actionData.TargetCharacters[i]);
                            isLeveled = RewardsCalculator.CheckIfCharacterLeveledUp(originStats);
                        }
                        else
                        {
                            XPgained += RewardsCalculator.AttackXP(CombatSingleton.Instance.actionData.TargetCharacters[i]);
                            isLeveled = RewardsCalculator.CheckIfCharacterLeveledUp(originStats);
                        }
                    }
                }
            }
        }
        FinalMathCompleted = true;
    }

    private void TweenUIBattleEndInitiated()
    {
        string originCharacterText = "XP + " + XPgained.ToString();
        if (isLeveled)
        {
            originCharacterText = "Level Up!!!";
        }


        if (CombatSingleton.Instance.actionData.Preview == PREVIEWMODE.Item)
        {
            actionUI.TweenUIBattleEndInitiated_Item(CombatPrefab, UiTargetParent, UiOriginParent, originCharacterText);

        }
        else if (CombatSingleton.Instance.actionData.Preview == PREVIEWMODE.Capture)
        {

            actionUI.TweenUIBattleEndInitiated_Capture(isHit, CombatPrefab, UiTargetParent, UiOriginParent, originCharacterText);
        }
        else
        {
            actionUI.TweenUIBattleEndInitiated(isHit, CombatPrefab, UiTargetParent, UiOriginParent, originCharacterText);
        }
        UItweenEndInitiated = true;
    }
    
    private void IsTweenUIBattleEndCompleted()
    {
        UItweenEndCompleted = actionUI.IsTweenUIBattleEndCompleted();
    }



    /**************************************************************
     * 
     *          Reset when action is completed
     * 
     * *************************************************************/

    private void RestActionCheckPoints()
    {
        MathCompleted = false;
        UItweenStartInitiated = false;
        UItweenStartCompleted = false;
        AnimateActionInitiated = false;
        AnimateActionCompleted = false;
        FinalMathCompleted = false;
        UItweenEndInitiated = false;
        UItweenEndCompleted = false;
        ActionCompleted = false;

        XPgained = 0;
        isLeveled = false;
    }

    private void RestBattleState()
    {
        CombatSingleton.Instance.actionData.ResetActionData();

        if(CombatSingleton.Instance.battleSystem.CurrentTeam == 0)
        {
            CombatSingleton.Instance.battleSystem.State = BATTLESTATE.PLAYERTURN;
        }
        else
        {
            CombatSingleton.Instance.battleSystem.State = BATTLESTATE.ENEMYTURN;
        }
    }
}
