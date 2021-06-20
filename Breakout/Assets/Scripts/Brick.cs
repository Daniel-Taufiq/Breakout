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
    public AudioSource audioSource;
    public ParticleSystem deathParticles; // implement later
    

    // Start is called before the first frame update
    void Start()
    {
        transform.Rotate(rotator * (transform.position.x + transform.position.y) * 0.1f);
        renderer = GetComponent<Renderer>();
        audioSource = GetComponent<AudioSource>();
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
        //audioSource.Play();
        
        // only flash the brick color if it has a hit marker greater than 1
        if (hits <= 0)
        {
            FindObjectOfType<AudioManager>().Play("Destroy");
            GameManager.Instance.Score += points;
            Destroy(gameObject);
        }
        else
        {
            FindObjectOfType<AudioManager>().Play("Pop_sound");
        }
        renderer.sharedMaterial = hitMaterial;
        Invoke("RestoreMaterial", 0.05f);
    }

    void RestoreMaterial()
    {
        renderer.sharedMaterial = originalMaterial;
    }
}
