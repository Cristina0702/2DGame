using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;

    public Rigidbody2D rb;
    public Animator animator;

    //public AudioSource collectPowerUp;

    Vector2 movement;

    //public GameObject pickUpText;


    void Start()
    {

        //pickUpText.gameObject.SetActive(false);
    }


    // Update is called once per frame
    void Update()
    {

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

    }

    void FixedUpdate()
    {
        //rb.MovePosition(rb.position * movement * moveSpeed * Time.fixedDeltaTime);
        rb.velocity = movement * moveSpeed * Time.fixedDeltaTime;
    }

    //void OnTriggerEnter2D(Collider2D collision) {
    //    if (collision.gameObject.tag.Equals("A") || 
    //        collision.gameObject.tag.Equals("B") ||
    //        collision.gameObject.tag.Equals("C"))
    //    {
    //        pickUpText.gameObject.SetActive(true);
    //    }

    //    else if (collision.gameObject.tag.Equals("Speed")) {
    //        isBoost = true;
    //        boost = 2f;
    //        Destroy(collision.gameObject);
    //        collectPowerUp.Play();
    //    }

    //    else if (collision.gameObject.tag.Equals("Heart")) {

    //        gameObject.GetComponent<HealthScript>().health ++;
    //        Destroy(collision.gameObject);
    //        collectPowerUp.Play();
    //    }


    //}

    //void OnTriggerExit2D(Collider2D collision) {
    //    if (collision.gameObject.tag.Equals("A") || 
    //        collision.gameObject.tag.Equals("B") ||
    //        collision.gameObject.tag.Equals("C"))
    //    {
    //        pickUpText.gameObject.SetActive(false);
    //    }
    //}
}
