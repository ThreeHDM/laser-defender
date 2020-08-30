using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Wave Config")]

//Cambiamos de MonoBehaviour a ScriptableObject. A SO is a class that let us store data in stand alone assets. Keeps data out of our scripts. It's lightweight. They don't have to be attached to GameObjects
public class WaveConfig : ScriptableObject
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject pathPrefab;
    [SerializeField] float timeBetweenSpawns = 0.5f;
    [SerializeField] float spawnRandomFactor = 0.3f; // creamos este field para agregar variación al spawn
    [SerializeField] int numberOfEnemies = 5;
    [SerializeField] float moveSpeed = 2f;

    //Creamos los getters
    public GameObject GetEnemyPrefab() { return enemyPrefab;}
    public GameObject GetPathPrefab() { return pathPrefab; }
    public float GetTimeBetweenSpawns() { return timeBetweenSpawns; }
    public float GetSpawnRandomFactor() { return spawnRandomFactor; }
    public int GetNumberOfEnemies() { return numberOfEnemies; }
    public float GetMoveSpeed() { return moveSpeed; }
}
