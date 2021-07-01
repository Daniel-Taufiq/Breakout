using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    public GameObject[] itemPrefab;
    public float Radius = 1;
    private bool calledOnce = false;
    private int x = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(calledOnce == false && GameManager.Instance.state.Equals(GameManager.State.PLAY))
        {
            calledOnce = true;
            StartCoroutine (SpawnObjectAtRandom());
        }
    }

    IEnumerator SpawnObjectAtRandom() { 
        while(true)
        {
            int wait_time = Random.Range (0, 10);
            yield return new WaitForSeconds (wait_time);
            Vector3 randomPos = Random.insideUnitCircle * Radius;
            //Instantiate(itemPrefab[Random.Range(0, itemPrefab.Length)], randomPos, Quaternion.identity);
            Instantiate(itemPrefab[3], randomPos, Quaternion.identity);
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(this.transform.position, Radius);
    }

    void OnCollisionEnter(Collision collision)
    {       
        // delete object
        //Destroy(itemPrefab[0]);
        if(gameObject.tag == "AdditionalBall")
        {
            Destroy(itemPrefab[0]);
        }
        else if(gameObject.tag == "FasterBall")
        {
            Destroy(itemPrefab[0]);
        }
        else if(gameObject.tag == "AdditionalLife")
        {
            Destroy(itemPrefab[0]);
        }
        else if(gameObject.tag == "LargerPlayer")
        {
            Destroy(itemPrefab[0]);
        }
        // add powerup
        if(gameObject.tag == "AdditionalBall")
        {
            GameManager.Instance.AddBall();
        }
        else if(itemPrefab[0].tag == "FasterBall")
        {
            GameManager.Instance.IncreaseBallSpeed();
            //Ball.IncreaseSpeedPowerUp();
        }
        else if(gameObject.tag == "AdditionalLife")
        {
            GameManager.Instance.Balls++;
        }
        else if(gameObject.tag == "LargerPlayer")
        {
            GameManager.Instance.IncreasePlayerScale();
        }
    }
}
