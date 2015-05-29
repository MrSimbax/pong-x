using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public float speed = 10.0f;
    public string axis = "Vertical";

    [HideInInspector] public int score;

    private new Rigidbody2D rigidbody;
    private Vector2 initialPosition;
    private Vector2 previousVelocity;
    private bool paused;

    public void Reset()
    {
        transform.position = initialPosition;
        rigidbody.velocity = new Vector2(0.0f, 0.0f);
        score = 0;
        paused = false;
    }

    public void Pause()
    {
        if (!paused)
        {
            previousVelocity = rigidbody.velocity;
            paused = true;
        }
        else
        {
            Debug.LogWarning("Player is already paused!");
        }
    }

    public void Resume()
    {
        if (paused)
        {
            rigidbody.velocity = previousVelocity;
            paused = false;
        }
        else
        {
            Debug.LogWarning("Player is not paused!");
        }
    }

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        score = 0;
        initialPosition = transform.position;
    }

    void FixedUpdate()
    {
        if (!paused)
        {
            rigidbody.velocity = new Vector2(0, Input.GetAxis(axis) * speed);
        }
    }
}
