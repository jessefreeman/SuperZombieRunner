using jessefreeman.utools;
using UnityEngine;

public class InstantVelocityRange : InstantVelocity, IRecyle
{
    public Vector2 velocityXRange = new Vector2(0, 0);

    public void Restart()
    {
        velocity.x = Random.Range(velocityXRange.x, velocityXRange.y);
    }

    public void Shutdown()
    {
    }
}