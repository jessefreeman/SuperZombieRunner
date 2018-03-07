using UnityEngine;

public class RandomAnimationFrame : MonoBehaviour
{
    public Animator[] animators;

    // Use this for initialization
    private void Start()
    {
        var value = Random.Range(0.0f, 1.0f);
        foreach (var animator in animators)
            animator.Play(0, -1,
                value); // public void Play(int stateNameHash, int layer = -1, float normalizedTime = float.NegativeInfinity);
    }
}