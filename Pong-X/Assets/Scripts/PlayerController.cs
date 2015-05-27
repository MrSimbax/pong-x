using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public float speed = 10.0f;
    public string axis = "Vertical";

    [HideInInspector] public int score;

    private Rigidbody2D rigidbody;
    private Vector2 initialPosition;

    public void Reset()
    {
        transform.position = initialPosition;
        rigidbody.velocity = new Vector2(0.0f, 0.0f);
    }

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        score = 0;
        initialPosition = transform.position;
    }

    void FixedUpdate()
    {
        rigidbody.velocity = new Vector2(0, Input.GetAxis(axis) * speed);
    }
}
