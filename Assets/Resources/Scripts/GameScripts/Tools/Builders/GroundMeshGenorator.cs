using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GamingTools
{
    public class GroundMeshGenorator
    {

        private GameObject quad;

        private Mesh mesh;

        private Vector3[] vertices;
        private int[] triangles;
        private Color[] colors;

        private Vector3[] squareVerts;

        private float xSize = 20;
        private float zSize = 20;
        private float scale = 20f;
        private float offsetX = 5f;
        private float offsetY = 5f;
        private float heightScale = 2f;
        private float persistance = 1f;
        private float lacunarity = 1f;
        private float octives = 5f;
        private float verticiesDistance = 10;
        private float startX = 10;
        private float startY = 10;

        private float renderDistance = 2;

        private Gradient gradient;
        private Material material;

        public List<Vector4> WalkingSquares;

        private List<GameObject> ChunkGrid;

        public GameObject square1;
        private GameObject empty1;

        private PerlinNoise pearl;

        private Vector2 centerChunk;

        private Material mat;




        /**************************************************************************************************************
         * 
         * 
         *                          The public functions
         * 
         * 
         * ************************************************************************************************************/

        public GroundMeshGenorator(Material mat)
        {
            this.mat = mat;
            ChunkGrid = new List<GameObject>();
            centerChunk = new Vector2(0, 0);
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 60;

            gradient = new Gradient();

            squareVerts = new Vector3[4];
            pearl = new PerlinNoise();
            empty1 = new GameObject();
            square1 = GameObject.Instantiate(empty1);
            square1.name = "Ground";
            square1.tag = "Ground";
            GameObject.Destroy(empty1);
            square1.AddComponent<MeshFilter>();
            square1.AddComponent<MeshRenderer>();
            square1.AddComponent<MeshCollider>();
            square1.GetComponent<MeshRenderer>().material = material;
            square1.GetComponent<MeshCollider>().sharedMesh = new Mesh();
            mesh = square1.GetComponent<MeshCollider>().sharedMesh;
            square1.GetComponent<MeshFilter>().mesh = mesh;
            square1.isStatic = true;
            startX = xSize;
            squareVerts[0] = new Vector3(0, 0, 0);
            squareVerts[1] = new Vector3(1, 0, 0);
            squareVerts[2] = new Vector3(0, 0, 1);
            squareVerts[3] = new Vector3(1, 0, 1);

        }

        public void Scale(float xSize, float zSize, float offsetX, float offsetY, float startX, float startY)
        {
                this.renderDistance = xSize;
                this.zSize = zSize;
                this.offsetX = offsetX;
                this.offsetY = offsetY;
                this.startX = startX;
                this.startY = startY;
        }
        public void Scale(float[] sixItems)
        {
            this.renderDistance = sixItems[0];
            this.zSize = sixItems[1];
            this.offsetX = sixItems[2];
            this.offsetY = sixItems[3];
            this.startX = sixItems[4];
            this.startY = sixItems[5];
        }

        public void Noise(float heightScale, float persistance, float lacunarity, float octives, int verticiesDistance, float scale)
        {
                this.heightScale = heightScale;
                this.persistance = persistance;
                this.lacunarity = lacunarity;
                this.octives = octives;
                this.verticiesDistance = verticiesDistance;
                this.scale = scale;
        }

        public void GenerateMesh()
        {
            SetTriangelsAtStart();
            QuickChunk(0, 0);
            UpdateListMesh(square1);
            //FirstLoadTicketsAroundPlayer((int)centerChunk.x, (int)centerChunk.y);
            AlltheIntialChunks(renderDistance);
        }

        private void AlltheIntialChunks(float layers)
        {
            for (int x = 0; x <= layers; x++)
            {
                ChunkVerts((x * 2) + 1, x);
            }
        }


        /**************************************************************************************************************
         * 
         * 
         *                          The private functions
         * 
         * 
         * ************************************************************************************************************/

        private void ChunkVerts(float size, float circleLayer)
        {

            for (int x = 0; x < size; x++)
            {
                FirstLoadTicketsAroundPlayer((int)centerChunk.x + (((size - (1 + circleLayer)) - x) * (10)), (int)centerChunk.y + (10 * circleLayer));
            }
            for (int x = 1; x < size; x++)
            {
                FirstLoadTicketsAroundPlayer((int)centerChunk.x + (10 * circleLayer), (int)centerChunk.y + (((size - (1 + circleLayer)) - x) * (10)));
            }
            for (int x = 0; x < size - 1; x++)
            {
                FirstLoadTicketsAroundPlayer((int)centerChunk.x + ((-(size - (1 + circleLayer)) + x) * (10)), (int)centerChunk.y - (10 * circleLayer));
            }
            for (int x = 1; x < size - 1; x++)
            {
                FirstLoadTicketsAroundPlayer((int)centerChunk.x - (10 * circleLayer), (int)centerChunk.y + ((-(size - (1 + circleLayer)) + x) * (10)));
            }

        }

        private void FirstLoadTicketsAroundPlayer(float startX, float startZ)
        {
            int counter = 0;
            GameObject parent = new GameObject();
            parent.transform.position = new Vector3((float)startX, 0, (float)startZ);
            parent.name = "Grid" + startX.ToString() + "_" + startZ.ToString();
            for (float z = startX; z < startX + 10; z += 1)
            {
                for (float i = startZ; i < startZ + 10; i += 1)
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
            parent.transform.parent = square1.transform;
        }

        private void QuickChunk(float startX, float startZ)
        {
            colors = new Color[squareVerts.Length];

            for (int i = 0; i < squareVerts.Length; i++)
            {
                squareVerts[i].y = pearl.Noise(startX + squareVerts[i].x, startZ + squareVerts[i].z, xSize, 
                    zSize, scale, offsetX, offsetY, heightScale, lacunarity, persistance, octives);
                vertices = squareVerts;
                float height = Mathf.InverseLerp(0, 1, vertices[i].y);
                colors[i] = gradient.Evaluate(height);
                //colors[i] = Color.green;
            }
        }
        public float GetTheNoiseY(Vector3 vect)
        {
            return pearl.Noise(vect.x, vect.z, xSize,
                    zSize, scale, offsetX, offsetY, heightScale, lacunarity, persistance, octives);
        }

        private void addMeshToMe(GameObject noMesh)
        {
            mesh = new Mesh();
            noMesh.AddComponent<MeshFilter>();
            noMesh.AddComponent<MeshRenderer>();
            noMesh.AddComponent<MeshCollider>();
            noMesh.GetComponent<MeshRenderer>().material = mat;
            noMesh.GetComponent<MeshCollider>().sharedMesh = new Mesh();
            mesh = noMesh.GetComponent<MeshCollider>().sharedMesh;
            noMesh.GetComponent<MeshFilter>().mesh = mesh;
            noMesh.isStatic = true;

            noMesh.tag = "Ground";

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

        private void UpdateListMesh(GameObject thisMesh)
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
}
