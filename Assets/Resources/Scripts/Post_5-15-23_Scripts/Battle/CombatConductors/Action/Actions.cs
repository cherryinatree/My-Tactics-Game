using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actions : MonoBehaviour
{

    List<GameObject> path = new List<GameObject>();
    public float speed = 3;
    private bool isMoving = false;
    private bool firstMove = true;

    public CombatCharacterInfo characterInfoCard;



    private void Start()
    {
        CombatSingleton.Instance.actionData.actions = this;
    }

    public void PreviewMovement()
    {
        CombatSingleton.Instance.actionData.ResetActionData();
        Character character = CombatSingleton.Instance.FocusCharacter.GetComponent<CombatCharacter>().myStats;

        if(character.actionsRemaining >= 2)
        {
            PathDisplay.MovemmentSquares(CombatSingleton.Instance.CursorCube, SHAPE.PLUS, character.speed*2, CUBEPHASE.DOUBLEMOVE);
        }

        PathDisplay.MovemmentSquares(CombatSingleton.Instance.CursorCube, SHAPE.PLUS, character.speed, CUBEPHASE.MOVE);
        CombatSingleton.Instance.CursorCube.GetComponent<Cube>().PreviousPhase = CUBEPHASE.ORIGINCUBE;

        CombatSingleton.Instance.actionData.Preview = PREVIEWMODE.Move;
    }

    public void PreviewAbility(int abilityID)
    {
        CombatSingleton.Instance.actionData.ResetActionData();
        JsonRetriever jsonRetriever = new JsonRetriever();
        Abilities ability = jsonRetriever.Load1Ability(abilityID);

        PathDisplay.AbilitySquares(CombatSingleton.Instance.CursorCube, ability);
        CombatSingleton.Instance.CursorCube.GetComponent<CubePhase>().BecomeCursor();

        if (CombatSingleton.Instance.actionData.Preview == PREVIEWMODE.Off)
        {
            CombatSingleton.Instance.actionData.OriginCharacter = CombatSingleton.Instance.FocusCharacter;
        }


        CombatSingleton.Instance.actionData.ChosenAbility = ability;

        if (ability.isSingleTarget)
        {

            CombatSingleton.Instance.actionData.Preview = PREVIEWMODE.Action;
        }
        else
        {

            CombatSingleton.Instance.actionData.Preview = PREVIEWMODE.ActionMulti;
        }
    }

    public void PreviewAIAbility(int abilityID)
    {
        //CombatSingleton.Instance.actionData.ResetActionData();
        JsonRetriever jsonRetriever = new JsonRetriever();
        Abilities ability = jsonRetriever.Load1Ability(abilityID);

        PathDisplay.AbilitySquares(CombatSingleton.Instance.CursorCube, ability);
        CombatSingleton.Instance.CursorCube.GetComponent<CubePhase>().BecomeCursor();

        if (CombatSingleton.Instance.actionData.Preview == PREVIEWMODE.Off)
        {
            CombatSingleton.Instance.actionData.OriginCharacter = CombatSingleton.Instance.FocusCharacter;
        }


        CombatSingleton.Instance.actionData.ChosenAbility = ability;

        if (ability.isSingleTarget)
        {

            CombatSingleton.Instance.actionData.Preview = PREVIEWMODE.Action;
        }
        else
        {

            CombatSingleton.Instance.actionData.Preview = PREVIEWMODE.ActionMulti;
        }
    }

    public void PreviewCapture()
    {

        CombatSingleton.Instance.actionData.ResetActionData();
        PathDisplay.CaptureSquares(CombatSingleton.Instance.CursorCube);

        if (CombatSingleton.Instance.actionData.Preview == PREVIEWMODE.Off)
        {
            CombatSingleton.Instance.actionData.OriginCharacter = CombatSingleton.Instance.FocusCharacter;
        }

        CombatSingleton.Instance.actionData.Preview = PREVIEWMODE.Capture;
    }

    public void PreviewItem(int id)
    {
        if (id != -1)
        {
            CombatSingleton.Instance.actionData.ResetActionData();
            JsonRetriever jsonRetriever = new JsonRetriever();
            Item item = jsonRetriever.Load1Item(id);

            PathDisplay.ItemSquares(CombatSingleton.Instance.CursorCube);
            if (CombatSingleton.Instance.actionData.Preview == PREVIEWMODE.Off)
            {
                CombatSingleton.Instance.actionData.OriginCharacter = CombatSingleton.Instance.FocusCharacter;
            }

            CombatSingleton.Instance.actionData.ChosenItem = item;
            CombatSingleton.Instance.actionData.Preview = PREVIEWMODE.Item;
        }

    }
    public void PreviewAIItem(int id)
    {
        if (id != -1)
        {
            //CombatSingleton.Instance.actionData.ResetActionData();
            JsonRetriever jsonRetriever = new JsonRetriever();
            Item item = jsonRetriever.Load1Item(id);

            PathDisplay.ItemSquares(CombatSingleton.Instance.CursorCube);
            if (CombatSingleton.Instance.actionData.Preview == PREVIEWMODE.Off)
            {
                CombatSingleton.Instance.actionData.OriginCharacter = CombatSingleton.Instance.FocusCharacter;
            }

            CombatSingleton.Instance.actionData.ChosenItem = item;
            CombatSingleton.Instance.actionData.Preview = PREVIEWMODE.Item;
        }

    }

    public void PreviewFacing()
    {

        CombatSingleton.Instance.actionData.ResetActionData();
        CombatSingleton.Instance.FocusCharacter.GetComponentInChildren<FacingArrows>().ArrowsActive();

        CombatSingleton.Instance.actionData.Preview = PREVIEWMODE.Facing;
        CombatSingleton.Instance.isUiOn = true;
    }


    private void Update()
    {
        if (isMoving)
        {
            move();
        }
    }

    private void move()
    {
       


        float stepp = speed * Time.deltaTime;
        float yIndex = path[0].transform.position.y + 0.5f;

        Transform character = CombatSingleton.Instance.FocusCharacter.transform;

        if (firstMove)
        {
            firstMove = false;
            character.gameObject.GetComponent<CombatCharacter>().FaceDirection(faceThisWay(
                character.gameObject.GetComponent<CombatCharacter>().myCube.transform.position, path[0].transform.position));
        }
        character.gameObject.GetComponent<Animator>().SetTrigger("Moving");


        Vector3 pathV3 = new Vector3(path[0].transform.position.x, character.position.y, path[0].transform.position.z);
        character.position = Vector3.MoveTowards(character.position, pathV3, stepp);
        character.position = new Vector3(character.position.x, yIndex, character.position.z);


        Vector2 characterV2 = new Vector2(character.position.x, character.position.z);
        Vector2 pathV2 = new Vector2(path[0].transform.position.x, path[0].transform.position.z);



        if (Vector2.Distance(characterV2, pathV2) < 0.0001f)
        {
            if (path.Count == 1)
            {
                character.gameObject.GetComponent<Animator>().ResetTrigger("Moving");
                PositionCharacterOnCube();
                isMoving = false;
                firstMove = true;
                if(CombatSingleton.Instance.battleSystem.CurrentTeam == 0)
                {
                    CombatSingleton.Instance.battleSystem.PlayerGainsControl();
                }
            }
            else
            {

                character.gameObject.GetComponent<CombatCharacter>().FaceDirection(faceThisWay(
                path[0].transform.position, path[1].transform.position));
            }


   
            path.RemoveAt(0);
        }
        
    }

    private float faceThisWay(Vector3 start, Vector3 end)
    {
        start.y = 0;
        end.y = 0;
        Vector3 dir = (start - end).normalized;

        float direction = 0;
        if(dir.x == 1)
        {
            direction = 270;
        }
        else if (dir.x == -1)
        {

            direction = 90;
        }
        else if (dir.z == 1)
        {

            direction = 180;
        }
        else if (dir.z == -1)
        {

            direction = 0;
        }

        return direction;
    }


    public void FaceWhichWay()
    {

        CombatSingleton.Instance.actionData.ResetActionData();
    } 

    public void Movement()
    {
        isMoving = true;
        path = Astar.AstarPath(CombatSingleton.Instance.FocusCharacter.GetComponent<CombatCharacter>().myCube, CombatSingleton.Instance.CursorCube, true);
        CubeManipulator.ResetAllCubes();
        if(CombatSingleton.Instance.battleSystem.CurrentTeam == 0)
        {
            CombatSingleton.Instance.battleSystem.PlayerLosesControl(5f);
        }
    }


    private void PositionCharacterOnCube()
    {
        float yIndex = path[0].transform.position.y + 0.5f;
        CombatSingleton.Instance.FocusCharacter.transform.position =
            new Vector3(path[0].transform.position.x, yIndex, path[0].transform.position.z);
        CombatSingleton.Instance.FocusCharacter.GetComponent<CombatCharacter>().NewCube(path[0]);
    }


    public void Capture()
    {
        CubeManipulator.ResetAllCubes();
        CombatSingleton.Instance.battleSystem.State = BATTLESTATE.ACTION;

        characterInfoCard.UpdateCard();
    }
    public void Abiliy()
    {
        CubeManipulator.ResetAllCubes();
        CombatSingleton.Instance.battleSystem.State = BATTLESTATE.ACTION;

        characterInfoCard.UpdateCard();
    }

    public void Item()
    {
        CubeManipulator.ResetAllCubes();
        CombatSingleton.Instance.battleSystem.State = BATTLESTATE.ACTION;

        characterInfoCard.UpdateCard();
    }
    public void Special()
    {
        CubeTrigger cubeTrigger = 
            CombatSingleton.Instance.FocusCharacter.GetComponent<CombatCharacter>().myCube.GetComponent<CubeTrigger>();

        if (cubeTrigger != null)
        {
            if (!cubeTrigger.isSingleUse)
            {
                activateTrigger(cubeTrigger);
            }
            else
            {
                if (!cubeTrigger.IsUsed())
                {
                    activateTrigger(cubeTrigger);
                }
            }
        }
    }

    private void activateTrigger(CubeTrigger cubeTrigger)
    {

        CombatSingleton.Instance.actionData.ResetActionData();
        cubeTrigger.ActivateTriggers();
        CharacterManipulator.RemoveActionPoints(CombatSingleton.Instance.FocusCharacter, 1);
    }
}
