using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TriggerEvent : MonoBehaviour
{

    private static bool isFirst = true;

    public enum TriggerType { triggers, changes, movement }
    public TriggerType type;

    public TriggerMoveData moveData;
    public TriggerData triggerData;
    public MyChange myChanges;



    protected void SetUpData()
    {
        if (isFirst)
        {
            CombatSingleton.Instance.SaveTriggers = new List<GameObject>();
            CombatSingleton.Instance.SaveObjects = new List<GameObject>();
            CombatSingleton.Instance.SaveMovers = new List<GameObject>();
            isFirst = false;
        }
        if (SaveData.Current.mainData.loadSceneData.boardChanges == null)
        {
            SaveData.Current.mainData.loadSceneData.boardChanges = new BoardChanges();
        }

        if (type == TriggerType.triggers)
        {
            if (SaveData.Current.mainData.loadSceneData.boardChanges.triggers != null)
            {
                FindPreviousChanges();
            }
            CombatSingleton.Instance.SaveTriggers.Add(gameObject);
        }
        else if (type == TriggerType.changes)
        {
            if (SaveData.Current.mainData.loadSceneData.boardChanges.changes != null)
            {
                FindPreviousChanges();
            }
            CombatSingleton.Instance.SaveObjects.Add(gameObject);
        }
        else if (type == TriggerType.movement)
        {
            if (SaveData.Current.mainData.loadSceneData.boardChanges.movers != null)
            {
                FindPreviousChanges();
            }
            CombatSingleton.Instance.SaveMovers.Add(gameObject);
        }
        
    }

    public void UploadChangesToSave()
    {

        if (SaveData.Current.mainData.loadSceneData.boardChanges == null)
            SaveData.Current.mainData.loadSceneData.boardChanges = new BoardChanges();

        if (SaveData.Current.mainData.loadSceneData.boardChanges.triggers == null)
            SaveData.Current.mainData.loadSceneData.boardChanges.triggers = new List<TriggerData>();
        if (SaveData.Current.mainData.loadSceneData.boardChanges.movers == null)
            SaveData.Current.mainData.loadSceneData.boardChanges.movers = new List<TriggerMoveData>();
        if (SaveData.Current.mainData.loadSceneData.boardChanges.changes == null)
            SaveData.Current.mainData.loadSceneData.boardChanges.changes = new List<MyChange>();



        if (type == TriggerType.triggers) UploadTrigger();
        if (type == TriggerType.movement) UploadMovers();
        if (type == TriggerType.changes) UploadChangers();
    }

    private void UploadTrigger()
    {

        triggerData.nameID = gameObject.name;

        if (SaveData.Current.mainData.loadSceneData.boardChanges.triggers.Count > 0)
        {
            bool isInList = false;
            foreach (TriggerData change in SaveData.Current.mainData.loadSceneData.boardChanges.triggers)
            {
                if (change.nameID == gameObject.name)
                {
                    change.isUsed = triggerData.isUsed;
                    isInList = true;
                    break;
                }
            }

            if (!isInList)
            {
                SaveData.Current.mainData.loadSceneData.boardChanges.triggers.Add(triggerData);
            }
        }
        else
        {
            SaveData.Current.mainData.loadSceneData.boardChanges.triggers.Add(triggerData);
        }
    }
    private void UploadMovers()
    {
        moveData.nameID = gameObject.name;
        if (SaveData.Current.mainData.loadSceneData.boardChanges.movers.Count > 0)
        {
            bool isInList = false;
            foreach (TriggerMoveData change in SaveData.Current.mainData.loadSceneData.boardChanges.movers)
            {
                if (change.nameID == gameObject.name)
                {
                    change.currentDestination = moveData.currentDestination;
                    isInList = true;
                    break;
                }
            }

            if (!isInList)
            {
                SaveData.Current.mainData.loadSceneData.boardChanges.movers.Add(moveData);
            }
        }
        else
        {
            SaveData.Current.mainData.loadSceneData.boardChanges.movers.Add(moveData);
        }
    }
    private void UploadChangers()
    {
        myChanges.position = transform.position;
        myChanges.rotation = transform.rotation.eulerAngles;
        myChanges.nameID = gameObject.name;
        if (SaveData.Current.mainData.loadSceneData.boardChanges.changes.Count > 0)
        {
            bool isInList = false;
            foreach (MyChange change in SaveData.Current.mainData.loadSceneData.boardChanges.changes)
            {
                if (change.nameID == gameObject.name)
                {
                    change.position = myChanges.position;
                    change.rotation = myChanges.rotation;
                    isInList = true;
                    break;
                }
            }

            if (!isInList)
            {
                SaveData.Current.mainData.loadSceneData.boardChanges.changes.Add(myChanges);
            }
        }
        else
        {
            SaveData.Current.mainData.loadSceneData.boardChanges.changes.Add(myChanges);
        }
    }








    private void FindPreviousChanges()
    {
        if (type == TriggerType.triggers)
        {
            foreach (TriggerData change in SaveData.Current.mainData.loadSceneData.boardChanges.triggers)
            {
                if (change.nameID == gameObject.name)
                {
                    LoadPreviousChanges(change);
                    break;
                }
            }
        }
        if (type == TriggerType.changes)
        {
            foreach (MyChange change in SaveData.Current.mainData.loadSceneData.boardChanges.changes)
            {
                if (change.nameID == gameObject.name)
                {
                    LoadPreviousChanges(change);
                    break;
                }
            }
        }
        if (type == TriggerType.movement)
        {
            foreach (TriggerMoveData change in SaveData.Current.mainData.loadSceneData.boardChanges.movers)
            {
                if (change.nameID == gameObject.name)
                {
                    LoadPreviousChanges(change);
                    break;
                }
            }
        }
    }

    public void LoadPreviousChanges(TriggerData changes)
    {
        triggerData = changes;
    }
    public void LoadPreviousChanges(MyChange changes)
    {
        myChanges = changes;
        transform.position = changes.position;
        transform.eulerAngles = new Vector3(changes.rotation.x, changes.rotation.y, changes.rotation.z);
    }
    public void LoadPreviousChanges(TriggerMoveData changes)
    {
        moveData = changes;
    }

    public abstract void TriggerMe();

    
}
