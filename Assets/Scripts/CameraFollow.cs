using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    private Vector3 initial_pos;
    public Transform player;
    public GameObject background;
    private float width;
    private float height;
    public Camera self;

    void Start() {
        initial_pos = transform.position;
        SpriteRenderer sp = background.GetComponent<SpriteRenderer>();
        width = sp.bounds.extents.x  *2;
        height = sp.bounds.extents.y *2;
        
    }

    void FixedUpdate() {

        if (player.position.y <= height/2 - self.orthographicSize &&
            player.position.y >= -height/2 + self.orthographicSize) {
            transform.position = new Vector3(transform.position.x + initial_pos.x, player.transform.position.y + initial_pos.y, transform.position.z);

        }


    }

}
