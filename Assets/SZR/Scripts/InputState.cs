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
using UnityEngine;

public class InputState : MonoBehaviour
{
    public float absVelX;
    public float absVelY;

    public bool actionButton;

    private Rigidbody2D body2d;
    public bool standing;
    public float standingThreshold = 1;

    public bool running
    {
        get
        {
            var running = !(absVelX > 0 && absVelY < standingThreshold);

            return running;
        }
    }

    private void Awake()
    {
        body2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        actionButton = false;
//        if (!Input.GetKeyDown(KeyCode.Escape))
//            actionButton = Input.anyKey; // && !);//characterActions.Jump.WasPressed;
    }

    private void FixedUpdate()
    {
        absVelX = Math.Abs(body2d.velocity.x);
        absVelY = Math.Abs(body2d.velocity.y);

        standing = absVelY <= standingThreshold;
    }
}