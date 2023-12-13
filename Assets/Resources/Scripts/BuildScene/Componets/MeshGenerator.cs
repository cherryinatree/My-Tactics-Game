using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Jobs;
using UnityEngine;
using System.Threading;

[RequireComponent(typeof(MeshFilter))]
public class MeshGenerator : MonoBehaviour
{
    Thread thread;


    public GameObject quad;

    Mesh mesh;

    Vector3[] vertices;
    int[] triangles;
    Color[] colors;

    Vector3[] squareVerts;

    public int xSize = 20;
    public int zSize = 20;
    public float scale = 20f;
    public float offsetX = 5f;
    public float offsetY = 5f;
    public float heightScale = 2f;
    public float persistance = 1f;
    public float lacunarity = 1f;
    public float octives = 5f;
    public int verticiesDistance = 10;
    public int startX = 10;
    public int startY = 10;

    private float playerGridPositionX;
    private float playerGridPositionY;

    public int renderDistance = 2;

    public Gradient gradient;

    private float minimumTerrainHeight;
    private float maximumTerrainHeight;

    public Material material;

    public List<Vector4> WalkingSquares;
    List<GameObject> grids;

    List<GameObject> ChunkGrid;

    GameObject square1;
    GameObject square2; 
    GameObject empty;
    GameObject empty1;

    GameObject Player;
    GamingTools.PerlinNoise pearl;

    int aCount;
    int aCounter;

    float[,] theYs;
    Vector2 centerChunk;

    // Start is called before the first frame update
    void Awake()
    {
        ChunkGrid = new List<GameObject>();
        centerChunk = new Vector2(0, 0);
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 600;

        aCount = -renderDistance;
        aCounter = 0;


        playerGridPositionX = 0;
        playerGridPositionY = 0;

        theYs = new float[6500, 6500];

        squareVerts = new Vector3[4];
        pearl = new GamingTools.PerlinNoise();
         grids = new List<GameObject>();
         empty = new GameObject();
         empty1 = new GameObject();
         square1 = Instantiate(empty);
         square1.AddComponent<MeshFilter>();
         square1.AddComponent<MeshRenderer>();
         square1.AddComponent<MeshCollider>();
         square1.GetComponent<MeshRenderer>().material = material;
         square1.GetComponent<MeshCollider>().sharedMesh = new Mesh();
         mesh = square1.GetComponent<MeshCollider>().sharedMesh;
         square1.GetComponent<MeshFilter>().mesh = mesh;
        square1.isStatic = true;
        /*CreateShapeSquare();
        UpdateListMesh(square1);
        square1.transform.parent = empty1.transform;
        grids.Add(square1);*/
        startX = xSize;
         float origionalY = 0;
        Player = GameObject.Find("MainCharacter(Clone)");
        //mesh = new Mesh();
        // GetComponent<MeshFilter>().mesh = mesh;
        squareVerts[0] = new Vector3(0, 0, 0);
        squareVerts[1] = new Vector3(1, 0, 0);
        squareVerts[2] = new Vector3(0, 0, 1);
        squareVerts[3] = new Vector3(1, 0, 1);

        SetTriangelsAtStart();

        QuickChunk(0,0);
        UpdateListMesh(square1);
        // FirstLoadTicketsAroundPlayer();
        //CreateMap();
        FirstLoadTicketsAroundPlayer((int)centerChunk.x, (int)centerChunk.y);
        //thread = new Thread(AlltheIntialChunks);
        //Invoke("AlltheIntialChunks", 0.5f);
        AlltheIntialChunks(renderDistance);
        //Invoke("CreateMap",0f);
        /*
        for (int x = 0; x < 5; x++)
        {
            for (int i = 0; i < 5; i++)
            {
                addMoreSquares();
            }
            startX += xSize * verticiesDistance;
            startY = 0;
        }
        /*
        for (int x = 0; x < verticiesDistance; x++)
        {
            for (int i = 0; i < verticiesDistance; i++)
            {
                GameObject a = Instantiate(empty);
                a.AddComponent<MeshFilter>();
                a.AddComponent<MeshRenderer>();
                a.AddComponent<MeshCollider>();
                a.GetComponent<MeshRenderer>().material = material;
                a.GetComponent<MeshCollider>().sharedMesh = new Mesh();
                mesh = a.GetComponent<MeshCollider>().sharedMesh;
                a.GetComponent<MeshFilter>().mesh = mesh;
                CreateThisShapeSquare();
                startX += xSize;
                UpdateListMesh(a);
                grids.Add(a);
                a.transform.parent = empty1.transform;
            }
            startY += zSize;
            startX = origionalX;
        }*/

        /* mesh = new Mesh();
         GetComponent<MeshFilter>().mesh = mesh;
         CreateShapeSquare();
         UpdateMesh();*/
    }


    
      void Update()
      {

        LoadTicketsAroundPlayer2();
      }
    
