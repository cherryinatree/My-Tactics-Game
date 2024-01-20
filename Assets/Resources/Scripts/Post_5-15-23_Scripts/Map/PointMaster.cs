using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PointMaster : MonoBehaviour
{

    public GameObject[] Points;

    private int currentNode;
    private int maxNode;

    public TMP_Text InfoName;
    public TMP_Text InfoDiscription;

    public int[] monsters;


    // Start is called before the first frame update
    void Start()
    {
        SaveManipulator.LoadSceneChange();
        FindCurrentNode();
        StartingPoint();
        SetInfoText();
    }

    // Update is called once per frame
    void Update()
    {
        cycleThroughPoints();
        AcceptDestination();
    }

    private void AcceptDestination()
    {
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            Accept();
        }
    }

    private void FindCurrentNode()
    {
        int whichPoint = 0;
        int level = SaveData.Current.mainData.playerData.level;
        for (int i = 0; i < Points.Length; i++)
        {
            if(Points[i].GetComponent<Point>().nodeID <= level)
            {
                whichPoint++;
            }
            else
            {
                break;
            }
        }

        currentNode = whichPoint-1;
        maxNode = currentNode;
    }

    private void StartingPoint()
    {

        Points[currentNode].GetComponent<Point>().Select();

        Transform pointTrans = Points[currentNode].transform;
        Camera.main.transform.position = new Vector3(pointTrans.position.x,
            Camera.main.transform.position.y, pointTrans.position.z);
    }

    private void cycleThroughPoints()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            Next();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            Previous();
        }
    }

    public void Accept()
    {
        Debug.Log(SaveData.Current.mainData.loadSceneData.currentSceneName);
        SaveData.Current.mainData.loadSceneData.currentSceneName = Points[currentNode].GetComponent<Point>().SceneLoadName;
        SaveData.Current.mainData.loadSceneData.isBattle = true;


        SaveManipulator.AutoSave();
        SaveManipulator.SaveSceneChange();
        SceneManager.LoadScene(Points[currentNode].GetComponent<Point>().SceneLoadName);
    }

    public void Next()
    {
        if (currentNode + 1 <= maxNode)
        {
            if (Points[currentNode + 1].GetComponent<Point>().nodeID < maxNode)
            {
                Select();
            }
        }
    }

    public void Previous()
    {
        if (currentNode > 0)
        {
            Unselect();
        }
    }

    private void Select()
    {
        Points[currentNode].GetComponent<Point>().UnSelect();
        currentNode++;
        Points[currentNode].GetComponent<Point>().Select();

        SetInfoText();
        SetCameraToCurrent();
    }

    private void Unselect()
    {
        Points[currentNode].GetComponent<Point>().UnSelect();
        currentNode--;
        Points[currentNode].GetComponent<Point>().Select();

        SetInfoText();
        SetCameraToCurrent();
    }

    private void SetInfoText()
    {
        InfoName.text = Points[currentNode].GetComponent<Point>().SceneName;
        InfoDiscription.text = Points[currentNode].GetComponent<Point>().Discription;
    }

    private void SetCameraToCurrent()
    {
        Transform pointTrans = Points[currentNode].transform;
        Camera.main.transform.position = new Vector3(pointTrans.position.x,
            Camera.main.transform.position.y, pointTrans.position.z);
    }
}
