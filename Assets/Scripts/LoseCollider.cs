using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseCollider : MonoBehaviour {
    private void OnTriggerStay2D(Collider2D trigger) // Ball is falling down
    {
        print("Trigger");
        LevelManager.instance.BallDestroyed();
    }
    private void OnCollisionEnter2D(Collision2D collision) // Ball is contacting with borders
    {
        print("Collision");
    }
}
