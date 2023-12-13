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
        itemPrefab = null;
        SpawnOnTarget_Item(item);

    }
    public void AnimateActionInitated_Capture(List<bool> isHit, GameObject CapturePrefab)
    {
        SpawnAboveTarget_Capture(isHit, CapturePrefab);

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
                    abilityInstance.transform.position = new Vector3(CombatSingleton.Instance.actionData.TargetCharacters[i].transform.position.x + (x),
                        CombatSingleton.Instance.actionData.TargetCharacters[i].transform.position.y + 1,
                        CombatSingleton.Instance.actionData.TargetCharacters[i].transform.position.z);

                    abilityInstance.transform.GetChild(0).GetComponent<MeshRenderer>().material = Resources.Load<Material>(
                        CombatSingleton.Instance.actionData.ChosenAbility.animation);
                    abilityInstance.GetComponent<MoveTowards>().Destination = CombatSingleton.Instance.actionData.TargetCharacters[i].transform.position;
                    abilityInstance.GetComponent<MoveTowards>().delay = 1.5f;
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
                abilityInstance = GameObject.Instantiate(AbilityPrefab);
                abilityInstance.transform.GetChild(0).GetComponent<MeshRenderer>().material = Resources.Load<Material>(
                    CombatSingleton.Instance.actionData.ChosenAbility.animation);
                abilityInstance.transform.position = CombatSingleton.Instance.actionData.OriginCharacter.transform.position;
                abilityInstance.GetComponent<MoveTowards>().Destination = CombatSingleton.Instance.actionData.TargetCharacters[i].transform.position;
            }
            else
            {
                Vector3 position = CombatSingleton.Instance.actionData.TargetCharacters[i].transform.position;
                abilityInstance = GameObject.Instantiate(AbilityPrefab);
                abilityInstance.transform.GetChild(0).GetComponent<MeshRenderer>().material = Resources.Load<Material>(
                    CombatSingleton.Instance.actionData.ChosenAbility.animation);
                abilityInstance.transform.position = CombatSingleton.Instance.actionData.OriginCharacter.transform.position;
                abilityInstance.GetComponent<MoveTowards>().Destination = new Vector3(position.x, position.y + 3, position.z);
            }
        }
    }
    private void Laser(List<bool> isHit, GameObject AbilityPrefab)
    {

        for (int i = 0; i < CombatSingleton.Instance.actionData.TargetCharacters.Count; i++)
        {
            if (isHit[i] == true)
            {
                abilityInstance = GameObject.Instantiate(AbilityPrefab);
                abilityInstance.transform.GetChild(0).GetComponent<MeshRenderer>().material = Resources.Load<Material>(
                    CombatSingleton.Instance.actionData.ChosenAbility.animation);
                abilityInstance.transform.position = CombatSingleton.Instance.actionData.OriginCharacter.transform.position;
                abilityInstance.GetComponent<MoveTowards>().Destination = CombatSingleton.Instance.actionData.TargetCharacters[i].transform.position;
            }
            else
            {
                Vector3 position = CombatSingleton.Instance.actionData.TargetCharacters[i].transform.position;
                abilityInstance = GameObject.Instantiate(AbilityPrefab);
                abilityInstance.transform.GetChild(0).GetComponent<MeshRenderer>().material = Resources.Load<Material>(
                    CombatSingleton.Instance.actionData.ChosenAbility.animation);
                abilityInstance.transform.position = CombatSingleton.Instance.actionData.OriginCharacter.transform.position;
                abilityInstance.GetComponent<MoveTowards>().Destination = new Vector3(position.x, position.y + 3, position.z);
            }
        }
    }
    private void SpawnOnTarget(List<bool> isHit, GameObject AbilityPrefab)
    {

        for (int i = 0; i < CombatSingleton.Instance.actionData.TargetCharacters.Count; i++)
        {
            if (isHit[i] == true)
            {
                GameObject target = CombatSingleton.Instance.actionData.TargetCharacters[i];
                abilityInstance = GameObject.Instantiate(AbilityPrefab);
                abilityInstance.transform.GetChild(0).GetComponent<MeshRenderer>().material = Resources.Load<Material>(
                    CombatSingleton.Instance.actionData.ChosenAbility.animation);
                abilityInstance.transform.position = CombatSingleton.Instance.actionData.TargetCharacters[i].transform.position;
                abilityInstance.GetComponent<MoveTowards>().Destination = new Vector3(target.transform.position.x, target.transform.position.y + 3,
                    target.transform.position.z);
            }
        }
    }
    private void SpawnAboveTarget(List<bool> isHit, GameObject AbilityPrefab)
    {

        for (int i = 0; i < CombatSingleton.Instance.actionData.TargetCharacters.Count; i++)
        {
            if (isHit[i] == true)
            {
                abilityInstance = GameObject.Instantiate(AbilityPrefab);
                abilityInstance.transform.position = new Vector3(CombatSingleton.Instance.actionData.TargetCharacters[i].transform.position.x,
                        CombatSingleton.Instance.actionData.TargetCharacters[i].transform.position.y + 2,
                        CombatSingleton.Instance.actionData.TargetCharacters[i].transform.position.z);
                abilityInstance.transform.localScale *= 4;
                abilityInstance.GetComponent<MoveTowards>().Destination = CombatSingleton.Instance.actionData.TargetCharacters[i].transform.position;
            }
            else
            {
                Vector3 position = CombatSingleton.Instance.actionData.TargetCharacters[i].transform.position;
                abilityInstance = GameObject.Instantiate(AbilityPrefab);
                abilityInstance.transform.position = new Vector3(CombatSingleton.Instance.actionData.TargetCharacters[i].transform.position.x,
                        CombatSingleton.Instance.actionData.TargetCharacters[i].transform.position.y + 2,
                        CombatSingleton.Instance.actionData.TargetCharacters[i].transform.position.z);
                abilityInstance.GetComponent<MoveTowards>().Destination = new Vector3(position.x+3, position.y, position.z);
            }
        }
    }


    /******************************************
     * 
     *          Item and Capture
     * 
     * ****************************************/

    private void SpawnOnTarget_Item(Item item)
    {

        for (int i = 0; i < CombatSingleton.Instance.actionData.TargetCharacters.Count; i++)
        {
            
        }
    }

    private void SpawnAboveTarget_Capture(List<bool> isHit, GameObject CapturePrefab)
    {

        for (int i = 0; i < CombatSingleton.Instance.actionData.TargetCharacters.Count; i++)
        {
            if (isHit[i] == true)
            {
                capturePrefab = GameObject.Instantiate(CapturePrefab);
                capturePrefab.transform.position = new Vector3(CombatSingleton.Instance.actionData.TargetCharacters[i].transform.position.x,
                        CombatSingleton.Instance.actionData.TargetCharacters[i].transform.position.y + 2,
                        CombatSingleton.Instance.actionData.TargetCharacters[i].transform.position.z);
                capturePrefab.GetComponent<MoveTowards>().Destination = CombatSingleton.Instance.actionData.TargetCharacters[i].transform.position;
                capturePrefab.GetComponent<MoveTowards>().delay = 0;
            }
            else
            {
                Vector3 position = CombatSingleton.Instance.actionData.TargetCharacters[i].transform.position;
                capturePrefab = GameObject.Instantiate(CapturePrefab);
                capturePrefab.transform.position = new Vector3(CombatSingleton.Instance.actionData.TargetCharacters[i].transform.position.x,
                        CombatSingleton.Instance.actionData.TargetCharacters[i].transform.position.y + 2,
                        CombatSingleton.Instance.actionData.TargetCharacters[i].transform.position.z);
                capturePrefab.GetComponent<MoveTowards>().Destination = new Vector3(position.x+3, position.y, position.z);
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
