using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public float speed = 10.0f;
    public string axis = "Vertical";

    [HideInInspector] public int score;

    private Rigidbody2D rigidbody;
    private Vector2 initialPosition;
    private Vector2 previousVelocity;
    private bool paused;

    public void Reset()
    {
        transform.position = initialPosition;
        Pause();
    }

    public void Pause()
    {
        previousVelocity = rigidbody.velocity;
        rigidbody.velocity = new Vector2(0.0f, 0.0f);
        paused = true;
    }

    public void Resume()
    {
        rigidbody.velocity = previousVelocity;
        paused = false;
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
