using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_data_loader : MonoBehaviour
{
    private Slider slider;
    private Player_Behaviour platformer_script;

    void Start() {
        slider = GameObject.Find("JetpackFuel_Slider").GetComponent<Slider>();
        platformer_script = GameObject.Find("Player").GetComponent<Player_Behaviour>();
        slider.maxValue = platformer_script.getMaxFuel();
        slider.minValue = platformer_script.getMinFuel();
    }

    void Update() {
        slider.value = platformer_script.getFuelValue();
    }
}
