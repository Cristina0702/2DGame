using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour
{
    public GameObject[] enemyPrefab;

    //EnemySprites enemyPrefabs;
    GameObject[] spawnPoints;
    public int populationSize;
    List<GameObject> population = new List<GameObject>();

    //public GameObject loading;

    private GameObject player;
    
    public static int level;
    public bool teleport, gameDone, destroyed, loadedEnemies, newLevelcheck;
    public float elapsed, timer, newElapsed, newTimer, loadTime;
    //float trialTime = 20;

    public static EnemyManager singleton;
    
    void Awake()
    {
        singleton = this;
        
        //enemyPrefabs = enemies.GetComponent<EnemySprites>();

        player = GameObject.FindGameObjectWithTag("Player");

        /*
        GameObject[] em = GameObject.FindGameObjectsWithTag("EnemyManager");

        if(em.Length > 1)
        {
            Destroy(em[1]);
        }

        DontDestroyOnLoad(this.gameObject);
        singleton = this;
        */
    }
    
    // Start is called before the first frame update
    void Start()
    {
        level = 1;
        teleport = gameDone = destroyed = loadedEnemies = newLevelcheck = false;
        timer = 3f;
        loadTime = 5f;
        elapsed = timer;

        populate();
    }

    void populate()
    {
        spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");

        populationSize = spawnPoints.Length;
        Debug.Log(populationSize);

        for (int i = 0; i < populationSize; ++i)
        {
            Vector3 pos = new Vector3(spawnPoints[i].transform.position.x, spawnPoints[i].transform.position.y, 0);
            GameObject b = Instantiate(enemyPrefab[Random.Range(0, enemyPrefab.Length)], pos, Quaternion.identity);
            b.GetComponent<EnemyAI>().Init();
            //b.GetComponent<EnemyAI>().findSpots(findSpots(spawnPoints[i]));

            population.Add(b);
        }
    }

    GameObject Breed(GameObject B1, GameObject B2, int spot)
    {
        Vector3 pos = new Vector3(spawnPoints[spot].transform.position.x, spawnPoints[spot].transform.position.y, 0);
        GameObject offspring = Instantiate(enemyPrefab[Random.Range(0, enemyPrefab.Length)], pos, Quaternion.identity) ;
        EnemyAI e = offspring.GetComponent<EnemyAI>();

        if(Random.Range(0,1000) > 10)
        {
            e.Init();
            //e.findSpots(findSpots(spawnPoints[spot]));
            e.dna.Mutate(100, 8, 5, 5, 5);
        }
        else
        {
            e.Init();
            //e.findSpots(findSpots(spawnPoints[spot]));
            e.dna.Combine(B1.GetComponent<EnemyAI>().dna, B2.GetComponent<EnemyAI>().dna);
        }
        return offspring;
    }

    public void newLevel()
    {
        //GameObject AX = GameObject.FindGameObjectWithTag("AX");
        //GameObject BX = GameObject.FindGameObjectWithTag("BX");
        //GameObject CX = GameObject.FindGameObjectWithTag("CX");

       
       
        //if (AX != null) AX.SetActive(false);
        //if (BX != null) BX.SetActive(false);
        //if (CX != null) CX.SetActive(false);
        

        for (int i = 0; i < population.Count; i++)
        {
            population[i].GetComponent<EnemyAI>().RemoveAllBalls();
            //population[i].SetActive(false);
        }

        teleport = false;
        populationSize = spawnPoints.Length;
        //populate
        List<GameObject> sortedlist = population.OrderByDescending(o => (o.GetComponent<EnemyAI>().hit_player)).ToList();
        int start = (int)(sortedlist.Count/2);
        Debug.Log(start);

        population.Clear();
        int spot = 0;
        int previous = start;
        while(spot < populationSize)
        {
            population.Add(Breed(sortedlist[previous], sortedlist[previous + 1],spot++));
            ++previous;
            Debug.Log(previous + " " + start);
            if (previous == sortedlist.Count-1) previous = start;
            Debug.Log(previous);
        }

        for(int i=0; i < sortedlist.Count; i++) {
            Destroy(sortedlist[i]);
        }
    }

    //public void LunchTime(){
    //    for(int i=0; i<population.Count; i++){
    //        population[i].GetComponent<EnemyAI>().RemoveAllBalls();
    //        population[i].SetActive(false);
    //    }

    //    spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
    //    int index = 0;
    //    for(int i = 0; i < spawnPoints.Length; i++){
    //        index = (index + 1) % food.Length;
    //        Vector3 pos = new Vector3(spawnPoints[i].transform.position.x, spawnPoints[i].transform.position.y, 0);
    //        Instantiate(food[index], pos, Quaternion.identity);
    //    }
    //}

    //public void ContinueGame(){
    //    GameObject[] fs = GameObject.FindGameObjectsWithTag("Food");
    //    foreach (GameObject f in fs)
    //    {
    //        Destroy(f);
    //    }

    //    //newLevel();
    //}
}
