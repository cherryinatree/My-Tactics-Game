using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CombatPanelManipulator
{


    public static void ActivatePanel(string activateMe)
    {

        for (int i = 0; i < CombatSingleton.Instance.Panels.Count; i++)
        {
            if (CombatSingleton.Instance.Panels[i].name == activateMe)
            {
                CombatSingleton.Instance.Panels[i].SetActive(true);
            }
        }
    }
    public static void DeactivatePanel(string activateMe)
    {

        for (int i = 0; i < CombatSingleton.Instance.Panels.Count; i++)
        {
            if (CombatSingleton.Instance.Panels[i].name == activateMe)
            {
                CombatSingleton.Instance.Panels[i].SetActive(false);
            }
        }
    }
    public static void ActivatePanel(int activateMe)
    {

        for (int i = 0; i < CombatSingleton.Instance.Panels.Count; i++)
        {
            if (i==activateMe)
            {
                CombatSingleton.Instance.Panels[i].SetActive(true);
            }
        }
    }
    public static void DeactivatePanel(int deactivateMe)
    {

        for (int i = 0; i < CombatSingleton.Instance.Panels.Count; i++)
        {
            if (i == deactivateMe)
            {
                CombatSingleton.Instance.Panels[i].SetActive(false);
            }
        }
    }

    public static void ResetPanels()
    {
        for (int i = 0; i < CombatSingleton.Instance.Panels.Count; i++)
        {
            CombatSingleton.Instance.Panels[i].SetActive(false);
        }
    }

}
