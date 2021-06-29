using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    float speed = 30f;
    Rigidbody rigidbody;
    Vector3 velocity;
    Renderer renderer;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();  // have access to RigidBody component on Ball object
        renderer = GetComponent<Renderer>();
        Invoke("Launch", 0.5f);
    }

    void Launch()
    {
        rigidbody.velocity = Vector3.up * speed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // shrink the current velocity to one unit and multiple by speed
        rigidbody.velocity = rigidbody.velocity.normalized * speed;
        velocity = rigidbody.velocity;

        if(!renderer.isVisible)
        {
            GameManager.Instance.Balls--;
            GameManager.Instance.DecBallCount();
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // ball will reflect off the normal plane from the Paddle object
        rigidbody.velocity = Vector3.Reflect(velocity, collision.contacts[0].normal);
    }
}
