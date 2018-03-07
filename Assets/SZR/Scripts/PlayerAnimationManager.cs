using jessefreeman.utools;
using UnityEngine;

public class PlayerAnimationManager : MonoBehaviour
{
    private Animator animator;
    private InputState inputState;

    private StatsManager statsManager
    {
        get { return GameObjectUtil.GetSingleton<StatsManager>(); }
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        inputState = GetComponent<InputState>();
    }

    // Update is called once per frame
    private void Update()
    {
        //  var running = true;

        //  if (inputState.absVelX > 0 && inputState.absVelY < inputState.standingThreshold)
        //  	running = false;

        animator.SetBool("Running", inputState.running);
    }

    public void PlayFootstep()
    {
        statsManager.UpdateStatValue("StepsTaken", 1);
    }
}