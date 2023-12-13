using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.AI;

public class BattleMaster
{

    private int currentTurn = 0;
    private int currentFocus = 0;
    private int outOfMoves = 0;

    private bool freeLook = false;
    private bool runOnce = false;
    private bool isGameOver = false;

    public static string ActionChosen = null;

    private GameObject previousFocus;

    public enum BATTLESTATE
    {
        IDLE = 0, MOVE = 1, ATTACK = 2, MAGIC = 3,
        ITEM = 4, SPECIAL = 5
    };

    public static BATTLESTATE battleState;

    public BattleMaster()
    {
        battleState = BATTLESTATE.IDLE;
        SetUpSingleton();
        SetUpHighlight();
        BattleCamera.updateFreeLook();
    }

    public void Controls()
    {
        if (BattleSingleton.Instance.NumberOfTeams > 1)
        {
            BattleCamera.CameraMovement();
            ChangeBattleState();
            BattleStates();
            CheckFocus();
            Targeting.TargetCheck();
        }
        else
        {
            if (!isGameOver)
            {
                isGameOver = true;
                EndGame();
            }
        }
    }

    private void CheckFocus()
    {
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {

            CharacterHighlight.OutlineMe(BattleSingleton.Instance.focusOnMe, 0f, Color.red);
        }


        if (previousFocus.transform != BattleSingleton.Instance.focusOnMe.transform)
        {

            CharacterHighlight.OutlineMe(previousFocus, 0f, Color.white);
            previousFocus = BattleSingleton.Instance.focusOnMe;
            CharacterHighlight.OutlineMe(previousFocus, 0.03f, Color.green);
        }
    }

    private void SetUpSingleton()
    {
        BattleSingleton.Instance.cam.Follow = BattleSingleton.Instance.focusOnMe.transform;
        BattleSingleton.Instance.cam.LookAt = BattleSingleton.Instance.focusOnMe.transform;
        BattleSingleton.Instance.transposer = BattleSingleton.Instance.cam.GetCinemachineComponent<CinemachineTransposer>();
        BattleSingleton.Instance.transposer.m_FollowOffset = Constants.CameraConstants.Default;
        BattleSingleton.Instance.aimCam.enabled = false;
        BattleSingleton.Instance.battleState = battleState;
    }

    private void SetUpHighlight()
    {
        CharacterHighlight.ResetHighlights();


        Targeting.target = null;
        previousFocus = BattleSingleton.Instance.focusOnMe;
        CharacterHighlight.OutlineMe(BattleSingleton.Instance.focusOnMe, 0.03f, Color.green);
    }

