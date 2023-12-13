using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class Highlighter
{
    private static Color ButtonOrgional = Color.white;
    private static Color ButtonHighlight = Color.yellow;


    public static void HighlightImage(GameObject Brightme)
    {
        Brightme.GetComponent<Image>().color = ButtonHighlight;
    }
    public static void DehighlightImage(GameObject Brightme)
    {
        Brightme.GetComponent<Image>().color = ButtonOrgional;
    }
}
