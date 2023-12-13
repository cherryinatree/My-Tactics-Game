using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GamingTools
{
    [System.Serializable]
    public class SaveNames
    {

        public List<string> theSaveNames;

        public SaveNames()
        {
            theSaveNames = new List<string>();
        }
        public SaveNames(SaveNames save)
        {
            theSaveNames = save.theSaveNames;
        }

    }
}
