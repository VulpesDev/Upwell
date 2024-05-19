using UnityEngine;

public class AudioManager : MonoBehaviour
{

    [SerializeField] AudioSource music_src;
    [SerializeField] AudioSource sfx_src;

    public AudioClip background;
    public AudioClip death;
    public AudioClip jump;
    public AudioClip jetpack;

    private void Start()
    {
        music_src.clip = background;
        music_src.Play();
    }

    public void play_sfx(AudioClip clip)
    {
        sfx_src.PlayOneShot(clip);

    }
}
