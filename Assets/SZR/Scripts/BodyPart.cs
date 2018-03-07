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