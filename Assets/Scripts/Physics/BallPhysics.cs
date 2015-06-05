using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public abstract class BallPhysics : MonoBehaviour {
    [Range(0.0f, 1000.0f)] public float maxSpeed;
    protected new Rigidbody2D rigidbody;

    protected void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }
}
