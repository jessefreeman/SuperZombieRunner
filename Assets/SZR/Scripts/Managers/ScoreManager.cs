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