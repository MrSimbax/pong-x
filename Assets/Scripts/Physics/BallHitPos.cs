using UnityEngine;

public class BallHitPos : BallPhysics {

    private float speed;
    private Bounds bounds;

    new void Awake()
    {
        base.Awake();
        speed = maxSpeed;
        bounds = GetComponent<Collider2D>().bounds;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Bounds racketBounds = collision.collider.bounds;
        if (collision.gameObject.tag == "Player")
        {
            float x = 0.0f;
            float y = 0.0f;

            if (HitRightRacket(racketBounds))
                x = -1.0f;
            else if (HitLeftRacket(racketBounds))
                x = 1.0f;

            Vector2 ballPos = transform.position;
            Vector2 racketPos = collision.transform.position;
            float racketHeight = collision.collider.bounds.size.y;
            y = HitFactor(ballPos, racketPos, racketHeight);

            Vector2 direction = new Vector2(x, y).normalized;
            rigidbody.velocity = direction * speed;
        }
    }

	float HitFactor(Vector2 ballPos, Vector2 racketPos, float racketHeight)
    {
        return (ballPos.y - racketPos.y) / racketHeight;
    }

    bool HitRightRacket(Bounds racketBounds)
    {
        return (bounds.min.x <= racketBounds.max.x);
    }

    bool HitLeftRacket(Bounds racketBounds)
    {
        return (bounds.max.x >= racketBounds.min.x);
    }
}
