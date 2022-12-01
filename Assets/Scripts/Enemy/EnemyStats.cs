using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats
{
    public int health;
    public float speed, vision, bulletSpeed, fireRate;

    public EnemyStats(float s, float v, float bS, float fR, int h)
    {
        setRandom(s,v,bS,fR,h);
    }

    public void setRandom(float s, float v, float bS, float fR, int h)
    {
        speed = Random.Range(30.0f, s);
        vision = Random.Range(3.0f, v);
        bulletSpeed = Random.Range(5.0f, bS);
        fireRate = (int) Random.Range(1, fR);
        health = Random.Range(1, h);
    }

    public void Combine(EnemyStats e1, EnemyStats e2)
    {
        int rand;
        rand = Random.Range(1, 6);
        switch (rand)
        {
            case 1: speed = Random.Range(0, 10) < 5 ? e1.speed : e2.speed; break;

            case 2: vision = Random.Range(0, 10) < 5 ? e1.vision : e2.vision; break;

            case 3: bulletSpeed = Random.Range(0, 10) < 5 ? e1.bulletSpeed : e2.bulletSpeed; break;

            case 4: fireRate = Random.Range(0, 10) < 5 ? e1.fireRate : e2.fireRate; break;

            case 5: health = Random.Range(0, 10) < 5 ? e1.health : e2.health;break;
        }
    }

    public void Mutate(float s, float v, float bS, float fR, int h)
    {
        int rand;
        rand = Random.Range(1, 6);
        switch (rand)
        {
            case 1: speed = Random.Range(1f,s); break;

            case 2: vision = Random.Range(1f, v); break;

            case 3: bulletSpeed = Random.Range(1f,bS); break;

            case 4: fireRate = Random.Range(1f,fR); break;

            case 5: health = Random.Range(1, h); break;
        }
    }
}
