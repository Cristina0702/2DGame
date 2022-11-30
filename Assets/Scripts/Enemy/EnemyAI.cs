using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    GameObject target;
    Transform targetpos;

    public Animator animator;

    public GameObject GFX;
    public GameObject enemyBullet;
    List<GameObject> activeBullets;

    private float speed ;
    private float vision;
    public int health = 0;
    private float bulletSpeed;
    private float fireRate;
    float nextFire;
    
    public float nextWaypointDistance;

    Path path;
    int currentWaypoint = 0;

    public EnemyStats dna;
    bool patrol = true;
    bool retreat = false;
    GameObject[] PatrolSpotsG;
    int nextPatrol;
    float patrolSpeed = 50f;
    float outOfVision = 6f;
    public int hit_player;

    bool reachedEndOfPath = false;

    Seeker seeker;
    Rigidbody2D rb;


    public void Init()
    {
        dna = new EnemyStats(100, 8, 10, 5, 5);
        speed = dna.speed;
        vision = dna.vision;
        bulletSpeed = dna.bulletSpeed;
        fireRate = dna.fireRate;
        health = dna.health;

        Debug.Log(speed + "," + vision + "," + bulletSpeed + "," + fireRate + "," + health);
    }

    // Start is called before the first frame update
    void Awake()
    {

        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        gameObject.tag = "Enemy";

        nextFire = Time.time;

        target = GameObject.FindGameObjectWithTag("Player");
        targetpos = target.transform;
        
        PatrolSpotsG = GameObject.FindGameObjectsWithTag("PatrolSpot");
        
        patrol = true;
        retreat = false;
        nextPatrol = 0;

        hit_player = 0;

        activeBullets = new List<GameObject>();

        InvokeRepeating("FindPath", 0f, .5f);
    }


    public void forcePatrolSpot(){
        patrol = true;
        retreat = true;
    }


    void FindPath()
    {
        if (patrol)
        {
            int new_patrol;
            if(Vector2.Distance(rb.position,PatrolSpotsG[nextPatrol].transform.position) < 2f)
            {
                retreat = false;
                do{
                    new_patrol = Random.Range(0, PatrolSpotsG.Length);
                } while (new_patrol == nextPatrol);
                nextPatrol = new_patrol;
            }
            UpdatePath(PatrolSpotsG[nextPatrol].transform);
        }
        else
        {
            UpdatePath(targetpos);
        }
    }
    
    
    void UpdatePath(Transform target)
    {
        if(seeker.IsDone())
            seeker.StartPath(rb.position, target.position, OnPathComplete);
    }
    
    
    void OnPathComplete(Path p)
    {

        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }
    
    void Update()
    {
        
        if (patrol)
        {
            if(!retreat){
                if (Vector2.Distance(rb.position, targetpos.position) < vision)
                {
                    patrol = false;
                }
            }
        } else
        {
            if(Vector2.Distance(rb.position, targetpos.position) > outOfVision)
            {
                patrol = true;
            }
        }
    }

    void CheckFire()
    {
        if(Time.time > nextFire)
        {
            GameObject b = Instantiate(enemyBullet, transform.position, Quaternion.identity);
            Debug.Log("Fire");
            b.GetComponent<EnemyBullet>().Shoot(targetpos, bulletSpeed);
            activeBullets.Add(b);
            nextFire = Time.time + fireRate*3f;
        }
    }




    // Update is called once per frame
    void FixedUpdate()
    {
        for (int i = 0; i < activeBullets.Count; i++)
        {
            if (activeBullets[i].GetComponent<EnemyBullet>().collided)
            {
                if (activeBullets[i].GetComponent<EnemyBullet>().hitplayer)
                {
                    ++hit_player;
                    //Debug.Log("No of hits: " + hit_player);
                }
                GameObject b = activeBullets[i];
                activeBullets.Remove(activeBullets[i]);
                Destroy(b);
            }
        }

        //if (healed) return;
        
        
        if (path == null)
            return;

        if(currentWaypoint >= path.vectorPath.Count)
        {
            return;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        
        Vector2 force;

        if (!patrol) {
            force = direction* (speed*50f) *Time.deltaTime;
            nextWaypointDistance = 0.5f;
            CheckFire();
        }
        else
        {
            force = direction * (patrolSpeed*50f) * Time.deltaTime;
            nextWaypointDistance = 0.5f;
        }

        animator.SetFloat("Horizontal", force.x);
        animator.SetFloat("Vertical", force.y);
        animator.SetFloat("Speed", force.sqrMagnitude);

        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        
        if(distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            --health;
        }

        if(collision.gameObject.tag == "Player")
        {
            ++hit_player;
            //Debug.Log("No of hits: "+ hit_player);
        }
    }

    public void RemoveAllBalls()
    {
        for (int i = 0; i < activeBullets.Count; i++)
        {  
            Destroy(activeBullets[i]);
        }
        activeBullets.Clear();
    }
}
