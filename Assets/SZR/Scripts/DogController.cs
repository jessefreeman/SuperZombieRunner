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

using UnityEngine;

public class DogController : MonoBehaviour
{
    protected Animator animator;
    private bool attack;

    public LayerMask attackLayer;
    protected Rigidbody2D body2D;

    protected RaycastHit2D[] collisions = new RaycastHit2D[1];
    private Vector2 defaultSpeed;
    protected InstantVelocity instantVel;

    public Transform lineOfSight;
    private readonly float resetDelay = .8f;
    public Vector2 runSpeed = new Vector2(0, 0);
    private float timeElapsed;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        body2D = GetComponent<Rigidbody2D>();
        instantVel = GetComponent<InstantVelocity>();
        defaultSpeed = instantVel.velocity;
    }

    protected virtual void FixedUpdate()
    {
        var startPos = transform.position;
        var linePos = lineOfSight.position;

        var endPos = new Vector2(linePos.x, linePos.y);

        Debug.DrawLine(startPos, endPos);

        var hits = Physics2D.LinecastNonAlloc(startPos, endPos, collisions, attackLayer);

        OnAttack(hits > 0);
    }

    protected virtual void OnAttack(bool attack)
    {
        if (!this.attack && attack)
        {
            this.attack = attack;

            animator.SetInteger("AnimState", 1);

            instantVel.velocity = runSpeed;
            timeElapsed = 0;
        }


        //TODO need a timer to rest when doesn't see the player
    }

    private void Update()
    {
        timeElapsed += Time.deltaTime;

        if (timeElapsed > resetDelay) ResetAttack();
    }

    public void ResetAttack()
    {
        animator.SetInteger("AnimState", 0);

        instantVel.velocity = defaultSpeed;
        timeElapsed = 0;
        attack = false;
    }
}