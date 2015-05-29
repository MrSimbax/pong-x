using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Rigidbody2D))]
public class BallController : MonoBehaviour
{
    public float speed = 50.0f;
    public float playerSpeedMargin = 0.5f;

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
        previousVelocity = rigidbody.velocity;
        rigidbody.velocity = new Vector2(0.0f, 0.0f);
    }

    public void Resume()
    {
        rigidbody.velocity = previousVelocity;
    }

    public void InitVelocity()
    {
        float sign_x = Random.value > 0.5f ? -1.0f : 1.0f;
        float sign_y = Random.value > 0.5f ? -1.0f : 1.0f;
        float x = Random.value * sign_x;
        float y = Random.value * sign_y;
        rigidbody.velocity = new Vector2(x, y) * speed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            float hitPos = CalcHitPosition(transform.position,
                                            collision.transform.position, 
                                            collision.collider.bounds.size.y);
            float xVelocity = CalcXVelocityOnHit(collision, collision.gameObject.GetComponent<PlayerController>().speed);
            rigidbody.velocity = new Vector2(xVelocity, hitPos * speed);
        }

        if (collision.gameObject.tag == "WallEnd" && OnReachedEnd != null)
        {
            OnReachedEnd();
        }
    }

    float CalcHitPosition(Vector2 ballPos, Vector2 playerPos, float playerHeight)
    {
        // Returns a value from -1 to 1
        // 1 - an upper edge of the player
        // 0 - a middle point of the player
        // -1 - a bottom edge of the player
        return (ballPos.y - playerPos.y) / playerHeight;
    }

    float CalcXVelocityOnHit(Collision2D collision, float playerSpeed)
    {
        // Let players control the speed of the ball
        float x = rigidbody.velocity.x;
        float playerY = collision.rigidbody.velocity.y /  playerSpeed;
        if (playerY > playerSpeedMargin)
        {
            x = playerY * speed;
        }
        return x;
    }
}
