using jessefreeman.utools;
using UnityEngine;

public class PositionManager : MonoBehaviour, IRecyle
{
    public Vector2 offset = Vector2.zero;
    public string statLabel = "Undefined";
    public GameObject target;

    public bool triggered;
    public int value = 1;

    private StatsManager statsManager
    {
        get { return GameObjectUtil.GetSingleton<StatsManager>(); }
    }

    private ScoreManager scoreManager
    {
        get { return GameObjectUtil.GetSingleton<ScoreManager>(); }
    }

    public void Restart()
    {
        triggered = false;
        target = GameObject.FindWithTag("Player");
    }

    public void Shutdown()
    {
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (triggered || target == null)
        {
            triggered = true;
            return;
        }

        var curPosX = transform.position.x + offset.x;
        var targetPosX = target.transform.position.x;

        if (curPosX < targetPosX) Trigger();
    }

    private void Trigger()
    {
        triggered = true;
        statsManager.UpdateStatValue(statLabel, 1);
        scoreManager.IncreaseScore(value);
    }
}