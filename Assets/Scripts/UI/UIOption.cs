using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIOption : MonoBehaviour
{
    [SerializeField] Button _back;
    [SerializeField] Slider volumeSlider;
    // public AudioSource audioSource;

    void Start()
    {
        _back.onClick.AddListener(ToMenu);

        // if (audioSource && volumeSlider)
        // {
        //     volumeSlider.value = audioSource.volume;
        //     volumeSlider.onValueChanged.AddListener(SetVolume);
        // }
    }

    private void ToMenu() {
        Manager.Instance.LoadScene(Manager.Scene.MainMenu);
    }

    // void SetVolume(float volume) {
    //     if (audioSource != null) {
    //         audioSource.volume = volume;
    //     }
    // }
}
