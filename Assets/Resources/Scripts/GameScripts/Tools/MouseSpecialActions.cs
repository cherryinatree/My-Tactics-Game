using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public static class MouseSpecialActions
{
    [SerializeField] private static GraphicRaycaster m_Raycaster;
    [SerializeField] private static Ray m_RaycasterGameObject;
    private static PointerEventData m_PointerEventData;
    [SerializeField] private static EventSystem m_EventSystem;
    [SerializeField] private static RectTransform canvasRect;


    public static void setUp()
    {
        m_Raycaster = GameObject.Find("Canvas").GetComponent<GraphicRaycaster>();
        m_EventSystem = GameObject.Find("Canvas").GetComponent<EventSystem>();
    }

    public static string MouseListenerTag()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            return hit.transform.tag;
        }
        return "no results";
    }
    public static Vector3 MouseListenerVector3()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            return hit.transform.position;
        }
        return Vector3.zero;
    }
    public static Vector3 MouseListenerVector3BuildParamaters()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
                return hit.transform.position;
            
        }
        return Vector3.zero;
    }

    public static string MouseListenerName()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            return hit.transform.gameObject.GetComponent<CharacterController>().myStats.name;
        }
        return "no results";
    }
    public static string MouseListenerObecjtName()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            return hit.transform.name;
        }
        return "no results";
    }
    public static GameObject MouseListenerGameObject()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            return hit.transform.gameObject;
        }
        return null;
    }
    public static string MouseListenerPanelName(string listenFor)
    {

        m_PointerEventData = new PointerEventData(m_EventSystem);
        //Set the Pointer Event Position to that of the game object
        m_PointerEventData.position = Input.mousePosition;

        //Create a list of Raycast Results
        List<RaycastResult> results = new List<RaycastResult>();

        //Raycast using the Graphics Raycaster and mouse click position
        m_Raycaster.Raycast(m_PointerEventData, results);
        if (results.Count > 0)
        {

            if (results[0].gameObject.name == listenFor)
            {
                return results[0].gameObject.name;
            }
            else
            {
                return results[0].gameObject.name;
            }
        }
        return "no results";
    }
    public static string MouseListenerPanelTag(string listenFor)
    {

        m_PointerEventData = new PointerEventData(m_EventSystem);
        //Set the Pointer Event Position to that of the game object
        m_PointerEventData.position = Input.mousePosition;

        //Create a list of Raycast Results
        List<RaycastResult> results = new List<RaycastResult>();

        //Raycast using the Graphics Raycaster and mouse click position
        m_Raycaster.Raycast(m_PointerEventData, results);
        if (results.Count > 0)
        {
            if (results[0].gameObject.name == listenFor)
            {
                return results[0].gameObject.tag;
            }
            else if (results[1].gameObject.tag == listenFor)
            {
                return results[1].gameObject.tag;
            }
            else
            {

                return results[0].gameObject.tag;
            }
        }
        return "no results";
    }
    public static GameObject MouseListenerPanelGameObject(string listenFor)
    {

        m_PointerEventData = new PointerEventData(m_EventSystem);
        //Set the Pointer Event Position to that of the game object
        m_PointerEventData.position = Input.mousePosition;

        //Create a list of Raycast Results
        List<RaycastResult> results = new List<RaycastResult>();

        //Raycast using the Graphics Raycaster and mouse click position
        m_Raycaster.Raycast(m_PointerEventData, results);
        if (results.Count > 0)
        {
            
            if (results[0].gameObject.name == listenFor)
            {
                return results[0].gameObject;
            }
            else if (results[1].gameObject.tag == listenFor)
            {
                return results[1].gameObject;
            }
            else
            {

                return results[0].gameObject;
            }
        }
        return null;
    }

    public static void ShowStatCard()
    {
        if (MouseListenerTag() == "PlayerCharacter" || MouseListenerTag() == "Team1" ||
               MouseListenerTag() == "Team2")
        {
            GameObject character = MouseListenerGameObject();

            UserInterface.ShowCharacterInfo(character.GetComponent<CharacterController>().myStats);
        }
        else
        {
            UserInterface.TurnOffOrOn("CharacterInfoPanel", false);
        }
    }
}
