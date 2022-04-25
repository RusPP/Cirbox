using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This code will ensure that a TrailRenderer and BoxCollider
// are on the GameObject the script is attached to
[RequireComponent(typeof(TrailRenderer), typeof(BoxCollider))]
public class ClickAndSwipe : MonoBehaviour
{
    private Camera _camera;
    private Vector3 mousePos;
    private TrailRenderer trail;
    private BoxCollider _collider;
    private bool swiping;
    private float cameraOffsetZ = 10f;
    private GameManager gameManager;
    private void Awake()
    {
        _camera = Camera.main;
        trail = GetComponent<TrailRenderer>();
        _collider = GetComponent<BoxCollider>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        trail.enabled = false;
        _collider.enabled = false;
    }

    private void Update()
    {
        CheckSwiping();
        // If swiping is true, we will update the mouse position
        if (swiping)
        {
            UpdateMousePosition();
        }
        
    }

    private void UpdateMousePosition()
    {
        mousePos = _camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, 
            Input.mousePosition.y, cameraOffsetZ));
        transform.position = mousePos;
    }

    private void UpdateComponents()
    {    
            trail.enabled = swiping;
            _collider.enabled = swiping;    
    }

    // Set swiping to true when the left mouse button is held down, else set it to false.
    private void CheckSwiping()
    {
        if (!gameManager.isPaused)
        {
            if (Input.GetMouseButtonDown(0))
            {
                swiping = true;
                UpdateComponents();
            }
            else if (Input.GetMouseButtonUp(0))
            {
                swiping = false;
                UpdateComponents();
            }
        }
        else
        {
            swiping = false;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<BallCounter>() && 
            !collision.gameObject.GetComponent<BallCounter>().inBox && gameManager.gameIsOn && !gameManager.isPaused)
        {
            collision.gameObject.GetComponent<BallCounter>().DestroyTarget();
        }
    }
}
