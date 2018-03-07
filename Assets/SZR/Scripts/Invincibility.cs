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