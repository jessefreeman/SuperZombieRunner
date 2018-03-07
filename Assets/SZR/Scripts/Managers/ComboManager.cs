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