    private void AlltheIntialChunks(int layers)
    {
        for (int x = 1; x <= layers; x++)
        {
            ChunkVerts((x * 2) + 1, x);
        }
    }

    private void CreateMap()
    {
        for (int z = 0; z < 6500; z++)
        {
            for (int x = 0; x < 6500; x++)
            {

                theYs[x,z] = pearl.Noise(x * 0.3f, z * 0.3f, xSize, zSize, scale, offsetX, offsetY, heightScale, lacunarity, persistance, octives);

                if (theYs[x, z] > maximumTerrainHeight)
                {
                    maximumTerrainHeight = theYs[x, z];
                }

                if (theYs[x, z] < minimumTerrainHeight)
                {
                    minimumTerrainHeight = theYs[x, z];
                }

            }
        }
    }



    private void ChunkVerts(int size, int circleLayer)
    {

        for (int x = 0; x < size; x++)
        {
            FirstLoadTicketsAroundPlayer((int)centerChunk.x + (((size-(1+circleLayer)) - x) * (10)), (int)centerChunk.y + (10 * circleLayer));
        }
        for (int x = 1; x < size; x++)
        {
            FirstLoadTicketsAroundPlayer((int)centerChunk.x + (10 * circleLayer), (int)centerChunk.y + (((size - (1 + circleLayer)) - x) * (10)));
        }
        for (int x = 0; x < size-1; x++)
        {
            FirstLoadTicketsAroundPlayer((int)centerChunk.x + ((-(size - (1 + circleLayer)) + x) * (10)), (int)centerChunk.y - (10 * circleLayer));
        }
        for (int x = 1; x < size-1; x++)
        {
            FirstLoadTicketsAroundPlayer((int)centerChunk.x - (10 * circleLayer), (int)centerChunk.y + ((-(size - (1 + circleLayer)) + x) * (10)));
        }

    }

    private void FirstLoadTicketsAroundPlayer(int startX, int startZ)
    {
        int counter = 0;
        GameObject parent = new GameObject();
        parent.transform.position = new Vector3((float)startX, 0, (float)startZ);
        parent.name = "Grid" + startX.ToString() + "_" + startZ.ToString();
        for (int z = startX; z < startX+10; z += 1)
        {
            for (int i = startZ; i < startZ+10; i += 1)
            {
                GameObject square3 = new GameObject();
                addMeshToMe(square3);
                square3.transform.parent = parent.transform;
                QuickChunk(i, z);
                square3.transform.position = new Vector3(i, 0, z);
                UpdateListMesh(square3);
                square3.name = "mesh" + counter.ToString();
                counter++;
            }
        }
        ChunkGrid.Add(parent);
        parent.transform.parent = empty1.transform;
    }





    private void QuickChunk(int startX, int startZ)
    {
        colors = new Color[squareVerts.Length];

        for (int i = 0; i < squareVerts.Length; i++)
        {
            squareVerts[i].y = pearl.Noise(startX+squareVerts[i].x, startZ+squareVerts[i].z, xSize, zSize, scale, offsetX, offsetY, heightScale, lacunarity, persistance, octives);
            vertices = squareVerts;
            float height = Mathf.InverseLerp(0, 1, vertices[i].y);
            colors[i] = gradient.Evaluate(height);
        }
    }


