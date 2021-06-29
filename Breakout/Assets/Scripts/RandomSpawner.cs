using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    public GameObject itemPrefab;
    public GameObject powerUp;
    public float Radius = 1;
    private bool calledOnce = false;

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
            int wait_time = Random.Range (0, 50);
            yield return new WaitForSeconds (wait_time);
            Vector3 randomPos = Random.insideUnitCircle * Radius;
            powerUp = Instantiate(itemPrefab, randomPos, Quaternion.identity);
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(this.transform.position, Radius);
    }

    void OnCollisionEnter(Collision collision)
    {
        Destroy(itemPrefab);
        Debug.Log("tag is: " + gameObject.tag);
        if(itemPrefab.tag == "AdditionalBall")
        {
            GameManager.Instance.AddBall();
        }
        else if(itemPrefab.tag == "FasterBall")
        {

        }
    }
}
