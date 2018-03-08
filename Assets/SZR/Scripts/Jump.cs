﻿// Copyright 2015 - 2018 Jesse Freeman
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

public class Jump : MonoBehaviour
{
    private Rigidbody2D body2d;
    public float forwardSpeed = 20;
    private InputState inputState;
    private Invincibility invincibility;

    public float jumpSpeed = 240f;
    private bool wasJumping;

    private StatsManager statsManager
    {
        get { return GameObjectUtil.GetSingleton<StatsManager>(); }
    }

    private void Awake()
    {
        body2d = GetComponent<Rigidbody2D>();
        inputState = GetComponent<InputState>();
        invincibility = GetComponent<Invincibility>();
    }

    // Update is called once per frame
    private void Update()
    {
        var standing = inputState.standing;
        if (wasJumping && standing && !invincibility.invincible)
        {
            if (inputState.running)
                statsManager.UpdateStatValue("JumpCombo", 1);
            else
                statsManager.ResetStat("JumpCombo");

            wasJumping = false;
        }

        if (inputState.standing)
        {
            if (inputState.actionButton)
            {
                Action();
            }
        }
        else
        {
            wasJumping = true;
        }
    }

    public void Action()
    {
        if (inputState.standing)
        {
            body2d.velocity = new Vector2(transform.position.x < 0 ? forwardSpeed : 0, jumpSpeed);
            GameObjectUtil.GetSingleton<SoundManager>().PlayClip((int) Sounds.Jump);
        }
    }
}