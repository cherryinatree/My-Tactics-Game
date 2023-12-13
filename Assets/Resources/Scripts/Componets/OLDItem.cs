using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class OLDItem
{
    public bool consumable = false;
    public string name;
   // public int _GenericIDnumber;
    public int _IDnumber;
    // location should say inventory or (characterName)Inventory
    public string location;
    public string type;
    public string subType;
    public string image;
    public int amountInStack;
    // public enum TYPE { WEAPON = 0, ARMOR = 1, CONSUMABLE = 2, FURNITURE = 3 };
    // private TYPE _Type;

    public float _benifit0;
    private float _benifit1;
    private float _benifit2;
    private float _benifit3;
    private float _downSide0;
    private float _downSide1;
    private float _downSide2;
    private float _downSide3;
    private float _price;
   // private Vector3 _sizeMultiplier;
    //private Vector3 _position;
    private string _myPrefab;
    private string _name;



    public OLDItem(int id)
    {
        IDnumber = id;
    }
    public OLDItem()
    {
    }


    /*
     private void Awake()
     {
         LoadFiles load = new LoadFiles();
         Item item = load.itemStatFile(gameObject.GetComponent<Item>().IDnumber);

         IDnumber = item.IDnumber;
         //GenericIDnumber = item.GenericIDnumber;
        // Debug.Log("Generic: " + item.GenericIDnumber);
        // Type = item.Type;
         benifit0 = item.benifit0;
         benifit1 = item.benifit1;
         benifit2 = item.benifit2;
         benifit3 = item.benifit3;
         downSide0 = item.downSide0;
         downSide1 = item.downSide1;
         downSide2 = item.downSide2;
         downSide3 = item.downSide3;
         price = item.price;
         //sizeMultiplier = item.sizeMultiplier;
         //position = item.position;
         myPrefab = item.myPrefab;
         Name = item.Name;
         gameObject.name = Name;
         //gameObject.transform.position = position;
         //gameObject.transform.localScale = sizeMultiplier;
     }


     public Item(Item item)
     {
         IDnumber = item.IDnumber;
         //GenericIDnumber = item.GenericIDnumber;
        // Type = item.Type;
         benifit0 = item.benifit0;
         benifit1 = item.benifit1;
         benifit2 = item.benifit2;
         benifit3 = item.benifit3;
         downSide0 = item.downSide0;
         downSide1 = item.downSide1;
         downSide2 = item.downSide2;
         downSide3 = item.downSide3;
         price = item.price;
         //sizeMultiplier = item.sizeMultiplier;
         //position = item.position;
         myPrefab = item.myPrefab;
         Name = item.Name;
     }

   /*  public void SaveMeToFile()
     {
         string itemStats = IDnumber.ToString() +","+ GenericIDnumber.ToString() + "," +
             Type.ToString() + "," + benifit0.ToString() + "," + benifit1.ToString() + "," + benifit2.ToString() + "," +
             benifit3.ToString() + "," + downSide0.ToString() + "," + downSide1.ToString() + "," + downSide2.ToString() + "," +
             downSide3.ToString() + "," + price.ToString() + "," + sizeMultiplier.x.ToString() + "," +
             sizeMultiplier.y.ToString() + "," + sizeMultiplier.z.ToString() + ","+ position.x.ToString() + 
             "," + position.y.ToString() + "," + position.z.ToString() + "," + myPrefab + "," + Name;
         LoadFiles save = new LoadFiles();
         save.SaveItemStatFile(GenericIDnumber, itemStats);
     }*/
    // public void Update()
    // {
    //sizeMultiplier = gameObject.transform.localScale;
    //position = gameObject.transform.position;
    //  }

    /*  public void resizeMe(float x, float y, float z)
      {
          gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x +x,
              gameObject.transform.localScale.y + y, gameObject.transform.localScale.z + z);
      }*/

    public bool Consumable
    {
        get { return consumable; }
        set { consumable = value; }
    }
    public int IDnumber
    {
        get { return _IDnumber; }
        set { _IDnumber = value; }

    }
   /* public int GenericIDnumber
    {
        get { return _GenericIDnumber; }
        set { _GenericIDnumber = value; }
    }*/
    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }
    public float benifit0
    {
        get { return _benifit0; }
        set { _benifit0 = value; }
    }
    public float benifit1
    {
        get { return _benifit1; }
        set { _benifit1 = value; }
    }
    public float benifit2
    {
        get { return _benifit2; }
        set { _benifit2 = value; }
    }
    public float benifit3
    {
        get { return _benifit3; }
        set { _benifit3 = value; }
    }
    public float downSide0
    {
        get { return _downSide0; }
        set { _downSide0 = value; }
    }
    public float downSide1
    {
        get { return _downSide1; }
        set { _downSide1 = value; }
    }
    public float downSide2
    {
        get { return _downSide2; }
        set { _downSide2 = value; }
    }
    public float downSide3
    {
        get { return _downSide3; }
        set { _downSide3 = value; }
    }
    public float price
    {
        get { return _price; }
        set { _price = value; }
    }
   /* public Vector3 sizeMultiplier
    {
        get { return _sizeMultiplier; }
        set { _sizeMultiplier = value; }
    }
    public Vector3 position
    {
        get { return _position; }
        set { _position = value; }
    }
    */
    public string myPrefab
    {
        get { return _myPrefab; }
        set { _myPrefab = value; }
    }
}
