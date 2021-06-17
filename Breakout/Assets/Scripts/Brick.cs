using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public int hits = 1;
    public int points = 100; 
    public Vector3 rotator;
    public Material hitMaterial;
    Material originalMaterial;
    Renderer renderer;
    // Start is called before the first frame update
    void Start()
    {
        transform.Rotate(rotator * (transform.position.x + transform.position.y) * 0.1f);
        renderer = GetComponent<Renderer>();
        originalMaterial = renderer.sharedMaterial;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotator * Time.deltaTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        hits--;
        if (hits <= 0)
        {
            Destroy(gameObject);
        }
        renderer.sharedMaterial = hitMaterial;
        Invoke("RestoreMaterial", 0.05f);
    }

    void RestoreMaterial()
    {
        renderer.sharedMaterial = originalMaterial;
    }
}
