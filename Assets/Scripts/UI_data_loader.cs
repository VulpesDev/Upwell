using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_data_loader : MonoBehaviour
{
    private Slider slider;
    private Platformer platformer_script;

    // Start is called before the first frame update
    void Start()
    {
        slider = GameObject.Find("JetpackFuel_Slider").GetComponent<Slider>();
        platformer_script = GameObject.Find("Player").GetComponent<Platformer>();
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = platformer_script.getFuelValue();
    }
}