    private void LoadTicketsAroundPlayer2()
    {
        float myX = Mathf.Floor(Player.transform.position.x);
        float myY = Mathf.Floor(Player.transform.position.z);

        int size = (renderDistance * 2) + 1;

        Debug.Log(myY + ": " + playerGridPositionY);

        if (myY > playerGridPositionY + 10)
        {
            centerChunk.x += 10;
            playerGridPositionY += 10;
            for (int x = 0; x < size; x++)
            {
                FirstLoadTicketsAroundPlayer((int)centerChunk.x + (10 * renderDistance), (int)centerChunk.y + (((size - (1 + renderDistance)) - x) * (10)));
            }
        }
    }


    private void LoadTicketsAroundPlayer()
    {
        float x = Mathf.Floor(Player.transform.position.x / xSize);
        float y = Mathf.Floor(Player.transform.position.y / zSize);

        if (x > playerGridPositionX)
        {
            for (int z = (((int)y - renderDistance) * zSize); z <= (((int)y + renderDistance) * zSize); z += zSize)
            {
                GameObject square3 = new GameObject();
                square3.transform.position = new Vector3(((int)x + renderDistance) * xSize, 0, (int)y + z);
                square3.transform.parent = empty1.transform;
                addMeshToMe(square3);
                CreateSquareChunk((((int)x + renderDistance) * xSize), z);
                UpdateListMesh(square3);
                grids.Add(square3);
                square3.isStatic = true;
            }
            for (int i = 0; i < grids.Count; i++)
            {

                if (grids[i].transform.position.x < (((int)x - renderDistance) * xSize))
                {
                    GameObject destroyMe = grids[i];
                    grids.Remove(destroyMe);
                    Destroy(destroyMe);
                }
            }
            playerGridPositionX = x;
        }
        if (x < playerGridPositionX)
        {
            GameObject placeHolder = new GameObject();
            for (int z = (((int)y - renderDistance) * zSize); z <= (((int)y + renderDistance) * zSize); z += zSize)
            {

                GameObject square3 = new GameObject();
                square3.transform.position = new Vector3(((int)x - renderDistance) * xSize, 0, (int)y + z);
                square3.transform.parent = empty1.transform;
                addMeshToMe(square3);
                CreateSquareChunk((((int)x - renderDistance) * xSize), z);
                UpdateListMesh(square3);
                grids.Add(square3);
                square3.isStatic = true;
            }
            for (int i = 0; i < grids.Count; i++)
            {
                if (grids[i].transform.position.x > (((int)x + renderDistance) * xSize))
                {
                    GameObject destroyMe = grids[i];
                    grids.Remove(destroyMe);
                    Destroy(destroyMe);
                }
            }
            playerGridPositionX = x;
        }
        /*
        if (y > playerGridPositionY)
        {
            GameObject placeHolder = new GameObject();
            for (int z = (((int)x - renderDistance) * xSize); z <= (((int)x + renderDistance) * xSize); z += xSize)
            {

                placeHolder.transform.position = new Vector3((int)x + z, 0, ((int)y + renderDistance) *zSize);
                GameObject square3 = Instantiate(placeHolder, placeHolder.transform.position, empty1.transform.rotation);
                addMeshToMe(square3);
                CreateSquareChunk(z, (((int)y + renderDistance) * zSize));
                UpdateListMesh(square3);
                grids.Add(square3);
            }
            for (int i = 0; i < grids.Count; i++)
            {

                if (grids[i].transform.position.z < (((int)y - renderDistance) * zSize))
                {
                    GameObject destroyMe = grids[i];
                    grids.Remove(destroyMe);
                    Destroy(destroyMe);
                }
            }
            playerGridPositionY = y;
        }

        if (y < playerGridPositionY)
        {
            GameObject placeHolder = new GameObject();
            for (int z = (((int)x - renderDistance) * xSize); z <= (((int)x + renderDistance) * zSize); z += xSize)
            {

                placeHolder.transform.position = new Vector3((int)x + z, 0,((int)y - renderDistance) * zSize);
                GameObject square3 = Instantiate(placeHolder, placeHolder.transform.position, empty1.transform.rotation);
                addMeshToMe(square3);
                CreateSquareChunk(z, (((int)y - renderDistance) * zSize));
                UpdateListMesh(square3);
                grids.Add(square3);
            }
            for (int i = 0; i < grids.Count; i++)
            {

                if (grids[i].transform.position.z > (((int)y + renderDistance) * zSize))
                {
                    GameObject destroyMe = grids[i];
                    grids.Remove(destroyMe);
                    Destroy(destroyMe);
                }
            }
            playerGridPositionY = y;
        }
        
    */


    }

