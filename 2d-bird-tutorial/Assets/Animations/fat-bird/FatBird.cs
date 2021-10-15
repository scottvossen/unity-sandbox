using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FatBird : MonoBehaviour
{
    private Vector3 _initialPosition;
    private bool _hasLaunched = false;
    private float _idleTime;

    [SerializeField]
    private float _outOfBoundsDistance = 10;

    [SerializeField]
    private float _launchPower = 500;

    [SerializeField]
    private float _maxIdleSeconds = 3;

    private void Awake()
    {
        _initialPosition = transform.position;
    }

    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        var lineRender = GetComponent<LineRenderer>();
        lineRender.SetPosition(0, transform.position);
        lineRender.SetPosition(1, _initialPosition);

        var rigidBody = GetComponent<Rigidbody2D>();

        var isMoving = rigidBody.velocity.magnitude <= 0.1;
        var isOutOfBounds = transform.position.y > _outOfBoundsDistance 
            || transform.position.y < -1 * _outOfBoundsDistance
            || transform.position.x > _outOfBoundsDistance 
            || transform.position.x < -1 * _outOfBoundsDistance;

        // track idle time
        if (_hasLaunched && isMoving)
        {
            _idleTime += Time.deltaTime;
        }

        // reset the scene if out of bounds or idled out
        if (isOutOfBounds || _idleTime > _maxIdleSeconds)
        {
            var activeScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(activeScene.name);
        }
    }

    private void OnMouseDown()
    {
        // color the sprite
        var spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = Color.red;

        // turn on line renderer
        var lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = true;
    }

    private void OnMouseUp()
    {
        // remove sprite coloring
        var spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = Color.white;

        // turn off the line renderer
        var lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;

        // shoot the sprite
        var distanceToInitialPosition = _initialPosition - transform.position;

        var rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.AddForce(distanceToInitialPosition * _launchPower);
        rigidBody.gravityScale = 1;

        _hasLaunched = true;
    }

    private void OnMouseDrag()
    {
        var newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(newPosition.x, newPosition.y);
    }
}
