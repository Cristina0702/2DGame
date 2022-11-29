using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour {

    public int health;
    public GameObject healthText;
    void Start() {
        health = 5;

    }

    public void decrementHealth() {
        health --;

    }

    public void Update() {
        Debug.Log(health);
        // if (health == 0)
            //Load End Screen
    }
}
