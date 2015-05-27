using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Rigidbody2D))]
public class BallController : MonoBehaviour
{
    public float speed = 50.0f;
    public float playerSpeedMargin = 0.5f;

    private Rigidbody2D rigidbody;

	void Start ()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        InitVelocity();
	}

    void InitVelocity()
    {
        float x = Random.value;
        float y = Random.value;
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
        // Let players control speed of the ball
        float x = rigidbody.velocity.x;
        float playerY = collision.rigidbody.velocity.y /  playerSpeed;
        if (playerY > playerSpeedMargin)
        {
            x = playerY * speed;
        }
        return x;
    }
}
