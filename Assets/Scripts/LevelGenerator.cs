using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{


    public GameObject background;
    //public GameObject table;
    //public GameObject chair;
    //public GameObject A;
    //public GameObject B;
    //public GameObject C;
    //public GameObject letter;
    public GameObject brain;
    public GameObject bubble;
    public GameObject enemyManager;
    public GameObject patrolSpot;
    //public GameObject teacher;
    //public GameObject speed;
    //public GameObject heart;

    public float offset;
    public float density;


    //private Vector3 letterpos1;
    //private Vector3 letterpos2;
    //private Vector3 letterpos3;

    //private Vector3 speedpos1;
    //private Vector3 heartpos1;



    private Vector3 patrol_pos1;
    private Vector3 patrol_pos2;
    private Vector3 patrol_pos3;


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
        AstarPath.active.Scan();
        Instantiate(enemyManager, new Vector3(0, 0, 1), Quaternion.identity);
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
        //     GameObject[] bullies = GameObject.FindGameObjectsWithTag("bubble");

        //     foreach (GameObject bubble in bullies) {
        //         Destroy(bubble);
        //     }

        //     GenerateBullies();
        // }

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


    //public void GenerateLetters()
    //{

    //    //Destroy previous letters

    //    GameObject[] AList = GameObject.FindGameObjectsWithTag("A");
    //    foreach (GameObject letter in AList)
    //    {
    //        Destroy(letter);
    //    }

    //    GameObject[] BList = GameObject.FindGameObjectsWithTag("B");
    //    foreach (GameObject letter in BList)
    //    {
    //        Destroy(letter);
    //    }

    //    GameObject[] CList = GameObject.FindGameObjectsWithTag("C");
    //    foreach (GameObject letter in CList)
    //    {
    //        Destroy(letter);
    //    }

    //    do
    //    {
    //        letterpos1 = new Vector3(Random.Range(-width / 2f + 1f, width / 2f - 1f),
    //                                        Random.Range(-height / 2f + 1f, height / 2f - 1f),
    //                                        1f);
    //    } while (!new_pos(letterpos1));
    //    //////

    //    do
    //    {
    //        letterpos2 = new Vector3(Random.Range(-width / 2f + 1f, width / 2f - 1f),
    //                                Random.Range(-height / 2f + 1f, height / 2f - 1f),
    //                                1f);
    //    } while (letterpos2.x < letterpos1.x + offset && letterpos2.x > letterpos1.x - offset &&
    //            letterpos2.y < letterpos1.y + offset && letterpos2.y > letterpos1.y - offset ||
    //            !new_pos(letterpos2));


    //    do
    //    {
    //        letterpos3 = new Vector3(Random.Range(-width / 2f + 1f, width / 2f - 1f),
    //                                Random.Range(-height / 2f + 1f, height / 2f - 1f),
    //                                1f);
    //    } while ((letterpos3.x < letterpos1.x + offset && letterpos3.x > letterpos1.x - offset &&
    //             letterpos3.y < letterpos1.y + offset && letterpos3.y > letterpos1.y - offset) ||
    //             (letterpos3.x < letterpos2.x + offset && letterpos3.x > letterpos2.x - offset &&
    //             letterpos3.y < letterpos2.y + offset && letterpos3.y > letterpos2.y - offset) ||
    //             !new_pos(letterpos3));



    //    Instantiate(A, letterpos1, Quaternion.identity, transform);
    //    Instantiate(B, letterpos2, Quaternion.identity, transform);
    //    Instantiate(C, letterpos3, Quaternion.identity, transform);


    //}

    public void GeneratePatrolSpots()
    {

        do
        {
            patrol_pos1 = new Vector3(Random.Range(-width / 2f + 1f, width / 2f - 1f),
                                            Random.Range(-height / 2f + 1f, height / 2f - 1f),
                                            1f);
        } while (!new_pos(patrol_pos1));

        do
        {
            patrol_pos2 = new Vector3(Random.Range(-width / 2f + 1f, width / 2f - 1f),
                                    Random.Range(-height / 2f + 1f, height / 2f - 1f),
                                    1f);
        } while (patrol_pos2.x < patrol_pos1.x + offset && patrol_pos2.x > patrol_pos1.x - offset &&
                patrol_pos2.y < patrol_pos1.y + offset && patrol_pos2.y > patrol_pos1.y - offset ||
                !new_pos(patrol_pos2));


        do
        {
            patrol_pos3 = new Vector3(Random.Range(-width / 2f + 1f, width / 2f - 1f),
                                    Random.Range(-height / 2f + 1f, height / 2f - 1f),
                                    1f);
        } while ((patrol_pos3.x < patrol_pos1.x + offset && patrol_pos3.x > patrol_pos1.x - offset &&
                 patrol_pos3.y < patrol_pos1.y + offset && patrol_pos3.y > patrol_pos1.y - offset) ||
                 (patrol_pos3.x < patrol_pos2.x + offset && patrol_pos3.x > patrol_pos2.x - offset &&
                 patrol_pos3.y < patrol_pos2.y + offset && patrol_pos3.y > patrol_pos2.y - offset) ||
                 !new_pos(patrol_pos3));



        Instantiate(patrolSpot, patrol_pos1, Quaternion.identity, transform);
        Instantiate(patrolSpot, patrol_pos2, Quaternion.identity, transform);
        Instantiate(patrolSpot, patrol_pos3, Quaternion.identity, transform);
    }
    public void GenerateBullies()
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


        Instantiate(bubble, enemy_pos1, Quaternion.identity, transform);
        Instantiate(bubble, enemy_pos2, Quaternion.identity, transform);
        Instantiate(bubble, enemy_pos3, Quaternion.identity, transform);
        Instantiate(bubble, enemy_pos4, Quaternion.identity, transform);

    }


    //public void GenerateHeart()
    //{
    //    GameObject[] HeartList = GameObject.FindGameObjectsWithTag("Heart");
    //    foreach (GameObject heart in HeartList)
    //    {
    //        Destroy(heart);
    //    }

    //    do
    //    {
    //        heartpos1 = new Vector3(Random.Range(-width / 2f + 1f, width / 2f - 1f),
    //                                Random.Range(-height / 2f + 1f, height / 2f - 1f),
    //                                1f);
    //    } while (!new_pos(heartpos1));

    //    used_pos_x.Add(heartpos1.x);
    //    used_pos_y.Add(heartpos1.y);

    //    Instantiate(heart, heartpos1, Quaternion.identity, transform);
    //}

    void GenerateLevel()
    {

        //GenerateLetters();
        //bool teacherExists = false;

        //For every possible square

        for (float x = -width / 2f + 1f; x < width / 2f - 1f; x += offset)
        {
            for (float y = -height / 2f + 1f; y < height / 2f - 1f; y += offset)
            {

                if (x < offset && x > -offset && y < offset && y > -offset)
                    continue;


                Vector3 pos = new Vector3(x, y, 1f);

                if (Random.Range(0f, 1f) > density)
                { 
                    //spawning brain wall if random num is greater than set density
                    Instantiate(brain, pos, Quaternion.identity, transform);
                    used_pos_x.Add(x);
                    used_pos_y.Add(y);
                }
            }

        }

        GenerateBullies();
        GeneratePatrolSpots();
        //GenerateSpeed();
        //GenerateHeart();

    }

}
