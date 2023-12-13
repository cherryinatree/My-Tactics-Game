using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace GamingTools
{
    public static class Spawner
    {

        private static List<Vector3> vectors = new List<Vector3>();
        public static void SpawnMultipleObjects(List<GameObject> gameObjects, List<Vector3> locations)
        {
            GameObject groupHolder = new GameObject();
            groupHolder.name = gameObjects[0].name;
            for (int x = 0; x < gameObjects.Count; x++)
            {
                GameObject.Instantiate(gameObjects[x], locations[x], Quaternion.identity).transform.parent = groupHolder.transform;
            }
        }

        public static void SpawnOneObject(GameObject item, Vector3 location)
        {

            GameObject.Instantiate(item, location, Quaternion.identity);
        }


        public static List<Vector3> GetVerticeLocation(List<Vector3> vectors, int x, int min, int max)
        {
            List<Vector3> locations = new List<Vector3>();
            for (int i = 0; i < x; i++)
            {
                int rando = Random.Range(min, max);
                locations.Add(vectors[rando]);
                vectors.RemoveAt(rando);
                max = max-1;
            }

            return locations;
        }

        public static void SpawnGroups(string[] spawnMe, GroundMeshGenorator mesh, float startTenthX, float startTenthZ, float sizeInTenthsX, float sizeInTenthsZ)
        {
            List<GameObject> items = GetGameObjects(spawnMe);

            if (vectors.Count == 0)
            {
                foreach (Transform vert in mesh.square1.GetComponentsInChildren<Transform>())
                {
                    float y = mesh.GetTheNoiseY(vert.position);
                    Vector3 position = new Vector3(vert.position.x, y, vert.position.z);
                    vectors.Add(position);
                }
            }

            float mapStartX = ParseMesh.GetMeshStartX(vectors);
            float mapStartZ = ParseMesh.GetMeshStartZ(vectors);

            float endX = ParseMesh.GetMeshEndX(vectors);
            float endZ = ParseMesh.GetMeshEndZ(vectors);


            float lengthX = (Mathf.Sqrt(mapStartX * mapStartX)) + endX;
            float widthZ = (Mathf.Sqrt(mapStartZ * mapStartZ)) + endZ;

            float sizeX = sizeInTenthsX * (lengthX / 10);
            float sizeZ = sizeInTenthsZ * (widthZ / 10);

            float startX = startTenthX * (lengthX / 10);
            float startZ = startTenthZ * (widthZ / 10);

            List<Vector3> itemGroup1 = new List<Vector3>();
            itemGroup1 = ParseMesh.ParseVectors(vectors, sizeX, sizeZ, mapStartX + startX, mapStartZ + startZ);

            List<Vector3> Iverts = Spawner.GetVerticeLocation(itemGroup1, items.Count, 0, itemGroup1.Count - 1);
            Spawner.SpawnMultipleObjects(items, Iverts);
        }

        public static void SpawnPCs(List<Stats> stats, GroundMeshGenorator mesh, float startTenthX, float startTenthZ, float sizeInTenthsX, float sizeInTenthsZ)
        {
           
            string[] spawnMe = new string[stats.Count];
            int i = 0;
            foreach (var item in stats)
            {
                spawnMe[i] = "PCs/" +item.CharacterClass;
                i++;
            }
            List<GameObject> items = GetGameObjects(spawnMe);

            if (vectors.Count == 0)
            {
                foreach (Transform vert in mesh.square1.GetComponentsInChildren<Transform>())
                {
                    float y = mesh.GetTheNoiseY(vert.position);
                    Vector3 position = new Vector3(vert.position.x, y, vert.position.z);
                    vectors.Add(position);
                }
            }

            float mapStartX = ParseMesh.GetMeshStartX(vectors);
            float mapStartZ = ParseMesh.GetMeshStartZ(vectors);

            float endX = ParseMesh.GetMeshEndX(vectors);
            float endZ = ParseMesh.GetMeshEndZ(vectors);


            float lengthX = (Mathf.Sqrt(mapStartX * mapStartX)) + endX;
            float widthZ = (Mathf.Sqrt(mapStartZ * mapStartZ)) + endZ;

            float sizeX = sizeInTenthsX * (lengthX / 10);
            float sizeZ = sizeInTenthsZ * (widthZ / 10);

            float startX = startTenthX * (lengthX / 10);
            float startZ = startTenthZ * (widthZ / 10);

            List<Vector3> itemGroup1 = new List<Vector3>();
            itemGroup1 = ParseMesh.ParseVectors(vectors, sizeX, sizeZ, mapStartX + startX, mapStartZ + startZ);

            List<Vector3> Iverts = Spawner.GetVerticeLocation(itemGroup1, items.Count, 0, itemGroup1.Count - 1);
           // Spawner.SpawnMultipleObjects(items, Iverts);

            GameObject groupHolder = new GameObject();
            groupHolder.name = "PCs";
            for (int x = 0; x < items.Count; x++)
            {
                GameObject spawned =  GameObject.Instantiate(items[x], Iverts[x], Quaternion.identity);
                spawned.transform.parent = groupHolder.transform;
                spawned.GetComponent<CharacterController>().myStats = stats[x];
                spawned.GetComponent<CharacterController>().myStats.actionsLeft = spawned.GetComponent<CharacterController>().myStats.actions;
                spawned.GetComponent<CharacterController>().myStats.health = spawned.GetComponent<CharacterController>().myStats.maxHealth;
            }
        }

        public static void SpawnEnemies(List<string> enemyNames, GroundMeshGenorator mesh, float startTenthX, float startTenthZ, float sizeInTenthsX, float sizeInTenthsZ)
        {

            string[] spawnMe = new string[enemyNames.Count];
            int i = 0;
            foreach (var item in enemyNames)
            {
                spawnMe[i] = "Enemies/" + item;
                i++;
            }
            List<GameObject> items = GetGameObjects(spawnMe);

            if (vectors.Count == 0)
            {
                foreach (Transform vert in mesh.square1.GetComponentsInChildren<Transform>())
                {
                    float y = mesh.GetTheNoiseY(vert.position);
                    Vector3 position = new Vector3(vert.position.x, y, vert.position.z);
                    vectors.Add(position);
                }
            }

            float mapStartX = ParseMesh.GetMeshStartX(vectors);
            float mapStartZ = ParseMesh.GetMeshStartZ(vectors);

            float endX = ParseMesh.GetMeshEndX(vectors);
            float endZ = ParseMesh.GetMeshEndZ(vectors);


            float lengthX = (Mathf.Sqrt(mapStartX * mapStartX)) + endX;
            float widthZ = (Mathf.Sqrt(mapStartZ * mapStartZ)) + endZ;

            float sizeX = sizeInTenthsX * (lengthX / 10);
            float sizeZ = sizeInTenthsZ * (widthZ / 10);

            float startX = startTenthX * (lengthX / 10);
            float startZ = startTenthZ * (widthZ / 10);

            List<Vector3> itemGroup1 = new List<Vector3>();
            itemGroup1 = ParseMesh.ParseVectors(vectors, sizeX, sizeZ, mapStartX + startX, mapStartZ + startZ);

            List<Vector3> Iverts = Spawner.GetVerticeLocation(itemGroup1, items.Count, 0, itemGroup1.Count - 1);
            // Spawner.SpawnMultipleObjects(items, Iverts);

            GameObject groupHolder = new GameObject();
            groupHolder.name = "Enemies";
            for (int x = 0; x < items.Count; x++)
            {
                GameObject spawned = GameObject.Instantiate(items[x], Iverts[x], Quaternion.identity);
                spawned.transform.LookAt(mesh.square1.transform);
                spawned.transform.parent = groupHolder.transform;
                spawned.GetComponent<CharacterController>().myStats = CharacterCreator.EnemyCharacter(CharacterCreator.ENEMYLIST.Golem, -1);
                spawned.GetComponent<CharacterController>().myStats.actionsLeft = spawned.GetComponent<CharacterController>().myStats.actions;
                spawned.GetComponent<CharacterController>().myStats.health = spawned.GetComponent<CharacterController>().myStats.maxHealth;
            }
        }

        private static List<GameObject> GetGameObjects(string[] itemList)
        {
            List<GameObject> items = new List<GameObject>();
            for (int i = 0; i < itemList.Length; i++)
            {

                items.Add(ResourseLoader.GetGameObject("Prefabs/" + itemList[i]));
            }

            return items;
        }

        public static void Clear()
        {
            vectors.Clear();
        }
    }

}
