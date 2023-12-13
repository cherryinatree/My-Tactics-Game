using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleMono : MonoBehaviour
{

    private float timeRemaining = 5;
    public GameObject defender;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
        }else
        {
            Destroy(gameObject);
        }
        if (defender != null)
        {
            Vector3 defenPosition = new Vector3(defender.transform.position.x, defender.transform.position.y + 1, defender.transform.position.z);
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, defenPosition, 0.1f);
        }

    }
}
