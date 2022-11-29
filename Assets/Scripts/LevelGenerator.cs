using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{

    public GameObject background;
    public GameObject brainWall;
    public GameObject thoughtBubble;
    public GameObject enemyManager;
    public GameObject patrolSpot;

    //public GameObject speed;
    //public GameObject heart;

    public float offset;
    public float density;

    //private Vector3 speedpos1;
    //private Vector3 heartpos1;



    //private Vector3 patrol_pos1;
    //private Vector3 patrol_pos2;
    //private Vector3 patrol_pos3;


    private Vector3 enemy_pos1;
    private Vector3 enemy_pos2;
    private Vector3 enemy_pos3;
    private Vector3 enemy_pos4;


    private List<float> used_pos_x;
    private List<float> used_pos_y;
    //private List<Vector3> letterPos;


    public float width;
    public float height;

    void Start()
    {

        used_pos_x = new List<float>();
        used_pos_y = new List<float>();

        SpriteRenderer sp = background.GetComponent<SpriteRenderer>();
        width = sp.bounds.extents.x * 2;
        height = sp.bounds.extents.y * 2;


        GenerateLevel();
        //AstarPath.active.Scan();
        //Instantiate(enemyManager, new Vector3(0, 0, 1), Quaternion.identity);
    }


    void Update()
    {

        // if (Input.GetKeyDown("space")) {
        //     GameObject[] letters = GameObject.FindGameObjectsWithTag("letter");

        //     foreach (GameObject letter in letters) {
        //         Destroy(letter);
        //     }

        //     GenerateLetters();
        // }

        // if (Input.GetKeyDown("enter")) {
        //     GameObject[] bullies = GameObject.FindGameObjectsWithTag("thoughtBubble");

        //     foreach (GameObject thoughtBubble in bullies) {
        //         Destroy(thoughtBubble);
        //     }

        //     GenerateBullies();
        // }

    }


    void GenerateLevel()
    {

        //For every possible square

        for (float x = -width / 2f + 1f; x < width / 2f - 1f; x += offset)
        {
            for (float y = -height / 2f + 1f; y < height / 2f - 1f; y += offset)
            {
                Vector3 pos = new Vector3(x, y, 1f);

                if (Random.Range(0f, 1f) > density)
                { //Spawn table
                    Instantiate(brainWall, pos, Quaternion.identity, transform);
                    used_pos_x.Add(x);
                    used_pos_y.Add(y);
                }
            }

        }

    }

    bool new_pos(Vector3 pos)
    {

        for (int i = 0; i < used_pos_x.Count; i++)
        {
            if (Vector3.Distance(new Vector3(used_pos_x[i], used_pos_y[i], 1f), pos) <= offset)
                return false;
        }

        return true;
    }

    public void GenerateEnemies()
    {


        do
        {
            enemy_pos1 = new Vector3(Random.Range(-width / 2f + 1f, width / 2f - 1f),
                                    Random.Range(-height / 2f + 1f, height / 2f - 1f),
                                    1f);
        } while (!new_pos(enemy_pos1));


        do
        {
            enemy_pos2 = new Vector3(Random.Range(-width / 2f + 1f, width / 2f - 1f),
                                    Random.Range(-height / 2f + 1f, height / 2f - 1f),
                                    1f);
        } while ((enemy_pos2.x < enemy_pos1.x + offset && enemy_pos2.x > enemy_pos1.x - offset &&
                enemy_pos2.y < enemy_pos1.y + offset && enemy_pos2.y > enemy_pos1.y - offset) ||
                !new_pos(enemy_pos2));


        do
        {
            enemy_pos3 = new Vector3(Random.Range(-width / 2f + 1f, width / 2f - 1f),
                                    Random.Range(-height / 2f + 1f, height / 2f - 1f),
                                    1f);
        } while ((enemy_pos3.x < enemy_pos1.x + offset && enemy_pos3.x > enemy_pos1.x - offset &&
                 enemy_pos3.y < enemy_pos1.y + offset && enemy_pos3.y > enemy_pos1.y - offset) ||
                 (enemy_pos3.x < enemy_pos2.x + offset && enemy_pos3.x > enemy_pos2.x - offset &&
                 enemy_pos3.y < enemy_pos2.y + offset && enemy_pos3.y > enemy_pos2.y - offset) ||
                 !new_pos(enemy_pos3));


        do
        {
            enemy_pos4 = new Vector3(Random.Range(-width / 2f + 1f, width / 2f - 1f),
                                    Random.Range(-height / 2f + 1f, height / 2f - 1f),
                                    1f);
        } while ((enemy_pos4.x < enemy_pos1.x + offset && enemy_pos4.x > enemy_pos1.x - offset &&
                 enemy_pos4.y < enemy_pos1.y + offset && enemy_pos4.y > enemy_pos1.y - offset) ||
                 (enemy_pos4.x < enemy_pos2.x + offset && enemy_pos4.x > enemy_pos2.x - offset &&
                 enemy_pos4.y < enemy_pos2.y + offset && enemy_pos4.y > enemy_pos2.y - offset) ||
                 !new_pos(enemy_pos3));


        Instantiate(thoughtBubble, enemy_pos1, Quaternion.identity, transform);
        Instantiate(thoughtBubble, enemy_pos2, Quaternion.identity, transform);
        Instantiate(thoughtBubble, enemy_pos3, Quaternion.identity, transform);
        Instantiate(thoughtBubble, enemy_pos4, Quaternion.identity, transform);

    }

    //public void GeneratePatrolSpots()
    //{

    //    do
    //    {
    //        patrol_pos1 = new Vector3(Random.Range(-width / 2f + 1f, width / 2f - 1f),
    //                                        Random.Range(-height / 2f + 1f, height / 2f - 1f),
    //                                        1f);
    //    } while (!new_pos(patrol_pos1));

    //    do
    //    {
    //        patrol_pos2 = new Vector3(Random.Range(-width / 2f + 1f, width / 2f - 1f),
    //                                Random.Range(-height / 2f + 1f, height / 2f - 1f),
    //                                1f);
    //    } while (patrol_pos2.x < patrol_pos1.x + offset && patrol_pos2.x > patrol_pos1.x - offset &&
    //            patrol_pos2.y < patrol_pos1.y + offset && patrol_pos2.y > patrol_pos1.y - offset ||
    //            !new_pos(patrol_pos2));


    //    do
    //    {
    //        patrol_pos3 = new Vector3(Random.Range(-width / 2f + 1f, width / 2f - 1f),
    //                                Random.Range(-height / 2f + 1f, height / 2f - 1f),
    //                                1f);
    //    } while ((patrol_pos3.x < patrol_pos1.x + offset && patrol_pos3.x > patrol_pos1.x - offset &&
    //             patrol_pos3.y < patrol_pos1.y + offset && patrol_pos3.y > patrol_pos1.y - offset) ||
    //             (patrol_pos3.x < patrol_pos2.x + offset && patrol_pos3.x > patrol_pos2.x - offset &&
    //             patrol_pos3.y < patrol_pos2.y + offset && patrol_pos3.y > patrol_pos2.y - offset) ||
    //             !new_pos(patrol_pos3));



    //    Instantiate(patrolSpot, patrol_pos1, Quaternion.identity, transform);
    //    Instantiate(patrolSpot, patrol_pos2, Quaternion.identity, transform);
    //    Instantiate(patrolSpot, patrol_pos3, Quaternion.identity, transform);
    //}
    

}
