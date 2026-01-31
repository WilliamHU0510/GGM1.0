using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManagement : MonoBehaviour
{
    public List<EnemySpawnerNew> spawners;
    public List<SpawnConfig> spawnConfigs;
    int currentSpawnerIndex = 0;
    float currentTime = 0f;
    public int gameLevel = 1;
    public static SpawnerManagement instance;
    void Awake()
    {
        instance = this;
        //SpawnerManagement.instance.gameLevel
    }
    [System.Serializable]
    public class SpawnConfig{
        public float intervalTime = 1f;
        public float randomRange = 1f;
        public bool isRandomSpawner = false;
        public int spawnerCount = 1;
        public List<int> spawnerIndexes = new List<int>();
        public bool isRandomEnemy = false;
        public List<int> enemyIndexes = new List<int>();
    }
    // Start is called before the first frame update
    void Start()
    {
        foreach(SpawnConfig spawnConfig in spawnConfigs){
            spawnConfig.intervalTime = spawnConfig.intervalTime + Random.Range(-spawnConfig.randomRange, spawnConfig.randomRange);
        }
    }

    // Update is called once per frame
    void Update()
    {
        TimerUpdate();
    }
    void TimerUpdate()
    {
        currentTime += Time.deltaTime;
        if(currentTime >= spawnConfigs[currentSpawnerIndex].intervalTime){
            currentTime = 0f;
            SpawnEnemy();
            currentSpawnerIndex++;
            if(currentSpawnerIndex >= spawnConfigs.Count){
                gameLevel++;
                currentSpawnerIndex = 0;
            }
        }
    }
    void SpawnEnemy()
    {
        SpawnConfig spawnConfig = spawnConfigs[currentSpawnerIndex];
        if(spawnConfig.isRandomSpawner){
            List<int> spawnerIndexes = new List<int>();
            while(spawnerIndexes.Count < spawnConfig.spawnerCount){
                int spawnerIndex = Random.Range(0, spawners.Count);
                if(!spawnerIndexes.Contains(spawnerIndex)){
                    spawnerIndexes.Add(spawnerIndex);
                }
            }
            for(int i = 0; i < spawnerIndexes.Count; i++){
                if(spawnConfig.isRandomEnemy){
                    spawners[spawnerIndexes[i]].SpawnRandomEnemy();
                }
                else{
                    spawners[spawnerIndexes[i]].SpawnEnemy(spawnConfig.enemyIndexes[i]);
                }
            }
        }
        else{
            for(int i = 0; i < spawnConfig.spawnerIndexes.Count; i++){
                if(spawnConfig.isRandomEnemy){
                    spawners[spawnConfig.spawnerIndexes[i]].SpawnRandomEnemy();
                }
                else{
                    spawners[spawnConfig.spawnerIndexes[i]].SpawnEnemy(spawnConfig.enemyIndexes[i]);
                }
            }
        }
    }
}
