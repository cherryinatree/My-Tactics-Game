using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public static class CharacterHighlight
{


    public static void ResetHighlights()
    {
        foreach (List<GameObject> items in BattleSingleton.Instance.Combatants)
        {
            foreach (GameObject item in items)
            {

                item.transform.Find("Body").GetComponentInChildren<Renderer>().material.SetFloat("_OutlineWidth", 0);
                item.transform.Find("Body").GetComponentInChildren<Renderer>().material.SetColor("_OutlineColor", Color.white);
            }
        }
    }


    public static void OutlineMe(GameObject character, float someValue, Color color)
    {


        character.transform.Find("Body").GetComponentInChildren<Renderer>().material.SetFloat("_OutlineWidth", someValue);
        character.transform.Find("Body").GetComponentInChildren<Renderer>().material.SetColor("_OutlineColor", color);
    }
}
