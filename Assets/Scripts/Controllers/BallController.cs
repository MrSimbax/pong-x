using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BallPhysics))]
public class BallController : MonoBehaviour
{
    [Range(0.0f, 1000.0f)] public float initialSpeed;
    public bool initOnStart;

    new Rigidbody2D rigidbody;
    BallPhysics ballPhysics;

    Vector2 initialPosition;

    public delegate void ReachEndAction();
    public static event ReachEndAction OnReachedEnd;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        ballPhysics = GetComponent<BallPhysics>();
        initialPosition = transform.position;
        initialSpeed = Mathf.Clamp(initialSpeed, 0.0f, ballPhysics.maxSpeed);
    }

    void Start()
    {
        if (initOnStart) InitVelocity();
    }

    public void Reset()
    {
        transform.position = initialPosition;
        rigidbody.velocity = new Vector2(0.0f, 0.0f);
    }

    public void InitVelocity()
    {
        float x = Random.value > 0.5f ? -1.0f : 1.0f;
        float y = Random.value * (Random.value > 0.5f ? -1.0f : 1.0f);
        rigidbody.velocity = new Vector2(x, y) * initialSpeed;
    }
    
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "WallEnd" && OnReachedEnd != null)
        {
            OnReachedEnd();
        }
    }
}
