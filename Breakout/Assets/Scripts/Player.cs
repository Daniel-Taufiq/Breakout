using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody rigidbody;
    public static Player instance;
    Vector3 temp;
    bool increaseSize = false;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        instance = this;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rigidbody.MovePosition(new Vector3(Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, 0, 50)).x, -17, 0));
        if(increaseSize == true && transform.localScale.x < 10f)
        {
            temp = transform.localScale;
            temp.x += Time.deltaTime * 1.5f;
            transform.localScale = temp;
        }

    }

    public void IncreasePlayerSize()
    {
        increaseSize = true;
        // temp = transform.localScale;
        // temp.x = 10f; // original is 5
        // transform.localScale = temp;
    }

    public void DecreasePlayerSize()
    {
        temp = transform.localScale;
        temp.x = 5f;
        transform.localScale = temp;
    }
}
