using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CubeManipulator 
{

    public static void ResetAllCubes()
    {
        foreach(Cube cube in CombatSingleton.Instance.Cubes)
        {
            cube.AstarReset();
        }
    }

    public static void ChangeCursorCube(Vector3 diection)
    {
        GameObject cube = CubeRetriever.FindCubeInDirection(CombatSingleton.Instance.CursorCube, diection);
        if (cube != null)
        {
            CombatSingleton.Instance.CursorCube.GetComponent<Cube>().NoLongerCursor();
            cube.GetComponent<Cube>().BecomeCursor();
        }
    }

}
