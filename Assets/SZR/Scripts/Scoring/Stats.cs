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

using System;
using System.Text;
using jessefreeman.utools;
using SZR.Scripts;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Stats : MonoBehaviour
{
    private StatsManager statsManager;

    private Text textField;
    private StringBuilder sb;
    private IRetrieveRank rankRetreiver;
    
    public bool showGameOverSummary;
    
    private void Awake()
    {
        textField = GetComponent<Text>();
    }

    private void Start()
    {
        statsManager = GameObjectUtil.GetSingleton<StatsManager>();
        
        rankRetreiver = GetComponent<IRetrieveRank>();
        
        Debug.Log("Can get rank " + (rankRetreiver != null));
        
        sb = new StringBuilder();
        
        UpdateDisplay();
    }

    private void OnEnable()
    {
        UpdateDisplay();
    }
//    string color = "#a2a2a2";

    private string CalculateRank(string stat, string rankColor = "#a2a2a2", string template = "({0})")
    {
        // TODO need to get real rank and show correct grammar

        if (rankRetreiver == null)
            return "";
        
        var rank = rankRetreiver.GetRank("playerSession", stat, statsManager.GetStatValue(stat));
        
        if (rank <= 10)
        {
            rankColor = rank == 1 ? "#ffff00" : "#ffffff";
        }
        
        return " <color=" + rankColor + ">"+string.Format(template, AddOrdinal(rank))+"</color> ";
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
    
    private string GenerateStatText(string label, string stat, int padding = 0, string color = "#a2a2a2")
    {
        // TODO use text format to clean this up
        var text = "<color=" + color + ">"+label+":</color> " + ((int) statsManager.GetStatValue(stat)).ToString("D"+padding);

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
            sb.Length = 0;
            
            sb.Append(GenerateStatText("TOTAL TIME", "Time", 6));
            sb.Append(GenerateStatText("TOTAL STEPS", "StepsTaken", 4));
            sb.Append(GenerateStatText("ZOMBIES AVOIDED", "Zombie", 3));
            sb.Append(GenerateStatText("OBSTACLES JUMPED OVER", "Obstacles", 2));
            sb.Append(GenerateStatText("MAX COMBO", "MaxCombo", 2));
            sb.Append(GenerateStatText("ZOMBIE DOGS AVOIDED", "ZombieDog", 2));
            
            if (!showGameOverSummary)
            {
                sb.Append(GenerateStatText("SCORE", "Score", 6));
                sb.Insert(0, "\n");
                
                var activeAcievement =
                    GameObjectUtil.GetSingleton<AchievementManager>().currenAchievement as GenericAchievement;
                if (activeAcievement != null)
                {
                    var achiveColor = activeAcievement.IsCompleted() ? "#159436" : "#ff0000";
                    sb.Append("\n<color=" + "a2a2a2" + ">CURRENT TASK:</color>\n<color='" + achiveColor + "'>" +
                                 activeAcievement.GetMessage() + "</color>");
                }
            }
            else
            {
                sb.Append(GenerateStatText("FINAL SCORE", "Score", 6, "#FF0000"));
                
                if (rankRetreiver != null)
                sb.Append("\n<color=#ff0000>YOUR GLOBAL RANK:</color>\n"+CalculateRank("Score", "#ffffff", "{0} PLACE")+"\n\n\n");
            }
            
            textField.text = sb.ToString();
        }
        else
        {
            textField.text = "";
        }
    }
}