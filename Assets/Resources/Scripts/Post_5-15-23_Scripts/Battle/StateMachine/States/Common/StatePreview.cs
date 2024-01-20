using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatePreview
{


    public static bool isInRangeOfAnyAbility(GameObject enemy, List<Abilities> ability, CombatCharacter character)
    {
        foreach(Abilities abil in ability)
        {
            if(isInRange(enemy, abil, character.myCube))
            {
                return true;
            }
        }

        return false;
    }


    public static bool isInRangeOfAnyAbilityIfMoved(GameObject enemy, List<Abilities> ability, CombatCharacter character)
    {
        List<GameObject> moveCubes = StateCubeInfo.CubesWithinMoveRange(character);

        foreach (GameObject square in moveCubes)
        {
            foreach (Abilities abil in ability)
            {
                if (isInRange(enemy, abil, square))
                {
                    return true;
                }
            }

        }

        return false;
    }



    public static bool isInRange(GameObject enemy, Abilities ability, GameObject originCube)
    {

        List<GameObject> list = FindActionSquares.FindAllActionSquares(originCube,
            FindActionSquares.StringToShape(ability.shape), ability.maxDistance);
        GameObject enemyCube = enemy.GetComponent<CombatCharacter>().myCube;

       // Debug.Log("list Count: " + list.Count);


        foreach (GameObject square in list)
        {
            if (square == enemyCube)
            {
                //Debug.Log("Square | enemy cube: " + square.name + " : " + enemyCube.name);
                return true;
            }
        }

        return false;
    }

    public static bool isInRange(GameObject enemy, int distance, GameObject originCube)
    {
        List<GameObject> list = FindActionSquares.FindAllActionSquares(originCube, SHAPE.PLUS, distance);
        GameObject enemyCube = enemy.GetComponent<CombatCharacter>().myCube;

        foreach (GameObject square in list)
        {
            if (square == enemyCube)
            {
                return true;
            }
        }

        return false;
    }

    public static bool isInRangeIfMoved(GameObject enemy, Abilities ability, CombatCharacter character)
    {
        //Debug.Log("If I moved");
        List<GameObject> moveCubes = StateCubeInfo.CubesWithinAttackRange(character, ability);
        GameObject enemyCube = enemy.GetComponent<CombatCharacter>().myCube;

        foreach (GameObject square in moveCubes)
        {
            if (square == enemyCube)
            {
                return true;
            }
        }

        return false;
    }

    public static bool isInRangeIfMoved(GameObject enemy, int distance, CombatCharacter character)
    {
        List<GameObject> moveCubes = StateCubeInfo.CubesWithinAttackRange(character, distance);
        GameObject enemyCube = enemy.GetComponent<CombatCharacter>().myCube;

        foreach (GameObject square in moveCubes)
        {
            if (square == enemyCube)
            {
                return true;
            }
        }

        return false;
    }


    //current philosphy is the character should favor being close to the character
    //that they are healing. 
    public static GameObject BestSquareToMoveToHeal(GameObject ally, CombatCharacter character)
    {
        List<GameObject> moveCubes = StateCubeInfo.CubesWithinMoveRange(character);
        GameObject allyCube = ally.GetComponent<CombatCharacter>().myCube;


        GameObject BestCube = new GameObject();
        int shortestDistance = 100;

        foreach (GameObject square in moveCubes)
        {
            List<GameObject> path = Astar.AstarPath(square, allyCube, true);
            if (path.Count < shortestDistance)
            {
                shortestDistance = path.Count;
                BestCube = square;
            }
        }

        return BestCube;
    }

    // prefer height then beind. If ranged, prefer height, distance, then behind
    public static GameObject BestSquareToMoveToAttack(GameObject target, Abilities ability, CombatCharacter character)
    {
        List<GameObject> moveCubes = StateCubeInfo.CubesWithinMoveRange(character);
        moveCubes.Add(character.myCube);
        GameObject targetCube = target.GetComponent<CombatCharacter>().myCube;
        List<GameObject> cubesThatCanHit = new List<GameObject>();
        List<GameObject> highestHitCubes = new List<GameObject>();
        List<GameObject> farCubes = new List<GameObject>();
        List<GameObject> behindCubes = new List<GameObject>();

        bool isRanged = ability.maxDistance > 1;
        float height = 0;

       // Debug.Log("Move cubes count: " + moveCubes.Count);

        for (int i = 0; i < moveCubes.Count; i++)
        {
            if (isInRange(target, ability, moveCubes[i]))
            {
               // Debug.Log("Added cube: " + moveCubes[i].name);
                cubesThatCanHit.Add(moveCubes[i]);
            }
        }
       // Debug.Log("cubes That Can Hit count: " + cubesThatCanHit.Count);

        for (int i = 0; i < cubesThatCanHit.Count; i++)
        {
            if(cubesThatCanHit[i].transform.position.y > height)
            {
                height = cubesThatCanHit[i].transform.position.y;
            }
        }

        height = Mathf.Floor(height);
/*
        for (int i = 0; i < cubesThatCanHit.Count; i++)
        {
            if (cubesThatCanHit[i].transform.position.y > height)
            {
                height = cubesThatCanHit[i].transform.position.y;
            }
        }
*/


        for (int i = 0; i < cubesThatCanHit.Count; i++)
        {
            if (cubesThatCanHit[i].transform.position.y >= height)
            {
                highestHitCubes.Add(cubesThatCanHit[i]);
            }
        }

       // Debug.Log("Height cubes count: " + highestHitCubes.Count);
        if (isRanged)
        {
            //Debug.Log("ranged");
            float distance = 0;
            for (int i = 0; i < highestHitCubes.Count; i++)
            {
                if (Vector3.Distance(target.gameObject.transform.position, highestHitCubes[i].transform.position) > distance)
                {
                    distance = Vector3.Distance(target.gameObject.transform.position, highestHitCubes[i].transform.position);
                }
            }
            distance = Mathf.Floor(distance);


            for (int i = 0; i < highestHitCubes.Count; i++)
            {
                if (Vector3.Distance(target.gameObject.transform.position, highestHitCubes[i].transform.position) >= distance)
                {
                    farCubes.Add(highestHitCubes[i]);
                }
            }


            //Debug.Log("Far cubes count: " + farCubes.Count);
            behindCubes = BehindCubes(farCubes, target);



           // Debug.Log("Behind cube count: " + behindCubes.Count);
          /*  if (behindCubes.Count > 0)
            {
                Debug.Log("return behind");

                if (behindCubes.Count == 0) return null;

                return FindFarthestCube(behindCubes, character);
            }
            else
            {*/
                if (farCubes.Count == 0) return null;
              //  Debug.Log("return far");
                return FindFarthestCube(farCubes, character);
            //}
        }
        else
        {

            //Debug.Log("melee");
            behindCubes = BehindCubes(highestHitCubes, target);

           // Debug.Log("Behind cube count: " + behindCubes.Count);
            if (behindCubes.Count > 0)
            {


                if (behindCubes.Count == 0) return null;
               // Debug.Log("return behind");
                return FindClosestCube(behindCubes, character);
            }
            else
            {
               // Debug.Log("Height cubes count: " + highestHitCubes.Count);
                //Debug.Log("return high");
                if (highestHitCubes.Count == 0) return null;
                return FindClosestCube(highestHitCubes, character);
            }
        }

    }


    private static GameObject FindFarthestCube(List<GameObject> cubes, CombatCharacter character)
    {
        GameObject farthestcube = cubes[0];
        //Debug.Log("cube count: " + cubes.Count);
        for (int i = 0; i < cubes.Count; i++)
        {
            float dist1 = Vector3.Distance(character.gameObject.transform.position, farthestcube.transform.position);
            float dist2 = Vector3.Distance(character.gameObject.transform.position, cubes[i].transform.position);

            if (dist2 > dist1)
            {
                farthestcube = cubes[i];
            }
        }

        return farthestcube;
    }
    private static GameObject FindClosestCube(List<GameObject> cubes, CombatCharacter character)
    {
        GameObject closest = cubes[0];
        //Debug.Log("cube count: " + cubes.Count);
        for (int i = 0; i < cubes.Count; i++)
        {
            float dist1 = Vector3.Distance(character.gameObject.transform.position, closest.transform.position);
            float dist2 = Vector3.Distance(character.gameObject.transform.position, cubes[i].transform.position);

            if (dist2 < dist1)
            {
                closest = cubes[i];
            }
        }

        return closest;
    }

    private static List<GameObject> BehindCubes(List<GameObject> list, GameObject target)
    {
        List<GameObject> behindCubes = new List<GameObject>();

        Vector3 direction = -target.transform.forward;
        bool isX = false;
        bool isNegative = false;
        float distance = 0;


        if (direction.x > 0.7)
        {
            isX = true;
        }
        if (direction.x < -0.7)
        {
            isNegative = true;
            isX = true;
        }
        if (direction.z < -0.7)
        {
            isNegative = true;
            isX = true;
        }
        if (direction.z > 0.7)
        {
            isX = true;
        }

        

        for (int i = 0; i < list.Count; i++)
        {
            if (isX)
            {
                if (isNegative)
                {
                    if (list[i].transform.position.x < (target.transform.position.x - 0.5f))
                    {
                        behindCubes.Add(list[i]);
                    }
                }
                else
                {
                    if (list[i].transform.position.x > (target.transform.position.x + 0.5f))
                    {
                        behindCubes.Add(list[i]);
                    }
                }
            }
            else
            {
                if (isNegative)
                {
                    if (list[i].transform.position.y < (target.transform.position.y - 0.5f))
                    {
                        behindCubes.Add(list[i]);
                    }
                }
                else
                {
                    if (list[i].transform.position.y > (target.transform.position.y + 0.5f))
                    {
                        behindCubes.Add(list[i]);
                    }
                }
            }
        }

        return behindCubes;

    }
}
