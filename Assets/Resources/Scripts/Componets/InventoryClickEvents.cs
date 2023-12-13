using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryClickEvents : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{


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
        //Debug.Log("Worked");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //throw new System.NotImplementedException();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //throw new System.NotImplementedException();
    }
}
