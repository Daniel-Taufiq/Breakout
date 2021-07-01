using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ball : MonoBehaviour
{
    //float speed = 30f;
    Rigidbody rigidbody;
    Vector3 velocity;
    Renderer renderer;
 
    static public Ball instance;
    private static float speed = 30f;
  
    
    
    public static float Speed
    {
        get { return speed; }
        set { speed = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();  // have access to RigidBody component on Ball object
        renderer = GetComponent<Renderer>();
        instance = this;
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

    static public void IncreaseBallSpeed()
    {
        Ball.Speed = 45f;
    }

    static public void DecreaseBallSpeed()
    {
        Ball.Speed = 30f;
    }

    IEnumerator StartTimer()
    {
        int wait_time = 5;
        yield return new WaitForSeconds (wait_time);
        Ball.Speed = 30f;
    }


    static public void IncreaseSpeedPowerUp()
    {
        Ball.Speed = 45f;
        instance.StartCoroutine("StartTimer");
    }

    

}
