using jessefreeman.utools;
using UnityEngine;
using UnityEngine.UI;

public class Stats : MonoBehaviour
{
    private StatsManager statsManager;

    private Text textField;

    private void Awake()
    {
        textField = GetComponent<Text>();
    }

    private void Start()
    {
        statsManager = GameObjectUtil.GetSingleton<StatsManager>();
        UpdateDisplay();
    }

    private void OnEnable()
    {
        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        if (statsManager)
        {
            var color = "#a2a2a2";

            var statsText = "<color='" + color + "'>SCORE:</color> " +
                            ((int) statsManager.GetStatValue("Score")).ToString("D6");
            statsText += "\n<color='" + color + "'>TOTAL TIME:</color> " +
                         StatsManager.TimeElapsedToString(statsManager.GetTimeElapsed());
            statsText += "\n<color='" + color + "'>TOTAL STEPS:</color> " +
                         statsManager.GetStatValue("StepsTaken");
            statsText += "\n<color='" + color + "'>ZOMBIES AVOIDED:</color> " +
                         ((int) statsManager.GetStatValue("Zombie")).ToString("D3");
            statsText += "\n<color='" + color + "'>OBSTACLES JUMPED OVER:</color> " +
                         ((int) statsManager.GetStatValue("Obstacles")).ToString("D2");
            statsText += "\n<color='" + color + "'>MAX COMBO:</color> " +
                         ((int) statsManager.GetStatValue("MaxCombo")).ToString("D2") + "X";
            statsText += "\n<color='" + color + "'>ZOMBIE DOGS AVOIDED:</color> " +
                         ((int) statsManager.GetStatValue("ZombieDog")).ToString("D2");

            var activeAcievement =
                GameObjectUtil.GetSingleton<AchievementManager>().currenAchievement as GenericAchievement;
            if (activeAcievement != null)
            {
                var achiveColor = activeAcievement.IsCompleted() ? "#159436" : "#ff0000";
                statsText += "\n\n<color='" + color + "'>CURRENT TASK:</color>\n<color='" + achiveColor + "'>" +
                             activeAcievement.GetMessage() + "</color>";
            }

            textField.text = statsText;
        }
        else
        {
            textField.text = "";
        }
    }
}