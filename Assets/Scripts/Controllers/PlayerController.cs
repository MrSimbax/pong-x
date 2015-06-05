using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Range(0.0f, 1000.0f)] public float speed;
    public string axis;

    [HideInInspector] public int score;

    new Rigidbody2D rigidbody;
    Vector2 initialPosition;
    Vector2 previousVelocity;
    bool paused;

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
            rigidbody.velocity = new Vector2(0.0f, 0.0f);
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
