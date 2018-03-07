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