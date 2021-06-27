using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    public GameObject itemPrefab;
    public float Radius = 1;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine (SpawnObjectAtRandom());
    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator SpawnObjectAtRandom() {
        while(true)
        {
            int wait_time = Random.Range (0, 50);
            yield return new WaitForSeconds (wait_time);
            Vector3 randomPos = Random.insideUnitCircle * Radius;
            Instantiate(itemPrefab, randomPos, Quaternion.identity);
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(this.transform.position, Radius);
    } 
}
