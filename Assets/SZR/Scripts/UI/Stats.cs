// Copyright 2015 - 2018 Jesse Freeman
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated
// documentation files (the "Software"), to deal in the Software without restriction, including without limitation the
// rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of
// the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
// COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR
// OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//

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