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