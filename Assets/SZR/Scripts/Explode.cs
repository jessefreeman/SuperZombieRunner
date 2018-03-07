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