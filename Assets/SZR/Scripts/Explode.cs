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

public class Explode : MonoBehaviour
{
    public delegate void OnDestroy();

    public GameObject bodyPart;

    private ObjectPool pool;

    public int totalParts;
    public Vector2 xRange = new Vector2(-50, 50);
    public Vector2 yRange = new Vector2(100, 400);
    public event OnDestroy DestroyCallback;

    private void OnCollisionEnter2D(Collision2D target)
    {
        if (target.gameObject.tag == "Deadly") OnExplode();
    }

    public void OnExplode()
    {
        gameObject.GetComponent<DestroyOffscreen>().OnOutOfBounds();

        if (DestroyCallback != null) DestroyCallback();

        var t = transform;

        for (var i = 0; i < totalParts; i++)
        {
            var clone = GameObjectUtil.Instantiate(bodyPart, t.position).GetComponent<BodyPart>();
            clone.gameObject.SetActive(true);
            clone.GetComponent<Rigidbody2D>().AddForce(Vector3.right * Random.Range(xRange.x, xRange.y));
            clone.GetComponent<Rigidbody2D>().AddForce(Vector3.up * Random.Range(yRange.x, yRange.y));
        }
    }
}