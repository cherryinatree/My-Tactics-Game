using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Constants;
using Cinemachine;

public class Build
{
    private GameObject home;
    public static bool placeBuildings = false;
    protected Material grass;
    GameObject newBuilding;


    public Build()
    {
        home = (GameObject)Resources.Load("Prefabs/Buildings/FullBuildings/House", typeof(GameObject));
        addListeners();
    }

    private void addListeners()
    {
        GameObject.Find("BuildButton0").GetComponent<Button>().onClick.AddListener(delegate
        { BuildOn(GameObject.Find("BuildButton0").GetComponent<Button>()); });
    }


    public void EditBuildingPosition()
    {

    }

    public void BuildOn(Button btn)
    {
        Debug.Log("Button Pressed");
        placeBuildings = true;
        HomeSingleton.Instance.homeMain.EscapeKey();
        BuyBuilding(btn);
    }

    public void BuyBuilding(Button btn)
    {
        //Debug.Log(btn.name);
        if (btn.name == "BuildButton0")
        {
            GameObject newBuilding0 = GameObject.Instantiate(home, Vector3.zero, Quaternion.identity);
            newBuilding = newBuilding0;
            Scanner.ChangeLayers(newBuilding, 2);
            var transposer = HomeSingleton.Instance.cam.GetCinemachineComponent<CinemachineTransposer>();
            transposer.m_FollowOffset = Constants.CameraConstants.BuildView;
            
            //newBuilding.transform.position = new Vector3(newBuilding.transform.position.x * -10,
            // newBuilding.transform.position.y * -10, newBuilding.transform.position.x * -10);
            //  newBuilding.layer = 2;
        }
    }

    public void BuildUpdate()
    /*test*/
    {

        if (placeBuildings)
        {

            if (MouseSpecialActions.MouseListenerTag() == "Ground")
            {
                Vector3 hitMousePosition = MouseSpecialActions.MouseListenerVector3BuildParamaters();
                if(hitMousePosition != Vector3.zero)
                {
                    PlaceBuildingHover(hitMousePosition);
                    PlaceBuilding(hitMousePosition);
                }
            }
        }
    }


    private void PlaceBuilding(Vector3 hitMousePosition)
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (hitMousePosition != Vector3.zero)
            {
                var transposer = HomeSingleton.Instance.cam.GetCinemachineComponent<CinemachineTransposer>();
                transposer.m_FollowOffset = Constants.CameraConstants.HomeDefault;

                placeBuildings = false;
                Scanner.ChangeLayers(newBuilding, 0);
                //Scanner.AddNavObstacle(newBuilding);
                Debug.Log("placed");
            }
            else
            {
                Debug.Log("Can't place here");
            }
        }
    }

    public void PlaceBuildingHover(Vector3 hitCube)
    {
        // Debug.Log(hitCube);
        if (hitCube != null)
        {
            if (newBuilding.name == "WeaponShopAndInside(Clone)")
            {
                newBuilding.transform.position = new Vector3(hitCube.x + 0.7f,
                hitCube.y + 1.2f, hitCube.z - 0.5f);
            }
            else
            {

                newBuilding.transform.position = new Vector3(hitCube.x + 0.7f,
                    hitCube.y + 0.5f, hitCube.z - 0.5f);
            }
        }
    }

}
