using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject[] enemies;
    private int enemyID;
    private int spawnChance;
    public int spawnPercentage = 90;
    // Start is called before the first frame update
    void Start()
    {
        spawnChance = Random.Range(1, 100);
        if (spawnChance <= spawnPercentage)
        {
            enemyID = Random.Range(0, enemies.Length);
            Instantiate(enemies[enemyID], transform.position, Quaternion.identity, this.transform.parent);
        }
        Destroy(this);
    }
}
