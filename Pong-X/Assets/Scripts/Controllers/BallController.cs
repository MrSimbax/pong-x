using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Rigidbody2D))]
public class BallController : MonoBehaviour
{
    public float speed;

    private new Rigidbody2D rigidbody;
    private Vector2 initialPosition;
    private Vector2 previousVelocity;

    public delegate void ReachEndAction();
    public static event ReachEndAction OnReachedEnd;

	void Start ()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        initialPosition = transform.position;
	}

    public void Reset()
    {
        transform.position = initialPosition;
        rigidbody.velocity = new Vector2(0.0f, 0.0f);
    }

    public void Pause()
    {
        // Immediately stop
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

            rigidbody.velocity = new Vector2(rigidbody.velocity.x + collision.rigidbody.velocity.x,
                                             rigidbody.velocity.y + collision.rigidbody.velocity.y);
        }

        if (collision.gameObject.tag == "WallEnd" && OnReachedEnd != null)
        {
            OnReachedEnd();
        }
    }

    float CalcHitPosition(Vector2 ballPos, Vector2 playerPos, float playerHeight)
    {
        // 1 - an upper edge of the player
        // 0 - a middle point of the player
        // -1 - a bottom edge of the player
        return (ballPos.y - playerPos.y) / playerHeight;
    }
}
