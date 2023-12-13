using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using GamingTools;
using Cinemachine;

public class HomeGameMaster : MonoBehaviour
{
    public Material mat;
    private GroundMeshGenorator mesh;

    public int mapX = 7;

    private List<List<GameObject>> Combatants = new List<List<GameObject>>();
    public CinemachineVirtualCamera cam;
    public Cinemachine3rdPersonAim aimCam;
    private BattleMaster battle;
    private int teams = 2;
    private SaveFile save;

    private GameObject freeLookObject;
    private GameObject focusOnMe;

    private HomeMain home;

    // Start is called before the first frame update
    void Awake()
    {
        LoadGame();
        MapSetUp();
        UiSetUp();
        ScriptSetUp();
        CameraSetUp();
        SetUpSingleton();
    }

    

    private void LoadGame()
    {
        if (SaveSystem._lastLoadedFile == null)
        {
            SaveNames saveNames = SaveSystem.SaveNamesFinder.LoadTheNames("Names");
            int last = saveNames.theSaveNames.Count - 1;
            save = SaveSystem.LoadGameData(saveNames.theSaveNames[last]);
        }
        else
        {
            save = SaveSystem.LoadGameData(SaveSystem._lastLoadedFile);
            Debug.Log(save.saveName);
        }
    }

    private void SetUpSingleton()
    {
        HomeSingleton.Instance.save = save;
        HomeSingleton.Instance.controlPanel = new HomeControlPanel();
        HomeSingleton.Instance.homeMain = home;
        HomeSingleton.Instance.cam = cam;
        HomeSingleton.Instance.FocusOnCard = HomeSingleton.Instance.save.characterRoster[0];
    }

    private void UiSetUp()
    {
        MouseSpecialActions.setUp();
    }

    private void ScriptSetUp()
    {
        List<GameObject> PCs = new List<GameObject>();
        foreach (GameObject character in GameObject.FindGameObjectsWithTag("PlayerCharacter"))
        {
            PCs.Add(character);
            character.AddComponent<Rigidbody>();
            character.GetComponent<Rigidbody>().useGravity = false;
            character.GetComponent<Rigidbody>().isKinematic = true;
        }
        Combatants.Add(PCs);
    }

    private void MapSetUp()
    {

        GenerateMap();
        GameObject.Find("Ground").transform.localScale *= 2;
        GameObject.Find("Ground").transform.tag = "Ground";
        GenerateNavSystem();
        BuildObjects();
    }

    public void CameraSetUp()
    {
        freeLookObject = GameObject.Find("FreeLookObject");
        focusOnMe = GameObject.Find(Combatants[0][0].name);
        cam.Follow = focusOnMe.transform;
        cam.LookAt = focusOnMe.transform;
        var transposer = cam.GetCinemachineComponent<CinemachineTransposer>();
        transposer.m_FollowOffset = Constants.CameraConstants.Default;
        aimCam.enabled = false;
        updateFreeLook(focusOnMe);
        home = new HomeMain(cam);
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        home.Controls();
    }


    private void Movement()
    {

        if (Input.GetKey(KeyCode.W))
        {
            focusOnMe.GetComponent<NavMeshAgent>().SetDestination(focusOnMe.transform.position + focusOnMe.transform.forward);
        }
        if (Input.GetKey(KeyCode.S))
        {
            focusOnMe.GetComponent<NavMeshAgent>().SetDestination(focusOnMe.transform.position - focusOnMe.transform.forward);
        }
        if (Input.GetKey(KeyCode.A))
        {
            focusOnMe.transform.Rotate(new Vector3(0, -90 * Time.deltaTime, 0));
        }
        if (Input.GetKey(KeyCode.D))
        {
            focusOnMe.transform.Rotate(new Vector3(0, 90 * Time.deltaTime, 0));
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            focusOnMe.GetComponent<NavMeshAgent>().speed = 6f;
        }
        else
        {
            focusOnMe.GetComponent<NavMeshAgent>().speed = 3f;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            focusOnMe.transform.position += Vector3.up * 100;
        }
    }
    private void updateFreeLook(GameObject copy)
    {
        freeLookObject.transform.position = copy.transform.position;
        freeLookObject.transform.rotation = copy.transform.rotation;
    }
    private void GenerateMap()
    {
        mesh = new GroundMeshGenorator(mat);
        mesh.Scale(Constants.MapConstants.home_scale);
        mesh.Noise(1,1,1,1,1,1);
        mesh.GenerateMesh();
    }

    private void GenerateNavSystem()
    {
        AddingNav addNav = new AddingNav();
        addNav.addNavSurface(mesh.square1);
    }

    private void BuildObjects()
    {

        string[] itemList ={
            "Terrain/Tree","Terrain/Tree","Terrain/Tree"
        };

        Spawner.SpawnGroups(itemList, mesh, 2, 2, 7, 7);

        Spawner.SpawnPCs(save.characterRoster, mesh, 1, 1, 1, 1);


        mesh.square1.GetComponent<NavMeshSurface>().BuildNavMesh();
    }
}
