using jessefreeman.utools;
using UnityEngine;

public class DestroyOffscreen : MonoBehaviour
{
    public delegate void OnDestroy();

    private Rigidbody2D body2d;


    private bool offscreen;
    private float offscreenX;

    public float offset = 16f;
    public event OnDestroy DestroyCallback;

    private void Awake()
    {
        body2d = GetComponent<Rigidbody2D>();
    }

    // Use this for initialization
    private void Start()
    {
        offscreenX = Screen.width / PixelPerfectCamera.pixelsToUnits / 2 + offset;
    }

    // Update is called once per frame
    private void Update()
    {
        var posX = transform.position.x;
        var dirX = body2d.velocity.x;

        if (Mathf.Abs(posX) > offscreenX)
        {
            if (dirX < 0 && posX < -offscreenX)
                offscreen = true;
            else if (dirX > 0 && posX > offscreenX)
                offscreen = true;
        }
        else
        {
            offscreen = false;
        }

        if (offscreen) OnOutOfBounds();
    }

    public void OnOutOfBounds()
    {
        offscreen = false;
        GameObjectUtil.Destroy(gameObject);

        if (DestroyCallback != null) DestroyCallback();
    }
}