    private void addMeshToMe(GameObject noMesh)
    {
        mesh = new Mesh();
        noMesh.AddComponent<MeshFilter>();
        noMesh.AddComponent<MeshRenderer>();
        noMesh.AddComponent<MeshCollider>();
        noMesh.GetComponent<MeshRenderer>().material = material;
        noMesh.GetComponent<MeshCollider>().sharedMesh = new Mesh();
        mesh = noMesh.GetComponent<MeshCollider>().sharedMesh;
        noMesh.GetComponent<MeshFilter>().mesh = mesh;
        noMesh.isStatic = true;

    }

    void CreateSquareChunk(int startX, int startY)
    {
        vertices = new Vector3[(xSize + 1) * (zSize + 1)];

        for (int i = 0, z = startY, a = 0; z <= startY + zSize; z++,a++)
        {
            for (int x = startX, b=0; x <= startX + xSize; x++,b++)
            {
                //float y = Mathf.PerlinNoise(x * 0.3f, z * 0.3f) * 2f;
                GamingTools.PerlinNoise pearl = new GamingTools.PerlinNoise();
                float y = pearl.Noise(x * 0.3f, z * 0.3f, xSize, zSize, scale, offsetX, offsetY, heightScale, lacunarity, persistance, octives);
                if (y <= -100) y = 0.1f;
                //vertices[i] = new Vector3(x * verticiesDistance, y, z * verticiesDistance);
                vertices[i] = new Vector3(b, y, a);
                //CreateShapeSquareInsideSquares((int)vertices[i].x, (int)vertices[i].y, verticiesDistance);
                if (y > maximumTerrainHeight)
                {
                    maximumTerrainHeight = y;
                }
                if (y < minimumTerrainHeight)
                {
                    minimumTerrainHeight = y;
                }

                i++;
            }
        }
        AddTriangles();
    }


    private void addMoreSquares()
    {

        int origionalX = startX;

        for (int i = 0; i < verticiesDistance; i++)
        {
            for (int x = 0; x < verticiesDistance; x++)
            {
                square2 = Instantiate(square1);
                square2.transform.position = new Vector3(square1.transform.position.x + startX, square1.transform.position.y, square1.transform.position.z + startY);
                startX += xSize;
                square2.transform.parent = empty1.transform;
            }
            startY += zSize;
            startX = origionalX;
        }
        if (startY > 50000)
        {
            startX = origionalX + xSize*verticiesDistance;
            startY = 0;
        }
        if(startX >= 50000)
        {
            CancelInvoke();
        }
    }

    void CreateShapeSquare()
    {
        vertices = new Vector3[(xSize + 1) * (zSize + 1)];

        for (int i = 0, z = startY; z <= startY + zSize; z++)
        {
            for (int x = startX; x <= startX + xSize; x++)
            {
                //float y = Mathf.PerlinNoise(x * 0.3f, z * 0.3f) * 2f;
                GamingTools.PerlinNoise pearl = new GamingTools.PerlinNoise();
                float y = pearl.Noise(x*0.3f, z*0.3f, xSize, zSize, scale, offsetX, offsetY, heightScale, lacunarity, persistance, octives);
                if (y <= -100) y = 0.1f;
                //vertices[i] = new Vector3(x * verticiesDistance, y, z * verticiesDistance);
                vertices[i] = new Vector3(x, y, z);
                //CreateShapeSquareInsideSquares((int)vertices[i].x, (int)vertices[i].y, verticiesDistance);
                if (y > maximumTerrainHeight)
                {
                    maximumTerrainHeight = y;
                }
                if(y < minimumTerrainHeight)
                {
                    minimumTerrainHeight = y;
                } 

                i++;
            }
        }
        AddTriangles();
    }


