using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapEditor : MonoBehaviour 
{
    public int[] makeMeYellow;
    public int gridx;

    public int gridy;
    public float yChange;
    public bool applyChange = false;

    public GameObject useMyMesh;

    public Vector4[] squares;
    // Start is called before the first frame update
    void Start()
    {
        squares = new Vector4[100];
        for (int x = 0, count = 0, vert = 0; x < 10; x++)
        {

            for (int i = 0; i < 10; i++)
            {
                squares[count].w = count + 0;
                squares[count].x = count + 1;
                squares[count].y = count + 11;
                squares[count].z = count + 12;
                count++;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            applyChange = true;
        }


        if (applyChange == true)
        {
            Mesh mesh = useMyMesh.GetComponent<MeshFilter>().sharedMesh;
            List<Color> verts = new List<Color>();
            mesh.GetColors(verts);

            for (int i = 0; i < verts.Count; i++)
            {

                verts[i] = Color.red;

            }

            foreach (int a in makeMeYellow)
            {
                verts[(int)squares[a].w] = Color.yellow;
                verts[(int)squares[a].x] = Color.yellow;
                verts[(int)squares[a].y] = Color.yellow;
                verts[(int)squares[a].z] = Color.yellow;
            }

            
            /*
            verts[0] = new Vector3(
                verts[0].x,
                verts[0].y + 0.2f,
                verts[0].z);
            verts[1] = new Vector3(
                verts[1].x,
                verts[1].y + 0.2f,
                verts[1].z);
            verts[11] = new Vector3(
                    verts[11].x,
                    verts[11].y + 0.2f,
                    verts[11].z);
            verts[12] = new Vector3(
                    verts[12].x,
                    verts[12].y + 0.2f,
                    verts[12].z);
                    */

            // verts[0] = Color.yellow;
            // verts[1] = Color.yellow;
            //verts[11] = Color.yellow;
            //verts[12] = Color.yellow;
            //verts[13] = Color.yellow;
            mesh.SetColors(verts);
            mesh.RecalculateNormals();
            /* List<Vector3> verts = new List<Vector3>();
             mesh = useMyMesh.GetComponent<MeshCollider>().sharedMesh;
             mesh.vertices[1].y = 5;
             useMyMesh.GetComponent<MeshCollider>().sharedMesh.SetVertices(mesh.vertices);
             useMyMesh.GetComponent<MeshCollider>().sharedMesh.RecalculateNormals();
             applyChange = false;*/
            applyChange = false;
            Debug.Log(useMyMesh.GetComponent<MeshCollider>().sharedMesh.vertices[1]);
            Debug.Log(useMyMesh.name);
        }
    }
}
