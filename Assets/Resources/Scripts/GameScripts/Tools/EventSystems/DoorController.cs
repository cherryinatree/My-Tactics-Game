using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{

    public int id;
    private bool isClosed = true;

    // Start is called before the first frame update
    void Start()
    {
        GameEvents.current.onDoorwayTriggerEnter += OnDoorwayOpen;
    }

    private void OnDoorwayOpen(int id)
    {
        if (id == this.id)
        {
            if (isClosed)
            {

                gameObject.transform.Rotate(new Vector3(0, -90, 0));
                isClosed = false;
            }
            else
            {

                gameObject.transform.Rotate(new Vector3(0, 90, 0));
                isClosed = true;
            }
        }
    }
}
