using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Astar 
{



    public static bool WithAbilityRange(bool isMove)
    {

        List<GameObject> path = Astar.AstarPath(CombatSingleton.Instance.FocusCharacter.GetComponent<CombatCharacter>().myCube, 
            CombatSingleton.Instance.CursorCube, isMove);
        if (path.Count <= CombatSingleton.Instance.actionData.ChosenAbility.multiTargetRange)
        {
            return true;
        }

        return false;
    }







    /********************************************************************************
    * 
    * 
    * 
    * 
    *                      A* pathfinding
    * 
    * 
    * 
    * 
    * ******************************************************************************/


    public static List<GameObject> AstarPath(GameObject start, GameObject target, bool isMove)
   {

       List<GameObject> toSearch = new List<GameObject> { start };
       List<GameObject> completed = new List<GameObject>();

       while (toSearch.Count > 0)
       {
           GameObject currentCube = GetLowestFCostNode(toSearch);
           toSearch.Remove(currentCube);
           completed.Add(currentCube);

           if (currentCube == target)
           {
               return GetFinishedList(start, target);
           }

           List<GameObject> neighbors = new List<GameObject>();
            if (isMove)
            {
                neighbors = currentCube.GetComponent<CubeNeighbors>().FindNeighborCubes(SHAPE.PLUS);
            }
            else
            {

                neighbors = currentCube.GetComponent<CubeNeighbors>().FindAllNeighborCubes(SHAPE.PLUS);
            }

           foreach (GameObject item in neighbors)
           {
               if (completed.Contains(item))
               {
                   continue;
               }

               item.GetComponent<CubeAstar>().CalculateGandH(start, target, item);


               item.GetComponent<CubeAstar>().PreviousCube = currentCube;

               if (!toSearch.Contains(item))
               {
                   toSearch.Add(item);
               }
           }

       }

       return new List<GameObject>();
   }

   private static GameObject GetLowestFCostNode(List<GameObject> toSearch)
   {
       GameObject lowestFCostNode = toSearch[0];

       for (int i = 1; i < toSearch.Count; i++)
       {
           if (toSearch[i].GetComponent<CubeAstar>().F < lowestFCostNode.GetComponent<CubeAstar>().F)
               lowestFCostNode = toSearch[i];
       }

       return lowestFCostNode;
   }

   private static List<GameObject> GetFinishedList(GameObject start, GameObject end)
   {
       List<GameObject> finishedList = new List<GameObject>();

       GameObject currentCube = end;

       while (currentCube != start)
       {
           finishedList.Add(currentCube);
           currentCube = currentCube.GetComponent<CubeAstar>().PreviousCube;
       }
       finishedList.Reverse();
       return finishedList;
   }

}