    private void Movement()
    {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (Vector3.Distance(hit.point, BattleSingleton.Instance.focusOnMe.transform.position) < BattleSingleton.Instance.focusOnMe.GetComponent<CharacterController>().myStats.speed)
                {
                    NavMeshPath path = new NavMeshPath();
                    if (NavMesh.CalculatePath(BattleSingleton.Instance.focusOnMe.transform.position, hit.point, NavMesh.AllAreas, path))
                    {

                        List<Vector3> corners = new List<Vector3>();
                        BattleSingleton.Instance.focusOnMe.GetComponent<LineRenderer>().positionCount = path.corners.Length;
                        if (path.corners.Length > 1)
                            BattleSingleton.Instance.focusOnMe.GetComponent<LineRenderer>().SetPositions(path.corners);

                        if (Input.GetMouseButtonDown(0) && path.corners.Length > 1)
                        {
                            BattleSingleton.Instance.focusOnMe.GetComponent<NavMeshAgent>().SetDestination(hit.point);
                            ResetBattleState();
                            BattleSingleton.Instance.focusOnMe.GetComponent<LineRenderer>().positionCount = 0;
                            BattleSingleton.Instance.focusOnMe.GetComponent<CharacterController>().myStats.actionsLeft -= 1;
                       
                        if (BattleSingleton.Instance.focusOnMe.GetComponent<CharacterController>().myStats.actionsLeft <= 0)
                        {
                            
                            BattleCamera.SwitchDelay((Vector3.Distance(hit.point, BattleSingleton.Instance.focusOnMe.transform.position)/
                                BattleSingleton.Instance.focusOnMe.GetComponent<NavMeshAgent>().speed)+0.5f);
                        }
                    }
                    }
                }
            }

        
    }


    private void BattleStates()
    {
        if (battleState == BATTLESTATE.IDLE)
        {
            MouseSpecialActions.ShowStatCard();

            if(Targeting.target != null)
            {

                Targeting.NullTheTarget();
            }
        }
        if (BattleSingleton.Instance.focusOnMe.GetComponent<CharacterController>().myStats.actionsLeft > 0)
        {
            if (battleState == BATTLESTATE.MOVE)
            {
                if (BattleSingleton.Instance.focusOnMe.GetComponent<CharacterController>().myStats.actionsLeft > 0)
                {
                    Movement();
                }
            }
            if (battleState == BATTLESTATE.ATTACK)
            {
                Attacking();
            }
            if (battleState == BATTLESTATE.MAGIC)
            {
                UseMagic();
            }
            if (battleState == BATTLESTATE.ITEM)
            {
                if (BattleSingleton.Instance.focusOnMe.GetComponent<CharacterController>().myStats.actionsLeft > 0)
                {
                    SelectTarget();
                    ItemMenu();
                }
            }
        }
        else
        {
            UserInterface.TurnAllOff();
        }
        BattleSingleton.Instance.battleState = battleState;
    }

    private void SelectTarget()
    {
        if (runOnce)
        {
            Targeting.SelectTarget();
            runOnce = false;
            if (Targeting.target == null)
            {
                battleState = BATTLESTATE.IDLE;
                return;
            }
            UserInterface.InfoChance();
            UserInterface.TurnOffOrOn("ChancePanel");

        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            Targeting.TargetPrevious();
            UserInterface.InfoChance();
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            Targeting.TargetNext();
            UserInterface.InfoChance();
        }
    }

    private void Attacking()
    {
        SelectTarget();
        if(Targeting.target == null) return;
        AttackMenu();
        ChooseAction(BattleMechanics.AttackType.Attack);
    }

    private void UseMagic()
    {
        SelectTarget();
        if (Targeting.target == null) return;
        ChooseAction(BattleMechanics.AttackType.Magic);
        MagicMenu();
    }

    private void ChooseAction(BattleMechanics.AttackType attackType)
    {
        string[,] ActionList = null;
        if (attackType == BattleMechanics.AttackType.Magic)
        {
            ActionList = Constants.SpellList.spells;
        }
        if (attackType == BattleMechanics.AttackType.Attack)
        {
            ActionList = Constants.ActionsList.Actions;
        }
        if (ActionChosen != null && ActionList != null)
        {
            Debug.Log(ActionChosen);
            string targetType = "";
            for (int i = 0; i < ActionList.GetLength(0); i++)
            {
                if (ActionList[i, 0] == ActionChosen)
                {
                    targetType = ActionList[i, 1];
                }
            }
            if (targetType == "Enemy")
            {
                BattleMechanics.Attack(BattleSingleton.Instance.focusOnMe, Targeting.target, attackType, ActionChosen);
            }
            ActionChosen = null;
        }
    }

    public void AttackMenu()
    {
        UserInterface.AttackPanel(BattleSingleton.Instance.focusOnMe.GetComponent<CharacterController>().myStats);
    }
    public void MagicMenu()
    {
        UserInterface.MagicPanel(BattleSingleton.Instance.focusOnMe.GetComponent<CharacterController>().myStats);
    }
    public void ItemMenu()
    {
        UserInterface.ItemsPanel(BattleSingleton.Instance.focusOnMe.GetComponent<CharacterController>().myStats);
    }

    private void ChangeBattleState()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            battleState = BATTLESTATE.MOVE;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            battleState = BATTLESTATE.ATTACK;
            UserInterface.TurnAllOff();
            runOnce = true;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            battleState = BATTLESTATE.MAGIC;
            UserInterface.TurnAllOff();
            runOnce = true;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            battleState = BATTLESTATE.ITEM;
            UserInterface.TurnAllOff();
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            battleState = BATTLESTATE.SPECIAL;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Clear();
        }
    }

    private void Clear()
    {
        Targeting.NullTheTarget();
        Targeting.target = null;
        UserInterface.TurnAllOff();
        battleState = BATTLESTATE.IDLE;
        BattleSingleton.Instance.aimCam.enabled = false;
        var transposer = BattleSingleton.Instance.cam.GetCinemachineComponent<CinemachineTransposer>();
        transposer.m_FollowOffset = Constants.CameraConstants.Default;
    }
    public static void RemoteChangeBattleState(BATTLESTATE action)
    {
        battleState = action;
    }

    private void ResetBattleState()
    {
        battleState = BATTLESTATE.IDLE;
    }

    public void EndGame()
    {
        EndBattle.EndTheBattle();
    }
}
