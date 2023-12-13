using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsTheWorldBuilds
{
    Mesh mesh;

    Vector3[] vertices;
    int[] triangles;
    Color[] colors;

    public int xSize = 5;
    public int zSize = 5;
    public float scale = 20f;
    public float offsetX = 5f;
    public float offsetY = 5f;
    public float heightScale = 2f;
    public float persistance = 1f;
    public float lacunarity = 1f;
    public float octives = 5f;
    public int verticiesDistance = 10;

    public Gradient gradient;

    private float minimumTerrainHeight;
    private float maximumTerrainHeight;

    void CreateShapeSquare()
    {
        vertices = new Vector3[(xSize + 1) * (zSize + 1)];

        for (int i = 0, z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                //float y = Mathf.PerlinNoise(x * 0.3f, z * 0.3f) * 2f;
                GamingTools.PerlinNoise pearl = new GamingTools.PerlinNoise();
                float y = pearl.Noise(x * 0.3f, z * 0.3f, xSize, zSize, scale, offsetX, offsetY, heightScale, lacunarity, persistance, octives);
                if (y <= -100) y = 0.1f;
                vertices[i] = new Vector3(x * verticiesDistance, y, z * verticiesDistance);

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

    private void AddTriangles()
    {
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


        GameObject newGameObject = GameObject.Instantiate(new GameObject(), new Vector3(1, 5, 1), Quaternion.identity);
        newGameObject.AddComponent<MeshFilter>();
        newGameObject.AddComponent<MeshRenderer>();
        newGameObject.AddComponent<MeshCollider>();
        newGameObject.GetComponent<MeshFilter>().mesh = mesh;

        newGameObject.GetComponent<MeshCollider>().sharedMesh = mesh;
    }
}
