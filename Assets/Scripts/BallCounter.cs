using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BallCounter : MonoBehaviour
{
    public bool inBox = false;
    [SerializeField] private GameObject correctBox;
    [SerializeField] private ParticleSystem explosionParticle;
    private GameManager gameManager;
    
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Box"))
        {
            inBox = true;
            if (other.name == correctBox.name)
            {
                gameManager.ComboCounter(1);
                gameManager.CountPoints(5);
            }
            else 
            {
                gameManager.ComboCounter(0);
                gameManager.LivesCount(-1);
            }
        }      
    }
    public void DestroyTarget()
    {
        Destroy(gameObject);
        Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
        DestroyParticles();
    }

    private void DestroyParticles()
    {
        if (explosionParticle.time > explosionParticle.main.startLifetime.constantMax)
        {
            DestroyImmediate(explosionParticle);
        }
    }
}
