using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using GamingTools;
using Cinemachine;

public class GameMaster : MonoBehaviour
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

    // Start is called before the first frame update
    void Start()
    {
        LoadSave();
        GenerateMap();
        GenerateNavSystem();
        BuildObjects();
        AddCombatants();
        SetUpSingleton();
        SetUpBattleMaster();
        MouseSpecialActions.setUp();
        UserInterface.CollectUI();
    }

    // Update is called once per frame
    void Update()
    {
        battle.Controls();
    }

    private void LoadSave()
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
        }
    }

    private void SetUpSingleton()
    {

        BattleSingleton.Instance.Combatants = Combatants;
        BattleSingleton.Instance.ourTurn = BattleSingleton.Instance.Combatants[0];
        BattleSingleton.Instance.focusOnMe = BattleSingleton.Instance.ourTurn[0];
        BattleSingleton.Instance.freeLookObject = GameObject.Find("FreeLookObject");
        BattleSingleton.Instance.targetGroup = GameObject.Find("TargetGroup");
        BattleSingleton.Instance.cam = cam;
        BattleSingleton.Instance.aimCam = aimCam;
        BattleSingleton.Instance.currentTurn = 0;
        BattleSingleton.Instance.NumberOfTeams = teams;
        BattleSingleton.Instance.save = save;
        BattleSingleton.Instance.particles = new List<GameObject>();
    }

    private void AddCombatants()
    {

        List<GameObject> PCs = new List<GameObject>();
        foreach (GameObject character in GameObject.FindGameObjectsWithTag("PlayerCharacter"))
        {
            PCs.Add(character);
        }
        Combatants.Add(PCs);
        for (int i = 1; i < teams; i++)
        {
            List<GameObject> otherTeam = new List<GameObject>();
            foreach (GameObject character in GameObject.FindGameObjectsWithTag("Team" + i.ToString()))
            {
                otherTeam.Add(character);
            }
            Combatants.Add(otherTeam);
        }
    }

    private void SetUpBattleMaster()
    {
        battle = new BattleMaster();
    }

    private void GenerateMap()
    {
        mesh = new GroundMeshGenorator(mat);
        mesh.Scale(Constants.MapConstants.map2_scale);
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
        Spawner.SpawnPCs(save.characterBattleTeam, mesh, 1, 1, 1, 1);
        BuildEnemies();


        mesh.square1.GetComponent<NavMeshSurface>().BuildNavMesh();
    }

    private void BuildEnemies()
    {
        for (int i = 0; i < save.enemies.Count; i++)
        {
            List<string> enemies = new List<string>();
            for (int x = 0; x < save.numberOfEnemies[i]; x++)
            {
                enemies.Add(save.enemies[i]);
            }
            Spawner.SpawnEnemies(enemies, mesh, 9, 9-i, 1, 1);
        }

    }

}
