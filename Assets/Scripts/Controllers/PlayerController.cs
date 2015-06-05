using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Range(0.0f, 1000.0f)] public float speed;
    public string axis;

    new Rigidbody2D rigidbody;
    Vector2 initialPosition;

    public void Reset()
    {
        transform.position = initialPosition;
        rigidbody.velocity = new Vector2(0.0f, 0.0f);
    }

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        initialPosition = transform.position;
    }

    void FixedUpdate()
    {
        rigidbody.velocity = new Vector2(0, Input.GetAxis(axis) * speed);
    }
}
