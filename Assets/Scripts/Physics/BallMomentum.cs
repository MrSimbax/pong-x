using UnityEngine;

public class BallMomentum : BallPhysics {

	void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            float x = rigidbody.velocity.x + collision.rigidbody.velocity.x;
            float y = rigidbody.velocity.y + collision.rigidbody.velocity.y;

            // It won't be fun if the ball is too fast
            x = Mathf.Sign(x) * Mathf.Clamp(Mathf.Abs(x), 0.0f, maxSpeed);
            y = Mathf.Sign(y) * Mathf.Clamp(Mathf.Abs(y), 0.0f, maxSpeed);

            rigidbody.velocity = new Vector2(x, y);
        }
    }
}
