using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsPlayerCanBuild : MonoBehaviour
{
    Mesh mesh;

    List<Vector3> vertices;
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
    public int verticiesDistance = 1;

    public Gradient gradient;

    private float minimumTerrainHeight;
    private float maximumTerrainHeight;

    public void CreateShapeSquare()
    {
        mesh = new Mesh();

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



    public void CreateRectangularObject()
    {

        Vector3 origin = new Vector3(0, 0, 0);
        float radius = 4;
        






        mesh = new Mesh();
        vertices = new List<Vector3>();
        float[] y = new float[10];
        float[] x = new float[100];
        float[] z = new float[100];
        int r = 0;
        int count = 0;/*
        for (float i = 0; i < Mathf.PI*2; i+=((Mathf.PI*2)/10))
        {
           
                y[count] = Mathf.Sin(i);
                count++;
            
        }
        

        for (int k = 0, l = 0; k < 10; k++)
        {
            for (float i = 0; i < Mathf.PI * 2; i += ((Mathf.PI * 2) / 10))
            {
                x[l] = Mathf.Cos(i) * (1 - y[k]);
                z[l] = Mathf.Sin(i) * (1 - y[k]);

                l++;
            }

        }*/
        int counter = 0;
        int counter2 = 0;
        for(float i = 0; i <= Mathf.PI/2; i += Mathf.PI/2/10)
        {
            y[counter] = Mathf.Sin(i);
            for (float q = 0; q <= Mathf.PI*2; q+=(Mathf.PI*2)/10)
            {
                
                x[counter2] = Mathf.Sin(q) * (Mathf.Sin(Mathf.PI/11)*i+1);
                z[counter2] = Mathf.Cos(q) * (Mathf.Sin(Mathf.PI / 11) * i+1);
                counter2++;
               Debug.Log(y[counter]);
                //Debug.Log(counter2);
            }
            counter++;
        }
        //Debug.Log(y.Length + "aaaaa");


        counter = 0;
        counter2 = 0;
        for (int p = 0; p < y.Length; p++)
        {
            for (int i = 0; i < y.Length; i++)
            {
                vertices.Add(new Vector3(x[counter2], y[counter], z[counter2]));
                if (i == 5)
                {
                    Debug.Log(x[counter2] +" : " + y[p]+ " : "+z[counter2]);

                }
                counter2++;

            }
        }


        SphereTriangles();


    }


    private void SphereTriangles()
    {
        triangles = new int[100 * 6];
        int vert = 0;
        int tris = 0;

        for(int r =0; r < 10; r++)
        {

            for (int z = 0; z < 10; z++)
            {
                triangles[tris + 0] = vert + 0;
                triangles[tris + 1] = vert + 10 + 1;
                triangles[tris + 2] = vert + 1;
                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + 10 + 1;
                triangles[tris + 5] = vert + 10 + 2;

                vert++;
                tris += 6;


            }
        }

/*
        colors = new Color[vertices.Count];
        gradient = new Gradient();
        for (int x = 0; x < vertices.Count; x++)
        {
            float height = Mathf.InverseLerp(minimumTerrainHeight, maximumTerrainHeight, vertices[x].y);
            colors[x] = Color.red;
        }*/

        UpdateMesh();
    }





    public void CreateHouse()
    {

        mesh = new Mesh();
        vertices = new List<Vector3>();

        /* vertices.Add(new Vector3(0, 0, 0)); i++;
         vertices.Add(new Vector3(0, 1, 0)); i++;
         vertices.Add(new Vector3(1, 0, 0)); i++;
         vertices.Add(new Vector3(1, 0, 0)); i++;
         vertices.Add(new Vector3(0, 1, 0)); i++;
         vertices.Add(new Vector3(1, 1, 0)); i++;
         vertices.Add(new Vector3(0, 1, 0)); i++;
         vertices.Add(new Vector3(2, 2, 0)); i++;
         vertices.Add(new Vector3(2, 1, 0)); i++;
         vertices.Add(new Vector3(2, 1, 0)); i++;
         vertices.Add(new Vector3(2, 2, 0)); i++;
         vertices.Add(new Vector3(4, 1, 0)); i++;
         vertices.Add(new Vector3(3, 0, 0)); i++;
         vertices.Add(new Vector3(3, 1, 0)); i++;
         vertices.Add(new Vector3(4, 0, 0)); i++;
         vertices.Add(new Vector3(4, 0, 0)); i++;
         vertices.Add(new Vector3(3, 1, 0)); i++;
         vertices.Add(new Vector3(4, 1, 0)); i++;*/


        float[] xyz = new float[3];
        xyz[0] = 3;
        xyz[1] = 2;
        xyz[2] = 0;
        xyz = CreateSquareXYRight(xyz, 4, 1f);
        xyz = CreateSquareXYForward(xyz, 1, 1f);

        //VerticleSquareRight(10, 0,10, 0, 0, 0, 1);

        xyz = CreateSquareXYLeft(xyz, 4, 1f);



      /*  for (int i = 0; i < vertices.Count; i++)
        {
            vertices[i] *= 2;
        }*/

        /*
        vertices[i] = new Vector3(0, 0, 0); i++;
        vertices[i] = new Vector3(0, 1, 0); i++;
        vertices[i] = new Vector3(1, 0, 0); i++;
        vertices[i] = new Vector3(1, 0, 0); i++;
        */

        VertsAreTriangles();
    }

    public float[] CreateSquareXYRight(float[] xyz, int length, float scale)
    {
        float theX = xyz[0];
        float theY = xyz[1];

        for (float b = theY; b < theY+1; b++)
        {
            for (float a = theX; a <= (theX + length); a++)
            {
                VerticleSquareRight(a, 0, b, 0, xyz[2], 0, scale);
                xyz[0] = a * scale;
                xyz[1] = b;
            }
        }
        return xyz;
    }
    public float[] CreateSquareXYForward(float[] xyz, int length, float scale)
    {
        float theX = xyz[2];
        float theY = xyz[1];

        for (float b = theY; b < theY + 1; b++)
        {
            for (float a = theX; a <= (theX + length); a++)
            {
                VerticleSquareForward(0, xyz[0], b, 0, a, 0, scale);
                xyz[2] = a*scale;
                xyz[1] = b;
            }
        }
        return xyz;
    }
    public float[] CreateSquareXYLeft(float[] xyz, int length, float scale)
    {

        float theX = xyz[0];
        float theY = xyz[1];

        for (float b = theY; b < theY + 1; b++)
        {
            for (float a = theX; a >= (theX - length); a--)
            {
                VerticleSquareRight(a, 0, b, 0, 0, xyz[2], scale);
                xyz[0] = a * scale;
                xyz[1] = b;
            }
        }
        return xyz;
    }
    /*
    public void CreateSquareXY(int x, int y, int z, int height, int length, int scale)
    {
        for (int i = 0, a = x; a <= length; a++)
        {
            for (int b = y; b <= height; b++)
            {
                VerticleSquareRight(a, 0, b, 0, xyz[2], 0, scale);
                i++;
            }
        }
    }*/


    private void VerticleSquareRight(float xOne,float xZero, float yOne, float yZero, float zOne, float zZero, float scale)
    {
        xOne *= scale;
        vertices.Add(new Vector3(xZero, yZero, zZero)); 
        vertices.Add(new Vector3(xZero, yOne, zZero)); 
        vertices.Add(new Vector3(xOne, yZero, zZero)); 
        vertices.Add(new Vector3(xOne, yZero, zZero));
        vertices.Add(new Vector3(xZero, yOne, zZero));
        vertices.Add(new Vector3(xOne, yOne, zZero));
    }
    private void VerticleSquareForward(float xOne, float xZero, float yOne, float yZero, float zOne, float zZero, float scale)
    {
        zOne *= scale;

        vertices.Add(new Vector3(xZero, yZero, zZero));
        vertices.Add(new Vector3(xZero, yOne, zZero));
        vertices.Add(new Vector3(xZero, yZero, zOne));
        vertices.Add(new Vector3(xZero, yZero, zOne));
        vertices.Add(new Vector3(xZero, yOne, zZero));
        vertices.Add(new Vector3(xZero, yOne, zOne));
    }


    private void VertsAreTriangles()
    {
        triangles = new int[vertices.Count*3];
        int vert = 0;
        int tris = 0;

        for (int z = 0; z < vertices.Count; z++)
        {
            triangles[z] = z;

        }

        colors = new Color[vertices.Count];
        gradient = new Gradient();
          for (int z = 0; z < vertices.Count; z++)
           {
                   //float height = Mathf.InverseLerp(minimumTerrainHeight, maximumTerrainHeight, vertices[i].y);
                   colors[z] = gradient.Evaluate(0.5f);
                   colors[z] = Color.red;

               
           }
        for (int i = 0; i < vertices.Count; i++)
        {
            colors[i] = Color.red;
        }
        UpdateMesh();

    }

    private void AddTriangles()
    {
        triangles = new int[6];
        int vert = 0;
        int tris = 0;

        for (int z = 0; z < 1; z++)
        {
                triangles[tris + 0] = vert + 0;
                triangles[tris + 1] = vert + 1 + 1;
                triangles[tris + 2] = vert + 1;
                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + 1 + 1;
                triangles[tris + 5] = vert + 1 + 2;

                vert++;
                tris += 6;

            

        }

        colors = new Color[vertices.Count];
        gradient = new Gradient();
            for (int x = 0; x < vertices.Count; x++)
            {
                float height = Mathf.InverseLerp(minimumTerrainHeight, maximumTerrainHeight, vertices[x].y);
                colors[x] = Color.red;
            }
        
        UpdateMesh();
    }

 


    void UpdateMesh()
    {

        mesh.Clear();

        Vector3[] vert = new Vector3[vertices.Count];
        for (int i = 0; i < vertices.Count; i++)
        {
            vert[i] = vertices[i];
        }


        mesh.vertices = vert;
        mesh.triangles = triangles;
       // mesh.colors = colors;

        mesh.RecalculateNormals();


        GameObject newGameObject = GameObject.Instantiate(new GameObject(), new Vector3(-1, 1, 5), Quaternion.identity);
        newGameObject.AddComponent<MeshFilter>();
        newGameObject.AddComponent<MeshRenderer>();
        newGameObject.AddComponent<MeshCollider>();
        newGameObject.GetComponent<MeshFilter>().mesh = mesh;

        newGameObject.GetComponent<MeshCollider>().sharedMesh = mesh;
        



    }
}
