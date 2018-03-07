using jessefreeman.utools;
using UnityEngine;

public class Jump : MonoBehaviour
{
    private Rigidbody2D body2d;
    public float forwardSpeed = 20;
    private InputState inputState;
    private Invincibility invincibility;

    public float jumpSpeed = 240f;
    private bool wasJumping;

    private StatsManager statsManager
    {
        get { return GameObjectUtil.GetSingleton<StatsManager>(); }
    }

    private void Awake()
    {
        body2d = GetComponent<Rigidbody2D>();
        inputState = GetComponent<InputState>();
        invincibility = GetComponent<Invincibility>();
    }

    // Update is called once per frame
    private void Update()
    {
        var standing = inputState.standing;
        if (wasJumping && standing && !invincibility.invincible)
        {
            if (inputState.running)
                statsManager.UpdateStatValue("JumpCombo", 1);
            else
                statsManager.ResetStat("JumpCombo");

            wasJumping = false;
        }

        if (inputState.standing)
        {
            if (inputState.actionButton)
            {
                body2d.velocity = new Vector2(transform.position.x < 0 ? forwardSpeed : 0, jumpSpeed);
                GameObjectUtil.GetSingleton<SoundManager>().PlayClip((int) Sounds.Jump);
            }

            //Debug.Log("Not Jumping "+inputState.running);
        }
        else
        {
            wasJumping = true;
        }
    }
}