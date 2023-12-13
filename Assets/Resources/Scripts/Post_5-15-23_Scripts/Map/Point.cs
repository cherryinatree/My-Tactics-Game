using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{


    public int nodeID;

    public string SceneLoadName;

    public string SceneName; 
    [TextArea(15, 20)]
    public string Discription;


    private Renderer rend;
    private Material origional;
    private Material selected;

    private void Awake()
    {

        rend = gameObject.GetComponent<Renderer>();
        origional = (Material)Resources.Load("Materials/LightBlue", typeof(Material));
        selected = (Material)Resources.Load("Materials/Yellow", typeof(Material));

        rend.material = origional;
    }

    public void Select()
    {
        rend.material = selected;
    }

    public void UnSelect()
    {

        rend.material = origional;
    }
}
