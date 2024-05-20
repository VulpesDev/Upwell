using UnityEngine;

public class AudioManager : MonoBehaviour
{

    [SerializeField] AudioSource music_src;
    [SerializeField] AudioSource sfx_src;

    public AudioClip background;
    public AudioClip death;
    public AudioClip jump;
    public AudioClip jetpackstart;
    public AudioClip jetpackmid;
    public AudioClip jetpackend;

    private void Start()
    {
        music_src.clip = background;
        music_src.Play();
    }

    public void play_sfx(AudioClip clip)
    {
        if (!sfx_src.isPlaying)
            sfx_src.PlayOneShot(clip);
    }
}
