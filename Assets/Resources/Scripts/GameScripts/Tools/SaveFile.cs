using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GamingTools
{
    [System.Serializable]
    public class SaveFile
    {
        public string saveName;
        public List<Stats> characterRoster;
        public List<Stats> characterBattleTeam;
        public GameProgress game;

        public int battleMap;
        public int enemyChallengeRating;
        public List<string> enemies;
        public List<int> numberOfEnemies;

        public int characterIDs;

        public SaveFile()
        {
            characterRoster = new List<Stats>();
            characterBattleTeam = new List<Stats>();
            characterIDs = 0;
        }
    }
}