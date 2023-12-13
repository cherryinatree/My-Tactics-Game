using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatPanelSubscribe : MonoBehaviour
{
    public bool isActivatedAtStart = false;
    private static bool hasReset = false;
    // Start is called before the first frame update
    void Awake()
    {

        if(CombatSingleton.Instance.Panels == null || !hasReset)
        {
            CombatSingleton.Instance.Panels = new List<GameObject>();
            hasReset = true;
        }
        CombatSingleton.Instance.Panels.Add(gameObject);

        if (!isActivatedAtStart)
        {
            gameObject.SetActive(false);
        }
    }

    private void Start()
    {

        hasReset = false;
    }

}
