using jessefreeman.utools;
using UnityEngine;

public class ComboManager : MonoBehaviour
{
    public delegate void OnCombo(int value);

    public float comboDelay = 2f;
    public int currentCombo;
    private float lastComboTime;

    private StatsManager statsManager;
    public event OnCombo ComboCallback;

    private void Start()
    {
        statsManager = GameObjectUtil.GetSingleton<StatsManager>();
    }

    private void Update()
    {
        var lastKillTime = statsManager.GetStatValue("LastKillTime");

        if (Time.time - lastKillTime > comboDelay && currentCombo > 0)
        {
            var maxCombo = statsManager.GetStatValue("MaxCombo");

            if (currentCombo > maxCombo) statsManager.ResetStat("MaxCombo", currentCombo);

            currentCombo = 0;
            statsManager.ResetStat("CurrentCombo");
            return;
        }

        if (lastComboTime != lastKillTime)
        {
            if (ComboCallback != null)
                ComboCallback(currentCombo);

            //TODO this is a hack so you only get feedback for any combos above 1
            statsManager.ResetStat("CurrentCombo", currentCombo);
            currentCombo++;


            lastComboTime = lastKillTime;
        }
    }
}