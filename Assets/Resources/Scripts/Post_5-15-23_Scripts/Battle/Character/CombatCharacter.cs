using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatCharacter : MonoBehaviour
{

    public int team = 0;
    public GameObject myCube;
    private Vector3 cubePosition;
    public Character myStats;
    public bool CombatAware = false;
    public List<GameObject> KnownEnemies = new List<GameObject>();


    private void Update()
    {
        if(myCube != null)
        {
            if(cubePosition != myCube.transform.position)
            {
                CubePosition();
            }
        }
    }


    public void CubePosition()
    {
        cubePosition = myCube.transform.position;
        transform.position = new Vector3(myCube.transform.position.x, myCube.transform.position.y + 0.5f, myCube.transform.position.z);
    }

    public void NewCube(GameObject newCube)
    {
        if(myCube != null)
        {
            myCube.GetComponent<Cube>().MyType = GROUNDTYPE.Ground;
        }
        myCube = newCube;
        myCube.GetComponent<Cube>().MyType = GROUNDTYPE.Occupied;
        CubePosition();
    }

    public void SetStats(Character stats, int team)
    {
        this.myStats = stats;
        this.team = team;
    }

    public void ResetActions()
    {
        myStats.actionsRemaining = myStats.maxActions;
    }

    public void ResetCharacter()
    {

        myStats.actionsRemaining = myStats.maxActions;
        myStats.currentMana = myStats.maxMana;
        myStats.currentHealth = myStats.maxHealth;
    }

    public void FaceDirection(float facing)
    {
        transform.Rotate(new Vector3(0, amountToRotate(facing), 0));
        myStats.facing = facing;
    }

    private float amountToRotate(float facing)
    {
        float turnAmount;
        float currentY = transform.eulerAngles.y;


        if (currentY == facing)
        {

            turnAmount = 0;
        }
        else
        {
            turnAmount = facing - currentY;
        }

        return turnAmount;
    }
}
