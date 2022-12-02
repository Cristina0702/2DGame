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
        //getting the initial position and background values
        initial_pos = transform.position;
        SpriteRenderer sr = background.GetComponent<SpriteRenderer>();
        width = sr.bounds.extents.x  *2;
        height = sr.bounds.extents.y *2;
        
    }

    void FixedUpdate() {
        //moving the camera with player
        if (player.position.y <= height/2 - self.orthographicSize && player.position.y >= -height/2 + self.orthographicSize) {
            transform.position = new Vector3(transform.position.x + initial_pos.x, player.transform.position.y + initial_pos.y, transform.position.z);
        }
    }

}
