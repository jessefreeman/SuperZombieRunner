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

public class DoorTrigger : MonoBehaviour, IRecyle
{
    public string statsLabel = "Door";


    public bool triggered;
    public int value;


    // TODO need to store last amount of steps X by value

    private StatsManager statsManager
    {
        get { return GameObjectUtil.GetSingleton<StatsManager>(); }
    }

    private ScoreManager scoreManager
    {
        get { return GameObjectUtil.GetSingleton<ScoreManager>(); }
    }

    private MessageBox messageBox
    {
        get { return GameObjectUtil.GetSingleton<MessageBox>(); }
    }

    public void Restart()
    {
        triggered = false;
    }

    public void Shutdown()
    {
    }

    private void OnTriggerExit2D(Collider2D target)
    {
        if (triggered)
            return;

        if (target.tag == "Player")
        {
            triggered = true;

            statsManager.UpdateStatValue(statsLabel, 1);


            var lastSteps = (int) statsManager.GetStatValue("LastSteps");
            var currentSteps = (int) statsManager.GetStatValue("StepsTaken");

            var newSteps = currentSteps - lastSteps;
            var score = value * newSteps;

            scoreManager.IncreaseScore(score);

            messageBox.ShowMessage(
                "+ " + scoreManager.CaluclateScore(score) + " POINTS FOR " + newSteps + " MORE STEPS!", 4);

            statsManager.UpdateStatValue("LastSteps", newSteps);
        }
    }
}