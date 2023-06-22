using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Ball : MonoBehaviour
{
    public static Ball instance;
    public Paddle paddle;

    public bool hasStarted = false;

    //needed for unity 5 coding
    public Rigidbody2D paddleToBallVector;
    private Vector3 ballStartPosition;
    public int ballDamage = 750;
    public bool levelStarted = false;

    private void Awake()
    {
        instance = this;
    }

    // Use this for initialization
    void Start()
    {
        //Account for scripting LvlMng, Paddle, Ball
        //Getting rigid body info
        paddleToBallVector = GetComponent<Rigidbody2D>();
        ballStartPosition = this.transform.position - paddle.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasStarted)
        {
            // Lock the ball relative to the paddle
            this.transform.position = paddle.transform.position + ballStartPosition;
        }

        if (levelStarted && !hasStarted)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                print("Mouse clicked, launch ball");
                hasStarted = true;
                //Since we are now using rigid body, we can call the velocity for Vector2
                this.paddleToBallVector.velocity = new Vector2(Random.Range(-3f, 3f), 8f);
            }
        }
        //Wait for left mouse  click to launch the ball
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 breakEndlessLoops = new Vector2();
        if (collision.gameObject.CompareTag("Breakable"))
            breakEndlessLoops = new Vector2(Random.Range(0f, .3f), Random.Range(0f, .3f));
        else
            breakEndlessLoops = new Vector2(Random.Range(.4f, 1f), Random.Range(.4f, 1f));

        if (hasStarted)
        {
            GetComponent<AudioSource>().Play();

            this.GetComponent<Rigidbody2D>().velocity += breakEndlessLoops;
        }
    }
}