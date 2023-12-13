using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceLoader
{
    private List<Sprite> _itemIcons;
    private List<GameObject> _merchants;
    private List<GameObject> _itemsWearable;
    private List<GameObject> _panels;
    private Image _image;

    public ResourceLoader()
    {
        ItemsIcons = new List<Sprite>();
        Merchants = new List<GameObject>();
        ItemsWearable = new List<GameObject>();
        Panels = new List<GameObject>();
        foreach (Sprite image in Resources.LoadAll<Sprite>("Sprites"))
        {
            ItemsIcons.Add(image);
        }
        foreach (GameObject merch in Resources.LoadAll<GameObject>("prefabs/MOBs/Merchants"))
        {
            Merchants.Add(merch);
        }
        foreach (GameObject merch in Resources.LoadAll<GameObject>("prefabs/Items/Wearable"))
        {
            ItemsWearable.Add(merch);
        }
        foreach (GameObject pan in GameObject.FindGameObjectsWithTag("Panel"))
        {
            Panels.Add(pan);
        }
        image = Resources.Load<Image>("prefabs/UIelements/Image");
    }

    public List<Sprite> ItemsIcons
    {
        get { return _itemIcons; }
        set { _itemIcons = value; }
    }
    public List<GameObject> Merchants
    {
        get { return _merchants; }
        set { _merchants = value; }
    }
    public List<GameObject> ItemsWearable
    {
        get { return _itemsWearable; }
        set { _itemsWearable = value; }
    }
    public List<GameObject> Panels
    {
        get { return _panels; }
        set { _panels = value; }
    }
    public Image image
    {
        get { return _image; }
        set { _image = value; }
    }
}
