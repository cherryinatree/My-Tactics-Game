using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StateCubeInfo 
{

    public static List<GameObject> CubesWithinMoveRange(CombatCharacter character)
    {
        return FindActionSquares.FindTheActionSquares(character.myCube, SHAPE.PLUS, character.myStats.speed);
    }


    public static GameObject ClosestCubeDoubleMove(CombatCharacter character, GameObject target)
    {
        List<GameObject> doubleMoveSquares = FindActionSquares.FindTheActionSquares(character.myCube, SHAPE.PLUS, (character.myStats.speed * 2));

        GameObject BestCube = doubleMoveSquares[0];
        int distance = 1000;

        CubeNeighbors neighbors = target.GetComponent<CombatCharacter>().myCube.GetComponent<CubeNeighbors>();
        List<GameObject> destination = neighbors.FindNeighborCubes(SHAPE.PLUS);
        List<GameObject> destinationWalkable = new List<GameObject>();

        for (int x = 0; x < destination.Count; x++)
        {
            if (destination[x].GetComponent<CubePhase>().type == GROUNDTYPE.Ground)
            {
                if (destination[x] != null)
                {
                    destinationWalkable.Add(destination[x]);
                }
            }
        }

        GameObject destinationCube = destinationWalkable[0];
        /*
        for (int i = 0; i < destinationWalkable.Count; i++)
        {
            float distance1 = Vector3.Distance(destinationCube.transform.position, character.transform.position);
            float distance2 = Vector3.Distance(destinationWalkable[i].transform.position, character.transform.position);


            if (distance2 < distance1)
            {
                destinationCube = destinationWalkable[i];
            }
        }*/

        List<GameObject> canWalkTo = new List<GameObject>();

        for (int i = 0; i < doubleMoveSquares.Count; i++)
        {
            for (int x = 0; x < destinationWalkable.Count; x++)
            {
                if(doubleMoveSquares[i] == destinationWalkable[x])
                {
                    canWalkTo.Add(destinationWalkable[x]);
                }
            }
        }

        if(canWalkTo.Count > 0)
        {
            destinationCube = canWalkTo[0];
            for (int i = 0; i < canWalkTo.Count; i++)
            {
                float distance1 = Vector3.Distance(destinationCube.transform.position, character.transform.position);
                float distance2 = Vector3.Distance(canWalkTo[i].transform.position, character.transform.position);


                if (distance2 < distance1)
                {
                    destinationCube = canWalkTo[i];
                }
            }
        }
        else
        {

            for (int i = 0; i < destinationWalkable.Count; i++)
            {
                float distance1 = Vector3.Distance(destinationCube.transform.position, character.transform.position);
                float distance2 = Vector3.Distance(destinationWalkable[i].transform.position, character.transform.position);


                if (distance2 < distance1)
                {
                    destinationCube = destinationWalkable[i];
                }
            }
        }



        for (int i = 0; i < doubleMoveSquares.Count; i++)
        {
            List<GameObject> path = Astar.AstarPath(doubleMoveSquares[i],
            destinationCube, true);

            //Debug.Log("path / distance: " + path.Count + " | " + distance);
            if (path.Count < distance)
            {
                //Debug.Log("path count" + path.Count);
                distance = path.Count;
                BestCube = doubleMoveSquares[i];
            }
        }

        return BestCube;
    }
    public static GameObject ClosestCubeSingleMove(CombatCharacter character, GameObject target)
    {
        List<GameObject> moveSquares = FindActionSquares.FindTheActionSquares(character.myCube, SHAPE.PLUS, (character.myStats.speed));

       // Debug.Log("single move count: " + moveSquares.Count);

        //Debug.Log("Closest character:" + target.name);
        GameObject BestCube = moveSquares[0];
        int distance = 1000;

        CubeNeighbors neighbors = target.GetComponent<CombatCharacter>().myCube.GetComponent<CubeNeighbors>();
        List<GameObject> destination = neighbors.FindNeighborCubes(SHAPE.PLUS);
        List<GameObject> destinationWalkable = new List<GameObject>();

        for (int x = 0; x < destination.Count; x++)
        {
            if (destination[x].GetComponent<CubePhase>().type == GROUNDTYPE.Ground)
            {
                if (destination[x] != null)
                {
                    destinationWalkable.Add(destination[x]);
                }
            }
        }
       // Debug.Log("dest walk count: " + destinationWalkable.Count);
       // Debug.Log("dest walk name" + destinationWalkable[0].name);

        GameObject destinationCube = destinationWalkable[0];
        /*
        for (int i = 0; i < destinationWalkable.Count; i++)
        {
            float distance1 = Vector3.Distance(destinationCube.transform.position, character.transform.position);
            float distance2 = Vector3.Distance(destinationWalkable[i].transform.position, character.transform.position);


            if (distance2 < distance1)
            {
                destinationCube = destinationWalkable[i];
            }
        }*/

        List<GameObject> canWalkTo = new List<GameObject>();

        for (int i = 0; i < moveSquares.Count; i++)
        {
            for (int x = 0; x < destinationWalkable.Count; x++)
            {
                if (moveSquares[i] == destinationWalkable[x])
                {
                    canWalkTo.Add(destinationWalkable[x]);
                }
            }
        }
       // Debug.Log("can walk to count: " + canWalkTo.Count);
        if (canWalkTo.Count > 0)
        {
            destinationCube = canWalkTo[0];
            for (int i = 0; i < canWalkTo.Count; i++)
            {
                float distance1 = Vector3.Distance(destinationCube.transform.position, character.transform.position);
                float distance2 = Vector3.Distance(canWalkTo[i].transform.position, character.transform.position);


                if (distance2 < distance1)
                {
                    destinationCube = canWalkTo[i];
                }
            }
        }
        else
        {

            for (int i = 0; i < destinationWalkable.Count; i++)
            {
                float distance1 = Vector3.Distance(destinationCube.transform.position, character.transform.position);
                float distance2 = Vector3.Distance(destinationWalkable[i].transform.position, character.transform.position);


                if (distance2 < distance1)
                {
                    destinationCube = destinationWalkable[i];
                }
            }
        }



        for (int i = 0; i < moveSquares.Count; i++)
        {
            List<GameObject> path = Astar.AstarPath(moveSquares[i],
            destinationCube, true);

            //Debug.Log("path / distance: " + path.Count + " | " + distance);
            if (path.Count < distance)
            {
                //Debug.Log("path count" + path.Count);
                distance = path.Count;
                BestCube = moveSquares[i];
            }
        }

        return BestCube;





        /* GameObject BestCube = moveSquares[0];
         int distance = 1000;


         for (int i = 0; i < moveSquares.Count; i++)
         {
             List<GameObject> path = Astar.AstarPath(moveSquares[i],
             target.GetComponent<CombatCharacter>().myCube, true);

             if (path.Count < distance)
             {
                 distance = path.Count;
                 BestCube = moveSquares[i];
             }
         }

         return BestCube;*/
    }




    public static List<GameObject> CubesWithinAttackRange(CombatCharacter character, Abilities ability)
    {
        return FindActionSquares.FindAllActionSquares(character.myCube,
            FindActionSquares.StringToShape(ability.shape), (character.myStats.speed + ability.maxDistance));
    }
    public static List<GameObject> CubesWithinAttackRange(CombatCharacter character, int distance)
    {
        return FindActionSquares.FindTheActionSquares(character.myCube,SHAPE.PLUS, (character.myStats.speed + distance));
    }

    public static bool IsThereAPath(CombatCharacter character, GameObject target)
    {
        List<GameObject> path = Astar.AstarPath(character.GetComponent<CombatCharacter>().myCube, 
            target, true);

        if (path == null || path.Count <= 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    public static bool IsThereAPathToHit(CombatCharacter character, GameObject target)
    {
        List<GameObject> path = Astar.AstarPath(character.GetComponent<CombatCharacter>().myCube,
            target.GetComponent<CombatCharacter>().myCube, false);

        if (path == null || path.Count !> 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public static List<GameObject> CharactersThatCanBeAttacked(CombatCharacter character, Abilities ability)
    {
        List<GameObject> list = FindActionSquares.FindAllActionSquares(character.myCube, 
            FindActionSquares.StringToShape(ability.shape), (character.myStats.speed+ability.maxDistance));

        List<GameObject> attackableCharacters = new List<GameObject>();

        for(int i = 0; i < list.Count; i++)
        {
            for (int x = 0; x < CombatSingleton.Instance.Combatants.Count; x++)
            {
                if(CombatSingleton.Instance.Combatants[x].GetComponent<CombatCharacter>().team != character.team)
                {
                    if(CombatSingleton.Instance.Combatants[x].GetComponent<CombatCharacter>().myCube == list[i])
                    {
                        attackableCharacters.Add(CombatSingleton.Instance.Combatants[x]);
                    }
                }
            }
        }
        return attackableCharacters;
    }

    public static List<Abilities> WhichAbilitiesCanHit(List<Abilities> abilities, CombatCharacter character, GameObject target)
    {
        List<Abilities> updatedAbilites = new List<Abilities>();

        GameObject targetCube = target.GetComponent<CombatCharacter>().myCube;

        for (int x = 0; x < abilities.Count; x++)
        {

            List<GameObject> list = FindActionSquares.FindAllActionSquares(character.myCube,
                FindActionSquares.StringToShape(abilities[x].shape), abilities[x].maxDistance);

            foreach (GameObject a in list)
            {
                if(a == targetCube)
                {
                    updatedAbilites.Add(abilities[x]);
                    break;
                }
            }
        }

        return updatedAbilites;
    } 


    public static bool CanIHitMultipleEnemiesIfMoved(Abilities ability, List<GameObject> enemies, CombatCharacter character)
    {
        List<GameObject> moveCubes = CubesWithinMoveRange(character);
        int enemiesHit = 0;

        for (int i = 0; i < moveCubes.Count; i++)
        {
            int currentHit = HowManyEnemiesHitAtThisCube(ability, enemies, moveCubes[i]);
            if (currentHit > enemiesHit)
            {
                enemiesHit = currentHit;
            }
        }
        if(enemiesHit > 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    public static bool CanIHitMultipleEnemiesIfStayStill(Abilities ability, List<GameObject> enemies, CombatCharacter character)
    {
        //int enemiesHit = 0;

        int currentHit = HowManyEnemiesHitAtThisCube(ability, enemies, character.myCube);
        
        if (currentHit > 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    public static GameObject FromWhichCubeWouldTheMostBeHit(Abilities ability, List<GameObject> enemies, CombatCharacter character)
    {
        List<GameObject> moveCubes = CubesWithinMoveRange(character);
        List<GameObject> cubesThatHitTheMost = new List<GameObject>();
        GameObject bestCube = moveCubes[0];
        int enemiesHit = 0;

        Debug.Log("Ability max: " + ability.maxDistance);
        Debug.Log("move cubes count: " + moveCubes.Count);
        for (int i = 0; i < moveCubes.Count; i++)
        {
            int currentHit = HowManyEnemiesHitAtThisCube(ability, enemies, moveCubes[i]);

            if (currentHit > enemiesHit)
            {
                Debug.Log("How many enemies hit: " + currentHit);
                enemiesHit = currentHit;
            }
        }

        for (int i = 0; i < moveCubes.Count; i++)
        {
            int currentHit = HowManyEnemiesHitAtThisCube(ability, enemies, moveCubes[i]);
            if (currentHit == enemiesHit)
            {
                cubesThatHitTheMost.Add(moveCubes[i]);
            }
        }
        Debug.Log("cubes that hit the most count: " + cubesThatHitTheMost.Count);
        bestCube = cubesThatHitTheMost[0];
        float distance = 0;

        for (int i = 0; i < cubesThatHitTheMost.Count; i++)
        {
            float dist1 = Vector3.Distance(enemies[0].transform.position, bestCube.transform.position);
            float dist2 = Vector3.Distance(enemies[0].transform.position, cubesThatHitTheMost[i].transform.position);


            if (dist1 < dist2)
            {
                bestCube = cubesThatHitTheMost[i];
            }
        }


        return bestCube;
    }

    public static int HowManyEnemiesHitAtThisCube(Abilities ability, List<GameObject> enemies, GameObject originCube)
    {
        int distance = 0;

        if (ability.isSingleTarget)
        {
            distance = ability.maxDistance;
        }
        else
        {
            distance = ability.multiTargetRange;
        }


        List<GameObject> list = FindActionSquares.FindAllActionSquares(originCube,
            FindActionSquares.StringToShape(ability.shape), distance);

        GameObject bestCube = list[0];

        int enemiesHit = 0;

        for (int i = 0; i < list.Count; i++)
        {
            int currentHit = CharactersHitAtThisSquareWithMultiTargetAbility(ability, enemies, list[i]).Count;
            if (currentHit > enemiesHit)
            {
                enemiesHit = currentHit;
            }
        }

        return enemiesHit;
    }

    public static GameObject WhichCubeWouldTheMostEnemiesBeHit(Abilities ability, List<GameObject> enemies, GameObject originCube)
    {
        int distance = 0;

        if (ability.isSingleTarget)
        {
            distance = ability.maxDistance;
        }
        else
        {
            distance = ability.multiTargetRange;
        }


        List<GameObject> list = FindActionSquares.FindAllActionSquares(originCube,
            FindActionSquares.StringToShape(ability.shape), distance);

        GameObject bestCube = list[0];

        int enemiesHit = 0;

        for (int i = 0; i < list.Count; i++)
        {
            int currentHit = CharactersHitAtThisSquareWithMultiTargetAbility(ability, enemies, list[i]).Count;
            if (currentHit > enemiesHit)
            {
                bestCube = list[i];
                enemiesHit = currentHit;
            }
        }

        return bestCube;
    }

    public static List<GameObject> CharactersHitAtThisSquareWithMultiTargetAbility(Abilities ability, 
        List<GameObject> enemies, GameObject originCube)
    {
        List<GameObject> squares = PathDisplay.AbilitySquares(originCube, ability);
        List<GameObject> enemiesHit = new List<GameObject>();

        for (int i = 0; i < squares.Count; i++)
        {
            for (int x = 0; x < enemies.Count; x++)
            {

                if (squares[i] == enemies[x].GetComponent<CombatCharacter>().myCube)
                {
                    enemiesHit.Add(enemies[x]);
                }
            }
        }
        return enemiesHit;
    }
}