    void CreateThisShapeSquare()
    {
        vertices = new Vector3[(xSize + 1) * (zSize + 1)];

        for (int i = 0, z = startY; z <= startY + zSize; z++)
        {
            for (int x = startX; x <= startX + xSize; x++)
            {
                //float y = Mathf.PerlinNoise(x * 0.3f, z * 0.3f) * 2f;
                //PerlinNoise pearl = new PerlinNoise();
                //float y = pearl.Noise(x * 0.3f, z * 0.3f, xSize, zSize, scale, offsetX, offsetY, heightScale, lacunarity, persistance, octives);
                //if (y <= -100) y = 0.1f;
                //vertices[i] = new Vector3(x * verticiesDistance, y, z * verticiesDistance);
                vertices[i] = new Vector3(x, 0, z);
                //CreateShapeSquareInsideSquares((int)vertices[i].x, (int)vertices[i].y, verticiesDistance);
                i++;
            }
        }
        AddTheseTriangles();
    }

    private void AddTheseTriangles()
    {
        //WalkingSquares = new List<Vector4>();
        triangles = new int[xSize * zSize * 6];
        int vert = 0;
        int tris = 0;

        for (int z = 0; z < zSize; z++)
        {
            for (int x = 0; x < xSize; x++)
            {
                triangles[tris + 0] = vert + 0;
                triangles[tris + 1] = vert + xSize + 1;
                triangles[tris + 2] = vert + 1;
                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + xSize + 1;
                triangles[tris + 5] = vert + xSize + 2;
                vert++;
                tris += 6;

            }
            vert++;

        }

        colors = new Color[vertices.Length];

        for (int i = 0, z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                float height = Mathf.InverseLerp(0, 1, vertices[i].y);
                colors[i] = gradient.Evaluate(height);
                i++;
            }
        }

    }


    private void SetTriangelsAtStart()
    {
        //WalkingSquares = new List<Vector4>();
        triangles = new int[6];
        int vert = 0;
        int tris = 0;

                triangles[tris + 0] = vert + 0;
                triangles[tris + 1] = vert + 1 + 1;
                triangles[tris + 2] = vert + 1;
                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + 1 + 1;
                triangles[tris + 5] = vert + 1 + 2;
    }


    private void AddTriangles()
    {
        //WalkingSquares = new List<Vector4>();
        triangles = new int[xSize * zSize * 6];
        int vert = 0;
        int tris = 0;

        for (int z = 0; z < zSize; z++)
        {
            for (int x = 0; x < xSize; x++)
            {
                triangles[tris + 0] = vert + 0;
                triangles[tris + 1] = vert + xSize + 1;
                triangles[tris + 2] = vert + 1;
                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + xSize + 1;
                triangles[tris + 5] = vert + xSize + 2;
                vert++;
                tris += 6;

            }
            vert++;

        }

        colors = new Color[vertices.Length];

        for (int i = 0, z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                float height = Mathf.InverseLerp(minimumTerrainHeight, maximumTerrainHeight, vertices[i].y);
                colors[i] = gradient.Evaluate(height);
                i++;
            }
        }

    }


    void UpdateMesh()
    {
        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.colors = colors;

        mesh.RecalculateNormals();


        MeshCollider collider = gameObject.GetComponent<MeshCollider>();
        collider.sharedMesh = mesh;

    }
    void UpdateListMesh(GameObject thisMesh)
    {
        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.colors = colors;

        mesh.RecalculateNormals();


        MeshCollider collider = thisMesh.GetComponent<MeshCollider>();
        collider.sharedMesh = mesh;
    }
}
