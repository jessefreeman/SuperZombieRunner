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
    public bool showGameOverSummary;
    
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
    string color = "#a2a2a2";

    private string CalculateRank(string stat)
    {
        // TODO need to get real rank and show correct grammar

        var rank = Random.Range(1, 15);
        
        var rankColor = "#a2a2a2";

        if (rank < 10)
        {
            rankColor = "FF00000";
        }
        
        return " <color='" + rankColor + "'>("+AddOrdinal(rank)+")</color> ";
    }
    
    public string AddOrdinal(int num)
    {
        if( num <= 0 ) return num.ToString();

        switch(num % 100)
        {
            case 11:
            case 12:
            case 13:
                return num + "th";
        }

        switch(num % 10)
        {
            case 1:
                return num + "st";
            case 2:
                return num + "nd";
            case 3:
                return num + "rd";
            default:
                return num + "th";
        }

    }
    
    private string GenerateStatText(string label, string stat, int padding = 0)
    {
        // TODO use text format to clean this up
        var text = "<color='" + color + "'>"+label+":</color> " + ((int) statsManager.GetStatValue(stat)).ToString("D"+padding);

        if (showGameOverSummary)
        {
            text += CalculateRank(stat);
        }

        text += "\n";
        
        return text;
    }
    
    
    
    
    private void UpdateDisplay()
    {
        if (statsManager)
        {

            Debug.Log("Update Stats " + showGameOverSummary);

            var statsText = GenerateStatText("SCORE", "Score", 6);//"<color='" + color + "'>SCORE:</color> " + ((int) statsManager.GetStatValue("Score")).ToString("D6");
            
            // TODO need a way to get time similar to other stats
            //statsText += GenerateStatText("TOTAL TIME", "Score");//"\n<color='" + color + "'>TOTAL TIME:</color> " + StatsManager.TimeElapsedToString(statsManager.GetTimeElapsed());
            
            statsText += GenerateStatText("TOTAL STEPS", "StepsTaken", 4);//"\n<color='" + color + "'>TOTAL STEPS:</color> " + statsManager.GetStatValue("StepsTaken");
            statsText += GenerateStatText("ZOMBIES AVOIDED", "Zombie", 3);//"\n<color='" + color + "'>ZOMBIES AVOIDED:</color> " + ((int) statsManager.GetStatValue("Zombie")).ToString("D3");
            statsText += GenerateStatText("OBSTACLES JUMPED OVER", "Obstacles", 2);//"\n<color='" + color + "'>OBSTACLES JUMPED OVER:</color> " + ((int) statsManager.GetStatValue("Obstacles")).ToString("D2");
            statsText += GenerateStatText("MAX COMBO", "MaxCombo", 2);//"\n<color='" + color + "'>MAX COMBO:</color> " + ((int) statsManager.GetStatValue("MaxCombo")).ToString("D2") + "X";
            statsText += GenerateStatText("ZOMBIE DOGS AVOIDED", "ZombieDog", 2);//"\n<color='" + color + "'>ZOMBIE DOGS AVOIDED:</color> " + ((int) statsManager.GetStatValue("ZombieDog")).ToString("D2");

            if (!showGameOverSummary)
            {

                statsText = "\n" + statsText;
                
                var activeAcievement =
                    GameObjectUtil.GetSingleton<AchievementManager>().currenAchievement as GenericAchievement;
                if (activeAcievement != null)
                {
                    var achiveColor = activeAcievement.IsCompleted() ? "#159436" : "#ff0000";
                    statsText += "\n<color='" + color + "'>CURRENT TASK:</color>\n<color='" + achiveColor + "'>" +
                                 activeAcievement.GetMessage() + "</color>";
                }
            }
            else
            {
                // TODO show global rank
                
                statsText += "\n<color='" + color + "'>YOUR GLOBAL RANK:</color>\n100th PLACE\n\n\n";
            }
            
            textField.text = statsText;
        }
        else
        {
            textField.text = "";
        }
    }
}