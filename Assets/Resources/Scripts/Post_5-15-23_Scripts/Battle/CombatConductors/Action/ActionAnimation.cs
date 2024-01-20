using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionAnimation
{
    GameObject abilityInstance;



    GameObject abilityPrefab;
    GameObject capturePrefab;
    GameObject itemPrefab;


    public void AnimateActionInitated_Item(Item item)
    {
        itemPrefab = GameObject.Instantiate(GamingTools.ResourseLoader.GetGameObject(item.animation));
        SpawnOnTarget_Item(item);

    }
    public void AnimateActionInitated_Capture(List<bool> isHit, GameObject CapturePrefab)
    {
        SpawnCapture(isHit, CapturePrefab);

    }




    public void AnimateActionInitated_Range(List<bool> isHit, GameObject AbilityPrefab, Abilities ability)
    {

        switch (ability.animationType)
        {
            case ("Melee"):
                Melee(isHit, AbilityPrefab);
                break;
            case ("Missle"):
                Missle(isHit, AbilityPrefab);
                break;
            case ("Laser"):
                Laser(isHit, AbilityPrefab);
                break;
            case ("SpawnOnTarget"):
                SpawnOnTarget(isHit, AbilityPrefab);
                break;
            case ("SpawnAboveTarget"):
                SpawnAboveTarget(isHit, AbilityPrefab);
                break;
            case ("SpawnAtTargetFeet"):
                SpawnAtTargetFeet(isHit, AbilityPrefab);
                break;
        }



    }


    /******************************************
     * 
     *          The Instantiation
     * 
     * ****************************************/

    private void Melee(List<bool> isHit, GameObject AbilityPrefab)
    {
        for (int i = 0; i < CombatSingleton.Instance.actionData.TargetCharacters.Count; i++)
        {
            if (isHit[i] == true)
            {

                for (int x = 0; x < 3; x++)
                {
                    abilityInstance = GameObject.Instantiate(AbilityPrefab);
                    Vector3 position = CombatSingleton.Instance.actionData.OriginCharacter.transform.Find("Instantiates").Find("Cast").position;

                    abilityInstance.transform.position = position;
                    abilityInstance.transform.LookAt(
                        CombatSingleton.Instance.actionData.TargetCharacters[i].transform.Find("Instantiates").Find("HitSpot").position);

                }
            }
        }
    }
    private void Missle(List<bool> isHit, GameObject AbilityPrefab)
    {
        for (int i = 0; i < CombatSingleton.Instance.actionData.TargetCharacters.Count; i++)
        {
            if (isHit[i] == true)
            {
                Vector3 position = CombatSingleton.Instance.actionData.TargetCharacters[i].transform.Find("Instantiates").Find("HitSpot").position;
                abilityInstance = GameObject.Instantiate(AbilityPrefab);
                Transform originPosition = CombatSingleton.Instance.actionData.OriginCharacter.transform.Find("Instantiates").Find("Cast");
                abilityInstance.transform.position = originPosition.position;
                abilityInstance.transform.LookAt(position);
            }
            else
            {
                Vector3 position = CombatSingleton.Instance.actionData.TargetCharacters[i].transform.Find("Instantiates").Find("MissSpot").position;
                abilityInstance = GameObject.Instantiate(AbilityPrefab);
                Transform originPosition = CombatSingleton.Instance.actionData.OriginCharacter.transform.Find("Instantiates").Find("Cast");
                abilityInstance.transform.position = originPosition.position;
                abilityInstance.transform.LookAt(position);
            }
        }
    }

    private void Laser(List<bool> isHit, GameObject AbilityPrefab)
    {


        for (int i = 0; i < CombatSingleton.Instance.actionData.TargetCharacters.Count; i++)
        {
            if (isHit[i] == true)
            {
                Vector3 position = CombatSingleton.Instance.actionData.TargetCharacters[i].transform.Find("Instantiates").Find("HitSpot").position;
                abilityInstance = GameObject.Instantiate(AbilityPrefab);
                Transform originPosition = CombatSingleton.Instance.actionData.OriginCharacter.transform.Find("Instantiates").Find("Cast");
                abilityInstance.transform.position = originPosition.position;
                abilityInstance.transform.LookAt(position);
            }
            else
            {
                Vector3 position = CombatSingleton.Instance.actionData.TargetCharacters[i].transform.Find("Instantiates").Find("MissSpot").position;
                abilityInstance = GameObject.Instantiate(AbilityPrefab);
                Transform originPosition = CombatSingleton.Instance.actionData.OriginCharacter.transform.Find("Instantiates").Find("Cast");
                abilityInstance.transform.position = originPosition.position;
                abilityInstance.transform.LookAt(position);
            }
        }
    }
    private void SpawnOnTarget(List<bool> isHit, GameObject AbilityPrefab)
    {

        for (int i = 0; i < CombatSingleton.Instance.actionData.TargetCharacters.Count; i++)
        {
            if (isHit[i] == true)
            {
                Vector3 position = CombatSingleton.Instance.actionData.TargetCharacters[i].transform.Find("Instantiates").Find("Body").position;
                abilityInstance = GameObject.Instantiate(AbilityPrefab);
                abilityInstance.transform.position = position;
            }
        }
    }
    private void SpawnAboveTarget(List<bool> isHit, GameObject AbilityPrefab)
    {
        bool atLeast1IsHit = false;

        for (int i = 0; i < CombatSingleton.Instance.actionData.TargetCharacters.Count; i++)
        {

            if (isHit[i] == true)
            {
                atLeast1IsHit = true;
            }
        }

        if (atLeast1IsHit)
        {
            abilityInstance = GameObject.Instantiate(AbilityPrefab);
            Vector3 position = CombatSingleton.Instance.CursorCube.transform.position;
            abilityInstance.transform.position = new Vector3(position.x, position.y + 3, position.z);
            abilityInstance.transform.LookAt(position);
        }
        else
        {

            abilityInstance = GameObject.Instantiate(AbilityPrefab);
            Vector3 position = CombatSingleton.Instance.CursorCube.transform.position;
            abilityInstance.transform.position = new Vector3(position.x, position.y + 3, position.z);
            abilityInstance.transform.LookAt(new Vector3(position.x, position.y + 4, position.z));
        }
    }
    private void SpawnAtTargetFeet(List<bool> isHit, GameObject AbilityPrefab)
    {
        bool atLeast1IsHit = false;

        for (int i = 0; i < CombatSingleton.Instance.actionData.TargetCharacters.Count; i++)
        {

            if (isHit[i] == true)
            {
                atLeast1IsHit = true;
            }
        }

        if (atLeast1IsHit)
        {
            abilityInstance = GameObject.Instantiate(AbilityPrefab);
            if (CombatSingleton.Instance.actionData.TargetCharacters.Count > 1)
            {
                Vector3 position = CombatSingleton.Instance.CursorCube.transform.position;
                abilityInstance.transform.position = new Vector3(position.x, position.y + 0.5f, position.z);
            }
            else 
            {

                Vector3 position = CombatSingleton.Instance.actionData.TargetCharacters[0].transform.Find("Instantiates").Find("Feet").position;
                abilityInstance.transform.position = new Vector3(position.x, position.y, position.z);
            }
        }
        else
        {
            abilityInstance = GameObject.Instantiate(AbilityPrefab);
            Vector3 position = CombatSingleton.Instance.CursorCube.transform.position;
            abilityInstance.transform.position = new Vector3(position.x, position.y + 4, position.z);
            abilityInstance.transform.LookAt(new Vector3(position.x, position.y + 6, position.z));
        }
    }


    /******************************************
     * 
     *          Item and Capture
     * 
     * ****************************************/

    private void SpawnOnTarget_Item(Item item)
    {
       // Debug.Log("Target Characters count: " + CombatSingleton.Instance.actionData.TargetCharacters.Count);
        for (int i = 0; i < CombatSingleton.Instance.actionData.TargetCharacters.Count; i++)
        {

            //Debug.Log("Target Characters i: " + i);
            itemPrefab = GameObject.Instantiate(itemPrefab);
            Vector3 position = CombatSingleton.Instance.actionData.TargetCharacters[0].transform.Find("Instantiates").Find("Body").position;
            itemPrefab.transform.position = position;
        }
    }

    private void SpawnCapture(List<bool> isHit, GameObject CapturePrefab)
    {

        for (int i = 0; i < CombatSingleton.Instance.actionData.TargetCharacters.Count; i++)
        {
            if (isHit[i] == true)
            {
                capturePrefab = GameObject.Instantiate(CapturePrefab);
                Vector3 position = CombatSingleton.Instance.actionData.TargetCharacters[0].transform.Find("Instantiates").Find("Body").position;
                capturePrefab.transform.position = position;
            }
            else
            {
                Vector3 position = CombatSingleton.Instance.actionData.TargetCharacters[i].transform.position;
                capturePrefab = GameObject.Instantiate(CapturePrefab);
                capturePrefab.transform.position = new Vector3(CombatSingleton.Instance.actionData.TargetCharacters[i].transform.position.x,
                        CombatSingleton.Instance.actionData.TargetCharacters[i].transform.position.y + 2,
                        CombatSingleton.Instance.actionData.TargetCharacters[i].transform.position.z);
            }
        }
    }

    /******************************************
     * 
     *          CheckIfDone
     * 
     * ****************************************/

    public bool isAnimateActionCompleted_Range()
    {
        if (abilityInstance == null)
        {
            return true;
        }

        return false;
    }
    public bool isAnimateActionCompleted_Item()
    {
        if (itemPrefab == null)
        {
            return true;
        }

        return false;
    }
    public bool isAnimateActionCompleted_Capture()
    {
        if (capturePrefab == null)
        {
            return true;
        }

        return false;
    }
}
