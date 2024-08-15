using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class waveController : MonoBehaviour
{
    [System.Serializable]
    public class SpawnAreas //class for each area 
    {
        public string areaName; //holds name of current area player is in - used for deciding where to spawn wave. set in ontriggerstay method
        public Transform[] spawnPoints; //arry of spawn points inside of areas - assigned in inspector
    }


    public SpawnAreas[] spawnAreas; 
    public GameObject enemy1; //enemy game object
    public GameObject enemy2;
    
    private int type2SpawnRate = 2;
    private int enemiesToSpawn = 5; //initial number of enemies to spawn
    private int enemiesRemainingToSpawn;
    private int enemiesRemainingAlive;
    private float timeBetweenWaves = 5.0f; //time between waves

    public TextMeshProUGUI displayWave; //wave number text
    public int waveNumber = 0;
    public pickupController pickup;
    private string currentPlayerArea = "";

    void Start()
    {

       //StartNextWave(); //called in pickup controller script when pistol is picked up
       
    }

    public void StartNextWave() //after current wave ends calls next wave with more enemies
    {
        waveNumber++; //increment wave number
        enemiesRemainingToSpawn = enemiesToSpawn;
        enemiesRemainingAlive = enemiesToSpawn;
        StartCoroutine(SpawnWave());
        enemiesToSpawn += 3; //increase the enemies for the next wave by 3 each time
        displayWave.text = "Wave: " + waveNumber.ToString(); //update wave number text when next wave starts
        
    }



    IEnumerator SpawnWave() //spawn wave slowly
    {
        int spawnCounter = 0; //enemy spawned

        while (enemiesRemainingToSpawn > 0) //spawn enemies until done
        {
            enemiesRemainingToSpawn--; //decrement amount of enemies to spawn
            spawnCounter++; //increment amount spawned

            Transform[] spawnPointsInArea = GetSpawnPointsForArea(currentPlayerArea); //get array of spawn points based on player location
            if (spawnPointsInArea != null && spawnPointsInArea.Length > 0) //if spawn found
            {
                Transform spawnPoint = spawnPointsInArea[Random.Range(0, spawnPointsInArea.Length)]; //select random location in area
                GameObject enemyToSpawn = spawnCounter % type2SpawnRate == 0 ? enemy1 : enemy2; //choose both type on enemies
                GameObject spawnedEnemy = Instantiate(enemyToSpawn, spawnPoint.position, spawnPoint.rotation); //instantiate enemy
             
                if (enemyToSpawn == enemy1) 
                {
                spawnedEnemy.GetComponent<EnemyAI>().OnDeath += OnEnemyDeath; //get enemy from engine
                } 
                else if (enemyToSpawn == enemy2) 
                {
                    spawnedEnemy.GetComponent<EnemyAI2>().OnDeath += OnEnemyDeath; //get enemy from engine
                } 
             }

            yield return new WaitForSeconds(1.0f); //wait 1 second between every spawned enemy
        }
    }



    Transform[] GetSpawnPointsForArea(string areaName) //spawn points in array. returns array of spawnpoints in areas on the map
    {
        foreach (var area in spawnAreas) //loops through each area in spawnAreas
        {
            if (area.areaName == areaName) //checks if current area matches area name
            {
                return area.spawnPoints; //if true return correct spawn points
            }
        }
        return null; //no spawn area found
    }


    void OnEnemyDeath()
    {
        enemiesRemainingAlive--; //decrease enemies alive when they die

        if (enemiesRemainingAlive == 0)
        {
            StartCoroutine(StartNextWaveAfterDelay(timeBetweenWaves)); //next wave after set time
        }
    }

    IEnumerator StartNextWaveAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        StartNextWave();
    }

    void OnTriggerStay(Collider other) //runs while player is inside a box colliders
    {

        //all areas of the map
        //each area has 1-5 spawn points
        //when staying in area "currentPlayerArea" variable is changed to the area the player is in

    if (other.CompareTag("Area1"))
    {
        currentPlayerArea = "Area1";
    }

       if (other.CompareTag("Area2"))
    {
        currentPlayerArea = "Area2";
    }

           if (other.CompareTag("Area3"))
    {
        currentPlayerArea = "Area3";
    }

           if (other.CompareTag("Area4"))
    {
        currentPlayerArea = "Area4";
    }

           if (other.CompareTag("Area5"))
    {
        currentPlayerArea = "Area5";
    }

           if (other.CompareTag("Area6"))
    {
        currentPlayerArea = "Area6";
    }

    }

}
