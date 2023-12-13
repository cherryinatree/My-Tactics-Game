using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AddingNav
{
   
    public void addNavSurface(GameObject addToMe)
    {
        addToMe.AddComponent(typeof( NavMeshSurface));
    }
}
