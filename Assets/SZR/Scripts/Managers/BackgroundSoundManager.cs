using jessefreeman.utools;
using UnityEngine;

public class BackgroundSoundManager : MonoBehaviour
{
    public RecycledSound currentTrack;

    public Sounds sound;
    public float volume = .5f;

    // Use this for initialization
    private void Start()
    {
        var soundManager = GameObjectUtil.GetSingleton<SoundManager>();

        if (soundManager != null)
        {
            currentTrack = soundManager.PlayClip((int) sound, Vector3.zero, true, volume, false, true);
            currentTrack.FadeSound(0, volume, 1f);
        }
    }

    public void FadeSound(float start, float end, float delay = 1f)
    {
        if (currentTrack != null)
            currentTrack.FadeSound(start, end, delay);
    }
}