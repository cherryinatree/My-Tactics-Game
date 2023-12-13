using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuItemSelector : MonoBehaviour
{

    public GameObject Content;
    private int focus = 0;

    private void OnEnable()
    {
        focus = 0;
        Highlighter.HighlightImage(Content.transform.GetChild(0).gameObject);
    }

    public void Next()
    {
        Highlighter.DehighlightImage(Content.transform.GetChild(focus).gameObject);

        if(focus < Content.transform.childCount-1)
        {
            focus++;
        }
        else
        {
            focus = 0;
        }

        Highlighter.HighlightImage(Content.transform.GetChild(focus).gameObject);
    }

    public void Previous()
    {
        Highlighter.DehighlightImage(Content.transform.GetChild(focus).gameObject);

        if (focus != 0)
        {
            focus--;
        }
        else
        {
            focus = Content.transform.childCount-1;
        }

        Highlighter.HighlightImage(Content.transform.GetChild(focus).gameObject);
    }

    public void SelectButton()
    {
        Content.transform.GetChild(focus).gameObject.GetComponent<Button>().onClick.Invoke();
    }

}
