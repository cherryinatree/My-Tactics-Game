using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BATTLESTATE { START, PLAYERTURN, TURNCHANGE, ACTION, ENEMYTURN, WIN, LOST}
public class BattleSystem : MonoBehaviour
{

    public BATTLESTATE State;
    public BATTLESTATE TempState;
    private bool doesPlayerHaveKeyboardMouseControl;
    public int CurrentTeam;
    Timer timer;
    private int numberOfTeam;

    public float TurnChangeDelay = 3;


    // Start is called before the first frame update
    void Start()
    {
        State = BATTLESTATE.PLAYERTURN;
        TempState = State;
        CurrentTeam = 0;
        doesPlayerHaveKeyboardMouseControl = true;
        timer = new Timer(2f);
        CombatSingleton.Instance.battleSystem = this;
        FindNumberOfTeams();
    }

    private void Update()
    {

        RegainControl();
        EndTurnChange();
        //ActionFinished();
    }

    private void EndTurnChange()
    {
        if(State == BATTLESTATE.TURNCHANGE)
        {
            if (timer.ClockTick())
            {
                if(CurrentTeam == 0)
                {
                    State = BATTLESTATE.PLAYERTURN;
                    PlayerGainsControl();
                }
                else
                {
                    State = BATTLESTATE.ENEMYTURN;
                }
            }
        }
    }

    private void ActionFinished()
    {
        if(TempState != State)
        {
            if(TempState != BATTLESTATE.PLAYERTURN && TempState != BATTLESTATE.ENEMYTURN)
            {

                CombatSingleton.Instance.actionData.ResetActionData();
            }
            TempState = State;
        }
    }

    private void FindNumberOfTeams()
    {
        numberOfTeam = 0;
        foreach (GameObject character in CombatSingleton.Instance.Combatants)
        {
            if (character.GetComponent<CombatCharacter>().team > numberOfTeam)
            {
                numberOfTeam = character.GetComponent<CombatCharacter>().team;
            }
        }
    }

    public void TurnChange()
    {
        State = BATTLESTATE.TURNCHANGE;
        NextTurn();
        if (CurrentTeam != 0)
        {
            PlayerLosesControl();
        }
        timer.NewStopTime(TurnChangeDelay);
        timer.RestartTimer();
    }

    private void NextTurn()
    {
        if (CombatSingleton.Instance.battleSystem.CurrentTeam >= numberOfTeam)
        {
            CombatSingleton.Instance.battleSystem.CurrentTeam = 0;
        }
        else
        {
            CombatSingleton.Instance.battleSystem.CurrentTeam++;
        }
        ResetMoves();
    }
    private void RegainControl()
    {
        if(CurrentTeam == 0)
        {
            if (!doesPlayerHaveKeyboardMouseControl)
            {
                if (timer.ClockTick())
                {
                    doesPlayerHaveKeyboardMouseControl = true;
                }
            }
        }
    }

    public void PlayerGainsControl()
    {
        doesPlayerHaveKeyboardMouseControl = true;
    }

    public void PlayerLosesControl()
    {
        doesPlayerHaveKeyboardMouseControl = false;
    }
    public void PlayerLosesControl(float forHowLong)
    {
        doesPlayerHaveKeyboardMouseControl = false;
        timer.NewStopTime(forHowLong);
        timer.RestartTimer();
    }

    public bool isKeyboardControl
    {
        get { return doesPlayerHaveKeyboardMouseControl; }
    }
    private void ResetMoves()
    {
        foreach (GameObject character in CombatSingleton.Instance.Combatants)
        {
            character.GetComponent<CombatCharacter>().ResetActions();
        }
    }
}
