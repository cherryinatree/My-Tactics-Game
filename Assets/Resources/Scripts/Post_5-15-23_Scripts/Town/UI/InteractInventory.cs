using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InteractInventory : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{

    public Image imageHolder;
    private Image drageMe;
    private static Vector3 origionalPosition;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (InventoryManagement.dragMe == null)
        {
            InventoryManagement.ImageToDrag(this.GetComponent<Image>());
        }
        else
        {
            InventoryManagement.UnclickDragImage();
        }
        Debug.Log("Worked");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
