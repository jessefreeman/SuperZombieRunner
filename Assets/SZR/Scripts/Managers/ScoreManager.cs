using jessefreeman.utools;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private readonly string scoreLabel = "Score";

    private StatsManager statsManager
    {
        get { return GameObjectUtil.GetSingleton<StatsManager>(); }
    }

    public void IncreaseScore(int value)
    {
        statsManager.UpdateStatValue(scoreLabel, CaluclateScore(value));
    }

    public int GetScore()
    {
        return (int) statsManager.GetStatValue(scoreLabel);
    }

    public int CaluclateScore(int value)
    {
        var combo = (int) statsManager.GetStatValue("JumpCombo");

        if (combo < 1)
            combo = 1;

        if (combo > 10)
            combo = 10;

        return value * combo;
    }
}