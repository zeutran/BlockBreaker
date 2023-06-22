using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    public float speed = 50f; // movement speed
    public float smoothness = 2f; // smoothness of movement
    private float _targetDestination;
    private Rigidbody2D _rigidbody2D;
    private bool _isBorder = false;
    private Rigidbody2D _ballRigidbody2D;
    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        _targetDestination = _rigidbody2D.position.x;
        _ballRigidbody2D = Ball.instance.paddleToBallVector;// target destination is the current position of the object
    }

    // Update is called once per frame
    void Update()
    {
       
        float movementAmount = Input.GetAxis("Horizontal") * speed; // get horizontal input
        _targetDestination += movementAmount * Time.deltaTime; // update target destination
        Vector3 newLocation =
            new Vector3(_targetDestination, _rigidbody2D.position.y); // create new location vector
        _rigidbody2D.position =
            Vector3.Lerp(_rigidbody2D.position, newLocation,
                smoothness * Time.deltaTime); // move object to new location with smoothness
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            _ballRigidbody2D.velocity = new Vector2(_ballRigidbody2D.velocity.x *-1.9f, _ballRigidbody2D.velocity.y);
        }
    }
}