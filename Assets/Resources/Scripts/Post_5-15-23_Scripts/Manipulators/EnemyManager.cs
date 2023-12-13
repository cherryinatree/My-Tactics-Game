using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    private List<Character> enemyRoster;
    private List<Character> monsterRoster;
    private List<Character> villiabRoster;
    JsonRetriever jsonRetriever;

    public EnemyManager()
    {
        jsonRetriever = new JsonRetriever();
        // Add enemy definitions to the roster
        // e.g., enemyRoster.Add(new Enemy("Enemy1", 1, 100, 20, 10));
        // Add more enemies as needed
    }

    public Character GetRandomEnemy(int minLevel, int maxLevel)
    {
        enemyRoster = jsonRetriever.LoadAllCharacters(RetrieverConstants.JsonEnemy);
        // Filter enemies based on player's level
        List<Character> availableEnemies = new List<Character>();

        foreach (Character character in enemyRoster)
        {
            if(character.level <= maxLevel && character.level >= minLevel)
            {
                availableEnemies.Add(character);
            }
        }
        
        if (availableEnemies.Count == 0)
        {
            // Handle the case when no suitable enemy is found
            return null;
        }

        // Select a random enemy from the availableEnemies list
        int randomIndex = Random.Range(0, availableEnemies.Count);
        return availableEnemies[randomIndex];
    }
    public Character GetEnemyById(int id)
    {
        enemyRoster = jsonRetriever.LoadAllCharacters(RetrieverConstants.JsonEnemy);
        // Filter enemies based on player's level
        Character enemy = new Character();

        foreach (Character character in enemyRoster)
        {
            if (character.id == id)
            {
                enemy = character;
            }
        }

        return enemy;
    }
    public Character GetRandomMonster(int minLevel, int maxLevel)
    {
        monsterRoster = jsonRetriever.LoadAllCharacters(RetrieverConstants.JsonMonster);
        // Filter enemies based on player's level
        List<Character> availableMonsters = new List<Character>();

        foreach (Character character in monsterRoster)
        {
            if (character.level <= maxLevel && character.level >= minLevel)
            {
                availableMonsters.Add(character);
            }
        }

        if (availableMonsters.Count == 0)
        {
            // Handle the case when no suitable enemy is found
            return null;
        }

        // Select a random enemy from the availableEnemies list
        int randomIndex = Random.Range(0, availableMonsters.Count);
        return availableMonsters[randomIndex];
    }


    public Character GetMonsterById(int id)
    {
        monsterRoster = jsonRetriever.LoadAllCharacters(RetrieverConstants.JsonMonster);
        // Filter enemies based on player's level
        Character monster = new Character();

        foreach (Character character in monsterRoster)
        {
            if (character.id == id)
            {
                monster = character;
            }
        }

        return monster;
    }


    public Character GetVillianById(int id)
    {
        villiabRoster = jsonRetriever.LoadAllCharacters(RetrieverConstants.JsonVillian);
        // Filter enemies based on player's level
        Character villian = new Character();

        foreach (Character character in enemyRoster)
        {
            if (character.id == id)
            {
                villian = character;
            }
        }

        return villian;
    }
}
