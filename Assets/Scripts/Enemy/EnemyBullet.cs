using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private Vector2 moveDir;
    public bool hitplayer;
    public bool collided;
    GameObject hs;

    private void Awake()
    {
        hitplayer = false;
        collided = false;
    }

    public void Shoot(Transform target, float bulletSpeed)
    {
        rb2d = this.GetComponent<Rigidbody2D>();

        moveDir = (target.position - transform.position).normalized * bulletSpeed/2;

        Debug.DrawLine(transform.position, target.position);

        rb2d.velocity = new Vector2(moveDir.x, moveDir.y);
    }


    
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            hitplayer = true;
        }

        collided = true;
    }
    
}

