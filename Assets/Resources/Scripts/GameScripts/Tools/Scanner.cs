using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public static class Scanner 
{
    
    public static void ChangeLayers(GameObject changeMe, int newLayer)
    {
        changeMe.layer = newLayer;

        RecursiveLayerLoop(changeMe, newLayer);
    }

    private static void RecursiveLayerLoop(GameObject changeMe, int newLayer)
    {
        for (int i = 0; i < changeMe.transform.childCount; i++)
        {
            GameObject child = changeMe.transform.GetChild(i).gameObject;
            if(child.transform.childCount > 0)
            {
                RecursiveLayerLoop(child, newLayer);
            }
            child.layer = newLayer;
        }
    }
    public static void AddNavObstacle(GameObject changeMe)
    {
        changeMe.AddComponent<NavMeshObstacle>();
        changeMe.GetComponent<NavMeshObstacle>().carving = true;

        RecursiveNavObstacleLoop(changeMe);
    }

    private static void RecursiveNavObstacleLoop(GameObject changeMe)
    {
        for (int i = 0; i < changeMe.transform.childCount; i++)
        {
            GameObject child = changeMe.transform.GetChild(i).gameObject;
            child.AddComponent<NavMeshObstacle>();
            changeMe.GetComponent<NavMeshObstacle>().carving = true;
            if (child.transform.childCount > 0)
            {
                RecursiveNavObstacleLoop(child);
            }
        }
    }
}
