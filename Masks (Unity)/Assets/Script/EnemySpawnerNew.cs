using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerNew : MonoBehaviour
{
    public List<GameObject> enemyPrefabs;
    // Start is called before the first frame update
    public void SpawnEnemy(int index)
    {
        GameObject enemy = Instantiate(enemyPrefabs[index], transform.position, Quaternion.identity);
    }
    public void SpawnRandomEnemy()
    {
        int index = Random.Range(0, enemyPrefabs.Count);
        SpawnEnemy(index);
    }
}
