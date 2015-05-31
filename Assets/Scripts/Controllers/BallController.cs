using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class BallController : MonoBehaviour
{
    [Range(0.0f, 1000.0f)] public float speed;
    [Range(0.0f, 1000.0f)] public float maxSpeed;

    private new Rigidbody2D rigidbody;
    private new BoxCollider2D collider;
    private Vector2 initialPosition;
    private Vector2 previousVelocity;

    public delegate void ReachEndAction();
    public static event ReachEndAction OnReachedEnd;

	void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
        initialPosition = transform.position;
        Mathf.Clamp(speed, 0.0f, maxSpeed);
	}

    public void Reset()
    {
        transform.position = initialPosition;
        rigidbody.velocity = new Vector2(0.0f, 0.0f);
        rigidbody.simulated = true;
    }

    public void Pause()
    {
        // Immediately stop
        rigidbody.simulated = false;
        previousVelocity = rigidbody.velocity;
        rigidbody.velocity = new Vector2(0.0f, 0.0f);
    }

    public void Resume()
    {
        rigidbody.velocity = previousVelocity;
    }

    public void InitVelocity()
    {
        float x = Random.value > 0.5f ? -1.0f : 1.0f;
        float y = Random.value * (Random.value > 0.5f ? -1.0f : 1.0f);
        rigidbody.velocity = new Vector2(x, y) * speed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // Assume the ball and the pad have the same mass
            // A momentum conservation law holds
            // 
            // m * ball_velocity + m * pad_velocity = m * ball_velocity_after + m * pad_velocity_after
            // ball_velocity + pad_velocity = ball_velocity_after + pad_velocity_after
            // 
            // The pad is still moving after collision because of external force (the player input),
            // so we can assume that pad_velocity_after = 0
            //
            // ball_velocity + pad_velocity = ball_velocity_after
            //
            // Equation works in each dimension.

            float x = rigidbody.velocity.x + collision.rigidbody.velocity.x;
            float y = rigidbody.velocity.y + collision.rigidbody.velocity.y;

            // It won't be fun if the ball is too fast
            x = Mathf.Sign(x) * Mathf.Clamp(Mathf.Abs(x), 0.0f, maxSpeed);
            y = Mathf.Sign(y) * Mathf.Clamp(Mathf.Abs(y), 0.0f, maxSpeed);

            rigidbody.velocity = new Vector2(x, y);
        }
    }
    
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "WallEnd" && OnReachedEnd != null)
        {
            OnReachedEnd();
        }
    }
}
