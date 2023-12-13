using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterActions
{

    public void Movement(GameObject focusOnMe)
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            if (Vector3.Distance(hit.point, focusOnMe.transform.position) < focusOnMe.GetComponent<CharacterController>().myStats.speed)
            {
                NavMeshPath path = new NavMeshPath();
                if (NavMesh.CalculatePath(focusOnMe.transform.position, hit.point, NavMesh.AllAreas, path))
                {

                    List<Vector3> corners = new List<Vector3>();
                    focusOnMe.GetComponent<LineRenderer>().positionCount = path.corners.Length;
                    if (path.corners.Length > 1)
                        focusOnMe.GetComponent<LineRenderer>().SetPositions(path.corners);

                    if (Input.GetMouseButtonDown(0) && path.corners.Length > 1)
                    {
                        focusOnMe.GetComponent<NavMeshAgent>().SetDestination(hit.point);
                        focusOnMe.GetComponent<LineRenderer>().positionCount = 0;
                        focusOnMe.GetComponent<CharacterController>().myStats.actionsLeft -= 1;
                    }
                }
            }
        }
    }
}
