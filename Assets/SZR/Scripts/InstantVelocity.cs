using UnityEngine;

public class InstantVelocity : MonoBehaviour
{
    private Rigidbody2D body2d;

    public Vector2 velocity = Vector2.zero;

    private void Awake()
    {
        body2d = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        body2d.velocity = velocity;
    }
}