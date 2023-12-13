using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestButtons : MonoBehaviour
{

    public CombatControlsEnable enact;
    // Start is called before the first frame update
    void Start()
    {

        enact = new CombatControlsEnable();
    }

    // Update is called once per frame
    void Update()
    {
        TestButtonList();
    }


    private void TestButtonList()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            enact.Key("testTurnChange");
        }
    }
}
