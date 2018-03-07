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

using System.Collections;
using jessefreeman.utools;
using UnityEngine;

public class Invincibility : MonoBehaviour, IRecyle
{
    public float invincibilityDelay = 1f;
    public bool invincible;
    public string layerNameInvincible = "Invincible";
    public string layerNamePlayer = "Player";

    public void Restart()
    {
        invincible = true;
        gameObject.layer = LayerMask.NameToLayer(layerNameInvincible);
        StartCoroutine(EndInvicability());
    }

    public void Shutdown()
    {
    }

    protected void Update()
    {
        if (invincible)
        {
            //float alpha = transform.renderer.material.color.a;
            var newAlpha = Mathf.Round(Time.time * 10) % 2 == 0 ? .5f : 1;
            ChangeAlpha(newAlpha);
        }
    }

    private IEnumerator EndInvicability()
    {
        yield return new WaitForSeconds(invincibilityDelay);
        invincible = false;
        gameObject.layer = LayerMask.NameToLayer(layerNamePlayer);

        ChangeAlpha(1f);
    }

    private void ChangeAlpha(float value)
    {
        transform.GetComponent<Renderer>().material.color = new Color(1, 1, 1, value);
    }
}