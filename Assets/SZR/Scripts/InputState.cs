using System;
using UnityEngine;

public class InputState : MonoBehaviour
{
    public float absVelX;
    public float absVelY;

    public bool actionButton;

    private Rigidbody2D body2d;
    public bool standing;
    public float standingThreshold = 1;

    public bool running
    {
        get
        {
            var running = !(absVelX > 0 && absVelY < standingThreshold);

            return running;
        }
    }

    private void Awake()
    {
        body2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (!Input.GetKeyDown(KeyCode.Escape))
            actionButton = Input.anyKey; // && !);//characterActions.Jump.WasPressed;
    }

    private void FixedUpdate()
    {
        absVelX = Math.Abs(body2d.velocity.x);
        absVelY = Math.Abs(body2d.velocity.y);

        standing = absVelY <= standingThreshold;
    }
}