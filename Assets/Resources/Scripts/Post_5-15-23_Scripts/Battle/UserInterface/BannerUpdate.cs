using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BannerUpdate : MonoBehaviour
{

    public GameObject Banner;
    public Text BannerText;
    private BATTLESTATE state;

    // Start is called before the first frame update
    void Start()
    {
        state = BATTLESTATE.START;
        Banner.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        TurnChange();
    }

    private void TurnChange()
    {
        if(state != CombatSingleton.Instance.battleSystem.State)
        {
            state = CombatSingleton.Instance.battleSystem.State;
            if (state == BATTLESTATE.TURNCHANGE)
            {
                Banner.SetActive(true);
                if(CombatSingleton.Instance.battleSystem.CurrentTeam == 0)
                {
                    BannerText.text = "Your Turn";
                }
                else
                {
                    BannerText.text = "Enemy Turn";
                }
            }
            else
            {

                Banner.SetActive(false);
            }
        }
    }
}
