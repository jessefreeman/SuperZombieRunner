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

public class BodyPart : MonoBehaviour, IRecyle
{
    private Color end;

    private bool readyToDissapear;

    private SpriteRenderer spriteRenderer;
    private Color start;
    private float t;

    public void Restart()
    {
        GetComponent<Renderer>().material.color = start;
        t = 0;
        readyToDissapear = false;
    }

    public void Shutdown()
    {
        //DO NOTHING
    }

    // Use this for initialization
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        start = spriteRenderer.color;
        end = new Color(start.r, start.g, start.b, 0.0f);
    }

    private void OnCollisionEnter2D(Collision2D target)
    {
        if (target.gameObject.layer == LayerMask.NameToLayer("Solid") && !readyToDissapear)
        {
            readyToDissapear = true;
            GameObjectUtil.GetSingleton<SoundManager>().PlayClip((int) Sounds.Thud);
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (readyToDissapear)
        {
            t += Time.deltaTime;
            spriteRenderer.material.color = Color.Lerp(start, end, t / 2);

            GetComponent<Rigidbody2D>().velocity = new Vector2(-95, GetComponent<Rigidbody2D>().velocity.y);

            if (spriteRenderer.material.color.a <= 0.0)
                GameObjectUtil.Destroy(gameObject);
        }
    }
}