using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GamingTools;
using UnityEngine.AI;


public class CharacterController: MonoBehaviour
{
    public Stats myStats;
    private bool isDead = false;

    void Start()
    {
        if(myStats == null)
        {
            myStats = new Stats();
        }
        gameObject.AddComponent<NavMeshAgent>();
        gameObject.GetComponent<NavMeshAgent>().radius = 0.4f;
        gameObject.GetComponent<NavMeshAgent>().speed = 3f;
        gameObject.AddComponent<BoxCollider>();
        gameObject.AddComponent<LineRenderer>();
        gameObject.GetComponent<LineRenderer>().material = ResourseLoader.GetMaterial("Materials/BlueLine");
        gameObject.GetComponent<LineRenderer>().SetPosition(1, new Vector3(0, 0, 0));
    }

    private void Update()
    {
        if (!isDead)
        {
            if (GetComponent<NavMeshAgent>().remainingDistance > 0.3)
            {
                GetComponent<Animator>().SetBool("Moving", true);
            }
            else
            {
                GetComponent<Animator>().SetBool("Moving", false);
            }
            if (myStats.health == 0)
            {
                GetComponent<Animator>().SetBool("Dead", true);
                Death.RemoveGameObject(gameObject);
                isDead = true;
            }
        }
    }
}
